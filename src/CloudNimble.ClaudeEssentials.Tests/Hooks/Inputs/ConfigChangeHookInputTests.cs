using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="ConfigChangeHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class ConfigChangeHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeConfigChangeInput_WithUserSettings_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-cc-01",
                    "source": "user_settings"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeConfigChangeInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-cc-01");
            result.Source.Should().Be(ConfigSource.UserSettings);
        }

        [TestMethod]
        public void DeserializeConfigChangeInput_WithPolicySettings_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "source": "policy_settings"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeConfigChangeInput(json);

            result.Should().NotBeNull();
            result!.Source.Should().Be(ConfigSource.PolicySettings);
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeConfigChangeInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeConfigChangeInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeConfigChangeInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeConfigChangeInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
