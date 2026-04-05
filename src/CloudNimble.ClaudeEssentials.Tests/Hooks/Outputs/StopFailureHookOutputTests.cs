using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="StopFailureHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class StopFailureHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeStopFailureOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new StopFailureHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeStopFailureOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
