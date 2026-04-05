using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="WorktreeRemoveHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class WorktreeRemoveHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeWorktreeRemoveInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-wr-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeWorktreeRemoveInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-wr-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeWorktreeRemoveInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeWorktreeRemoveInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeWorktreeRemoveInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeWorktreeRemoveInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
