using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="McpToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class McpToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void McpToolInput_DeserializeWithMultipleProperties_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "server": "my-mcp-server",
                    "tool": "search_docs",
                    "query": "authentication"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpToolInput);

            result.Should().NotBeNull();
            result!.ExtensionData.Should().NotBeNull();
            result.ExtensionData.Should().HaveCount(3);
            result.ExtensionData!["server"].GetString().Should().Be("my-mcp-server");
            result.ExtensionData["tool"].GetString().Should().Be("search_docs");
            result.ExtensionData["query"].GetString().Should().Be("authentication");
        }

        [TestMethod]
        public void McpToolInput_DeserializeWithEmptyObject_ShouldDeserializeCorrectly()
        {
            var json = """
                {}
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpToolInput);

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void McpToolInput_DeserializeWithMixedValueTypes_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "server": "data-server",
                    "limit": 50,
                    "include_metadata": true
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpToolInput);

            result.Should().NotBeNull();
            result!.ExtensionData.Should().NotBeNull();
            result.ExtensionData!["server"].GetString().Should().Be("data-server");
            result.ExtensionData["limit"].GetInt32().Should().Be(50);
            result.ExtensionData["include_metadata"].GetBoolean().Should().BeTrue();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void McpToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new McpToolInput
            {
                ExtensionData = new Dictionary<string, JsonElement>
                {
                    ["server"] = JsonSerializer.SerializeToElement("test-server"),
                    ["action"] = JsonSerializer.SerializeToElement("query"),
                    ["max_results"] = JsonSerializer.SerializeToElement(25)
                }
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.McpToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpToolInput);

            result.Should().NotBeNull();
            result!.ExtensionData.Should().NotBeNull();
            result.ExtensionData.Should().HaveCount(3);
            result.ExtensionData!["server"].GetString().Should().Be("test-server");
            result.ExtensionData["action"].GetString().Should().Be("query");
            result.ExtensionData["max_results"].GetInt32().Should().Be(25);
        }

        #endregion

    }

}
