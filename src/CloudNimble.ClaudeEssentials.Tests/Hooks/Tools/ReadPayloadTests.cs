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
    /// Tests for Read tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class ReadPayloadTests
    {
        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeReadPreToolUse_WithFilePath_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "session_id": "test-session-read-001",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\README.md"
                    },
                    "tool_use_id": "toolu_01TestRead001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Be("D:\\Projects\\TestProject\\README.md");
        }

        [TestMethod]
        public void DeserializeReadPreToolUse_WithOffsetAndLimit_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\LargeFile.cs",
                        "offset": 100,
                        "limit": 50
                    },
                    "tool_use_id": "toolu_01TestReadOffset001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Contain("LargeFile.cs");
            result.ToolInput.Offset.Should().Be(100);
            result.ToolInput.Limit.Should().Be(50);
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeReadPostToolUse_WithFileContent_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code with sanitized content
            var json = """
                {
                    "session_id": "test-session-read-002",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-read-002.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\README.md"
                    },
                    "tool_response": {
                        "type": "text",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\README.md",
                            "content": "# Test Project\n\nThis is a test README file.\n\n## Features\n\n- Feature 1\n- Feature 2\n",
                            "numLines": 8,
                            "startLine": 1,
                            "totalLines": 8
                        }
                    },
                    "tool_use_id": "toolu_01TestReadPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Type.Should().Be("text");
            result.ToolResponse.File.Should().NotBeNull();
            result.ToolResponse.File!.FilePath.Should().Contain("README.md");
            result.ToolResponse.File.Content.Should().Contain("# Test Project");
            result.ToolResponse.File.NumLines.Should().Be(8);
            result.ToolResponse.File.StartLine.Should().Be(1);
            result.ToolResponse.File.TotalLines.Should().Be(8);
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithPaginatedContent_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\LargeFile.cs",
                        "offset": 50,
                        "limit": 25
                    },
                    "tool_response": {
                        "type": "text",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\LargeFile.cs",
                            "content": "    public void Method50() { }\n    public void Method51() { }\n",
                            "numLines": 25,
                            "startLine": 50,
                            "totalLines": 500
                        }
                    },
                    "tool_use_id": "toolu_01TestReadPaginated001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.File.Should().NotBeNull();
            result.ToolResponse.File!.NumLines.Should().Be(25);
            result.ToolResponse.File.StartLine.Should().Be(50);
            result.ToolResponse.File.TotalLines.Should().Be(500);
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithLargeFile_ShouldDeserializeCorrectly()
        {
            // Arrange - Simulating a larger file read
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\src\\Program.cs"
                    },
                    "tool_response": {
                        "type": "text",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\src\\Program.cs",
                            "content": "using System;\n\nnamespace TestProject\n{\n    public class Program\n    {\n        public static void Main(string[] args)\n        {\n            Console.WriteLine(\"Hello, World!\");\n        }\n    }\n}\n",
                            "numLines": 12,
                            "startLine": 1,
                            "totalLines": 12
                        }
                    },
                    "tool_use_id": "toolu_01TestReadLarge001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.File.Should().NotBeNull();
            result.ToolResponse.File!.Content.Should().Contain("using System;");
            result.ToolResponse.File.Content.Should().Contain("Hello, World!");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ReadToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new ReadToolInput
            {
                FilePath = "D:\\Projects\\TestProject\\test.cs",
                Offset = 10,
                Limit = 100
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ReadToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.FilePath.Should().Be(input.FilePath);
            result.Offset.Should().Be(input.Offset);
            result.Limit.Should().Be(input.Limit);
        }

        [TestMethod]
        public void ReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new ReadToolResponse
            {
                Type = "text",
                File = new ReadToolFileInfo
                {
                    FilePath = "D:\\test.txt",
                    Content = "Test content",
                    NumLines = 1,
                    StartLine = 1,
                    TotalLines = 1
                }
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Type.Should().Be(response.Type);
            result.File.Should().NotBeNull();
            result.File!.FilePath.Should().Be(response.File.FilePath);
            result.File.Content.Should().Be(response.File.Content);
        }

        #endregion
    }
}
