using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="WorktreeRemoveHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class WorktreeRemoveHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeWorktreeRemoveOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new WorktreeRemoveHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeWorktreeRemoveOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
