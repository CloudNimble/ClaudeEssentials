using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="SubagentStopHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class SubagentStopHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeSubagentStopInput_WithStopHookActive_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "hook_event_name": "SubagentStop",
                    "stop_hook_active": true
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput(json);

            result.Should().NotBeNull();
            result!.HookEventName.Should().Be(HookEventName.SubagentStop);
            result.StopHookActive.Should().BeTrue();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithStopHookInactive_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "11111111-2222-3333-4444-555555555555",
                    "cwd": "D:\\Projects\\TestProject",
                    "hook_event_name": "SubagentStop",
                    "stop_hook_active": false
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput(json);

            result.Should().NotBeNull();
            result!.StopHookActive.Should().BeFalse();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeSubagentStopInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeSubagentStopInput("{{invalid}}");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeSubagentStopInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeSubagentStopInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.StopHookActive.Should().BeFalse();
        }

        #endregion

    }

}
