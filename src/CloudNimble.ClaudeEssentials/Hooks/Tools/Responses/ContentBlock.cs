using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents a content block in tool responses, commonly used by MCP (Model Context Protocol) tools.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MCP tools wrap their responses in an array of content blocks. Each block has a type
    /// indicating the content format (typically "text" or "image") and the corresponding data.
    /// </para>
    /// <para>
    /// For text content blocks, the <see cref="Text"/> property contains the actual response data,
    /// which is often JSON that needs to be parsed separately. Use <see cref="GetTextContent{T}(JsonTypeInfo{T})"/>
    /// to deserialize the text content to a specific type.
    /// </para>
    /// <para>
    /// Example JSON payload from an MCP tool:
    /// <code>
    /// "tool_response": [
    ///   {
    ///     "type": "text",
    ///     "text": "{ \"links\": [\"https://example.com\"] }"
    ///   }
    /// ]
    /// </code>
    /// </para>
    /// <para>
    /// Example usage:
    /// <code>
    /// var blocks = hookInput.ToolResponse;
    /// var textBlock = blocks?.FirstOrDefault(b => b.IsText);
    /// var response = textBlock?.GetTextContent(MyJsonContext.Default.FirecrawlMapResponse);
    /// </code>
    /// </para>
    /// </remarks>
    public class ContentBlock
    {
        /// <summary>
        /// Gets a value indicating whether this is a text content block.
        /// </summary>
        [JsonIgnore]
        public bool IsText => string.Equals(Type, "text", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets a value indicating whether this is an image content block.
        /// </summary>
        [JsonIgnore]
        public bool IsImage => string.Equals(Type, "image", StringComparison.OrdinalIgnoreCase);
        /// <summary>
        /// Gets or sets the type of content in this block.
        /// </summary>
        /// <remarks>
        /// Common values include:
        /// <list type="bullet">
        /// <item><description><c>"text"</c> - Text content, with data in <see cref="Text"/></description></item>
        /// <item><description><c>"image"</c> - Image content, with data in <see cref="Data"/> and type in <see cref="MimeType"/></description></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the text content when <see cref="Type"/> is "text".
        /// </summary>
        /// <remarks>
        /// <para>
        /// For MCP tool responses, this typically contains JSON data that represents the
        /// actual tool response. You'll need to parse this JSON string separately to access
        /// the structured response data.
        /// </para>
        /// <para>
        /// Example usage:
        /// <code>
        /// var contentBlocks = hookInput.ToolResponse;
        /// var textBlock = contentBlocks?.FirstOrDefault(b => b.Type == "text");
        /// if (textBlock?.Text != null)
        /// {
        ///     var actualResponse = JsonSerializer.Deserialize&lt;MyResponseType&gt;(textBlock.Text);
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the base64-encoded data when <see cref="Type"/> is "image" or similar binary types.
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the content when <see cref="Type"/> is "image" or similar.
        /// </summary>
        /// <remarks>
        /// Common values include <c>"image/png"</c>, <c>"image/jpeg"</c>, etc.
        /// </remarks>
        [JsonPropertyName("mimeType")]
        public string? MimeType { get; set; }

        /// <summary>
        /// Deserializes the <see cref="Text"/> content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the text content to.</typeparam>
        /// <param name="jsonTypeInfo">The JSON type info for AOT-compatible deserialization.</param>
        /// <returns>The deserialized object, or <c>null</c> if <see cref="Text"/> is null or empty.</returns>
        /// <exception cref="JsonException">Thrown when the JSON is invalid or cannot be deserialized to the specified type.</exception>
        /// <remarks>
        /// <para>
        /// This method is AOT-compatible and requires a <see cref="JsonTypeInfo{T}"/> from a
        /// <see cref="JsonSerializerContext"/>. For example:
        /// </para>
        /// <code>
        /// var response = textBlock.GetTextContent(MyJsonContext.Default.FirecrawlMapResponse);
        /// </code>
        /// </remarks>
        public T? GetTextContent<T>(JsonTypeInfo<T> jsonTypeInfo)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return default;
            }

            return JsonSerializer.Deserialize(Text, jsonTypeInfo);
        }

        /// <summary>
        /// Attempts to deserialize the <see cref="Text"/> content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the text content to.</typeparam>
        /// <param name="jsonTypeInfo">The JSON type info for AOT-compatible deserialization.</param>
        /// <param name="result">When successful, contains the deserialized object; otherwise, the default value.</param>
        /// <returns><c>true</c> if deserialization succeeded; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <para>
        /// This method does not throw exceptions for invalid JSON. Use this when you want to
        /// safely attempt deserialization without exception handling.
        /// </para>
        /// <code>
        /// if (textBlock.TryGetTextContent(MyJsonContext.Default.FirecrawlMapResponse, out var response))
        /// {
        ///     // Use response
        /// }
        /// </code>
        /// </remarks>
        public bool TryGetTextContent<T>(JsonTypeInfo<T> jsonTypeInfo, out T? result)
        {
            result = default;

            if (string.IsNullOrEmpty(Text))
            {
                return false;
            }

            try
            {
                result = JsonSerializer.Deserialize(Text, jsonTypeInfo);
                return result != null;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
