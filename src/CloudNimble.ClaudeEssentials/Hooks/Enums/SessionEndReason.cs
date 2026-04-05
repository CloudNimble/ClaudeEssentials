using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the reason why a session ended.
    /// </summary>
    public enum SessionEndReason
    {

        /// <summary>
        /// Session ended due to a clear command.
        /// </summary>
        [JsonStringEnumMemberName("clear")]
        Clear,

        /// <summary>
        /// Session resumed, ending the previous session.
        /// </summary>
        [JsonStringEnumMemberName("resume")]
        Resume,

        /// <summary>
        /// Session ended due to user logout.
        /// </summary>
        [JsonStringEnumMemberName("logout")]
        Logout,

        /// <summary>
        /// Session ended due to user exiting from prompt input.
        /// </summary>
        [JsonStringEnumMemberName("prompt_input_exit")]
        PromptInputExit,

        /// <summary>
        /// Session ended because bypass permissions mode was disabled.
        /// </summary>
        [JsonStringEnumMemberName("bypass_permissions_disabled")]
        BypassPermissionsDisabled,

        /// <summary>
        /// Session ended for another unspecified reason.
        /// </summary>
        [JsonStringEnumMemberName("other")]
        Other

    }

}
