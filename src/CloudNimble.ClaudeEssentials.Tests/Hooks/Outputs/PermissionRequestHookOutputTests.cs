using System.Collections.Generic;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for <see cref="PermissionRequestHookOutput{TToolInput}"/>,
    /// <see cref="PermissionRequestSpecificOutput{TToolInput}"/>,
    /// and <see cref="PermissionRequestDecision{TToolInput}"/> serialization and round-trips.
    /// </summary>
    [TestClass]
    public class PermissionRequestHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializePermissionRequestOutput_AllowDecision_ShouldSerializeCorrectly()
        {
            var output = new PermissionRequestHookOutput<object>
            {
                Continue = true,
                HookSpecificOutput = new PermissionRequestSpecificOutput<object>
                {
                    Decision = new PermissionRequestDecision<object>
                    {
                        Behavior = PermissionRequestBehavior.Allow
                    }
                }
            };

            var json = ClaudeHooksSerializer.SerializePermissionRequestOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("decision")
                .GetProperty("behavior").GetString().Should().Be("allow");
        }

        [TestMethod]
        public void SerializePermissionRequestOutput_DenyWithMessage_ShouldSerializeCorrectly()
        {
            var output = new PermissionRequestHookOutput<object>
            {
                Continue = true,
                HookSpecificOutput = new PermissionRequestSpecificOutput<object>
                {
                    Decision = new PermissionRequestDecision<object>
                    {
                        Behavior = PermissionRequestBehavior.Deny,
                        Message = "Dangerous command blocked by policy",
                        Interrupt = true
                    }
                }
            };

            var json = ClaudeHooksSerializer.SerializePermissionRequestOutput(output);
            var doc = JsonDocument.Parse(json);

            var decision = doc.RootElement.GetProperty("hookSpecificOutput").GetProperty("decision");
            decision.GetProperty("behavior").GetString().Should().Be("deny");
            decision.GetProperty("message").GetString().Should().Be("Dangerous command blocked by policy");
            decision.GetProperty("interrupt").GetBoolean().Should().BeTrue();
        }

        [TestMethod]
        public void SerializePermissionRequestOutput_AllowWithUpdatedInput_ShouldSerializeCorrectly()
        {
            var output = new PermissionRequestHookOutput<object>
            {
                Continue = true,
                HookSpecificOutput = new PermissionRequestSpecificOutput<object>
                {
                    Decision = new PermissionRequestDecision<object>
                    {
                        Behavior = PermissionRequestBehavior.Allow,
                        UpdatedInput = new Dictionary<string, string> { ["command"] = "echo safe" }
                    }
                }
            };

            var json = ClaudeHooksSerializer.SerializePermissionRequestOutput(output);
            var doc = JsonDocument.Parse(json);

            var decision = doc.RootElement.GetProperty("hookSpecificOutput").GetProperty("decision");
            decision.GetProperty("updatedInput").GetProperty("command").GetString().Should().Be("echo safe");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void PermissionRequestDecision_RoundTrip_ShouldMaintainData()
        {
            var decision = new PermissionRequestDecision<object>
            {
                Behavior = PermissionRequestBehavior.Deny,
                Message = "Blocked",
                Interrupt = true
            };

            var json = JsonSerializer.Serialize(decision, ClaudeHooksJsonContext.Default.PermissionRequestDecisionObject);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PermissionRequestDecisionObject);

            result.Should().NotBeNull();
            result!.Behavior.Should().Be(PermissionRequestBehavior.Deny);
            result.Message.Should().Be("Blocked");
            result.Interrupt.Should().BeTrue();
        }

        [TestMethod]
        public void PermissionRequestSpecificOutput_HookEventName_ShouldBePermissionRequest()
        {
            var specific = new PermissionRequestSpecificOutput<object>();

            specific.HookEventName.Should().Be(HookEventName.PermissionRequest);
        }

        #endregion

    }

}
