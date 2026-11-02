using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the capability declaration stored on an artifact.
    /// </summary>
    public class ArtifactStoredDeclaration
    {

        #region Properties

        /// <summary>
        /// Gets or sets the stored capabilities.
        /// </summary>
        [JsonPropertyName("capabilities")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Capabilities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the declaration was carried forward from a previous version.
        /// </summary>
        [JsonPropertyName("carried")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Carried { get; set; }

        /// <summary>
        /// Gets or sets the stored runtime version.
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the preferred runtime version.
        /// </summary>
        [JsonPropertyName("preferredContract")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PreferredContract { get; set; }

        /// <summary>
        /// Gets or sets the guidance for reading the declaration.
        /// </summary>
        [JsonPropertyName("read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Read { get; set; }

        #endregion

    }

}
