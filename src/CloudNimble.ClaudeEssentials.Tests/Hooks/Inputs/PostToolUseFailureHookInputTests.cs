using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PostToolUseFailureHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PostToolUseFailureHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePostToolUseFailureInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-ptf-01",
                    "tool_name": "Write",
                    "tool_input": { "file_path": "/readonly/file.txt", "content": "test" },
                    "tool_use_id": "toolu_05PTF",
                    "error": "Permission denied: cannot write to /readonly/file.txt",
                    "is_interrupt": true
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePostToolUseFailureInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-ptf-01");
            result.ToolName.Should().Be("Write");
            result.ToolUseId.Should().Be("toolu_05PTF");
            result.Error.Should().Be("Permission denied: cannot write to /readonly/file.txt");
            result.IsInterrupt.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializePostToolUseFailureInput_WithoutIsInterrupt_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Bash",
                    "tool_use_id": "toolu_06PTF",
                    "error": "Command timed out"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePostToolUseFailureInput(json);

            result.Should().NotBeNull();
            result!.Error.Should().Be("Command timed out");
            result.IsInterrupt.Should().BeNull();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePostToolUseFailureInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePostToolUseFailureInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePostToolUseFailureInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePostToolUseFailureInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.Error.Should().BeEmpty();
            result.IsInterrupt.Should().BeNull();
        }

        #endregion

    }

}
