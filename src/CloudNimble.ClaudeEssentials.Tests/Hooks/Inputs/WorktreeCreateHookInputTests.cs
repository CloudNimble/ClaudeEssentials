using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="WorktreeCreateHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class WorktreeCreateHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeWorktreeCreateInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-wc-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeWorktreeCreateInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-wc-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeWorktreeCreateInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeWorktreeCreateInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeWorktreeCreateInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeWorktreeCreateInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
