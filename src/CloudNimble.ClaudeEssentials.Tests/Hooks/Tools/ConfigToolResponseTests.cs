using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ConfigToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class ConfigToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeConfig_GetOperation_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "Config",
                    "tool_input": {
                        "setting": "theme"
                    },
                    "tool_response": {
                        "success": true,
                        "operation": "get",
                        "setting": "theme",
                        "value": "dark"
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Success.Should().BeTrue();
            result.ToolResponse.Operation.Should().Be(ConfigOperation.Get);
            result.ToolResponse.Setting.Should().Be("theme");
            result.ToolResponse.Value.Should().NotBeNull();
            result.ToolResponse.Value!.Value.GetString().Should().Be("dark");
            result.ToolResponse.PreviousValue.Should().BeNull();
            result.ToolResponse.NewValue.Should().BeNull();
            result.ToolResponse.Error.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeConfig_SetOperation_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Config",
                    "tool_input": {
                        "setting": "theme",
                        "value": "light"
                    },
                    "tool_response": {
                        "success": true,
                        "operation": "set",
                        "setting": "theme",
                        "previousValue": "dark",
                        "newValue": "light"
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Success.Should().BeTrue();
            result.ToolResponse.Operation.Should().Be(ConfigOperation.Set);
            result.ToolResponse.Setting.Should().Be("theme");
            result.ToolResponse.PreviousValue.Should().NotBeNull();
            result.ToolResponse.PreviousValue!.Value.GetString().Should().Be("dark");
            result.ToolResponse.NewValue.Should().NotBeNull();
            result.ToolResponse.NewValue!.Value.GetString().Should().Be("light");
            result.ToolResponse.Value.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeConfig_WithError_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Config",
                    "tool_input": {
                        "setting": "nonexistent_setting"
                    },
                    "tool_response": {
                        "success": false,
                        "error": "Unknown setting: nonexistent_setting"
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Success.Should().BeFalse();
            result.ToolResponse.Error.Should().Be("Unknown setting: nonexistent_setting");
            result.ToolResponse.Operation.Should().BeNull();
            result.ToolResponse.Setting.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ConfigToolResponse_RoundTrip_GetOperation_ShouldMaintainData()
        {
            var response = new ConfigToolResponse
            {
                Success = true,
                Operation = ConfigOperation.Get,
                Setting = "theme",
                Value = JsonSerializer.SerializeToElement("dark")
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ConfigToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolResponse);

            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Operation.Should().Be(ConfigOperation.Get);
            result.Setting.Should().Be("theme");
            result.Value.Should().NotBeNull();
            result.Value!.Value.GetString().Should().Be("dark");
            result.PreviousValue.Should().BeNull();
            result.NewValue.Should().BeNull();
        }

        [TestMethod]
        public void ConfigToolResponse_RoundTrip_SetOperation_ShouldMaintainData()
        {
            var response = new ConfigToolResponse
            {
                Success = true,
                Operation = ConfigOperation.Set,
                Setting = "maxTokens",
                PreviousValue = JsonSerializer.SerializeToElement(1000),
                NewValue = JsonSerializer.SerializeToElement(2000)
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.ConfigToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigToolResponse);

            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Operation.Should().Be(ConfigOperation.Set);
            result.Setting.Should().Be("maxTokens");
            result.PreviousValue!.Value.GetInt32().Should().Be(1000);
            result.NewValue!.Value.GetInt32().Should().Be(2000);
        }

        #endregion

    }

}
