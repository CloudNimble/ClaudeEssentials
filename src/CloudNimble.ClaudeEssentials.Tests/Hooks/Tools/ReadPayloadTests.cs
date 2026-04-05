using System;
using CloudNimble.ClaudeEssentials.Hooks;
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

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.FilePath.Should().Be("D:\\Projects\\TestProject\\README.md");
        }

        [TestMethod]
        public void DeserializeReadPreToolUse_WithOffsetAndLimit_ShouldDeserializeCorrectly()
        {
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

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);

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

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<TextReadToolResponse>();
            var textResponse = (TextReadToolResponse)result.ToolResponse!;
            textResponse.File.Should().NotBeNull();
            textResponse.File.FilePath.Should().Contain("README.md");
            textResponse.File.Content.Should().Contain("# Test Project");
            textResponse.File.NumLines.Should().Be(8);
            textResponse.File.StartLine.Should().Be(1);
            textResponse.File.TotalLines.Should().Be(8);
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithPaginatedContent_ShouldDeserializeCorrectly()
        {
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

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<TextReadToolResponse>();
            var textResponse = (TextReadToolResponse)result.ToolResponse!;
            textResponse.File.Should().NotBeNull();
            textResponse.File.NumLines.Should().Be(25);
            textResponse.File.StartLine.Should().Be(50);
            textResponse.File.TotalLines.Should().Be(500);
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithLargeFile_ShouldDeserializeCorrectly()
        {
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

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeOfType<TextReadToolResponse>();
            var textResponse = (TextReadToolResponse)result.ToolResponse!;
            textResponse.File.Should().NotBeNull();
            textResponse.File.Content.Should().Contain("using System;");
            textResponse.File.Content.Should().Contain("Hello, World!");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ReadToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new ReadToolInput
            {
                FilePath = "D:\\Projects\\TestProject\\test.cs",
                Offset = 10,
                Limit = 100
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ReadToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolInput);

            result.Should().NotBeNull();
            result!.FilePath.Should().Be(input.FilePath);
            result.Offset.Should().Be(input.Offset);
            result.Limit.Should().Be(input.Limit);
        }

        [TestMethod]
        public void TextReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new TextReadToolResponse
            {
                File = new ReadToolFileInfo
                {
                    FilePath = "D:\\test.txt",
                    Content = "Test content",
                    NumLines = 1,
                    StartLine = 1,
                    TotalLines = 1
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().NotBeNull();
            result.Should().BeOfType<TextReadToolResponse>();
            var textResult = (TextReadToolResponse)result!;
            textResult.File.Should().NotBeNull();
            textResult.File.FilePath.Should().Be(response.File.FilePath);
            textResult.File.Content.Should().Be(response.File.Content);
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeReadPostToolUse_WithMissingTypeDiscriminator_ShouldThrowNotSupportedException()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\README.md"
                    },
                    "tool_response": {
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\README.md",
                            "content": "# Test",
                            "numLines": 1,
                            "startLine": 1,
                            "totalLines": 1
                        }
                    },
                    "tool_use_id": "toolu_01TestMalformed001"
                }
                """;

            var action = () => JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            action.Should().Throw<NotSupportedException>();
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithUnknownTypeValue_ShouldThrowJsonException()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\README.md"
                    },
                    "tool_response": {
                        "type": "unknown_type_value",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\README.md",
                            "content": "# Test",
                            "numLines": 1,
                            "startLine": 1,
                            "totalLines": 1
                        }
                    },
                    "tool_use_id": "toolu_01TestMalformed002"
                }
                """;

            var action = () => JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeReadPostToolUse_WithNullToolResponse_ShouldHaveNullToolResponse()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\README.md"
                    },
                    "tool_response": null,
                    "tool_use_id": "toolu_01TestMalformed003"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeReadPreToolUse_WithFilePathAsNumber_ShouldThrowJsonException()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": 12345
                    },
                    "tool_use_id": "toolu_01TestMalformed004"
                }
                """;

            var action = () => JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);

            action.Should().Throw<JsonException>();
        }

        #endregion
    }
}
