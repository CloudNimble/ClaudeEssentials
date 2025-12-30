using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for Edit tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class EditPayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeEditPreToolUse_WithStringReplacement_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-edit-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-edit-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\test-file.txt",
                        "old_string": "Test file for capturing Write tool payload",
                        "new_string": "Test file for capturing Write and Edit tool payloads"
                    },
                    "tool_use_id": "toolu_01TestEdit001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Edit");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.PermissionMode.Should().Be(PermissionMode.AcceptEdits);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("test-file.txt");
            result.ToolInput.OldString.Should().Contain("Write tool payload");
            result.ToolInput.NewString.Should().Contain("Write and Edit tool payloads");
        }

        [TestMethod]
        public void DeserializeEditPreToolUse_WithReplaceAll_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\Program.cs",
                        "old_string": "oldVariableName",
                        "new_string": "newVariableName",
                        "replace_all": true
                    },
                    "tool_use_id": "toolu_01TestEditReplaceAll001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.ReplaceAll.Should().BeTrue();
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeEditPostToolUse_WithStructuredPatch_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code with sanitized content
            var json = """
                {
                    "session_id": "test-session-edit-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\test-file.txt",
                        "old_string": "Test file for capturing Write tool payload",
                        "new_string": "Test file for capturing Write and Edit tool payloads"
                    },
                    "tool_response": {
                        "filePath": "D:\\Projects\\TestProject\\test-file.txt",
                        "oldString": "Test file for capturing Write tool payload",
                        "newString": "Test file for capturing Write and Edit tool payloads",
                        "originalFile": "Test file for capturing Write tool payload",
                        "structuredPatch": [
                            {
                                "oldStart": 1,
                                "oldLines": 1,
                                "newStart": 1,
                                "newLines": 1,
                                "lines": [
                                    "-Test file for capturing Write tool payload",
                                    "\\ No newline at end of file",
                                    "+Test file for capturing Write and Edit tool payloads",
                                    "\\ No newline at end of file"
                                ]
                            }
                        ],
                        "userModified": false,
                        "replaceAll": false
                    },
                    "tool_use_id": "toolu_01TestEditPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Edit");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.FilePath.Should().Contain("test-file.txt");
            result.ToolResponse.OldString.Should().Contain("Write tool payload");
            result.ToolResponse.NewString.Should().Contain("Write and Edit tool payloads");
            result.ToolResponse.OriginalFile.Should().NotBeEmpty();
            result.ToolResponse.StructuredPatch.Should().NotBeEmpty();
            result.ToolResponse.StructuredPatch.Should().HaveCount(1);
            result.ToolResponse.UserModified.Should().BeFalse();
            result.ToolResponse.ReplaceAll.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeEditPostToolUse_StructuredPatchHunk_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\Program.cs",
                        "old_string": "    var oldValue = 42;",
                        "new_string": "    var newValue = 100;"
                    },
                    "tool_response": {
                        "filePath": "D:\\Projects\\TestProject\\Program.cs",
                        "oldString": "    var oldValue = 42;",
                        "newString": "    var newValue = 100;",
                        "originalFile": "using System;\n\nclass Program\n{\n    static void Main()\n    {\n        var oldValue = 42;\n        Console.WriteLine(oldValue);\n    }\n}",
                        "structuredPatch": [
                            {
                                "oldStart": 5,
                                "oldLines": 3,
                                "newStart": 5,
                                "newLines": 3,
                                "lines": [
                                    " // Context line before",
                                    "-    var oldValue = 42;",
                                    "+    var newValue = 100;",
                                    " // Context line after"
                                ]
                            }
                        ],
                        "userModified": false,
                        "replaceAll": false
                    },
                    "tool_use_id": "toolu_01TestEditPatch001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();

            var hunk = result.ToolResponse!.StructuredPatch.First();
            hunk.OldStart.Should().Be(5);
            hunk.OldLines.Should().Be(3);
            hunk.NewStart.Should().Be(5);
            hunk.NewLines.Should().Be(3);
            hunk.Lines.Should().HaveCount(4);
            hunk.Lines.Should().Contain(l => l.StartsWith("-"));
            hunk.Lines.Should().Contain(l => l.StartsWith("+"));
        }

        [TestMethod]
        public void DeserializeEditPostToolUse_WithUserModified_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Edit",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\config.json",
                        "old_string": "\"debug\": false",
                        "new_string": "\"debug\": true"
                    },
                    "tool_response": {
                        "filePath": "D:\\Projects\\TestProject\\config.json",
                        "oldString": "\"debug\": false",
                        "newString": "\"debug\": true",
                        "originalFile": "{\n  \"debug\": false\n}",
                        "structuredPatch": [],
                        "userModified": true,
                        "replaceAll": false
                    },
                    "tool_use_id": "toolu_01TestEditUserMod001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.UserModified.Should().BeTrue();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void EditToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new EditToolInput
            {
                FilePath = "D:\\Projects\\test.cs",
                OldString = "old code",
                NewString = "new code",
                ReplaceAll = true
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.EditToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.FilePath.Should().Be(input.FilePath);
            result.OldString.Should().Be(input.OldString);
            result.NewString.Should().Be(input.NewString);
            result.ReplaceAll.Should().Be(input.ReplaceAll);
        }

        [TestMethod]
        public void EditToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new EditToolResponse
            {
                FilePath = "D:\\test.cs",
                OldString = "old",
                NewString = "new",
                OriginalFile = "original content",
                StructuredPatch =
                [
                    new StructuredPatchHunk
                    {
                        OldStart = 1,
                        OldLines = 1,
                        NewStart = 1,
                        NewLines = 1,
                        Lines = ["-old", "+new"]
                    }
                ],
                UserModified = false,
                ReplaceAll = false
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.EditToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.FilePath.Should().Be(response.FilePath);
            result.OldString.Should().Be(response.OldString);
            result.NewString.Should().Be(response.NewString);
            result.StructuredPatch.Should().HaveCount(1);
        }

        [TestMethod]
        public void StructuredPatchHunk_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var hunk = new StructuredPatchHunk
            {
                OldStart = 10,
                OldLines = 5,
                NewStart = 10,
                NewLines = 7,
                Lines = [" context", "-removed", "+added", " context"]
            };

            // Act
            var json = JsonSerializer.Serialize(hunk, ClaudeHooksJsonContext.Default.StructuredPatchHunk);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.StructuredPatchHunk);

            // Assert
            result.Should().NotBeNull();
            result!.OldStart.Should().Be(hunk.OldStart);
            result.OldLines.Should().Be(hunk.OldLines);
            result.NewStart.Should().Be(hunk.NewStart);
            result.NewLines.Should().Be(hunk.NewLines);
            result.Lines.Should().BeEquivalentTo(hunk.Lines);
        }

        #endregion
    }

}
