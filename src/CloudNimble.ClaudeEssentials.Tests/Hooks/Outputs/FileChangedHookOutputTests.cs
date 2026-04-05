using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="FileChangedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class FileChangedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeFileChangedOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new FileChangedHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeFileChangedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
