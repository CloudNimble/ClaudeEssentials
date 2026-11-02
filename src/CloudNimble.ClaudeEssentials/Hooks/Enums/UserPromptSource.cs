using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents who authored or injected a prompt.
    /// </summary>
    public enum UserPromptSource
    {

        /// <summary>
        /// The interactive composer.
        /// </summary>
        [JsonStringEnumMemberName("user")]
        User,

        /// <summary>
        /// A non-interactive entrypoint, such as <c>-p</c> or the Agent SDK.
        /// </summary>
        [JsonStringEnumMemberName("sdk")]
        Sdk,

        /// <summary>
        /// Claude Code itself.
        /// </summary>
        [JsonStringEnumMemberName("system")]
        System,

        /// <summary>
        /// A dynamic loop wakeup.
        /// </summary>
        [JsonStringEnumMemberName("loop_wakeup")]
        LoopWakeup,

        /// <summary>
        /// A scheduled task.
        /// </summary>
        [JsonStringEnumMemberName("schedule_wakeup")]
        ScheduleWakeup,

        /// <summary>
        /// A polled event.
        /// </summary>
        [JsonStringEnumMemberName("poll_event")]
        PollEvent

    }

}
