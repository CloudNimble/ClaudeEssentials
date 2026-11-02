using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="EnterWorktreeToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class EnterWorktreeToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void EnterWorktreeToolInput_DeserializeWithName_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "name": "feature/add-auth"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Name.Should().Be("feature/add-auth");
        }

        [TestMethod]
        public void EnterWorktreeToolInput_DeserializeWithoutName_ShouldDeserializeCorrectly()
        {
            var json = """
                {}
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Name.Should().BeNull();
            result.Path.Should().BeNull();
        }

        [TestMethod]
        public void EnterWorktreeToolInput_DeserializeWithPath_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "path": "D:\\Projects\\TestProject-worktree-1"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Path.Should().Be("D:\\Projects\\TestProject-worktree-1");
            result.Name.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void EnterWorktreeToolInput_RoundTrip_WithName_ShouldMaintainData()
        {
            var input = new EnterWorktreeToolInput
            {
                Name = "bugfix/fix-login"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Name.Should().Be(input.Name);
        }

        [TestMethod]
        public void EnterWorktreeToolInput_RoundTrip_WithoutName_ShouldOmitNullFields()
        {
            var input = new EnterWorktreeToolInput();

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            json.Should().NotContain("name");
            json.Should().NotContain("path");
        }

        [TestMethod]
        public void EnterWorktreeToolInput_RoundTrip_WithPath_ShouldMaintainData()
        {
            var input = new EnterWorktreeToolInput
            {
                Path = "D:\\Projects\\existing-worktree"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreeToolInput);

            json.Should().NotContain("name");
            result.Should().NotBeNull();
            result!.Path.Should().Be(input.Path);
            result.Name.Should().BeNull();
        }

        #endregion

    }

}
