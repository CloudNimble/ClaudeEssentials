using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="ConfigChangeHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class ConfigChangeHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeConfigChangeOutput_WithBlockDecision_ShouldSerializeCorrectly()
        {
            var output = new ConfigChangeHookOutput
            {
                Continue = true,
                Decision = HookDecision.Block,
                Reason = "Configuration changes are locked during deployment"
            };

            var json = ClaudeHooksSerializer.SerializeConfigChangeOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("decision").GetString().Should().Be("block");
            doc.RootElement.GetProperty("reason").GetString()
                .Should().Be("Configuration changes are locked during deployment");
        }

        [TestMethod]
        public void SerializeConfigChangeOutput_WithNullDecision_ShouldOmitDecision()
        {
            var output = new ConfigChangeHookOutput
            {
                Continue = true,
                Decision = null,
                Reason = null
            };

            var json = ClaudeHooksSerializer.SerializeConfigChangeOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.TryGetProperty("decision", out _).Should().BeFalse();
            doc.RootElement.TryGetProperty("reason", out _).Should().BeFalse();
        }

        #endregion

    }

}
