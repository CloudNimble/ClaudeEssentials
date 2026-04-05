using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="UserPromptSubmitHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class UserPromptSubmitHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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

        #endregion

    }

}
