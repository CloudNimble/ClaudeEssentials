using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="PreCompactHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class PreCompactHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializePreCompactInput_WithManualTrigger_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "trigger": "manual",
                    "custom_instructions": "Keep function signatures"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreCompactInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Trigger.Should().Be(CompactTrigger.Manual);
            result.CustomInstructions.Should().Be("Keep function signatures");
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithAutoTrigger_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "trigger": "auto"
                }
                """;

            // Act
            var result = ClaudeHooksSerializer.DeserializePreCompactInput(json);

            // Assert
            result.Should().NotBeNull();
            result!.Trigger.Should().Be(CompactTrigger.Auto);
            result.CustomInstructions.Should().BeNull();
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializePreCompactInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePreCompactInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithNullLiteral_ShouldReturnNull()
        {
            var result = ClaudeHooksSerializer.DeserializePreCompactInput("null");
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializePreCompactInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
            result.CustomInstructions.Should().BeNull();
        }

        [TestMethod]
        public void DeserializePreCompactInput_WithNumberLiteral_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializePreCompactInput("99");
            action.Should().Throw<JsonException>();
        }

        #endregion

    }

}
