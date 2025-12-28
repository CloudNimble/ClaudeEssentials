using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PermissionRequest hook.
    /// This hook runs when a permission dialog is shown and can automatically allow or deny permissions.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PermissionRequestHookInput<TToolInput> : ToolHookInputBase<TToolInput>
        where TToolInput : class
    {

        /// <summary>
        /// Gets or sets the message displayed in the permission dialog.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

    }

}
