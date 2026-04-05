using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="TaskToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class TaskToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTaskToolResponse_Completed_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Explore codebase",
                        "prompt": "Find all public API endpoints.",
                        "subagent_type": "Explore"
                    },
                    "tool_response": {
                        "result": "Found 12 public API endpoints across 4 controllers.",
                        "agentId": "agent_01ABC123XYZ789",
                        "status": "completed"
                    },
                    "tool_use_id": "toolu_01TestTaskPost001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Task");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Result.Should().Contain("12 public API endpoints");
            result.ToolResponse.AgentId.Should().Be("agent_01ABC123XYZ789");
            result.ToolResponse.Status.Should().Be(TaskExecutionStatus.Completed);
        }

        [TestMethod]
        public void DeserializeTaskToolResponse_WithoutOptionalFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Quick lookup",
                        "prompt": "Check the version number.",
                        "subagent_type": "Explore"
                    },
                    "tool_response": {
                        "result": "The current version is 2.1.0."
                    },
                    "tool_use_id": "toolu_01TestTaskMinimal001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Result.Should().Contain("2.1.0");
            result.ToolResponse.AgentId.Should().BeNull();
            result.ToolResponse.Status.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeTaskToolResponse_BackgroundRunning_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Background task",
                        "prompt": "Watch for file changes.",
                        "subagent_type": "general-purpose",
                        "run_in_background": true
                    },
                    "tool_response": {
                        "result": "Agent launched in background.",
                        "agentId": "agent_bg_001",
                        "status": "running"
                    },
                    "tool_use_id": "toolu_01TestTaskRunning001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Status.Should().Be(TaskExecutionStatus.Running);
            result.ToolResponse.AgentId.Should().Be("agent_bg_001");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TaskToolResponse_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var response = new TaskToolResponse
            {
                Result = "Task completed successfully with 5 findings.",
                AgentId = "agent_rt_001",
                Status = TaskExecutionStatus.Completed
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.TaskToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskToolResponse);

            result.Should().NotBeNull();
            result!.Result.Should().Be(response.Result);
            result.AgentId.Should().Be(response.AgentId);
            result.Status.Should().Be(response.Status);
        }

        [TestMethod]
        public void TaskToolResponse_RoundTrip_WithoutOptionalFields_ShouldMaintainData()
        {
            var response = new TaskToolResponse
            {
                Result = "Minimal result."
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.TaskToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskToolResponse);

            result.Should().NotBeNull();
            result!.Result.Should().Be(response.Result);
            result.AgentId.Should().BeNull();
            result.Status.Should().BeNull();
        }

        #endregion

    }

}
