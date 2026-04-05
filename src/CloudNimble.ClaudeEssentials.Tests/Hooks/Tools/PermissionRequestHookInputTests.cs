using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="PermissionRequestHookInput{TToolInput}"/> deserialization.
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

    }

}
