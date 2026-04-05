using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by an InstructionsLoaded hook.
    /// This hook runs when instructions (CLAUDE.md files, etc.) are loaded.
    /// </summary>
    public class InstructionsLoadedHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the absolute path to the instruction file that was loaded.
        /// </summary>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path glob patterns from the instruction file's frontmatter.
        /// Only present when the instruction file defines glob patterns.
        /// </summary>
        [JsonPropertyName("globs")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Globs { get; set; }

        /// <summary>
        /// Gets or sets the reason why the instruction file was loaded.
        /// </summary>
        [JsonPropertyName("load_reason")]
        public InstructionsLoadReason LoadReason { get; set; }

        /// <summary>
        /// Gets or sets the type of memory file that was loaded.
        /// </summary>
        [JsonPropertyName("memory_type")]
        public MemoryType MemoryType { get; set; }

        /// <summary>
        /// Gets or sets the parent instruction file path for includes.
        /// Only present when this file was loaded via an include directive.
        /// </summary>
        [JsonPropertyName("parent_file_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ParentFilePath { get; set; }

        /// <summary>
        /// Gets or sets the path that triggered a lazy load of this instruction file.
        /// Only present when the file was loaded via a path glob match.
        /// </summary>
        [JsonPropertyName("trigger_file_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TriggerFilePath { get; set; }

    }

}
