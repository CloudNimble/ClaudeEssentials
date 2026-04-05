using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="CwdChangedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class CwdChangedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeCwdChangedOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new CwdChangedHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializeCwdChangedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
