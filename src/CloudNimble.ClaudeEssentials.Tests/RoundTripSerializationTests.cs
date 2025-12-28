using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Enums;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests
{

    /// <summary>
    /// Tests for verifying that JSON payloads can be deserialized and re-serialized correctly.
    /// </summary>
    [TestClass]
    public class RoundTripSerializationTests
    {

        #region Input Round-Trip Tests

        [TestMethod]
        public void PreToolUseInput_RoundTrip_ShouldPreserveData()
        {
            // Arrange
            var originalJson = """
                {
                    "session_id": "test-session",
                    "transcript_path": "/path/to/transcript.jsonl",
                    "cwd": "/home/user",
                    "tool_name": "Write",
                    "tool_input": { "file_path": "/test.txt" },
                    "tool_use_id": "toolu_123"
                }
                """;

            // Act
            var input = ClaudeHooksSerializer.DeserializePreToolUseInput(originalJson);
            var reserialized = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.PreToolUseHookInputObject);
            var reparsed = ClaudeHooksSerializer.DeserializePreToolUseInput(reserialized);

            // Assert
            reparsed.Should().NotBeNull();
            reparsed!.SessionId.Should().Be(input!.SessionId);
            reparsed.ToolName.Should().Be(input.ToolName);
            reparsed.ToolUseId.Should().Be(input.ToolUseId);
            reparsed.CurrentWorkingDirectory.Should().Be(input.CurrentWorkingDirectory);
        }

        [TestMethod]
        public void StopInput_RoundTrip_ShouldPreserveData()
        {
            // Arrange
            var originalJson = """
                {
                    "session_id": "session-456",
                    "stop_hook_active": true
                }
                """;

            // Act
            var input = ClaudeHooksSerializer.DeserializeStopInput(originalJson);
            var reserialized = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.StopHookInput);
            var reparsed = ClaudeHooksSerializer.DeserializeStopInput(reserialized);

            // Assert
            reparsed.Should().NotBeNull();
            reparsed!.SessionId.Should().Be(input!.SessionId);
            reparsed.StopHookActive.Should().Be(input.StopHookActive);
        }

        #endregion

        #region Output Round-Trip Tests

        [TestMethod]
        public void PreToolUseOutput_RoundTrip_ShouldPreserveData()
        {
            // Arrange
            var original = new PreToolUseHookOutput<object>
            {
                Continue = true,
                SystemMessage = "Warning: large file",
                HookSpecificOutput = new PreToolUseSpecificOutput<object>
                {
                    PermissionDecision = PermissionDecision.Allow,
                    PermissionDecisionReason = "Approved by policy"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(original);
            var doc = JsonDocument.Parse(json);

            // Assert - verify structure is correct
            doc.RootElement.GetProperty("continue").GetBoolean().Should().Be(original.Continue);
            doc.RootElement.GetProperty("systemMessage").GetString().Should().Be(original.SystemMessage);
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecision").GetString().Should().Be("Allow");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecisionReason").GetString()
                .Should().Be("Approved by policy");
        }

        [TestMethod]
        public void StopOutput_RoundTrip_ShouldPreserveData()
        {
            // Arrange
            var original = new StopHookOutput
            {
                Continue = false,
                StopReason = "User requested stop",
                Decision = HookDecision.Block,
                Reason = "Blocking because tests failed"
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeStopOutput(original);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("continue").GetBoolean().Should().Be(original.Continue);
            doc.RootElement.GetProperty("stopReason").GetString().Should().Be(original.StopReason);
            doc.RootElement.GetProperty("decision").GetString().Should().Be("Block");
            doc.RootElement.GetProperty("reason").GetString().Should().Be(original.Reason);
        }

        [TestMethod]
        public void PostToolUseOutput_RoundTrip_ShouldPreserveData()
        {
            // Arrange
            var original = new PostToolUseHookOutput
            {
                Continue = true,
                Decision = HookDecision.Allow,
                HookSpecificOutput = new PostToolUseSpecificOutput
                {
                    AdditionalContext = "File was modified successfully"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePostToolUseOutput(original);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("decision").GetString().Should().Be("Allow");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString()
                .Should().Be("File was modified successfully");
        }

        #endregion

        #region Real-World Payload Tests

        [TestMethod]
        public void RealWorldPreToolUsePayload_ShouldDeserializeCorrectly()
        {
            // Arrange - simulates actual Claude Code payload
            var json = """
                {
                    "session_id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                    "transcript_path": "/Users/dev/.claude/transcripts/session.jsonl",
                    "cwd": "/Users/dev/myproject",
                    "permission_mode": "Default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "/Users/dev/myproject/src/main.cs",
                        "old_string": "Console.WriteLine",
                        "new_string": "Debug.WriteLine"
                    },
                    "tool_use_id": "toolu_01XyZ987654321"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.SessionId.Should().NotBeEmpty();
            result.ToolName.Should().Be("Edit");
            result.PermissionMode.Should().Be(PermissionMode.Default);
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
        }

        [TestMethod]
        public void RealWorldSessionStartPayload_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "new-session-id",
                    "transcript_path": "/tmp/transcript.jsonl",
                    "cwd": "/home/user/project",
                    "permission_mode": "AcceptEdits",
                    "hook_event_name": "SessionStart",
                    "source": "Startup",
                    "env_file": "/tmp/env_vars.txt"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializeSessionStartInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Source.Should().Be(SessionStartSource.Startup);
            result.EnvironmentFilePath.Should().Be("/tmp/env_vars.txt");
            result.PermissionMode.Should().Be(PermissionMode.AcceptEdits);
        }

        #endregion

        #region JSON Property Name Tests

        [TestMethod]
        public void Output_ShouldUseSnakeCasePropertyNames()
        {
            // Arrange
            var output = new PreToolUseHookOutput<object>
            {
                Continue = true,
                StopReason = "test",
                SuppressOutput = true,
                SystemMessage = "warning",
                HookSpecificOutput = new PreToolUseSpecificOutput<object>
                {
                    PermissionDecision = PermissionDecision.Ask,
                    PermissionDecisionReason = "reason"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(output);

            // Assert
            json.Should().Contain("\"continue\":");
            json.Should().Contain("\"stopReason\":");
            json.Should().Contain("\"suppressOutput\":");
            json.Should().Contain("\"systemMessage\":");
            json.Should().Contain("\"hookSpecificOutput\":");
            json.Should().Contain("\"permissionDecision\":");
            json.Should().Contain("\"permissionDecisionReason\":");
        }

        [TestMethod]
        public void Input_ShouldParseSnakeCasePropertyNames()
        {
            // Arrange - uses snake_case as Claude Code sends
            var json = """
                {
                    "session_id": "test",
                    "transcript_path": "/path",
                    "cwd": "/dir",
                    "permission_mode": "Default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Bash",
                    "tool_input": {},
                    "tool_use_id": "id123"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.SessionId.Should().Be("test");
            result.TranscriptPath.Should().Be("/path");
            result.CurrentWorkingDirectory.Should().Be("/dir");
        }

        #endregion

    }

}
