using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for Bash tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class BashPayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeBashPreToolUse_WithEchoCommand_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload from Claude Code with PII stripped
            var json = """
                {
                    "session_id": "test-session-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "echo \"test bash command\"",
                        "description": "Test bash for payload capture"
                    },
                    "tool_use_id": "toolu_01TestBashCommand001"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.SessionId.Should().Be("test-session-001");
            result.ToolName.Should().Be("Bash");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.PermissionMode.Should().Be(PermissionMode.Default);
            result.ToolInput.Should().NotBeNull();
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_ToStronglyTypedInput_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "test-session-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "dotnet build --configuration Release",
                        "description": "Build the project"
                    },
                    "tool_use_id": "toolu_01TestBashBuild001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("dotnet build --configuration Release");
            result.ToolInput.Description.Should().Be("Build the project");
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithTimeout_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "npm install",
                        "description": "Install dependencies",
                        "timeout": 300000
                    },
                    "tool_use_id": "toolu_01TestBashTimeout001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Timeout.Should().Be(300000);
        }

        [TestMethod]
        public void DeserializeBashPreToolUse_WithBackgroundFlag_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "npm run dev",
                        "description": "Start dev server",
                        "run_in_background": true
                    },
                    "tool_use_id": "toolu_01TestBashBackground001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.RunInBackground.Should().BeTrue();
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeBashPostToolUse_WithSuccessfulOutput_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-003",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-003.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "echo \"test bash command\"",
                        "description": "Test bash for payload capture"
                    },
                    "tool_response": {
                        "stdout": "test bash command\r",
                        "stderr": "",
                        "interrupted": false,
                        "isImage": false
                    },
                    "tool_use_id": "toolu_01TestBashPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Command.Should().Be("echo \"test bash command\"");
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Stdout.Should().Contain("test bash command");
            result.ToolResponse.Stderr.Should().BeEmpty();
            result.ToolResponse.Interrupted.Should().BeFalse();
            result.ToolResponse.IsImage.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WithStderrOutput_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "dotnet build",
                        "description": "Build project"
                    },
                    "tool_response": {
                        "stdout": "Build succeeded.\r\n",
                        "stderr": "warning CS0168: Variable declared but never used",
                        "interrupted": false,
                        "isImage": false
                    },
                    "tool_use_id": "toolu_01TestBashStderr001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Stdout.Should().Contain("Build succeeded");
            result.ToolResponse.Stderr.Should().Contain("warning");
        }

        [TestMethod]
        public void DeserializeBashPostToolUse_WhenInterrupted_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "npm run long-task",
                        "description": "Long running task",
                        "timeout": 5000
                    },
                    "tool_response": {
                        "stdout": "Starting task...\r\n",
                        "stderr": "",
                        "interrupted": true,
                        "isImage": false
                    },
                    "tool_use_id": "toolu_01TestBashInterrupt001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Interrupted.Should().BeTrue();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void BashToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new BashToolInput
            {
                Command = "git status",
                Description = "Check git status",
                Timeout = 60000,
                RunInBackground = false
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.BashToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.Command.Should().Be(input.Command);
            result.Description.Should().Be(input.Description);
            result.Timeout.Should().Be(input.Timeout);
            result.RunInBackground.Should().Be(input.RunInBackground);
        }

        [TestMethod]
        public void BashToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new BashToolResponse
            {
                Stdout = "Hello, World!\r\n",
                Stderr = "",
                Interrupted = false,
                IsImage = false
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.BashToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Stdout.Should().Be(response.Stdout);
            result.Stderr.Should().Be(response.Stderr);
            result.Interrupted.Should().Be(response.Interrupted);
            result.IsImage.Should().Be(response.IsImage);
        }

        #endregion
    }

}