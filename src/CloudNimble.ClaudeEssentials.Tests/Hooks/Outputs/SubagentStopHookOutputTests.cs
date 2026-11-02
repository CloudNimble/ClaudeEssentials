using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="SubagentStopHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class SubagentStopHookOutputTests
    {

        #region Serialization Tests

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
        public void SerializeSubagentStopOutput_WithApproveDecision_ShouldSerializeCorrectly()
        {
            var output = new SubagentStopHookOutput
            {
                Continue = true,
                Decision = HookDecision.Approve
            };

            var json = ClaudeHooksSerializer.SerializeSubagentStopOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("decision").GetString().Should().Be("approve");
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
