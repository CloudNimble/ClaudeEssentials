using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="FileUnchangedReadToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured file_unchanged Read logs exist in this repo.
    /// </summary>
    [TestClass]
    public class FileUnchangedReadToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeFileUnchangedRead_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "33333333-4444-5555-6666-777777777777",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\config.json"
                    },
                    "tool_response": {
                        "type": "file_unchanged",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\config.json"
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("Read");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().BeOfType<FileUnchangedReadToolResponse>();
            var unchanged = (FileUnchangedReadToolResponse)result.ToolResponse!;
            unchanged.File.FilePath.Should().Contain("config.json");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void FileUnchangedReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new FileUnchangedReadToolResponse
            {
                File = new FileUnchangedReadToolFileInfo
                {
                    FilePath = "/tmp/unchanged.txt"
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().BeOfType<FileUnchangedReadToolResponse>();
            var unchanged = (FileUnchangedReadToolResponse)result!;
            unchanged.File.FilePath.Should().Be("/tmp/unchanged.txt");
        }

        #endregion

    }

}
