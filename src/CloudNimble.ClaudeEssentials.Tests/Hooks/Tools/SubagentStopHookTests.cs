using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="SubagentStopHookInput"/> and <see cref="SubagentStopHookOutput"/>
    /// deserialization and serialization.
    /// </summary>
    [TestClass]
    public class SubagentStopHookTests
    {

        #region Input Deserialization Tests

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

        #region Output Serialization Tests

        [TestMethod]
        public void SerializeSubagentStopOutput_WithBlockDecision_ShouldSerializeCorrectly()
        {
            var output = new SubagentStopHookOutput
            {
                Continue = true,
                Decision = HookDecision.Block,
                Reason = "Subagent needs more time to complete analysis"
            };

            var json = ClaudeHooksSerializer.SerializeSubagentStopOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("decision").GetString().Should().Be("block");
            doc.RootElement.GetProperty("reason").GetString().Should().Contain("more time");
        }

        [TestMethod]
        public void SerializeSubagentStopOutput_WithAllowDecision_ShouldSerializeCorrectly()
        {
            var output = new SubagentStopHookOutput
            {
                Continue = true,
                Decision = HookDecision.Allow
            };

            var json = ClaudeHooksSerializer.SerializeSubagentStopOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("decision").GetString().Should().Be("allow");
        }

        [TestMethod]
        public void SerializeSubagentStopOutput_WithStopReason_ShouldSerializeCorrectly()
        {
            var output = new SubagentStopHookOutput
            {
                Continue = false,
                StopReason = "Policy violation detected"
            };

            var json = ClaudeHooksSerializer.SerializeSubagentStopOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeFalse();
            doc.RootElement.GetProperty("stopReason").GetString().Should().Be("Policy violation detected");
        }

        #endregion

    }

}
