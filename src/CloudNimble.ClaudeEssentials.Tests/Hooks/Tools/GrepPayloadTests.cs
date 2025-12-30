using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for Grep tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class GrepPayloadTests
    {
        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeGrepPreToolUse_WithPatternAndPath_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-grep-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-grep-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "tool_response",
                        "path": "D:\\Projects\\TestProject\\src"
                    },
                    "tool_use_id": "toolu_01TestGrep001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Grep");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("tool_response");
            result.ToolInput.Path.Should().Contain("src");
        }

        [TestMethod]
        public void DeserializeGrepPreToolUse_WithOutputMode_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "class\\s+\\w+",
                        "path": "D:\\Projects\\TestProject",
                        "output_mode": "content"
                    },
                    "tool_use_id": "toolu_01TestGrepContent001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.OutputMode.Should().Be("content");
        }

        [TestMethod]
        public void DeserializeGrepPreToolUse_WithGlobAndType_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "TODO",
                        "glob": "*.cs",
                        "type": "cs"
                    },
                    "tool_use_id": "toolu_01TestGrepGlob001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("TODO");
            result.ToolInput.Glob.Should().Be("*.cs");
            result.ToolInput.Type.Should().Be("cs");
        }

        [TestMethod]
        public void DeserializeGrepPreToolUse_WithContextLines_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "Exception",
                        "output_mode": "content",
                        "-A": 3,
                        "-B": 2,
                        "-C": 5
                    },
                    "tool_use_id": "toolu_01TestGrepContext001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.LinesAfter.Should().Be(3);
            result.ToolInput.LinesBefore.Should().Be(2);
            result.ToolInput.LinesContext.Should().Be(5);
        }

        [TestMethod]
        public void DeserializeGrepPreToolUse_WithCaseInsensitive_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "error",
                        "-i": true
                    },
                    "tool_use_id": "toolu_01TestGrepCase001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.CaseInsensitive.Should().BeTrue();
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeGrepPostToolUse_FilesWithMatchesMode_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-grep-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "tool_response",
                        "path": "D:\\Projects\\TestProject\\src"
                    },
                    "tool_response": {
                        "mode": "files_with_matches",
                        "filenames": [
                            "src\\Tests\\DeserializationTests.cs",
                            "src\\Hooks\\Inputs\\PostToolUseHookInput.cs"
                        ],
                        "numFiles": 2
                    },
                    "tool_use_id": "toolu_01TestGrepPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Grep");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Mode.Should().Be("files_with_matches");
            result.ToolResponse.Filenames.Should().NotBeNull();
            result.ToolResponse.Filenames.Should().HaveCount(2);
            result.ToolResponse.NumFiles.Should().Be(2);
        }

        [TestMethod]
        public void DeserializeGrepPostToolUse_ContentMode_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "class Program",
                        "output_mode": "content"
                    },
                    "tool_response": {
                        "mode": "content",
                        "content": "src\\Program.cs:5:    public class Program\nsrc\\Program.cs:10:    // Program entry point",
                        "numFiles": 1
                    },
                    "tool_use_id": "toolu_01TestGrepContent001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Mode.Should().Be("content");
            result.ToolResponse.Content.Should().NotBeNull();
            result.ToolResponse.Content.Should().Contain("class Program");
        }

        [TestMethod]
        public void DeserializeGrepPostToolUse_CountMode_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "public",
                        "output_mode": "count"
                    },
                    "tool_response": {
                        "mode": "count",
                        "counts": {
                            "src\\Program.cs": 5,
                            "src\\Utils.cs": 3
                        },
                        "numFiles": 2
                    },
                    "tool_use_id": "toolu_01TestGrepCount001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Mode.Should().Be("count");
            result.ToolResponse.Counts.Should().NotBeNull();
            result.ToolResponse.Counts.Should().ContainKey("src\\Program.cs");
            result.ToolResponse.Counts!["src\\Program.cs"].Should().Be(5);
        }

        [TestMethod]
        public void DeserializeGrepPostToolUse_NoMatches_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Grep",
                    "tool_input": {
                        "pattern": "nonexistent_pattern_xyz"
                    },
                    "tool_response": {
                        "mode": "files_with_matches",
                        "filenames": [],
                        "numFiles": 0
                    },
                    "tool_use_id": "toolu_01TestGrepNoMatch001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Filenames.Should().BeEmpty();
            result.ToolResponse.NumFiles.Should().Be(0);
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void GrepToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new GrepToolInput
            {
                Pattern = "class\\s+\\w+",
                Path = "D:\\Projects\\TestProject",
                OutputMode = "content",
                Glob = "*.cs",
                CaseInsensitive = true,
                LinesAfter = 2,
                LinesBefore = 2
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.GrepToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.Pattern.Should().Be(input.Pattern);
            result.Path.Should().Be(input.Path);
            result.OutputMode.Should().Be(input.OutputMode);
            result.Glob.Should().Be(input.Glob);
            result.CaseInsensitive.Should().Be(input.CaseInsensitive);
            result.LinesAfter.Should().Be(input.LinesAfter);
            result.LinesBefore.Should().Be(input.LinesBefore);
        }

        [TestMethod]
        public void GrepToolResponse_RoundTrip_FilesWithMatchesMode_ShouldMaintainData()
        {
            // Arrange
            var response = new GrepToolResponse
            {
                Mode = "files_with_matches",
                Filenames = ["file1.cs", "file2.cs"],
                NumFiles = 2
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.GrepToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Mode.Should().Be(response.Mode);
            result.Filenames.Should().BeEquivalentTo(response.Filenames);
            result.NumFiles.Should().Be(response.NumFiles);
        }

        [TestMethod]
        public void GrepToolResponse_RoundTrip_CountMode_ShouldMaintainData()
        {
            // Arrange
            var response = new GrepToolResponse
            {
                Mode = "count",
                Counts = new Dictionary<string, int>
                {
                    ["file1.cs"] = 10,
                    ["file2.cs"] = 5
                },
                NumFiles = 2
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.GrepToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Mode.Should().Be("count");
            result.Counts.Should().NotBeNull();
            result.Counts.Should().ContainKey("file1.cs");
            result.Counts!["file1.cs"].Should().Be(10);
        }

        #endregion

    }

}
