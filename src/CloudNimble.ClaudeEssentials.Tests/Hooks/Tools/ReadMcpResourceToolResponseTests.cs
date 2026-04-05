using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ReadMcpResourceToolResponse"/> and <see cref="McpResourceContent"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class ReadMcpResourceToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeReadMcpResource_WithTextContent_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "ReadMcpResource",
                    "tool_input": {
                        "uri": "file:///workspace/config.json",
                        "server": "test-mcp-server"
                    },
                    "tool_response": {
                        "contents": [
                            {
                                "uri": "file:///workspace/config.json",
                                "mimeType": "application/json",
                                "text": "{ \"key\": \"value\" }"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourcePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Contents.Should().HaveCount(1);
            result.ToolResponse.Contents[0].Uri.Should().Be("file:///workspace/config.json");
            result.ToolResponse.Contents[0].MimeType.Should().Be("application/json");
            result.ToolResponse.Contents[0].Text.Should().Contain("key");
            result.ToolResponse.Contents[0].BlobSavedTo.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeReadMcpResource_WithBlobContent_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ReadMcpResource",
                    "tool_input": {
                        "uri": "file:///workspace/image.png",
                        "server": "test-mcp-server"
                    },
                    "tool_response": {
                        "contents": [
                            {
                                "uri": "file:///workspace/image.png",
                                "mimeType": "image/png",
                                "blobSavedTo": "/tmp/mcp-blobs/image-abc123.png"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourcePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Contents.Should().HaveCount(1);
            result.ToolResponse.Contents[0].Uri.Should().Be("file:///workspace/image.png");
            result.ToolResponse.Contents[0].MimeType.Should().Be("image/png");
            result.ToolResponse.Contents[0].Text.Should().BeNull();
            result.ToolResponse.Contents[0].BlobSavedTo.Should().Be("/tmp/mcp-blobs/image-abc123.png");
        }

        [TestMethod]
        public void DeserializeReadMcpResource_WithMinimalContent_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ReadMcpResource",
                    "tool_input": {
                        "uri": "db://tables/users",
                        "server": "db-server"
                    },
                    "tool_response": {
                        "contents": [
                            {
                                "uri": "db://tables/users",
                                "text": "id,name,email\n1,Alice,alice@example.com"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourcePostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Contents.Should().HaveCount(1);
            result.ToolResponse.Contents[0].Uri.Should().Be("db://tables/users");
            result.ToolResponse.Contents[0].MimeType.Should().BeNull();
            result.ToolResponse.Contents[0].Text.Should().Contain("Alice");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ReadMcpResourceToolResponse_RoundTrip_TextContent_ShouldMaintainData()
        {
            var response = new ReadMcpResourceToolResponse
            {
                Contents =
                [
                    new McpResourceContent
                    {
                        Uri = "file:///workspace/data.csv",
                        MimeType = "text/csv",
                        Text = "col1,col2\nval1,val2"
                    }
                ]
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ReadMcpResourceToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourceToolResponse);

            result.Should().NotBeNull();
            result!.Contents.Should().HaveCount(1);
            result.Contents[0].Uri.Should().Be("file:///workspace/data.csv");
            result.Contents[0].MimeType.Should().Be("text/csv");
            result.Contents[0].Text.Should().Contain("col1");
            result.Contents[0].BlobSavedTo.Should().BeNull();
        }

        [TestMethod]
        public void McpResourceContent_RoundTrip_BlobContent_ShouldMaintainData()
        {
            var content = new McpResourceContent
            {
                Uri = "file:///workspace/archive.zip",
                MimeType = "application/zip",
                BlobSavedTo = "/tmp/mcp-blobs/archive-xyz.zip"
            };

            var json = JsonSerializer.Serialize(content, ClaudeHooksJsonContext.Default.McpResourceContent);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpResourceContent);

            result.Should().NotBeNull();
            result!.Uri.Should().Be("file:///workspace/archive.zip");
            result.MimeType.Should().Be("application/zip");
            result.Text.Should().BeNull();
            result.BlobSavedTo.Should().Be("/tmp/mcp-blobs/archive-xyz.zip");
        }

        #endregion

    }

}
