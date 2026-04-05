using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="WebFetchToolInput"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class WebFetchToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeWebFetchToolInput_WithUrlAndPrompt_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://docs.microsoft.com/dotnet/csharp/whats-new",
                        "prompt": "List the new features added in C# 12"
                    },
                    "tool_use_id": "toolu_01TestWebFetch001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("WebFetch");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Url.Should().Be("https://docs.microsoft.com/dotnet/csharp/whats-new");
            result.ToolInput.Prompt.Should().Contain("C# 12");
        }

        [TestMethod]
        public void DeserializeWebFetchToolInput_MinimalPayload_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://example.com/api/docs",
                        "prompt": "Extract the authentication requirements"
                    },
                    "tool_use_id": "toolu_01TestWebFetchMinimal001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPreToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Url.Should().Be("https://example.com/api/docs");
            result.ToolInput.Prompt.Should().Contain("authentication");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void WebFetchToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new WebFetchToolInput
            {
                Url = "https://learn.microsoft.com/aspnet/core/fundamentals",
                Prompt = "Summarize the middleware pipeline documentation"
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.WebFetchToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchToolInput);

            result.Should().NotBeNull();
            result!.Url.Should().Be(input.Url);
            result.Prompt.Should().Be(input.Prompt);
        }

        #endregion

    }

}
