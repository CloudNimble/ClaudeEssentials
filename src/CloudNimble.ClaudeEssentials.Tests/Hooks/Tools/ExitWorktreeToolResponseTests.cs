using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ExitWorktreeToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class ExitWorktreeToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeExitWorktree_RemoveWithDiscards_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "ExitWorktree",
                    "tool_input": {
                        "action": "remove"
                    },
                    "tool_response": {
                        "action": "remove",
                        "originalCwd": "/home/user/project",
                        "worktreePath": "/tmp/worktrees/feature-abc",
                        "worktreeBranch": "worktree/feature-abc",
                        "tmuxSessionName": "claude-worktree-1",
                        "discardedFiles": 3,
                        "discardedCommits": 1,
                        "message": "Removed worktree and returned to /home/user/project"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Action.Should().Be(WorktreeAction.Remove);
            result.ToolResponse.OriginalCwd.Should().Be("/home/user/project");
            result.ToolResponse.WorktreePath.Should().Be("/tmp/worktrees/feature-abc");
            result.ToolResponse.WorktreeBranch.Should().Be("worktree/feature-abc");
            result.ToolResponse.TmuxSessionName.Should().Be("claude-worktree-1");
            result.ToolResponse.DiscardedFiles.Should().Be(3);
            result.ToolResponse.DiscardedCommits.Should().Be(1);
            result.ToolResponse.Message.Should().Contain("Removed worktree");
        }

        [TestMethod]
        public void DeserializeExitWorktree_KeepWithoutOptionals_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ExitWorktree",
                    "tool_input": {
                        "action": "keep"
                    },
                    "tool_response": {
                        "action": "keep",
                        "originalCwd": "/home/user/project",
                        "worktreePath": "/tmp/worktrees/feature-xyz",
                        "message": "Exited worktree, kept at /tmp/worktrees/feature-xyz"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Action.Should().Be(WorktreeAction.Keep);
            result.ToolResponse.OriginalCwd.Should().Be("/home/user/project");
            result.ToolResponse.WorktreeBranch.Should().BeNull();
            result.ToolResponse.TmuxSessionName.Should().BeNull();
            result.ToolResponse.DiscardedFiles.Should().BeNull();
            result.ToolResponse.DiscardedCommits.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ExitWorktreeToolResponse_RoundTrip_AllFields_ShouldMaintainData()
        {
            var response = new ExitWorktreeToolResponse
            {
                Action = WorktreeAction.Remove,
                OriginalCwd = "/home/user/project",
                WorktreePath = "/tmp/worktrees/test-feature",
                WorktreeBranch = "worktree/test-feature",
                TmuxSessionName = "claude-session-1",
                DiscardedFiles = 5,
                DiscardedCommits = 2,
                Message = "Removed worktree and returned to /home/user/project"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ExitWorktreeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreeToolResponse);

            result.Should().NotBeNull();
            result!.Action.Should().Be(WorktreeAction.Remove);
            result.OriginalCwd.Should().Be("/home/user/project");
            result.WorktreePath.Should().Be("/tmp/worktrees/test-feature");
            result.WorktreeBranch.Should().Be("worktree/test-feature");
            result.TmuxSessionName.Should().Be("claude-session-1");
            result.DiscardedFiles.Should().Be(5);
            result.DiscardedCommits.Should().Be(2);
            result.Message.Should().Contain("Removed worktree");
        }

        [TestMethod]
        public void ExitWorktreeToolResponse_RoundTrip_MinimalFields_ShouldOmitNulls()
        {
            var response = new ExitWorktreeToolResponse
            {
                Action = WorktreeAction.Keep,
                OriginalCwd = "/home/user/project",
                WorktreePath = "/tmp/worktrees/temp-123",
                Message = "Kept worktree"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ExitWorktreeToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreeToolResponse);

            result.Should().NotBeNull();
            result!.Action.Should().Be(WorktreeAction.Keep);
            result.WorktreeBranch.Should().BeNull();
            result.TmuxSessionName.Should().BeNull();
            result.DiscardedFiles.Should().BeNull();
            result.DiscardedCommits.Should().BeNull();
        }

        #endregion

    }

}
