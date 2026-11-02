using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a permission update that removes directories.
    /// </summary>
    public class RemoveDirectoriesPermissionUpdate : PermissionUpdate
    {

        #region Properties

        /// <summary>
        /// Gets or sets the directories to remove.
        /// </summary>
        [JsonPropertyName("directories")]
        public List<string> Directories { get; set; } = [];

        #endregion

    }

}
