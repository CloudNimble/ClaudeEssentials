using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="TaskCompletedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class TaskCompletedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeTaskCompletedOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new TaskCompletedHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeTaskCompletedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
