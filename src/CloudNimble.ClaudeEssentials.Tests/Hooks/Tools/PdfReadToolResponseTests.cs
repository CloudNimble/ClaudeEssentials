using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="PdfReadToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured PDF Read logs exist in this repo.
    /// </summary>
    [TestClass]
    public class PdfReadToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePdfRead_WithPageRange_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "11111111-2222-3333-4444-555555555555",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\docs\\spec.pdf",
                        "pages": "1-5"
                    },
                    "tool_response": {
                        "type": "pdf",
                        "file": {
                            "filePath": "D:\\Projects\\TestProject\\docs\\spec.pdf",
                            "base64": "JVBERi0xLjQKMSAwIG9iago8PAovVHlwZSAvQ2F0YWxvZwovUGFnZXMgMiAwIFIKPj4KZW5kb2Jq",
                            "originalSize": 524288
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput!.Pages.Should().Be("1-5");
            result.ToolResponse.Should().BeOfType<PdfReadToolResponse>();
            var pdf = (PdfReadToolResponse)result.ToolResponse!;
            pdf.File.FilePath.Should().Contain("spec.pdf");
            pdf.File.Base64.Should().StartWith("JVBERi0");
            pdf.File.OriginalSize.Should().Be(524288);
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void PdfReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new PdfReadToolResponse
            {
                File = new PdfReadToolFileInfo
                {
                    FilePath = "/tmp/test.pdf",
                    Base64 = "JVBERi0xLjQ=",
                    OriginalSize = 1024
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().BeOfType<PdfReadToolResponse>();
            var pdf = (PdfReadToolResponse)result!;
            pdf.File.FilePath.Should().Be("/tmp/test.pdf");
            pdf.File.Base64.Should().Be("JVBERi0xLjQ=");
            pdf.File.OriginalSize.Should().Be(1024);
        }

        #endregion

    }

}
