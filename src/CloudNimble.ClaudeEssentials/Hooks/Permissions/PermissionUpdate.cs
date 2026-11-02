using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a change to Claude Code's permission rules, mode, or directories.
    /// </summary>
    /// <remarks>
    /// This is a polymorphic base type, and the <c>type</c> field in the JSON determines the derived type.
    /// </remarks>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(AddRulesPermissionUpdate), "addRules")]
    [JsonDerivedType(typeof(ReplaceRulesPermissionUpdate), "replaceRules")]
    [JsonDerivedType(typeof(RemoveRulesPermissionUpdate), "removeRules")]
    [JsonDerivedType(typeof(SetModePermissionUpdate), "setMode")]
    [JsonDerivedType(typeof(AddDirectoriesPermissionUpdate), "addDirectories")]
    [JsonDerivedType(typeof(RemoveDirectoriesPermissionUpdate), "removeDirectories")]
    public abstract class PermissionUpdate
    {

        #region Properties

        /// <summary>
        /// Gets or sets where the update is saved.
        /// </summary>
        [JsonPropertyName("destination")]
        public PermissionUpdateDestination Destination { get; set; }

        #endregion

    }

}
