using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="FileChangedHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class FileChangedHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeFileChangedInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-fc-01",
                    "cwd": "/home/user/project"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeFileChangedInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-fc-01");
            result.CurrentWorkingDirectory.Should().Be("/home/user/project");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeFileChangedInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeFileChangedInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeFileChangedInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeFileChangedInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
