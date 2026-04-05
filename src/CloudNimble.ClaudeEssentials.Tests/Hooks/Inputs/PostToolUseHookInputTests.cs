using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PostToolUseHookInput{TToolInput, TToolResponse}"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PostToolUseHookInputTests
    {

        #region Deserialization Tests

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

        #endregion

    }

}
