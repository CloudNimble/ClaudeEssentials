using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PostCompactHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PostCompactHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePostCompactInput_WithManualTrigger_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-pc-01",
                    "trigger": "manual"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePostCompactInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-pc-01");
            result.Trigger.Should().Be(CompactTrigger.Manual);
        }

        [TestMethod]
        public void DeserializePostCompactInput_WithAutoTrigger_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "trigger": "auto"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializePostCompactInput(json);

            result.Should().NotBeNull();
            result!.Trigger.Should().Be(CompactTrigger.Auto);
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePostCompactInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePostCompactInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePostCompactInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePostCompactInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
