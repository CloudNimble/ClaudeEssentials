using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="PostCompactHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class PostCompactHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializePostCompactOutput_WithContinue_ShouldSerializeCorrectly()
        {
            var output = new PostCompactHookOutput { Continue = true };

            var json = ClaudeHooksSerializer.SerializePostCompactOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
        }

        #endregion

    }

}
