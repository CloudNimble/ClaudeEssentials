using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="TaskOutputToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class TaskOutputToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void TaskOutputToolInput_DeserializeBlocking_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "task_id": "task_01AAAAAAAAAAAAAAAAAAAAAA",
                    "block": true,
                    "timeout": 30000
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskOutputToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be("task_01AAAAAAAAAAAAAAAAAAAAAA");
            result.Block.Should().BeTrue();
            result.Timeout.Should().Be(30000);
        }

        [TestMethod]
        public void TaskOutputToolInput_DeserializeNonBlocking_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "task_id": "task_01BBBBBBBBBBBBBBBBBBBBBB",
                    "block": false,
                    "timeout": 0
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskOutputToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be("task_01BBBBBBBBBBBBBBBBBBBBBB");
            result.Block.Should().BeFalse();
            result.Timeout.Should().Be(0);
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TaskOutputToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new TaskOutputToolInput
            {
                TaskId = "task_01CCCCCCCCCCCCCCCCCCCCCC",
                Block = true,
                Timeout = 60000
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TaskOutputToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskOutputToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be(input.TaskId);
            result.Block.Should().BeTrue();
            result.Timeout.Should().Be(input.Timeout);
        }

        #endregion

    }

}
