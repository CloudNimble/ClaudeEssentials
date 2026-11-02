using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a permission update that changes the permission mode.
    /// </summary>
    public class SetModePermissionUpdate : PermissionUpdate
    {

        #region Properties

        /// <summary>
        /// Gets or sets the new permission mode.
        /// </summary>
        [JsonPropertyName("mode")]
        public PermissionMode Mode { get; set; }

        #endregion

    }

}
