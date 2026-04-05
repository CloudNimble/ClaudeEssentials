using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PermissionRequestHookInput{TToolInput}"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PermissionRequestHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePermissionRequestInput_WithMessage_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PermissionRequest",
                    "tool_name": "Bash",
                    "tool_input": {
                        "command": "rm -rf temp/"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA",
                    "message": "Allow Bash to execute: rm -rf temp/?"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePermissionRequestInput(json);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Bash");
            result.HookEventName.Should().Be(HookEventName.PermissionRequest);
            result.Message.Should().Be("Allow Bash to execute: rm -rf temp/?");
            result.ToolInput.Should().NotBeNull();
        }

        [TestMethod]
        public void DeserializePermissionRequestInput_WithoutMessage_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "11111111-2222-3333-4444-555555555555",
                    "cwd": "D:\\Projects\\TestProject",
                    "hook_event_name": "PermissionRequest",
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\output.txt",
                        "content": "test"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePermissionRequestInput(json);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Write");
            result.Message.Should().BeNull();
        }

        #endregion

        #region Malformed Payload Tests

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
