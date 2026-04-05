using System.Text.Json;
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
                    "notification_type": "permission_prompt"
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
                    "source": "startup"
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
                    "source": "resume"
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
                    "reason": "logout"
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
                    "trigger": "manual",
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
                    "trigger": "auto"
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

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePostToolUseInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePostToolUseInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePostToolUseInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializePostToolUseInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePostToolUseInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePostToolUseInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.ToolName.Should().BeEmpty();
            result.ToolInput.Should().BeNull();
            result.ToolResponse.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePostToolUseInput_WithArrayInput_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePostToolUseInput("[1, 2, 3]");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeNotificationInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeNotificationInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeNotificationInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeNotificationInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeNotificationInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeNotificationInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.Message.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializeNotificationInput_WithNumberLiteral_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeNotificationInput("42");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeUserPromptSubmitInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeUserPromptSubmitInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeUserPromptSubmitInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeUserPromptSubmitInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeUserPromptSubmitInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeUserPromptSubmitInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.Prompt.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializeUserPromptSubmitInput_WithTruncatedJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeUserPromptSubmitInput("{\"prompt\": \"hello");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeStopInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeStopInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeStopInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeStopInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeStopInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeStopInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.StopHookActive.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSubagentStopInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.StopHookActive.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePreCompactInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializePreCompactInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePreCompactInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.CustomInstructions.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithNumberLiteral_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePreCompactInput("99");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSessionStartInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSessionStartInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSessionStartInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeSessionStartInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeSessionStartInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeSessionStartInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializeSessionStartInput_WithArrayInput_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSessionStartInput("[\"a\", \"b\"]");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSessionEndInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSessionEndInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSessionEndInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeSessionEndInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeSessionEndInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeSessionEndInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializePermissionRequestInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePermissionRequestInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePermissionRequestInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializePermissionRequestInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePermissionRequestInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePermissionRequestInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        [TestMethod]
        public void DeserializePermissionRequestInput_WithStringLiteral_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePermissionRequestInput("\"just a string\"");
            action.Should().Throw<JsonException>();
        }

        #endregion

    }

}
