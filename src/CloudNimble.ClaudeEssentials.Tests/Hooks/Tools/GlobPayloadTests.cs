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
    /// Tests for Glob tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class GlobPayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeGlobPreToolUse_WithPatternAndPath_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-glob-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-glob-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "**/*.cs",
                        "path": "D:\\Projects\\TestProject\\src\\Hooks\\Tools"
                    },
                    "tool_use_id": "toolu_01TestGlob001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Glob");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("**/*.cs");
            result.ToolInput.Path.Should().Contain("Hooks\\Tools");
        }

        [TestMethod]
        public void DeserializeGlobPreToolUse_WithPatternOnly_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "*.json"
                    },
                    "tool_use_id": "toolu_01TestGlobPattern001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Be("*.json");
            result.ToolInput.Path.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void DeserializeGlobPreToolUse_WithComplexPattern_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "src/**/*.{cs,csproj,json}",
                        "path": "D:\\Projects\\TestProject"
                    },
                    "tool_use_id": "toolu_01TestGlobComplex001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Pattern.Should().Contain("**/*.{cs,csproj,json}");
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeGlobPostToolUse_WithMultipleFiles_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-glob-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "**/*.cs",
                        "path": "D:\\Projects\\TestProject\\src\\Hooks\\Tools"
                    },
                    "tool_response": {
                        "filenames": [
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\ReadToolInput.cs",
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\WriteToolInput.cs",
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\EditToolInput.cs",
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\BashToolInput.cs",
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\GlobToolInput.cs",
                            "D:\\Projects\\TestProject\\src\\Hooks\\Tools\\GrepToolInput.cs"
                        ],
                        "durationMs": 721,
                        "numFiles": 6,
                        "truncated": false
                    },
                    "tool_use_id": "toolu_01TestGlobPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Glob");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Filenames.Should().NotBeNull();
            result.ToolResponse.Filenames.Should().HaveCount(6);
            result.ToolResponse.DurationMs.Should().Be(721);
            result.ToolResponse.NumFiles.Should().Be(6);
            result.ToolResponse.Truncated.Should().BeFalse();
        }

        [TestMethod]
        public void DeserializeGlobPostToolUse_WithTruncatedResults_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "**/*"
                    },
                    "tool_response": {
                        "filenames": [
                            "file1.cs",
                            "file2.cs",
                            "file3.cs"
                        ],
                        "durationMs": 1500,
                        "numFiles": 3,
                        "truncated": true
                    },
                    "tool_use_id": "toolu_01TestGlobTruncated001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Truncated.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeGlobPostToolUse_WithNoMatches_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "*.nonexistent"
                    },
                    "tool_response": {
                        "filenames": [],
                        "durationMs": 50,
                        "numFiles": 0,
                        "truncated": false
                    },
                    "tool_use_id": "toolu_01TestGlobEmpty001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Filenames.Should().BeEmpty();
            result.ToolResponse.NumFiles.Should().Be(0);
        }

        [TestMethod]
        public void DeserializeGlobPostToolUse_WithSingleFile_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "Glob",
                    "tool_input": {
                        "pattern": "README.md"
                    },
                    "tool_response": {
                        "filenames": [
                            "D:\\Projects\\TestProject\\README.md"
                        ],
                        "durationMs": 25,
                        "numFiles": 1,
                        "truncated": false
                    },
                    "tool_use_id": "toolu_01TestGlobSingle001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Filenames.Should().HaveCount(1);
            result.ToolResponse.Filenames.First().Should().Contain("README.md");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void GlobToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new GlobToolInput
            {
                Pattern = "**/*.cs",
                Path = "D:\\Projects\\TestProject"
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.GlobToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.Pattern.Should().Be(input.Pattern);
            result.Path.Should().Be(input.Path);
        }

        [TestMethod]
        public void GlobToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new GlobToolResponse
            {
                Filenames = ["file1.cs", "file2.cs", "file3.cs"],
                DurationMs = 150,
                NumFiles = 3,
                Truncated = false
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.GlobToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Filenames.Should().BeEquivalentTo(response.Filenames);
            result.DurationMs.Should().Be(response.DurationMs);
            result.NumFiles.Should().Be(response.NumFiles);
            result.Truncated.Should().Be(response.Truncated);
        }

        [TestMethod]
        public void GlobToolResponse_RoundTrip_WithTruncation_ShouldMaintainData()
        {
            // Arrange
            var response = new GlobToolResponse
            {
                Filenames = ["file1.cs"],
                DurationMs = 5000,
                NumFiles = 1,
                Truncated = true
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.GlobToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Truncated.Should().BeTrue();
        }

        #endregion

    }

}
