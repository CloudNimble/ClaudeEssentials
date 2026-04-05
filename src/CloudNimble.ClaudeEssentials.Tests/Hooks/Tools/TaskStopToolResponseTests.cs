using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="TaskStopToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class TaskStopToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTaskStop_WithCommand_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "TaskStop",
                    "tool_input": {
                        "task_id": "task_abc123"
                    },
                    "tool_response": {
                        "message": "Task stopped successfully",
                        "task_id": "task_abc123",
                        "task_type": "shell",
                        "command": "npm run dev"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Message.Should().Be("Task stopped successfully");
            result.ToolResponse.TaskId.Should().Be("task_abc123");
            result.ToolResponse.TaskType.Should().Be("shell");
            result.ToolResponse.Command.Should().Be("npm run dev");
        }

        [TestMethod]
        public void DeserializeTaskStop_WithoutCommand_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "TaskStop",
                    "tool_input": {
                        "task_id": "task_def456"
                    },
                    "tool_response": {
                        "message": "Background task terminated",
                        "task_id": "task_def456",
                        "task_type": "agent"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Message.Should().Be("Background task terminated");
            result.ToolResponse.TaskId.Should().Be("task_def456");
            result.ToolResponse.TaskType.Should().Be("agent");
            result.ToolResponse.Command.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TaskStopToolResponse_RoundTrip_WithCommand_ShouldMaintainData()
        {
            var response = new TaskStopToolResponse
            {
                Message = "Task stopped",
                TaskId = "task_test_789",
                TaskType = "shell",
                Command = "dotnet watch run"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.TaskStopToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolResponse);

            result.Should().NotBeNull();
            result!.Message.Should().Be("Task stopped");
            result.TaskId.Should().Be("task_test_789");
            result.TaskType.Should().Be("shell");
            result.Command.Should().Be("dotnet watch run");
        }

        [TestMethod]
        public void TaskStopToolResponse_RoundTrip_WithoutCommand_ShouldOmitNull()
        {
            var response = new TaskStopToolResponse
            {
                Message = "Agent task terminated",
                TaskId = "task_test_012",
                TaskType = "agent"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.TaskStopToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopToolResponse);

            result.Should().NotBeNull();
            result!.Message.Should().Be("Agent task terminated");
            result.TaskId.Should().Be("task_test_012");
            result.TaskType.Should().Be("agent");
            result.Command.Should().BeNull();
        }

        #endregion

    }

}
