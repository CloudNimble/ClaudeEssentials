using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a Notification hook.
    /// The Notification hook typically only uses base output fields.
    /// </summary>
    public class NotificationHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public NotificationSpecificOutput? HookSpecificOutput { get; set; }

    }

}
