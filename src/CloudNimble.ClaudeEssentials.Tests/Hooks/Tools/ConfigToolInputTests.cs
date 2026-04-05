using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ConfigToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class ConfigToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void ConfigToolInput_DeserializeGetSetting_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "setting": "theme"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolInput);

            result.Should().NotBeNull();
            result!.Setting.Should().Be("theme");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void ConfigToolInput_DeserializeSetSetting_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "setting": "permissions.defaultMode",
                    "value": "plan"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolInput);

            result.Should().NotBeNull();
            result!.Setting.Should().Be("permissions.defaultMode");
            result.Value.Should().NotBeNull();
            result.Value!.Value.GetString().Should().Be("plan");
        }

        [TestMethod]
        public void ConfigToolInput_DeserializeSetBooleanValue_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "setting": "telemetry.enabled",
                    "value": false
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolInput);

            result.Should().NotBeNull();
            result!.Setting.Should().Be("telemetry.enabled");
            result.Value.Should().NotBeNull();
            result.Value!.Value.GetBoolean().Should().BeFalse();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ConfigToolInput_RoundTrip_WithValue_ShouldMaintainData()
        {
            var input = new ConfigToolInput
            {
                Setting = "model",
                Value = JsonSerializer.SerializeToElement("opus")
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ConfigToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolInput);

            result.Should().NotBeNull();
            result!.Setting.Should().Be(input.Setting);
            result.Value.Should().NotBeNull();
            result.Value!.Value.GetString().Should().Be("opus");
        }

        [TestMethod]
        public void ConfigToolInput_RoundTrip_WithoutValue_ShouldOmitNullFields()
        {
            var input = new ConfigToolInput
            {
                Setting = "theme"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ConfigToolInput);

            json.Should().NotContain("value");
        }

        #endregion

    }

}
