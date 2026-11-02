using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Converters
{

    /// <summary>
    /// Converts an entry of the WebSearch <c>results</c> array, which is either a block of search hits or a string of text commentary from the model.
    /// </summary>
    public sealed class WebSearchResultContainerConverter : JsonConverter<WebSearchResultContainer>
    {

        #region Public Methods

        /// <inheritdoc />
        public override WebSearchResultContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.String)
            {
                return new WebSearchResultContainer { Commentary = reader.GetString() };
            }

            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException($"Unexpected token {reader.TokenType} for a WebSearch result.");
            }

            var container = new WebSearchResultContainer();
            while (reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
            {
                var name = reader.GetString();
                reader.Read();

                switch (name)
                {
                    case "tool_use_id":
                        container.ToolUseId = reader.GetString() ?? string.Empty;
                        break;
                    case "content":
                        container.Content = JsonSerializer.Deserialize(ref reader, ClaudeHooksJsonContext.Default.ListWebSearchResultItem) ?? [];
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return container;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, WebSearchResultContainer value, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(value);

            if (value.Commentary is not null)
            {
                writer.WriteStringValue(value.Commentary);
                return;
            }

            writer.WriteStartObject();
            writer.WriteString("tool_use_id", value.ToolUseId);
            writer.WritePropertyName("content");
            JsonSerializer.Serialize(writer, value.Content, ClaudeHooksJsonContext.Default.ListWebSearchResultItem);
            writer.WriteEndObject();
        }

        #endregion

    }

}
