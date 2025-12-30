using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for WebSearch tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class WebSearchPayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeWebSearchPreToolUse_WithQuery_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-websearch-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-websearch-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "NuGet package best practices"
                    },
                    "tool_use_id": "toolu_01TestWebSearch001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("WebSearch");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Query.Should().Be("NuGet package best practices");
        }

        [TestMethod]
        public void DeserializeWebSearchPreToolUse_WithAllowedDomains_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "C# async await patterns",
                        "allowed_domains": ["docs.microsoft.com", "learn.microsoft.com"]
                    },
                    "tool_use_id": "toolu_01TestWebSearchAllowed001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.AllowedDomains.Should().NotBeNull();
            result.ToolInput.AllowedDomains.Should().HaveCount(2);
            result.ToolInput.AllowedDomains.Should().Contain("docs.microsoft.com");
        }

        [TestMethod]
        public void DeserializeWebSearchPreToolUse_WithBlockedDomains_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "programming tutorials",
                        "blocked_domains": ["spam-site.com", "low-quality.com"]
                    },
                    "tool_use_id": "toolu_01TestWebSearchBlocked001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.BlockedDomains.Should().NotBeNull();
            result.ToolInput.BlockedDomains.Should().HaveCount(2);
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeWebSearchPostToolUse_WithResults_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code with sanitized content
            var json = """
                {
                    "session_id": "test-session-websearch-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "NuGet package development"
                    },
                    "tool_response": {
                        "query": "NuGet package development",
                        "results": [
                            {
                                "tool_use_id": "srvtoolu_01TestResult001",
                                "content": [
                                    {
                                        "title": "Creating NuGet packages - Microsoft Learn",
                                        "url": "https://learn.microsoft.com/nuget/create-packages"
                                    },
                                    {
                                        "title": "NuGet Package Best Practices",
                                        "url": "https://docs.microsoft.com/nuget/best-practices"
                                    }
                                ]
                            }
                        ],
                        "durationSeconds": 2.5
                    },
                    "tool_use_id": "toolu_01TestWebSearchPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("WebSearch");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Query.Should().Be("NuGet package development");
            result.ToolResponse.Results.Should().NotBeNull();
            result.ToolResponse.Results.Should().HaveCount(1);
            result.ToolResponse.DurationSeconds.Should().BeApproximately(2.5, 0.01);
        }

        [TestMethod]
        public void DeserializeWebSearchPostToolUse_ResultContent_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "dotnet cli commands"
                    },
                    "tool_response": {
                        "query": "dotnet cli commands",
                        "results": [
                            {
                                "tool_use_id": "srvtoolu_01TestResultContent001",
                                "content": [
                                    {
                                        "title": "dotnet command reference",
                                        "url": "https://docs.microsoft.com/dotnet/cli"
                                    },
                                    {
                                        "title": "dotnet build command",
                                        "url": "https://docs.microsoft.com/dotnet/cli/build"
                                    },
                                    {
                                        "title": "dotnet run command",
                                        "url": "https://docs.microsoft.com/dotnet/cli/run"
                                    }
                                ]
                            }
                        ],
                        "durationSeconds": 1.8
                    },
                    "tool_use_id": "toolu_01TestWebSearchContent001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();

            var firstResultContainer = result.ToolResponse!.Results.First();
            firstResultContainer.ToolUseId.Should().Be("srvtoolu_01TestResultContent001");
            firstResultContainer.Content.Should().HaveCount(3);
            firstResultContainer.Content[0].Title.Should().Be("dotnet command reference");
            firstResultContainer.Content[0].Url.Should().Be("https://docs.microsoft.com/dotnet/cli");
        }

        [TestMethod]
        public void DeserializeWebSearchPostToolUse_WithMultipleResults_ShouldDeserializeCorrectly()
        {
            // Arrange - WebSearch with multiple result containers
            var json = """
                {
                    "tool_name": "WebSearch",
                    "tool_input": {
                        "query": "test query"
                    },
                    "tool_response": {
                        "query": "test query",
                        "results": [
                            {
                                "tool_use_id": "srvtoolu_01TestSummary001",
                                "content": [
                                    {
                                        "title": "Test Result 1",
                                        "url": "https://example.com/test1"
                                    }
                                ]
                            },
                            {
                                "tool_use_id": "srvtoolu_01TestSummary002",
                                "content": [
                                    {
                                        "title": "Test Result 2",
                                        "url": "https://example.com/test2"
                                    }
                                ]
                            }
                        ],
                        "durationSeconds": 3.2
                    },
                    "tool_use_id": "toolu_01TestWebSearchSummary001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Results.Should().HaveCount(2);
            result.ToolResponse.DurationSeconds.Should().BeApproximately(3.2, 0.01);
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void WebSearchToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new WebSearchToolInput
            {
                Query = "C# best practices 2024",
                AllowedDomains = ["microsoft.com", "stackoverflow.com"],
                BlockedDomains = ["spam.com"]
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.WebSearchToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.Query.Should().Be(input.Query);
            result.AllowedDomains.Should().BeEquivalentTo(input.AllowedDomains);
            result.BlockedDomains.Should().BeEquivalentTo(input.BlockedDomains);
        }

        [TestMethod]
        public void WebSearchToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new WebSearchToolResponse
            {
                Query = "test query",
                Results =
                [
                    new WebSearchResultContainer
                    {
                        ToolUseId = "srv_001",
                        Content =
                        [
                            new WebSearchResultItem { Title = "Result 1", Url = "https://example.com/1" },
                            new WebSearchResultItem { Title = "Result 2", Url = "https://example.com/2" }
                        ]
                    }
                ],
                DurationSeconds = 1.5
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.WebSearchToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.Query.Should().Be(response.Query);
            result.Results.Should().HaveCount(1);
            result.Results[0].Content.Should().HaveCount(2);
            result.DurationSeconds.Should().Be(response.DurationSeconds);
        }

        [TestMethod]
        public void WebSearchResultItem_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var item = new WebSearchResultItem
            {
                Title = "Learn C# Programming",
                Url = "https://docs.microsoft.com/csharp"
            };

            // Act
            var json = JsonSerializer.Serialize(item, ClaudeHooksJsonContext.Default.WebSearchResultItem);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchResultItem);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be(item.Title);
            result.Url.Should().Be(item.Url);
        }

        #endregion

    }

}
