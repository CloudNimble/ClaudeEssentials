using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ExitWorktreeToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class ExitWorktreeToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void ExitWorktreeToolInput_DeserializeKeepAction_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "action": "keep"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Action.Should().Be(WorktreeAction.Keep);
            result.DiscardChanges.Should().BeNull();
        }

        [TestMethod]
        public void ExitWorktreeToolInput_DeserializeRemoveWithDiscard_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "action": "remove",
                    "discard_changes": true
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Action.Should().Be(WorktreeAction.Remove);
            result.DiscardChanges.Should().BeTrue();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ExitWorktreeToolInput_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var input = new ExitWorktreeToolInput
            {
                Action = WorktreeAction.Remove,
                DiscardChanges = true
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ExitWorktreeToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreeToolInput);

            result.Should().NotBeNull();
            result!.Action.Should().Be(input.Action);
            result.DiscardChanges.Should().BeTrue();
        }

        [TestMethod]
        public void ExitWorktreeToolInput_RoundTrip_WithoutDiscardChanges_ShouldOmitNullFields()
        {
            var input = new ExitWorktreeToolInput
            {
                Action = WorktreeAction.Keep
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ExitWorktreeToolInput);

            json.Should().NotContain("discard_changes");
        }

        #endregion

    }

}
