using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PermissionDenied hook.
    /// This hook runs when a permission request is denied.
    /// </summary>
    public class PermissionDeniedHookInput : ToolHookInputBase<object>
    {

        /// <summary>
        /// Gets or sets the classifier's explanation for why the permission was denied.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;

    }

}
