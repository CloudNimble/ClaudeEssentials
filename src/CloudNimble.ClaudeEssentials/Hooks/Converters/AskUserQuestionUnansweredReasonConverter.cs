using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Converters
{

    /// <summary>
    /// Converts the <c>unanswered</c> field of an AskUserQuestion annotation to and from a nullable <see cref="AskUserQuestionUnansweredReason"/>.
    /// </summary>
    /// <remarks>
    /// The schema instructs consumers to treat any unrecognized value as absent, so this converter reads unknown
    /// values as <see langword="null"/> rather than throwing as the default enum handling would.
    /// </remarks>
    public sealed class AskUserQuestionUnansweredReasonConverter : JsonConverter<AskUserQuestionUnansweredReason?>
    {

        #region Public Methods

        /// <inheritdoc />
        public override AskUserQuestionUnansweredReason? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.String)
            {
                reader.Skip();
                return null;
            }

            return reader.GetString() switch
            {
                "skipped" => AskUserQuestionUnansweredReason.Skipped,
                "replied_in_chat" => AskUserQuestionUnansweredReason.RepliedInChat,
                "dismissed" => AskUserQuestionUnansweredReason.Dismissed,
                _ => null
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, AskUserQuestionUnansweredReason? value, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(writer);

            switch (value)
            {
                case AskUserQuestionUnansweredReason.Skipped:
                    writer.WriteStringValue("skipped");
                    break;
                case AskUserQuestionUnansweredReason.RepliedInChat:
                    writer.WriteStringValue("replied_in_chat");
                    break;
                case AskUserQuestionUnansweredReason.Dismissed:
                    writer.WriteStringValue("dismissed");
                    break;
                default:
                    writer.WriteNullValue();
                    break;
            }
        }

        #endregion

    }

}
