using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the decision object for a PermissionRequest hook response.
    /// Contains the behavior decision and optional parameters.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters for updates.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PermissionRequestDecision<TToolInput>
        where TToolInput : class
    {

        /// <summary>
        /// Gets or sets the behavior to take for the permission request.
        /// </summary>
        [JsonPropertyName("behavior")]
        public PermissionRequestBehavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets optional modifications to the tool's input parameters.
        /// Only applicable when <see cref="Behavior"/> is <see cref="PermissionRequestBehavior.Allow"/>.
        /// </summary>
        [JsonPropertyName("updatedInput")]
        public TToolInput? UpdatedInput { get; set; }

        /// <summary>
        /// Gets or sets the message to display when the permission is denied.
        /// Only applicable when <see cref="Behavior"/> is <see cref="PermissionRequestBehavior.Deny"/>.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to interrupt the current operation.
        /// Only applicable when <see cref="Behavior"/> is <see cref="PermissionRequestBehavior.Deny"/>.
        /// </summary>
        [JsonPropertyName("interrupt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Interrupt { get; set; }

    }

}
