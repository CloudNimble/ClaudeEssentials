using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="CwdChangedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class CwdChangedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeCwdChangedInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-cwd-01",
                    "cwd": "/home/user/new-project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeCwdChangedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-cwd-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/new-project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeCwdChangedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeCwdChangedInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeCwdChangedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeCwdChangedInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
