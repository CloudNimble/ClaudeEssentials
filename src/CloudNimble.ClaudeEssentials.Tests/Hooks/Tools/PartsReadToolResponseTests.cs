using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="PartsReadToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured parts Read logs exist in this repo.
    /// </summary>
    [TestClass]
    public class PartsReadToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePartsRead_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\docs\\large-manual.pdf",
                        "pages": "1-20"
                    },
                    "tool_response": {
                        "type": "parts",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\docs\\large-manual.pdf",
                            "originalSize": 8388608,
                            "count": 20,
                            "outputDir": "C:\\Users\\TestUser\\AppData\\Local\\Temp\\pdf-parts-abc123"
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeOfType<PartsReadToolResponse>();
            var parts = (PartsReadToolResponse)result.ToolResponse!;
            parts.File.FilePath.Should().Contain("large-manual.pdf");
            parts.File.OriginalSize.Should().Be(8388608);
            parts.File.Count.Should().Be(20);
            parts.File.OutputDir.Should().Contain("pdf-parts-abc123");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void PartsReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new PartsReadToolResponse
            {
                File = new PartsReadToolFileInfo
                {
                    FilePath = "/tmp/big.pdf",
                    OriginalSize = 5000000,
                    Count = 50,
                    OutputDir = "/tmp/parts-output"
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().BeOfType<PartsReadToolResponse>();
            var parts = (PartsReadToolResponse)result!;
            parts.File.FilePath.Should().Be("/tmp/big.pdf");
            parts.File.OriginalSize.Should().Be(5000000);
            parts.File.Count.Should().Be(50);
            parts.File.OutputDir.Should().Be("/tmp/parts-output");
        }

        #endregion

    }

}
