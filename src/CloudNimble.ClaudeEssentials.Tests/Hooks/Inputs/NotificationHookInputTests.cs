using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="NotificationHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class NotificationHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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

        #endregion

    }

}
