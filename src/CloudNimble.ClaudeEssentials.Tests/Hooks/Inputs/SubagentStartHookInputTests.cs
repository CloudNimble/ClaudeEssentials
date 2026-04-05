using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="SubagentStartHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class SubagentStartHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeSubagentStartInput_WithValidJson_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-sa-01",
                    "agent_id": "agent-abc-123",
                    "agent_type": "Bash"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeSubagentStartInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-sa-01");
            result.AgentId.Should().Be("agent-abc-123");
            result.AgentType.Should().Be("Bash");
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeSubagentStartInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSubagentStartInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSubagentStartInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeSubagentStartInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.AgentId.Should().BeEmpty();
            result.AgentType.Should().BeEmpty();
        }

        #endregion

    }

}
