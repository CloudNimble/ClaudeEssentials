using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PreToolUseHookInput{TToolInput}"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PreToolUseHookInputTests
    {

        #region Deserialization Tests

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

        #region Malformed Payload Tests

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
