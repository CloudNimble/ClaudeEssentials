using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="TeammateIdleHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class TeammateIdleHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeTeammateIdleInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-ti-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeTeammateIdleInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-ti-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeTeammateIdleInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeTeammateIdleInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeTeammateIdleInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeTeammateIdleInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
