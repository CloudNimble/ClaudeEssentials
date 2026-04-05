using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="KillShellToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class KillShellToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeKillShellToolResponse_Success_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "KillShell",
                    "tool_input": {
                        "shell_id": "shell_abc123xyz"
                    },
                    "tool_response": {
                        "shellId": "shell_abc123xyz",
                        "success": true
                    },
                    "tool_use_id": "toolu_01TestKillShellPost001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("KillShell");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.ShellId.Should().Be("shell_abc123xyz");
            result.ToolResponse.Success.Should().BeTrue();
            result.ToolResponse.Error.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeKillShellToolResponse_Failure_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "KillShell",
                    "tool_input": {
                        "shell_id": "shell_invalid999"
                    },
                    "tool_response": {
                        "shellId": "shell_invalid999",
                        "success": false,
                        "error": "Shell not found or already terminated"
                    },
                    "tool_use_id": "toolu_01TestKillShellFail001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.ShellId.Should().Be("shell_invalid999");
            result.ToolResponse.Success.Should().BeFalse();
            result.ToolResponse.Error.Should().Be("Shell not found or already terminated");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void KillShellToolResponse_RoundTrip_Success_ShouldMaintainData()
        {
            var response = new KillShellToolResponse
            {
                ShellId = "shell_roundtrip_001",
                Success = true
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.KillShellToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellToolResponse);

            result.Should().NotBeNull();
            result!.ShellId.Should().Be(response.ShellId);
            result.Success.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [TestMethod]
        public void KillShellToolResponse_RoundTrip_Failure_ShouldMaintainData()
        {
            var response = new KillShellToolResponse
            {
                ShellId = "shell_roundtrip_002",
                Success = false,
                Error = "Process could not be terminated"
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.KillShellToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellToolResponse);

            result.Should().NotBeNull();
            result!.ShellId.Should().Be(response.ShellId);
            result.Success.Should().BeFalse();
            result.Error.Should().Be(response.Error);
        }

        #endregion

    }

}
