using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{
    /// <summary>
    /// Tests for ContentBlock deserialization, especially for MCP tool responses.
    /// </summary>
    [TestClass]
    public class ContentBlockTests
    {
        #region PostToolUse with ContentBlock[] Tests

        [TestMethod]
        public void DeserializeMcpToolResponse_WithContentBlockArray_ShouldDeserializeCorrectly()
        {
            // Arrange - Real MCP firecrawl_map response with ContentBlock wrapper
            var json = """
                {
                    "session_id": "e0f8803b-c95c-45f6-82be-528bc6310390",
                    "transcript_path": "C:\\Users\\User\\.claude\\projects\\test\\session.jsonl",
                    "cwd": "D:\\Work\\test",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "mcp__firecrawl__firecrawl_map",
                    "tool_input": {
                        "url": "https://example.com"
                    },
                    "tool_response": [
                        {
                            "type": "text",
                            "text": "{\n  \"links\": [\n    {\n      \"url\": \"https://example.com/blog\",\n      \"title\": \"Blog\",\n      \"description\": \"Our blog posts\"\n    },\n    {\n      \"url\": \"https://example.com/about\"\n    }\n  ]\n}"
                        }
                    ],
                    "tool_use_id": "toolu_01Mo9JHTA5dC46gdHHDJ2VeM"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ContentBlockTestContext.Default.McpPostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolName.Should().Be("mcp__firecrawl__firecrawl_map");
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse.Should().HaveCount(1);
            result.ToolResponse![0].Type.Should().Be("text");
            result.ToolResponse[0].IsText.Should().BeTrue();
            result.ToolResponse[0].IsImage.Should().BeFalse();
            result.ToolResponse[0].Text.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ContentBlock_GetTextContent_ShouldDeserializeInnerJson()
        {
            // Arrange
            var json = """
                {
                    "session_id": "test-session",
                    "transcript_path": "test.jsonl",
                    "cwd": "D:\\test",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "mcp__firecrawl__firecrawl_map",
                    "tool_input": {},
                    "tool_response": [
                        {
                            "type": "text",
                            "text": "{\"links\":[{\"url\":\"https://example.com/page1\",\"title\":\"Page 1\",\"description\":\"First page\"},{\"url\":\"https://example.com/page2\"}]}"
                        }
                    ],
                    "tool_use_id": "toolu_test"
                }
                """;

            var payload = JsonSerializer.Deserialize(json, ContentBlockTestContext.Default.McpPostToolUsePayload);
            var textBlock = payload!.ToolResponse!.First(b => b.IsText);

            // Act
            var firecrawlResponse = textBlock.GetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse);

            // Assert
            firecrawlResponse.Should().NotBeNull();
            firecrawlResponse!.Links.Should().HaveCount(2);
            firecrawlResponse.Links[0].Url.Should().Be("https://example.com/page1");
            firecrawlResponse.Links[0].Title.Should().Be("Page 1");
            firecrawlResponse.Links[0].Description.Should().Be("First page");
            firecrawlResponse.Links[1].Url.Should().Be("https://example.com/page2");
            firecrawlResponse.Links[1].Title.Should().BeNull();
        }

        [TestMethod]
        public void ContentBlock_TryGetTextContent_WithValidJson_ShouldReturnTrue()
        {
            // Arrange
            var contentBlock = new ContentBlock
            {
                Type = "text",
                Text = "{\"links\":[{\"url\":\"https://example.com\"}]}"
            };

            // Act
            var success = contentBlock.TryGetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse, out var result);

            // Assert
            success.Should().BeTrue();
            result.Should().NotBeNull();
            result!.Links.Should().HaveCount(1);
        }

        [TestMethod]
        public void ContentBlock_TryGetTextContent_WithInvalidJson_ShouldReturnFalse()
        {
            // Arrange
            var contentBlock = new ContentBlock
            {
                Type = "text",
                Text = "not valid json {"
            };

            // Act
            var success = contentBlock.TryGetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse, out var result);

            // Assert
            success.Should().BeFalse();
            result.Should().BeNull();
        }

        [TestMethod]
        public void ContentBlock_TryGetTextContent_WithNullText_ShouldReturnFalse()
        {
            // Arrange
            var contentBlock = new ContentBlock
            {
                Type = "text",
                Text = null
            };

            // Act
            var success = contentBlock.TryGetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse, out var result);

            // Assert
            success.Should().BeFalse();
            result.Should().BeNull();
        }

        [TestMethod]
        public void ContentBlock_GetTextContent_WithEmptyText_ShouldReturnNull()
        {
            // Arrange
            var contentBlock = new ContentBlock
            {
                Type = "text",
                Text = ""
            };

            // Act
            var result = contentBlock.GetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeRealFirecrawlMapPayload_ShouldDeserializeAllLinks()
        {
            // Arrange - Full real-world payload with 54 links (truncated for test)
            var json = """
                {
                    "session_id": "e0f8803b-c95c-45f6-82be-528bc6310390",
                    "transcript_path": "C:\\Users\\User\\.claude\\projects\\D--Work-manufacturers\\e0f8803b-c95c-45f6-82be-528bc6310390.jsonl",
                    "cwd": "D:\\Work\\manufacturers",
                    "permission_mode": "acceptEdits",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "mcp__firecrawl__firecrawl_map",
                    "tool_input": {
                        "url": "https://burnrate.io"
                    },
                    "tool_response": [
                        {
                            "type": "text",
                            "text": "{\n  \"links\": [\n    {\n      \"url\": \"https://burnrate.io/blog\",\n      \"title\": \"The Finance Tool for GTM Leaders | Blog\",\n      \"description\": \"Listen up, GTM leaders: YOU'RE the ones that generate non-dilutive cash flow.\"\n    },\n    {\n      \"url\": \"https://burnrate.io/request-access\"\n    },\n    {\n      \"url\": \"https://burnrate.io/perks\",\n      \"title\": \"Perk Page\",\n      \"description\": \"Chart your course for revenue growth.\"\n    },\n    {\n      \"url\": \"https://burnrate.io/about-us\",\n      \"title\": \"The Finance Tool for GTM Leaders | About Us\",\n      \"description\": \"We're a small team of passionate people.\"\n    },\n    {\n      \"url\": \"https://support.burnrate.io\",\n      \"title\": \"BurnRate: Staff Login\",\n      \"description\": \"Create your free Re:amaze account.\"\n    }\n  ]\n}"
                        }
                    ],
                    "tool_use_id": "toolu_01Mo9JHTA5dC46gdHHDJ2VeM"
                }
                """;

            // Act
            var payload = JsonSerializer.Deserialize(json, ContentBlockTestContext.Default.McpPostToolUsePayload);
            var textBlock = payload!.ToolResponse!.FirstOrDefault(b => b.IsText);
            var firecrawlResponse = textBlock?.GetTextContent(ContentBlockTestContext.Default.FirecrawlMapResponse);

            // Assert
            payload.Should().NotBeNull();
            payload.ToolName.Should().Be("mcp__firecrawl__firecrawl_map");

            firecrawlResponse.Should().NotBeNull();
            firecrawlResponse!.Links.Should().HaveCount(5);

            // Check first link has all properties
            firecrawlResponse.Links[0].Url.Should().Be("https://burnrate.io/blog");
            firecrawlResponse.Links[0].Title.Should().Contain("GTM Leaders");
            firecrawlResponse.Links[0].Description.Should().NotBeNullOrEmpty();

            // Check link with only URL
            firecrawlResponse.Links[1].Url.Should().Be("https://burnrate.io/request-access");
            firecrawlResponse.Links[1].Title.Should().BeNull();
            firecrawlResponse.Links[1].Description.Should().BeNull();

            // Check subdomain link
            firecrawlResponse.Links[4].Url.Should().Contain("support.burnrate.io");
        }

        [TestMethod]
        public void ContentBlock_IsText_ShouldBeCaseInsensitive()
        {
            // Arrange & Act & Assert
            new ContentBlock { Type = "text" }.IsText.Should().BeTrue();
            new ContentBlock { Type = "TEXT" }.IsText.Should().BeTrue();
            new ContentBlock { Type = "Text" }.IsText.Should().BeTrue();
            new ContentBlock { Type = "image" }.IsText.Should().BeFalse();
        }

        [TestMethod]
        public void ContentBlock_IsImage_ShouldBeCaseInsensitive()
        {
            // Arrange & Act & Assert
            new ContentBlock { Type = "image" }.IsImage.Should().BeTrue();
            new ContentBlock { Type = "IMAGE" }.IsImage.Should().BeTrue();
            new ContentBlock { Type = "Image" }.IsImage.Should().BeTrue();
            new ContentBlock { Type = "text" }.IsImage.Should().BeFalse();
        }

        #endregion
    }

    #region Test Models

    /// <summary>
    /// Test model for Firecrawl map response.
    /// </summary>
    public class FirecrawlMapResponse
    {
        [JsonPropertyName("links")]
        public FirecrawlLink[] Links { get; set; } = [];
    }

    /// <summary>
    /// Test model for a single Firecrawl link.
    /// </summary>
    public class FirecrawlLink
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Type alias for MCP PostToolUse payload with ContentBlock[] response.
    /// </summary>
    public class McpPostToolUsePayload : PostToolUseHookInput<object, ContentBlock[]>
    {
    }

    /// <summary>
    /// JSON serializer context for ContentBlock tests.
    /// </summary>
    [JsonSourceGenerationOptions(
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified,
        WriteIndented = false,
        UseStringEnumConverter = true)]
    [JsonSerializable(typeof(McpPostToolUsePayload))]
    [JsonSerializable(typeof(ContentBlock))]
    [JsonSerializable(typeof(ContentBlock[]))]
    [JsonSerializable(typeof(FirecrawlMapResponse))]
    [JsonSerializable(typeof(FirecrawlLink))]
    [JsonSerializable(typeof(FirecrawlLink[]))]
    [JsonSerializable(typeof(HookEventName))]
    [JsonSerializable(typeof(PermissionMode))]
    public partial class ContentBlockTestContext : JsonSerializerContext
    {
    }

    #endregion
}
