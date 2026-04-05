using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ImageReadToolResponse"/> deserialization and round-trips.
    /// Schema-based payloads — no captured image Read logs exist in this repo.
    /// </summary>
    [TestClass]
    public class ImageReadToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeImageRead_WithDimensions_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": {
                        "file_path": "D:\\Projects\\TestProject\\assets\\logo.png"
                    },
                    "tool_response": {
                        "type": "image",
                        "file": {
                            "base64": "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAC0lEQVQI12NgAAIABQABNjN9GQAAAAlwSFlzAAAWJQAAFiUBSVIk8AAAABl0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC4xNkRpr/UAAAAOSURBVGMYM2D4z8BQDwAECQGB/Wd9tAAAAABJRU5ErkJggg==",
                            "type": "image/png",
                            "originalSize": 119,
                            "dimensions": {
                                "originalWidth": 1920,
                                "originalHeight": 1080,
                                "displayWidth": 960,
                                "displayHeight": 540
                            }
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().BeOfType<ImageReadToolResponse>();
            var image = (ImageReadToolResponse)result.ToolResponse!;
            image.File.Base64.Should().StartWith("iVBORw0KGgo");
            image.File.MimeType.Should().Be("image/png");
            image.File.OriginalSize.Should().Be(119);
            image.File.Dimensions.Should().NotBeNull();
            image.File.Dimensions!.OriginalWidth.Should().Be(1920);
            image.File.Dimensions.OriginalHeight.Should().Be(1080);
            image.File.Dimensions.DisplayWidth.Should().Be(960);
            image.File.Dimensions.DisplayHeight.Should().Be(540);
        }

        [TestMethod]
        public void DeserializeImageRead_WithoutDimensions_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": { "file_path": "D:\\Projects\\TestProject\\icon.jpg" },
                    "tool_response": {
                        "type": "image",
                        "file": {
                            "base64": "/9j/4AAQSkZJRg==",
                            "type": "image/jpeg",
                            "originalSize": 8192
                        }
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            var image = (ImageReadToolResponse)result!.ToolResponse!;
            image.File.MimeType.Should().Be("image/jpeg");
            image.File.OriginalSize.Should().Be(8192);
            image.File.Dimensions.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeImageRead_PartialDimensions_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "Read",
                    "tool_input": { "file_path": "D:\\Projects\\TestProject\\photo.webp" },
                    "tool_response": {
                        "type": "image",
                        "file": {
                            "base64": "UklGRlYAAABXRUJQ",
                            "type": "image/webp",
                            "originalSize": 32768,
                            "dimensions": {
                                "originalWidth": 800,
                                "originalHeight": 600
                            }
                        }
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);

            var image = (ImageReadToolResponse)result!.ToolResponse!;
            image.File.MimeType.Should().Be("image/webp");
            image.File.Dimensions.Should().NotBeNull();
            image.File.Dimensions!.OriginalWidth.Should().Be(800);
            image.File.Dimensions.DisplayWidth.Should().BeNull();
            image.File.Dimensions.DisplayHeight.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ImageReadToolResponse_RoundTrip_ShouldMaintainData()
        {
            var response = new ImageReadToolResponse
            {
                File = new ImageReadToolFileInfo
                {
                    Base64 = "dGVzdA==",
                    MimeType = "image/gif",
                    OriginalSize = 42,
                    Dimensions = new ImageDimensionInfo
                    {
                        OriginalWidth = 100,
                        OriginalHeight = 50
                    }
                }
            };

            var json = JsonSerializer.Serialize<ReadToolResponse>(response, ClaudeHooksJsonContext.Default.ReadToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadToolResponse);

            result.Should().BeOfType<ImageReadToolResponse>();
            var image = (ImageReadToolResponse)result!;
            image.File.Base64.Should().Be("dGVzdA==");
            image.File.MimeType.Should().Be("image/gif");
            image.File.OriginalSize.Should().Be(42);
            image.File.Dimensions!.OriginalWidth.Should().Be(100);
        }

        #endregion

    }

}
