using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="InstructionsLoadedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class InstructionsLoadedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeInstructionsLoadedOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new InstructionsLoadedHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeInstructionsLoadedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
