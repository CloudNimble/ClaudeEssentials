using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="StopHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class StopHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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

        #endregion

    }

}
