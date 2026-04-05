using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="TeammateIdleHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class TeammateIdleHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeTeammateIdleOutput_WithStopReason_ShouldSerializeCorrectly()
        {
            var output = new TeammateIdleHookOutput
            {
                Continue = false,
                StopReason = "No more tasks available"
            };

            var json = ClaudeHooksSerializer.SerializeTeammateIdleOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeFalse();
            doc.RootElement.GetProperty("stopReason").GetString().Should().Be("No more tasks available");
        }

        #endregion

    }

}
