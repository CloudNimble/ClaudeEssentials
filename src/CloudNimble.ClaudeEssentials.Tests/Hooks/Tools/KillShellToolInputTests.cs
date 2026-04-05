using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="KillShellToolInput"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class KillShellToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeKillShellToolInput_WithShellId_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "KillShell",
                    "tool_input": {
                        "shell_id": "shell_abc123xyz"
                    },
                    "tool_use_id": "toolu_01TestKillShell001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("KillShell");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.ShellId.Should().Be("shell_abc123xyz");
        }

        [TestMethod]
        public void DeserializeKillShellToolInput_MinimalPayload_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "KillShell",
                    "tool_input": {
                        "shell_id": "shell_def456"
                    },
                    "tool_use_id": "toolu_01TestKillShellMinimal001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.ShellId.Should().Be("shell_def456");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void KillShellToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new KillShellToolInput
            {
                ShellId = "shell_roundtrip_001"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.KillShellToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellToolInput);

            result.Should().NotBeNull();
            result!.ShellId.Should().Be(input.ShellId);
        }

        #endregion

    }

}
