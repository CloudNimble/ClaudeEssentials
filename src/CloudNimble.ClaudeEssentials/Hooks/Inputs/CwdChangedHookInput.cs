using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a CwdChanged hook.
    /// This hook runs when the current working directory changes.
    /// </summary>
    public class CwdChangedHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the previous working directory.
        /// </summary>
        [JsonPropertyName("old_cwd")]
        public string OldCwd { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new working directory.
        /// </summary>
        [JsonPropertyName("new_cwd")]
        public string NewCwd { get; set; } = string.Empty;

    }

}
