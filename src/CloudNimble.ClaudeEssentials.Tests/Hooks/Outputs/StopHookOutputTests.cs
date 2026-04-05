using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="StopHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class StopHookOutputTests
    {

        #region Serialization Tests

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
            doc.RootElement.GetProperty("decision").GetString().Should().Be("block");
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
