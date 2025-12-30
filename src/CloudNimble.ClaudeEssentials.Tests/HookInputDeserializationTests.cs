using CloudNimble.ClaudeEssentials.Hooks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests
{

    /// <summary>
    /// Tests for deserializing hook input payloads from JSON.
    /// </summary>
    [TestClass]
    public class HookInputDeserializationTests
    {

        #region PreToolUseHookInput Tests

        [TestMethod]
        public void DeserializePreToolUseInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "abc123",
                    "transcript_path": "/path/to/transcript.jsonl",
                    "cwd": "/home/user/project",
                    "tool_name": "Write",
                    "tool_input": { "file_path": "/test.txt", "content": "hello" },
                    "tool_use_id": "toolu_01ABC"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.SessionId.Should().Be("abc123");
            result.TranscriptPath.Should().Be("/path/to/transcript.jsonl");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
            result.ToolName.Should().Be("Write");
            result.ToolUseId.Should().Be("toolu_01ABC");
            result.ToolInput.Should().NotBeNull();
        }

        [TestMethod]
        public void DeserializePreToolUseInput_WithMinimalJson_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Read",
                    "tool_use_id": "toolu_02XYZ"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.ToolUseId.Should().Be("toolu_02XYZ");
            result.SessionId.Should().BeEmpty();
        }

        #endregion

        #region PostToolUseHookInput Tests

        [TestMethod]
        public void DeserializePostToolUseInput_WithToolResponse_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "session456",
                    "tool_name": "Bash",
                    "tool_input": { "command": "ls -la" },
                    "tool_response": { "success": true, "output": "file1.txt" },
                    "tool_use_id": "toolu_03DEF"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePostToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolResponse.Should().NotBeNull();
        }

        #endregion

        #region NotificationHookInput Tests

        [TestMethod]
        public void DeserializeNotificationInput_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "sess789",
                    "message": "Claude needs your permission to use Bash",
                    "notification_type": "PermissionPrompt"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeNotificationInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Message.Should().Be("Claude needs your permission to use Bash");
            result.NotificationType.Should().Be(NotificationType.PermissionPrompt);
        }

        #endregion

        #region UserPromptSubmitHookInput Tests

        [TestMethod]
        public void DeserializeUserPromptSubmitInput_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "sess101",
                    "prompt": "Please help me refactor this code"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeUserPromptSubmitInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Prompt.Should().Be("Please help me refactor this code");
        }

        #endregion

        #region StopHookInput Tests

        [TestMethod]
        public void DeserializeStopInput_WithStopHookActiveTrue_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "sess202",
                    "stop_hook_active": true
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeStopInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.StopHookActive.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeStopInput_WithStopHookActiveFalse_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "stop_hook_active": false
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeStopInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.StopHookActive.Should().BeFalse();
        }

        #endregion

        #region SessionStartHookInput Tests

        [TestMethod]
        public void DeserializeSessionStartInput_WithStartupSource_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "new-session",
                    "source": "Startup"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeSessionStartInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Source.Should().Be(SessionStartSource.Startup);
        }

        [TestMethod]
        public void DeserializeSessionStartInput_WithResumeSource_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "source": "Resume"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeSessionStartInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Source.Should().Be(SessionStartSource.Resume);
        }

        #endregion

        #region SessionEndHookInput Tests

        [TestMethod]
        public void DeserializeSessionEndInput_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "ending-session",
                    "reason": "Logout"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeSessionEndInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Reason.Should().Be(SessionEndReason.Logout);
        }

        #endregion

        #region PreCompactHookInput Tests

        [TestMethod]
        public void DeserializePreCompactInput_WithManualTrigger_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "trigger": "Manual",
                    "custom_instructions": "Keep function signatures"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreCompactInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Trigger.Should().Be(CompactTrigger.Manual);
            result.CustomInstructions.Should().Be("Keep function signatures");
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithAutoTrigger_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "trigger": "Auto"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreCompactInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Trigger.Should().Be(CompactTrigger.Auto);
            result.CustomInstructions.Should().BeNull();
        }

        #endregion

        #region Edge Cases

        [TestMethod]
        public void DeserializePreToolUseInput_WithEmptyJson_ShouldReturnObjectWithDefaults()
        {
            // Arrange
            var json = "{}";

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().BeEmpty();
            result.SessionId.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializePreToolUseInput_WithNullJson_ShouldReturnNull()
        {
            // Arrange
            var json = "null";

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().BeNull();
        }

        #endregion

    }

}
