using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="EnterWorktreeToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class EnterWorktreeToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeEnterWorktree_WithBranch_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "EnterWorktree",
                    "tool_input": {
                        "description": "Create worktree for feature work"
                    },
                    "tool_response": {
                        "worktreePath": "/tmp/worktrees/feature-abc",
                        "worktreeBranch": "worktree/feature-abc",
                        "message": "Created worktree at /tmp/worktrees/feature-abc"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.WorktreePath.Should().Be("/tmp/worktrees/feature-abc");
            result.ToolResponse.WorktreeBranch.Should().Be("worktree/feature-abc");
            result.ToolResponse.Message.Should().Contain("Created worktree");
        }

        [TestMethod]
        public void DeserializeEnterWorktree_WithoutBranch_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "EnterWorktree",
                    "tool_input": {
                        "description": "Create worktree"
                    },
                    "tool_response": {
                        "worktreePath": "/tmp/worktrees/temp-123",
                        "message": "Created worktree at /tmp/worktrees/temp-123"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.WorktreePath.Should().Be("/tmp/worktrees/temp-123");
            result.ToolResponse.WorktreeBranch.Should().BeNull();
            result.ToolResponse.Message.Should().NotBeEmpty();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void EnterWorktreeToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new EnterWorktreeToolResponse
            {
                WorktreePath = "/tmp/worktrees/test-feature",
                WorktreeBranch = "worktree/test-feature",
                Message = "Created worktree at /tmp/worktrees/test-feature"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.EnterWorktreeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolResponse);

            result.Should().NotBeNull();
            result!.WorktreePath.Should().Be("/tmp/worktrees/test-feature");
            result.WorktreeBranch.Should().Be("worktree/test-feature");
            result.Message.Should().Contain("Created worktree");
        }

        [TestMethod]
        public void EnterWorktreeToolResponse_RoundTrip_WithoutBranch_ShouldMaintainData()
        {
            var response = new EnterWorktreeToolResponse
            {
                WorktreePath = "/tmp/worktrees/temp-456",
                Message = "Created worktree at /tmp/worktrees/temp-456"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.EnterWorktreeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolResponse);

            result.Should().NotBeNull();
            result!.WorktreePath.Should().Be("/tmp/worktrees/temp-456");
            result.WorktreeBranch.Should().BeNull();
        }

        #endregion

    }

}
