using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Projects tool.
    /// </summary>
    /// <remarks>
    /// This is a polymorphic base type, and the <c>method</c> field in the JSON determines the derived type.
    /// </remarks>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "method")]
    [JsonDerivedType(typeof(InfoProjectsToolResponse), "project_info")]
    [JsonDerivedType(typeof(ReadProjectsToolResponse), "project_read")]
    [JsonDerivedType(typeof(SearchProjectsToolResponse), "project_search")]
    [JsonDerivedType(typeof(WriteProjectsToolResponse), "project_write")]
    [JsonDerivedType(typeof(DeleteProjectsToolResponse), "project_delete")]
    [JsonDerivedType(typeof(MemoryListProjectsToolResponse), "project_memory_list")]
    [JsonDerivedType(typeof(MemoryReadProjectsToolResponse), "project_memory_read")]
    public abstract class ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets an optional notice about the project.
        /// </summary>
        [JsonPropertyName("notice")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Notice { get; set; }

        #endregion

    }

}
