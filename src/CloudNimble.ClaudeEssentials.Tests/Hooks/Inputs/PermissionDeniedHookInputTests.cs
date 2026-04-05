using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PermissionDeniedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PermissionDeniedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePermissionDeniedInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-pd-01",
                    "tool_name": "Bash",
                    "tool_input": { "command": "rm -rf /" },
                    "tool_use_id": "toolu_04PD",
                    "reason": "Dangerous command detected"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePermissionDeniedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-pd-01");
            result.ToolName.Should().Be("Bash");
            result.ToolInput.Should().NotBeNull();
            result.ToolUseId.Should().Be("toolu_04PD");
            result.Reason.Should().Be("Dangerous command detected");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePermissionDeniedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePermissionDeniedInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePermissionDeniedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePermissionDeniedInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.ToolName.Should().BeEmpty();
            result.Reason.Should().BeEmpty();
        }

        #endregion

    }

}
