using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="ElicitationHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class ElicitationHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeElicitationInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-el-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeElicitationInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-el-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeElicitationInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeElicitationInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeElicitationInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeElicitationInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
