using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a source synchronized into a project.
    /// </summary>
    public class ProjectSyncSource
    {

        #region Properties

        /// <summary>
        /// Gets or sets the source configuration.
        /// </summary>
        [JsonPropertyName("config")]
        public Dictionary<string, JsonElement> Config { get; set; } = [];

        /// <summary>
        /// Gets or sets the source type, which may be null.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? Type { get; set; }

        #endregion

    }

}
