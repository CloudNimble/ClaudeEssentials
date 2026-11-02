using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a question asked through the AskUserQuestion tool went unanswered.
    /// </summary>
    public enum AskUserQuestionUnansweredReason
    {

        /// <summary>
        /// The user skipped the question.
        /// </summary>
        [JsonStringEnumMemberName("skipped")]
        Skipped,

        /// <summary>
        /// The user sent a chat message instead.
        /// </summary>
        [JsonStringEnumMemberName("replied_in_chat")]
        RepliedInChat,

        /// <summary>
        /// The user closed the card.
        /// </summary>
        [JsonStringEnumMemberName("dismissed")]
        Dismissed

    }

}
