using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="TaskCreatedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class TaskCreatedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeTaskCreatedOutput_WithStopReason_ShouldSerializeCorrectly()
        {
            var output = new TaskCreatedHookOutput
            {
                Continue = false,
                StopReason = "Task queue is full"
            };

            var json = ClaudeHooksSerializer.SerializeTaskCreatedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeFalse();
            doc.RootElement.GetProperty("stopReason").GetString().Should().Be("Task queue is full");
        }

        #endregion

    }

}
