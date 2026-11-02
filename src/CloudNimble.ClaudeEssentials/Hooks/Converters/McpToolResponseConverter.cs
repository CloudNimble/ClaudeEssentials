using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Converters
{

    /// <summary>
    /// Converts the untagged MCP tool result union (a string, an array of content blocks, or an object) to and from an <see cref="McpToolResponse"/>.
    /// </summary>
    public sealed class McpToolResponseConverter : JsonConverter<McpToolResponse>
    {

        #region Public Methods

        /// <inheritdoc />
        public override McpToolResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => new McpToolResponse { Text = reader.GetString() },
                JsonTokenType.StartArray => new McpToolResponse { ContentBlocks = JsonSerializer.Deserialize(ref reader, ClaudeHooksJsonContext.Default.ListMcpContentBlock) },
                JsonTokenType.StartObject => new McpToolResponse { StructuredContent = JsonSerializer.Deserialize(ref reader, ClaudeHooksJsonContext.Default.DictionaryStringJsonElement) },
                _ => throw new JsonException($"Unexpected token {reader.TokenType} for an MCP tool result.")
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, McpToolResponse value, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(value);

            if (value.ContentBlocks is not null)
            {
                JsonSerializer.Serialize(writer, value.ContentBlocks, ClaudeHooksJsonContext.Default.ListMcpContentBlock);
                return;
            }

            if (value.StructuredContent is not null)
            {
                JsonSerializer.Serialize(writer, value.StructuredContent, ClaudeHooksJsonContext.Default.DictionaryStringJsonElement);
                return;
            }

            writer.WriteStringValue(value.Text ?? string.Empty);
        }

        #endregion

    }

}
