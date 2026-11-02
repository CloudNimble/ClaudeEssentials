using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a permission update that adds directories.
    /// </summary>
    public class AddDirectoriesPermissionUpdate : PermissionUpdate
    {

        #region Properties

        /// <summary>
        /// Gets or sets the directories to add.
        /// </summary>
        [JsonPropertyName("directories")]
        public List<string> Directories { get; set; } = [];

        #endregion

    }

}
