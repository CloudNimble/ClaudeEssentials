using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="PostToolUseHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class PostToolUseHookOutputTests
    {

        #region Serialization Tests

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
            doc.RootElement.GetProperty("decision").GetString().Should().Be("block");
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

    }

}
