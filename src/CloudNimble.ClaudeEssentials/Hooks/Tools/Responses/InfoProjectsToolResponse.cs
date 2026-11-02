using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_info</c> method.
    /// </summary>
    public class InfoProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the project documents.
        /// </summary>
        [JsonPropertyName("docs")]
        public List<ProjectDocumentInfo> Docs { get; set; } = [];

        /// <summary>
        /// Gets or sets the project files.
        /// </summary>
        [JsonPropertyName("files")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProjectFileInfo>? Files { get; set; }

        /// <summary>
        /// Gets or sets the project instructions.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the knowledge-base capacity of the project.
        /// </summary>
        [JsonPropertyName("knowledge")]
        public ProjectKnowledgeInfo Knowledge { get; set; } = new();

        /// <summary>
        /// Gets or sets the local file holding the full result.
        /// </summary>
        [JsonPropertyName("local_file")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocalFile { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the project's sync sources.
        /// </summary>
        [JsonPropertyName("sync_sources")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ProjectSyncSource>? SyncSources { get; set; }

        #endregion

    }

}
