using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ListMcpResourcesToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class ListMcpResourcesToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void ListMcpResourcesToolInput_DeserializeWithServer_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "server": "my-mcp-server"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesToolInput);

            result.Should().NotBeNull();
            result!.Server.Should().Be("my-mcp-server");
        }

        [TestMethod]
        public void ListMcpResourcesToolInput_DeserializeWithoutServer_ShouldDeserializeCorrectly()
        {
            var json = """
                {}
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesToolInput);

            result.Should().NotBeNull();
            result!.Server.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ListMcpResourcesToolInput_RoundTrip_WithServer_ShouldMaintainData()
        {
            var input = new ListMcpResourcesToolInput
            {
                Server = "docs-server"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ListMcpResourcesToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesToolInput);

            result.Should().NotBeNull();
            result!.Server.Should().Be(input.Server);
        }

        [TestMethod]
        public void ListMcpResourcesToolInput_RoundTrip_WithoutServer_ShouldOmitNullFields()
        {
            var input = new ListMcpResourcesToolInput();

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ListMcpResourcesToolInput);

            json.Should().NotContain("server");
        }

        #endregion

    }

}
