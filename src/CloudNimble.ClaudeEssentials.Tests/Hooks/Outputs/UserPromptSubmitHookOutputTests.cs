using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="UserPromptSubmitHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class UserPromptSubmitHookOutputTests
    {

        #region Serialization Tests

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
            doc.RootElement.GetProperty("decision").GetString().Should().Be("block");
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

    }

}
