using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="ElicitationResultHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class ElicitationResultHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeElicitationResultInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-er-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeElicitationResultInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-er-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeElicitationResultInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeElicitationResultInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeElicitationResultInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeElicitationResultInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
