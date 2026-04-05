using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="SessionStartHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class SessionStartHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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

        #endregion

    }

}
