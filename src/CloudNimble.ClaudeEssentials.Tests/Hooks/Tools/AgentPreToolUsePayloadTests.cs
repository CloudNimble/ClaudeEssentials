using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="AgentPreToolUsePayload"/> deserialization and <see cref="AgentToolInput"/> round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class AgentPreToolUsePayloadTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeAgentPreToolUse_WithExploreAgent_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\aaaaaaaa.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Explore codebase structure",
                        "prompt": "Find all public classes and list them.",
                        "subagent_type": "Explore"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Agent");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.PermissionMode.Should().Be(PermissionMode.Default);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Explore codebase structure");
            result.ToolInput.SubagentType.Should().Be("Explore");
            result.ToolInput.Prompt.Should().Contain("public classes");
        }

        [TestMethod]
        public void DeserializeAgentPreToolUse_WithAllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "11111111-2222-3333-4444-555555555555",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Agent",
                    "tool_input": {
                        "description": "Run code review",
                        "prompt": "Review the module for quality issues",
                        "subagent_type": "general-purpose",
                        "model": "opus",
                        "run_in_background": true,
                        "name": "reviewer-agent",
                        "team_name": "code-review",
                        "mode": "plan",
                        "isolation": "worktree"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Run code review");
            result.ToolInput.SubagentType.Should().Be("general-purpose");
            result.ToolInput.Model.Should().Be(ClaudeModel.Opus);
            result.ToolInput.RunInBackground.Should().BeTrue();
            result.ToolInput.Name.Should().Be("reviewer-agent");
            result.ToolInput.TeamName.Should().Be("code-review");
            result.ToolInput.Mode.Should().Be(PermissionMode.Plan);
            result.ToolInput.Isolation.Should().Be("worktree");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void AgentToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new AgentToolInput
            {
                Description = "Test agent",
                Prompt = "Do something useful",
                SubagentType = "Explore",
                Model = ClaudeModel.Sonnet,
                RunInBackground = false,
                Name = "my-agent",
                TeamName = "my-team",
                Mode = PermissionMode.Default,
                Isolation = "worktree"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.AgentToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentToolInput);

            result.Should().NotBeNull();
            result!.Description.Should().Be(input.Description);
            result.Prompt.Should().Be(input.Prompt);
            result.SubagentType.Should().Be(input.SubagentType);
            result.Model.Should().Be(input.Model);
            result.RunInBackground.Should().BeFalse();
            result.Name.Should().Be(input.Name);
            result.TeamName.Should().Be(input.TeamName);
            result.Mode.Should().Be(input.Mode);
            result.Isolation.Should().Be(input.Isolation);
        }

        [TestMethod]
        public void AgentToolInput_RoundTrip_WithNulls_ShouldOmitNullFields()
        {
            var input = new AgentToolInput
            {
                Description = "Minimal agent",
                Prompt = "Quick task"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.AgentToolInput);

            json.Should().NotContain("subagent_type");
            json.Should().NotContain("model");
            json.Should().NotContain("run_in_background");
            json.Should().NotContain("name");
            json.Should().NotContain("team_name");
            json.Should().NotContain("mode");
            json.Should().NotContain("isolation");
        }

        #endregion

    }

}
