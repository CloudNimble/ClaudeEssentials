using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Enums;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests
{

    /// <summary>
    /// Tests for serializing hook output payloads to JSON.
    /// </summary>
    [TestClass]
    public class HookOutputSerializationTests
    {

        #region PreToolUseHookOutput Tests

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
                .GetProperty("permissionDecision").GetString().Should().Be("Allow");
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
                .GetProperty("permissionDecision").GetString().Should().Be("Deny");
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

        #region PostToolUseHookOutput Tests

        [TestMethod]
        public void SerializePostToolUseOutput_WithBlockDecision_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new PostToolUseHookOutput
            {
                Decision = HookDecision.Block,
                Reason = "Tool output indicates error"
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePostToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("decision").GetString().Should().Be("Block");
            doc.RootElement.GetProperty("reason").GetString().Should().Be("Tool output indicates error");
        }

        [TestMethod]
        public void SerializePostToolUseOutput_WithAdditionalContext_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new PostToolUseHookOutput
            {
                HookSpecificOutput = new PostToolUseSpecificOutput
                {
                    AdditionalContext = "File was created successfully"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePostToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString().Should().Be("File was created successfully");
        }

        #endregion

        #region StopHookOutput Tests

        [TestMethod]
        public void SerializeStopOutput_WithBlockDecision_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new StopHookOutput
            {
                Decision = HookDecision.Block,
                Reason = "Tests must pass before stopping"
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeStopOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("decision").GetString().Should().Be("Block");
            doc.RootElement.GetProperty("reason").GetString().Should().Be("Tests must pass before stopping");
        }

        [TestMethod]
        public void SerializeStopOutput_WithContinueOnly_ShouldSerializeMinimally()
        {
            // Arrange
            var output = new StopHookOutput { Continue = true };

            // Act
            var json = ClaudeHooksSerializer.SerializeStopOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

        #region UserPromptSubmitHookOutput Tests

        [TestMethod]
        public void SerializeUserPromptSubmitOutput_WithBlockDecision_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new UserPromptSubmitHookOutput
            {
                Decision = HookDecision.Block,
                Reason = "Prompt contains restricted keywords"
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeUserPromptSubmitOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("decision").GetString().Should().Be("Block");
            doc.RootElement.GetProperty("reason").GetString().Should().Be("Prompt contains restricted keywords");
        }

        [TestMethod]
        public void SerializeUserPromptSubmitOutput_WithAdditionalContext_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new UserPromptSubmitHookOutput
            {
                HookSpecificOutput = new UserPromptSubmitSpecificOutput
                {
                    AdditionalContext = "User is working on authentication module"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeUserPromptSubmitOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString()
                .Should().Be("User is working on authentication module");
        }

        #endregion

        #region SessionStartHookOutput Tests

        [TestMethod]
        public void SerializeSessionStartOutput_WithContext_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new SessionStartHookOutput
            {
                HookSpecificOutput = new SessionStartSpecificOutput
                {
                    AdditionalContext = "Project uses .NET 10"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeSessionStartOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString().Should().Be("Project uses .NET 10");
        }

        #endregion

        #region Null Value Handling Tests

        [TestMethod]
        public void SerializeOutput_WithNullOptionalFields_ShouldOmitNullValues()
        {
            // Arrange
            var output = new PostToolUseHookOutput
            {
                Continue = true,
                Decision = null,
                Reason = null
            };

            // Act
            var json = ClaudeHooksSerializer.SerializePostToolUseOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.TryGetProperty("decision", out _).Should().BeFalse();
            doc.RootElement.TryGetProperty("reason", out _).Should().BeFalse();
        }

        #endregion

        #region SuppressOutput Tests

        [TestMethod]
        public void SerializeOutput_WithSuppressOutputFalse_ShouldOmitProperty()
        {
            // Arrange
            var output = new StopHookOutput
            {
                Continue = true,
                SuppressOutput = false
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeStopOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.TryGetProperty("suppressOutput", out _).Should().BeFalse();
        }

        [TestMethod]
        public void SerializeOutput_WithSuppressOutputTrue_ShouldIncludeProperty()
        {
            // Arrange
            var output = new StopHookOutput
            {
                Continue = true,
                SuppressOutput = true
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeStopOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("suppressOutput").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
