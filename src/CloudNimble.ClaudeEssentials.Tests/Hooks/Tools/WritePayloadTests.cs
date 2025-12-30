using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for Write tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class WritePayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeWritePreToolUse_WithFileContent_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "test-session-write-001",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\logs\\test-write.txt",
                        "content": "Test file for capturing Write tool payload"
                    },
                    "tool_use_id": "toolu_01TestWrite001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Write");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.PermissionMode.Should().Be(PermissionMode.AcceptEdits);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("test-write.txt");
            result.ToolInput.Content.Should().Contain("Test file");
        }

        [TestMethod]
        public void DeserializeWritePreToolUse_WithMultilineContent_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\src\\NewClass.cs",
                        "content": "using System;\n\nnamespace TestProject\n{\n    public class NewClass\n    {\n    }\n}\n"
                    },
                    "tool_use_id": "toolu_01TestWriteMultiline001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Content.Should().Contain("using System;");
            result.ToolInput.Content.Should().Contain("namespace TestProject");
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeWritePostToolUse_CreateNewFile_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code for creating a new file
            var json = """
                {
                    "session_id": "test-session-write-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\logs\\test-write.txt",
                        "content": "Test file for capturing Write tool payload"
                    },
                    "tool_response": {
                        "type": "create",
                        "filePath": "D:\\Projects\\TestProject\\logs\\test-write.txt",
                        "content": "Test file for capturing Write tool payload",
                        "structuredPatch": [],
                        "originalFile": null
                    },
                    "tool_use_id": "toolu_01TestWritePost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Write");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Type.Should().Be("create");
            result.ToolResponse.FilePath.Should().Contain("test-write.txt");
            result.ToolResponse.Content.Should().Contain("Test file");
            result.ToolResponse.StructuredPatch.Should().BeEmpty();
            result.ToolResponse.OriginalFile.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeWritePostToolUse_OverwriteFile_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\config.json",
                        "content": "{\n  \"setting\": \"new value\"\n}"
                    },
                    "tool_response": {
                        "type": "overwrite",
                        "filePath": "D:\\Projects\\TestProject\\config.json",
                        "content": "{\n  \"setting\": \"new value\"\n}",
                        "structuredPatch": [
                            {
                                "oldStart": 1,
                                "oldLines": 3,
                                "newStart": 1,
                                "newLines": 3,
                                "lines": [
                                    " {",
                                    "-  \"setting\": \"old value\"",
                                    "+  \"setting\": \"new value\"",
                                    " }"
                                ]
                            }
                        ],
                        "originalFile": "{\n  \"setting\": \"old value\"\n}"
                    },
                    "tool_use_id": "toolu_01TestWriteOverwrite001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Type.Should().Be("overwrite");
            result.ToolResponse.OriginalFile.Should().NotBeNull();
            result.ToolResponse.OriginalFile.Should().Contain("old value");
            result.ToolResponse.StructuredPatch.Should().NotBeEmpty();
        }

        [TestMethod]
        public void DeserializeWritePostToolUse_WithCSharpContent_ShouldDeserializeCorrectly()
        {
            // Arrange - Simulating writing a C# file
            var json = """
                {
                    "tool_name": "Write",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\src\\Models\\User.cs",
                        "content": "using System;\n\nnamespace TestProject.Models\n{\n    public class User\n    {\n        public int Id { get; set; }\n        public string Name { get; set; } = string.Empty;\n    }\n}\n"
                    },
                    "tool_response": {
                        "type": "create",
                        "filePath": "D:\\Projects\\TestProject\\src\\Models\\User.cs",
                        "content": "using System;\n\nnamespace TestProject.Models\n{\n    public class User\n    {\n        public int Id { get; set; }\n        public string Name { get; set; } = string.Empty;\n    }\n}\n",
                        "structuredPatch": [],
                        "originalFile": null
                    },
                    "tool_use_id": "toolu_01TestWriteCSharp001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Type.Should().Be("create");
            result.ToolResponse.Content.Should().Contain("public class User");
            result.ToolResponse.Content.Should().Contain("public int Id { get; set; }");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void WriteToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new WriteToolInput
            {
                FilePath = "D:\\Projects\\test.txt",
                Content = "Hello, World!\nLine 2\nLine 3"
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.WriteToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WriteToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.FilePath.Should().Be(input.FilePath);
            result.Content.Should().Be(input.Content);
        }

        [TestMethod]
        public void WriteToolResponse_RoundTrip_CreateType_ShouldMaintainData()
        {
            // Arrange
            var response = new WriteToolResponse
            {
                Type = "create",
                FilePath = "D:\\test.txt",
                Content = "New file content",
                StructuredPatch = [],
                OriginalFile = null
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.WriteToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WriteToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Type.Should().Be(response.Type);
            result.FilePath.Should().Be(response.FilePath);
            result.Content.Should().Be(response.Content);
            result.StructuredPatch.Should().BeEmpty();
            result.OriginalFile.Should().BeNull();
        }

        [TestMethod]
        public void WriteToolResponse_RoundTrip_OverwriteType_ShouldMaintainData()
        {
            // Arrange
            var response = new WriteToolResponse
            {
                Type = "overwrite",
                FilePath = "D:\\test.txt",
                Content = "Updated content",
                StructuredPatch =
                [
                    new StructuredPatchHunk
                    {
                        OldStart = 1,
                        OldLines = 1,
                        NewStart = 1,
                        NewLines = 1,
                        Lines = ["-Old content", "+Updated content"]
                    }
                ],
                OriginalFile = "Old content"
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.WriteToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WriteToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Type.Should().Be("overwrite");
            result.OriginalFile.Should().Be("Old content");
            result.StructuredPatch.Should().HaveCount(1);
        }

        #endregion

    }

}
