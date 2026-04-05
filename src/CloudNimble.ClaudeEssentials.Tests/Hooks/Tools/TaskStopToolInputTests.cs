using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="TaskStopToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class TaskStopToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void TaskStopToolInput_DeserializeWithTaskId_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "task_id": "task_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be("task_01AAAAAAAAAAAAAAAAAAAAAA");
            result.ShellId.Should().BeNull();
        }

        [TestMethod]
        public void TaskStopToolInput_DeserializeWithDeprecatedShellId_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "shell_id": "shell_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().BeNull();
            result.ShellId.Should().Be("shell_01BBBBBBBBBBBBBBBBBBBBBB");
        }

        [TestMethod]
        public void TaskStopToolInput_DeserializeWithBothIds_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "task_id": "task_01CCCCCCCCCCCCCCCCCCCCCC",
                    "shell_id": "shell_01DDDDDDDDDDDDDDDDDDDDDD"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be("task_01CCCCCCCCCCCCCCCCCCCCCC");
            result.ShellId.Should().Be("shell_01DDDDDDDDDDDDDDDDDDDDDD");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TaskStopToolInput_RoundTrip_WithTaskId_ShouldMaintainData()
        {
            var input = new TaskStopToolInput
            {
                TaskId = "task_01EEEEEEEEEEEEEEEEEEEEEE"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TaskStopToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolInput);

            result.Should().NotBeNull();
            result!.TaskId.Should().Be(input.TaskId);
            result.ShellId.Should().BeNull();
        }

        [TestMethod]
        public void TaskStopToolInput_RoundTrip_WithNoIds_ShouldOmitNullFields()
        {
            var input = new TaskStopToolInput();

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TaskStopToolInput);

            json.Should().NotContain("task_id");
            json.Should().NotContain("shell_id");
        }

        #endregion

    }

}
