using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ReadMcpResourceToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class ReadMcpResourceToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void ReadMcpResourceToolInput_Deserialize_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "server": "docs-server",
                    "uri": "file:///D:/Projects/TestProject/README.md"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourceToolInput);

            result.Should().NotBeNull();
            result!.Server.Should().Be("docs-server");
            result.Uri.Should().Be("file:///D:/Projects/TestProject/README.md");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ReadMcpResourceToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new ReadMcpResourceToolInput
            {
                Server = "config-server",
                Uri = "resource://settings/app-config"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ReadMcpResourceToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourceToolInput);

            result.Should().NotBeNull();
            result!.Server.Should().Be(input.Server);
            result.Uri.Should().Be(input.Uri);
        }

        #endregion

    }

}
