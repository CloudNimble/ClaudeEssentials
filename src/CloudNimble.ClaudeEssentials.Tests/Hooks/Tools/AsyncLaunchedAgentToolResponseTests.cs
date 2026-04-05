using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="AsyncLaunchedAgentToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured logs with async_launched exist in this repo.
    /// </summary>
    [TestClass]
    public class AsyncLaunchedAgentToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeAsyncLaunched_WithAllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "22222222-3333-4444-5555-666666666666",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Background analysis",
                        "prompt": "Analyze all source files for patterns",
                        "subagent_type": "Explore",
                        "run_in_background": true
                    },
                    "tool_response": {
                        "status": "async_launched",
                        "agentId": "c9876543210fedcba",
                        "description": "Background analysis",
                        "prompt": "Analyze all source files for patterns",
                        "outputFile": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\subagents\\agent-c9876543210fedcba.jsonl",
                        "canReadOutputFile": true
                    },
                    "tool_use_id": "toolu_01EEEEEEEEEEEEEEEEEEEEEE"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<AsyncLaunchedAgentToolResponse>();
            var launched = (AsyncLaunchedAgentToolResponse)result.ToolResponse!;
            launched.AgentId.Should().Be("c9876543210fedcba");
            launched.Description.Should().Be("Background analysis");
            launched.Prompt.Should().Contain("source files");
            launched.OutputFile.Should().Contain("agent-c9876543210fedcba.jsonl");
            launched.CanReadOutputFile.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeAsyncLaunched_WithoutCanRead_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Background task",
                        "prompt": "Run long analysis"
                    },
                    "tool_response": {
                        "status": "async_launched",
                        "agentId": "d1111222233334444",
                        "description": "Background task",
                        "prompt": "Run long analysis",
                        "outputFile": "/tmp/subagents/agent-d1111222233334444.jsonl"
                    },
                    "tool_use_id": "toolu_01FFFFFFFFFFFFFFFFFFFFFF"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);

            result.Should().NotBeNull();
            var launched = (AsyncLaunchedAgentToolResponse)result!.ToolResponse!;
            launched.CanReadOutputFile.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void AsyncLaunchedAgentToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new AsyncLaunchedAgentToolResponse
            {
                AgentId = "async-agent-456",
                Description = "Background analysis",
                Prompt = "Analyze codebase",
                OutputFile = "/tmp/agent-output.jsonl",
                CanReadOutputFile = true
            };

            var json = JsonSerializer.Serialize<AgentToolResponse>(response, ClaudeHooksJsonContext.Default.AgentToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentToolResponse);

            result.Should().BeOfType<AsyncLaunchedAgentToolResponse>();
            var launched = (AsyncLaunchedAgentToolResponse)result!;
            launched.AgentId.Should().Be("async-agent-456");
            launched.Description.Should().Be("Background analysis");
            launched.OutputFile.Should().Be("/tmp/agent-output.jsonl");
            launched.CanReadOutputFile.Should().BeTrue();
            launched.Prompt.Should().Be("Analyze codebase");
        }

        #endregion

    }

}
