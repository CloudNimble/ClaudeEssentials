using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents why a session ended.
    /// </summary>
    public enum SessionEndReason
    {

        /// <summary>
        /// The session was cleared.
        /// </summary>
        [JsonStringEnumMemberName("clear")]
        Clear,

        /// <summary>
        /// Another session was resumed.
        /// </summary>
        [JsonStringEnumMemberName("resume")]
        Resume,

        /// <summary>
        /// The user logged out.
        /// </summary>
        [JsonStringEnumMemberName("logout")]
        Logout,

        /// <summary>
        /// The user exited at the prompt input.
        /// </summary>
        [JsonStringEnumMemberName("prompt_input_exit")]
        PromptInputExit,

        /// <summary>
        /// Any other reason.
        /// </summary>
        [JsonStringEnumMemberName("other")]
        Other

    }

}
