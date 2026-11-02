using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="McpResourceInfo"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class ListMcpResourcesPayloadTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeListMcpResources_WithMultipleResources_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "ListMcpResources",
                    "tool_input": {
                        "server": "test-mcp-server"
                    },
                    "tool_response": [
                        {
                            "uri": "file:///workspace/config.json",
                            "name": "config.json",
                            "mimeType": "application/json",
                            "description": "Project configuration file",
                            "server": "test-mcp-server"
                        },
                        {
                            "uri": "file:///workspace/schema.graphql",
                            "name": "schema.graphql",
                            "mimeType": "text/plain",
                            "server": "test-mcp-server"
                        }
                    ],
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().HaveCount(2);

            result.ToolResponse[0].Uri.Should().Be("file:///workspace/config.json");
            result.ToolResponse[0].Name.Should().Be("config.json");
            result.ToolResponse[0].MimeType.Should().Be("application/json");
            result.ToolResponse[0].Description.Should().Be("Project configuration file");
            result.ToolResponse[0].Server.Should().Be("test-mcp-server");

            result.ToolResponse[1].Uri.Should().Be("file:///workspace/schema.graphql");
            result.ToolResponse[1].Description.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeListMcpResources_WithoutOptionalFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ListMcpResources",
                    "tool_input": {
                        "server": "minimal-server"
                    },
                    "tool_response": [
                        {
                            "uri": "db://tables/users",
                            "name": "users",
                            "server": "minimal-server"
                        }
                    ],
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().HaveCount(1);
            result.ToolResponse[0].Uri.Should().Be("db://tables/users");
            result.ToolResponse[0].Name.Should().Be("users");
            result.ToolResponse[0].MimeType.Should().BeNull();
            result.ToolResponse[0].Description.Should().BeNull();
            result.ToolResponse[0].Server.Should().Be("minimal-server");
        }

        [TestMethod]
        public void DeserializeListMcpResources_EmptyResources_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "ListMcpResources",
                    "tool_input": {
                        "server": "empty-server"
                    },
                    "tool_response": [],
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().BeEmpty();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ListMcpResourceInfo_RoundTrip_ShouldMaintainData()
        {
            var response = new List<McpResourceInfo>
            {
                new McpResourceInfo
                {
                    Uri = "file:///workspace/readme.md",
                    Name = "readme.md",
                    MimeType = "text/markdown",
                    Description = "Project readme",
                    Server = "docs-server"
                }
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ListMcpResourceInfo);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourceInfo);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result![0].Uri.Should().Be("file:///workspace/readme.md");
            result[0].Name.Should().Be("readme.md");
            result[0].MimeType.Should().Be("text/markdown");
            result[0].Description.Should().Be("Project readme");
            result[0].Server.Should().Be("docs-server");
        }

        [TestMethod]
        public void McpResourceInfo_RoundTrip_WithoutOptionals_ShouldOmitNulls()
        {
            var resource = new McpResourceInfo
            {
                Uri = "db://tables/orders",
                Name = "orders",
                Server = "db-server"
            };

            var json = JsonSerializer.Serialize(resource, ClaudeHooksJsonContext.Default.McpResourceInfo);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpResourceInfo);

            result.Should().NotBeNull();
            result!.Uri.Should().Be("db://tables/orders");
            result.Name.Should().Be("orders");
            result.Server.Should().Be("db-server");
            result.MimeType.Should().BeNull();
            result.Description.Should().BeNull();
        }

        #endregion

    }

}
