using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="SessionEndHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class SessionEndHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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

        #endregion

    }

}
