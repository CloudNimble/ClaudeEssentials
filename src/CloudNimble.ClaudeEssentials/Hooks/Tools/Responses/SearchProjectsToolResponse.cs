using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_search</c> method.
    /// </summary>
    public class SearchProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the project documents.
        /// </summary>
        [JsonPropertyName("docs")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Docs { get; set; }

        /// <summary>
        /// Gets or sets the search hits.
        /// </summary>
        [JsonPropertyName("hits")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProjectSearchHit>? Hits { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the project uses retrieval-augmented search.
        /// </summary>
        [JsonPropertyName("rag")]
        public bool Rag { get; set; }

        #endregion

    }

}
