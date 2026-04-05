using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="CompletedAgentToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class CompletedAgentToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeCompletedAgent_WithFullUsage_ShouldDeserializeCorrectly()
        {
            // Structure matches real captured completed Agent payloads, all values anonymized
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\aaaaaaaa.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Explore codebase structure",
                        "prompt": "Find all public classes and list them.",
                        "subagent_type": "Explore"
                    },
                    "tool_response": {
                        "status": "completed",
                        "prompt": "Find all public classes and list them.",
                        "agentId": "aa8f8cb60a203f3c0",
                        "agentType": "Explore",
                        "content": [
                            {
                                "type": "text",
                                "text": "Found 15 public classes across 3 namespaces."
                            }
                        ],
                        "totalDurationMs": 102526,
                        "totalTokens": 128453,
                        "totalToolUseCount": 35,
                        "usage": {
                            "input_tokens": 3,
                            "output_tokens": 5939,
                            "cache_creation_input_tokens": 329,
                            "cache_read_input_tokens": 122182,
                            "server_tool_use": {
                                "web_search_requests": 0,
                                "web_fetch_requests": 0
                            },
                            "service_tier": "standard",
                            "cache_creation": {
                                "ephemeral_1h_input_tokens": 0,
                                "ephemeral_5m_input_tokens": 329
                            }
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<CompletedAgentToolResponse>();
            var completed = (CompletedAgentToolResponse)result.ToolResponse!;
            completed.AgentId.Should().Be("aa8f8cb60a203f3c0");
            completed.AgentType.Should().Be("Explore");
            completed.Prompt.Should().Contain("public classes");
            completed.TotalDurationMs.Should().Be(102526);
            completed.TotalTokens.Should().Be(128453);
            completed.TotalToolUseCount.Should().Be(35);

            completed.Content.Should().HaveCount(1);
            completed.Content[0].Type.Should().Be(ContentBlockType.Text);
            completed.Content[0].Text.Should().Contain("15 public classes");

            completed.Usage.Should().NotBeNull();
            completed.Usage.InputTokens.Should().Be(3);
            completed.Usage.OutputTokens.Should().Be(5939);
            completed.Usage.CacheCreationInputTokens.Should().Be(329);
            completed.Usage.CacheReadInputTokens.Should().Be(122182);
            completed.Usage.ServiceTier.Should().Be("standard");

            completed.Usage.ServerToolUse.Should().NotBeNull();
            completed.Usage.ServerToolUse!.WebSearchRequests.Should().Be(0);
            completed.Usage.ServerToolUse.WebFetchRequests.Should().Be(0);

            completed.Usage.CacheCreation.Should().NotBeNull();
            completed.Usage.CacheCreation!.Ephemeral1hInputTokens.Should().Be(0);
            completed.Usage.CacheCreation.Ephemeral5mInputTokens.Should().Be(329);
        }

        [TestMethod]
        public void DeserializeCompletedAgent_WithNullUsageFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Quick search",
                        "prompt": "Find main entry point"
                    },
                    "tool_response": {
                        "status": "completed",
                        "prompt": "Find main entry point",
                        "agentId": "b1234567890abcdef",
                        "content": [
                            { "type": "text", "text": "Found the entry point." }
                        ],
                        "totalDurationMs": 5000,
                        "totalTokens": 2000,
                        "totalToolUseCount": 3,
                        "usage": {
                            "input_tokens": 1500,
                            "output_tokens": 500,
                            "cache_creation_input_tokens": null,
                            "cache_read_input_tokens": null,
                            "server_tool_use": null,
                            "service_tier": null,
                            "cache_creation": null
                        }
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);

            result.Should().NotBeNull();
            var completed = (CompletedAgentToolResponse)result!.ToolResponse!;
            completed.AgentType.Should().BeNull();
            completed.Usage.CacheCreationInputTokens.Should().BeNull();
            completed.Usage.CacheReadInputTokens.Should().BeNull();
            completed.Usage.ServerToolUse.Should().BeNull();
            completed.Usage.ServiceTier.Should().BeNull();
            completed.Usage.CacheCreation.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeCompletedAgent_WithMultipleContentBlocks_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Analyze files",
                        "prompt": "List test files"
                    },
                    "tool_response": {
                        "status": "completed",
                        "prompt": "List test files",
                        "agentId": "e5555666677778888",
                        "agentType": "general-purpose",
                        "content": [
                            { "type": "text", "text": "## Test Files" },
                            { "type": "text", "text": "Found 8 test files covering 12 classes." }
                        ],
                        "totalDurationMs": 15000,
                        "totalTokens": 25000,
                        "totalToolUseCount": 8,
                        "usage": {
                            "input_tokens": 18000,
                            "output_tokens": 7000,
                            "cache_creation_input_tokens": null,
                            "cache_read_input_tokens": null
                        }
                    },
                    "tool_use_id": "toolu_01DDDDDDDDDDDDDDDDDDDD"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);

            result.Should().NotBeNull();
            var completed = (CompletedAgentToolResponse)result!.ToolResponse!;
            completed.Content.Should().HaveCount(2);
            completed.Content[0].Text.Should().Contain("Test Files");
            completed.Content[1].Text.Should().Contain("8 test files");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void CompletedAgentToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new CompletedAgentToolResponse
            {
                AgentId = "test-agent-123",
                AgentType = "Explore",
                Content = [new AgentContentBlock { Type = ContentBlockType.Text, Text = "Result text" }],
                TotalToolUseCount = 10,
                TotalDurationMs = 5000,
                TotalTokens = 8000,
                Prompt = "Original prompt",
                Usage = new AgentUsageInfo
                {
                    InputTokens = 5000,
                    OutputTokens = 3000,
                    ServiceTier = "standard"
                }
            };

            var json = JsonSerializer.Serialize<AgentToolResponse>(response, ClaudeHooksJsonContext.Default.AgentToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentToolResponse);

            result.Should().BeOfType<CompletedAgentToolResponse>();
            var completed = (CompletedAgentToolResponse)result!;
            completed.AgentId.Should().Be("test-agent-123");
            completed.AgentType.Should().Be("Explore");
            completed.Content.Should().HaveCount(1);
            completed.TotalToolUseCount.Should().Be(10);
            completed.Usage.InputTokens.Should().Be(5000);
            completed.TotalDurationMs.Should().Be(5000);
            completed.TotalTokens.Should().Be(8000);
            completed.Prompt.Should().Be("Original prompt");
        }

        #endregion

    }

}
