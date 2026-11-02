using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the knowledge-base capacity of a project.
    /// </summary>
    public class ProjectKnowledgeInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the current knowledge size.
        /// </summary>
        [JsonPropertyName("knowledge_size")]
        public long KnowledgeSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum knowledge size.
        /// </summary>
        [JsonPropertyName("max_knowledge_size")]
        public long MaxKnowledgeSize { get; set; }

        #endregion

    }

}
