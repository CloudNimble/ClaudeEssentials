using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="WebFetchToolResponse"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class WebFetchToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeWebFetchToolResponse_WithoutResponseMetadata_ShouldUseDefaults()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://docs.microsoft.com/dotnet/whats-new",
                        "prompt": "Summarize the latest features"
                    },
                    "tool_response": {
                        "url": "https://docs.microsoft.com/dotnet/whats-new",
                        "result": "# What's New in .NET\n\n.NET 9 introduces performance improvements...",
                        "durationMs": 2350
                    },
                    "tool_use_id": "toolu_01TestWebFetchPost001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolName.Should().Be("WebFetch");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Url.Should().Contain("dotnet/whats-new");
            result.ToolResponse.Result.Should().Contain(".NET 9");
            result.ToolResponse.DurationMs.Should().Be(2350);
            result.ToolResponse.Bytes.Should().Be(0);
            result.ToolResponse.Code.Should().Be(0);
        }

        [TestMethod]
        public void DeserializeWebFetchToolResponse_NewSchemaFields_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://example.com/api/status",
                        "prompt": "Get the API status"
                    },
                    "tool_response": {
                        "url": "https://example.com/api/status",
                        "bytes": 4096,
                        "code": 200,
                        "codeText": "OK",
                        "result": "The API reports all services are operational.",
                        "durationMs": 1250
                    },
                    "tool_use_id": "toolu_01TestWebFetchNew001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Url.Should().Contain("api/status");
            result.ToolResponse.Bytes.Should().Be(4096);
            result.ToolResponse.Code.Should().Be(200);
            result.ToolResponse.CodeText.Should().Be("OK");
            result.ToolResponse.Result.Should().Contain("operational");
            result.ToolResponse.DurationMs.Should().Be(1250);
        }

        [TestMethod]
        public void DeserializeWebFetchToolResponse_WithRedirect_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://short.link/abc123",
                        "prompt": "Read the page content"
                    },
                    "tool_response": {
                        "url": "https://short.link/abc123",
                        "result": "Redirected to a different host.",
                        "code": 301,
                        "codeText": "Moved Permanently",
                        "durationMs": 500
                    },
                    "tool_use_id": "toolu_01TestWebFetchRedirect001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Code.Should().Be(301);
            result.ToolResponse.CodeText.Should().Be("Moved Permanently");
        }

        [TestMethod]
        public void DeserializeWebFetchToolResponse_LargePage_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "WebFetch",
                    "tool_input": {
                        "url": "https://example.com/large-page",
                        "prompt": "Summarize the content"
                    },
                    "tool_response": {
                        "url": "https://example.com/large-page",
                        "result": "This is a summary of a very large page...",
                        "durationMs": 5800,
                        "bytes": 524288,
                        "code": 200,
                        "codeText": "OK"
                    },
                    "tool_use_id": "toolu_01TestWebFetchTruncated001"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Bytes.Should().Be(524288);
            result.ToolResponse.DurationMs.Should().Be(5800);
            result.ToolResponse.Code.Should().Be(200);
            result.ToolResponse.CodeText.Should().Be("OK");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void WebFetchToolResponse_RoundTrip_WithMinimalFields_ShouldMaintainData()
        {
            var response = new WebFetchToolResponse
            {
                Url = "https://example.com/docs",
                Result = "Documentation content here.",
                DurationMs = 1500
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.WebFetchToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchToolResponse);

            result.Should().NotBeNull();
            result!.Url.Should().Be(response.Url);
            result.Result.Should().Be(response.Result);
            result.DurationMs.Should().Be(response.DurationMs);
            result.Bytes.Should().Be(0);
            result.Code.Should().Be(0);
            result.CodeText.Should().BeEmpty();
        }

        [TestMethod]
        public void WebFetchToolResponse_RoundTrip_WithAllFields_ShouldMaintainData()
        {
            var response = new WebFetchToolResponse
            {
                Url = "https://example.com/full",
                Bytes = 8192,
                Code = 200,
                CodeText = "OK",
                Result = "Processed result text.",
                DurationMs = 3200
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.WebFetchToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchToolResponse);

            result.Should().NotBeNull();
            result!.Url.Should().Be(response.Url);
            result.Bytes.Should().Be(response.Bytes);
            result.Code.Should().Be(response.Code);
            result.CodeText.Should().Be(response.CodeText);
            result.Result.Should().Be(response.Result);
            result.DurationMs.Should().Be(response.DurationMs);
        }

        #endregion

    }

}
