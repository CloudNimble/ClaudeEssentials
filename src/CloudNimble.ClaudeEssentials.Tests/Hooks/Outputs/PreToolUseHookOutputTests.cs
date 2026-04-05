using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="PreToolUseHookOutput{TToolInput}"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class PreToolUseHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializePreToolUseOutput_WithAllowDecision_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new PreToolUseHookOutput<object>
            {
                Continue = true,
                HookSpecificOutput = new PreToolUseSpecificOutput<object>
                {
                    PermissionDecision = PermissionDecision.Allow,
                    PermissionDecisionReason = "Auto-approved"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecision").GetString().Should().Be("allow");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecisionReason").GetString().Should().Be("Auto-approved");
        }

        [TestMethod]
        public void SerializePreToolUseOutput_WithDenyDecision_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new PreToolUseHookOutput<object>
            {
                Continue = true,
                HookSpecificOutput = new PreToolUseSpecificOutput<object>
                {
                    PermissionDecision = PermissionDecision.Deny,
                    PermissionDecisionReason = "Operation not permitted"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("permissionDecision").GetString().Should().Be("deny");
        }

        [TestMethod]
        public void SerializePreToolUseOutput_WithContinueFalse_ShouldIncludeStopReason()
        {
            // Arrange
            var output = new PreToolUseHookOutput<object>
            {
                Continue = false,
                StopReason = "Hook blocked execution"
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePreToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeFalse();
            doc.RootElement.GetProperty("stopReason").GetString().Should().Be("Hook blocked execution");
        }

        #endregion

    }

}
