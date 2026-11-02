using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents what initiated a model switch.
    /// </summary>
    public enum ModelSwitchSource
    {

        /// <summary>
        /// A command, such as <c>/model</c>, or the configuration UI.
        /// </summary>
        [JsonStringEnumMemberName("command")]
        Command,

        /// <summary>
        /// An interactive model picker.
        /// </summary>
        [JsonStringEnumMemberName("picker")]
        Picker,

        /// <summary>
        /// A headless request from the SDK, Remote Control, or an IDE.
        /// </summary>
        [JsonStringEnumMemberName("sdk")]
        Sdk,

        /// <summary>
        /// An automatic fallback. Reported only after the switch.
        /// </summary>
        [JsonStringEnumMemberName("auto")]
        Auto,

        /// <summary>
        /// Resuming a session. Reported only after the switch.
        /// </summary>
        [JsonStringEnumMemberName("resume")]
        Resume

    }

}
