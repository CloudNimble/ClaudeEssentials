using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="TaskToolInput"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class TaskToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTaskToolInput_MinimalPayload_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Explore codebase",
                        "prompt": "Find all public API endpoints and list them.",
                        "subagent_type": "Explore"
                    },
                    "tool_use_id": "toolu_01TestTask001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Task");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Explore codebase");
            result.ToolInput.Prompt.Should().Contain("public API endpoints");
            result.ToolInput.SubagentType.Should().Be("Explore");
            result.ToolInput.Model.Should().BeNull();
            result.ToolInput.Resume.Should().BeNull();
            result.ToolInput.RunInBackground.Should().BeNull();
            result.ToolInput.Name.Should().BeNull();
            result.ToolInput.TeamName.Should().BeNull();
            result.ToolInput.Mode.Should().BeNull();
            result.ToolInput.Isolation.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeTaskToolInput_AllFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Implement feature branch",
                        "prompt": "Create unit tests for the UserService class.",
                        "subagent_type": "general-purpose",
                        "model": "opus",
                        "resume": "agent_01ABC123XYZ789",
                        "run_in_background": true,
                        "name": "test-writer",
                        "team_name": "backend-team",
                        "mode": "acceptEdits",
                        "isolation": "worktree"
                    },
                    "tool_use_id": "toolu_01TestTaskFull001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Description.Should().Be("Implement feature branch");
            result.ToolInput.Prompt.Should().Contain("UserService");
            result.ToolInput.SubagentType.Should().Be("general-purpose");
            result.ToolInput.Model.Should().Be(ClaudeModel.Opus);
            result.ToolInput.Resume.Should().Be("agent_01ABC123XYZ789");
            result.ToolInput.RunInBackground.Should().BeTrue();
            result.ToolInput.Name.Should().Be("test-writer");
            result.ToolInput.TeamName.Should().Be("backend-team");
            result.ToolInput.Mode.Should().Be(PermissionMode.AcceptEdits);
            result.ToolInput.Isolation.Should().Be("worktree");
        }

        [TestMethod]
        public void DeserializeTaskToolInput_BackgroundAgent_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Task",
                    "tool_input": {
                        "description": "Run build watcher",
                        "prompt": "Monitor the build output and report errors.",
                        "subagent_type": "general-purpose",
                        "run_in_background": true,
                        "name": "build-watcher"
                    },
                    "tool_use_id": "toolu_01TestTaskBackground001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.RunInBackground.Should().BeTrue();
            result.ToolInput.Name.Should().Be("build-watcher");
            result.ToolInput.Model.Should().BeNull();
            result.ToolInput.Isolation.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TaskToolInput_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var input = new TaskToolInput
            {
                Description = "Search codebase",
                Prompt = "Find all files referencing the Config class.",
                SubagentType = "Explore",
                Model = ClaudeModel.Haiku,
                Resume = "agent_resume_001",
                RunInBackground = true,
                Name = "config-finder",
                TeamName = "platform",
                Mode = PermissionMode.Plan,
                Isolation = "worktree"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TaskToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskToolInput);

            result.Should().NotBeNull();
            result!.Description.Should().Be(input.Description);
            result.Prompt.Should().Be(input.Prompt);
            result.SubagentType.Should().Be(input.SubagentType);
            result.Model.Should().Be(input.Model);
            result.Resume.Should().Be(input.Resume);
            result.RunInBackground.Should().Be(input.RunInBackground);
            result.Name.Should().Be(input.Name);
            result.TeamName.Should().Be(input.TeamName);
            result.Mode.Should().Be(input.Mode);
            result.Isolation.Should().Be(input.Isolation);
        }

        [TestMethod]
        public void TaskToolInput_RoundTrip_MinimalFields_ShouldMaintainData()
        {
            var input = new TaskToolInput
            {
                Description = "Quick search",
                Prompt = "Find the main entry point.",
                SubagentType = "Explore"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TaskToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskToolInput);

            result.Should().NotBeNull();
            result!.Description.Should().Be(input.Description);
            result.Prompt.Should().Be(input.Prompt);
            result.SubagentType.Should().Be(input.SubagentType);
            result.Model.Should().BeNull();
            result.Resume.Should().BeNull();
            result.RunInBackground.Should().BeNull();
            result.Name.Should().BeNull();
            result.TeamName.Should().BeNull();
            result.Mode.Should().BeNull();
            result.Isolation.Should().BeNull();
        }

        #endregion

    }

}
