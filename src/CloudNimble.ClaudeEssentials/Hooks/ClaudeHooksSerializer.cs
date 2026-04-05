using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Provides static helper methods for serializing and deserializing Claude Code hook types.
    /// All methods use the AOT-compatible <see cref="ClaudeHooksJsonContext"/> for serialization.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class offers three levels of API for deserialization:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Named methods</strong> (e.g., <see cref="DeserializeBashPreToolUse"/>) —
    /// the simplest and most common path. Returns strongly-typed payloads with no configuration required.</description></item>
    /// <item><description><strong>Dynamic methods</strong> (e.g., <see cref="DeserializePreToolUseInput(string)"/>) —
    /// returns <c>PreToolUseHookInput&lt;object&gt;</c> for scenarios where you handle multiple tool types dynamically.</description></item>
    /// <item><description><strong>Generic methods</strong> (e.g., <see cref="DeserializePreToolUseInput{T}(string, JsonTypeInfo{T})"/>) —
    /// for advanced scenarios using custom <see cref="System.Text.Json.Serialization.JsonSerializerContext"/> registrations.</description></item>
    /// </list>
    /// </remarks>
    public static class ClaudeHooksSerializer
    {

        /// <summary>
        /// Gets the default <see cref="JsonSerializerOptions"/> configured for Claude Code hooks.
        /// This instance is configured with the <see cref="ClaudeHooksJsonContext"/> for AOT compatibility.
        /// </summary>
        public static JsonSerializerOptions DefaultOptions => ClaudeHooksJsonContext.Default.Options;

        #region Input Deserialization

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PreToolUseHookInput{TToolInput}"/> with dynamic tool input.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PreToolUseHookInput<object>? DeserializePreToolUseInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PreToolUseHookInputObject);
        }

        /// <summary>
        /// Deserializes a JSON string to a strongly-typed PreToolUse payload using the specified type info.
        /// </summary>
        /// <typeparam name="T">The payload type (e.g., <see cref="BashPreToolUsePayload"/>).</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="typeInfo">The JSON type info for the payload type.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        /// <example>
        /// <code>
        /// var payload = ClaudeHooksSerializer.DeserializePreToolUseInput(
        ///     json, ClaudeHooksJsonContext.Default.BashPreToolUsePayload);
        /// Console.WriteLine(payload?.ToolInput?.Command);
        /// </code>
        /// </example>
        public static T? DeserializePreToolUseInput<T>(string json, JsonTypeInfo<T> typeInfo)
            where T : class
        {
            return JsonSerializer.Deserialize(json, typeInfo);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PostToolUseHookInput{TToolInput, TToolResponse}"/> with dynamic types.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostToolUseHookInput<object, object>? DeserializePostToolUseInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PostToolUseHookInputObject);
        }

        /// <summary>
        /// Deserializes a JSON string to a strongly-typed PostToolUse payload using the specified type info.
        /// </summary>
        /// <typeparam name="T">The payload type (e.g., <see cref="BashPostToolUsePayload"/>).</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="typeInfo">The JSON type info for the payload type.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        /// <example>
        /// <code>
        /// var payload = ClaudeHooksSerializer.DeserializePostToolUseInput(
        ///     json, ClaudeHooksJsonContext.Default.BashPostToolUsePayload);
        /// Console.WriteLine(payload?.ToolResponse?.Stdout);
        /// </code>
        /// </example>
        public static T? DeserializePostToolUseInput<T>(string json, JsonTypeInfo<T> typeInfo)
            where T : class
        {
            return JsonSerializer.Deserialize(json, typeInfo);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PermissionRequestHookInput{TToolInput}"/> with dynamic tool input.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PermissionRequestHookInput<object>? DeserializePermissionRequestInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PermissionRequestHookInputObject);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="NotificationHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static NotificationHookInput? DeserializeNotificationInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotificationHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PreCompactHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PreCompactHookInput? DeserializePreCompactInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PreCompactHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SessionEndHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SessionEndHookInput? DeserializeSessionEndInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SessionEndHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SessionStartHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SessionStartHookInput? DeserializeSessionStartInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SessionStartHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="StopHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static StopHookInput? DeserializeStopInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.StopHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SubagentStopHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SubagentStopHookInput? DeserializeSubagentStopInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SubagentStopHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="UserPromptSubmitHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static UserPromptSubmitHookInput? DeserializeUserPromptSubmitInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.UserPromptSubmitHookInput);
        }

        #endregion

        #region PreToolUse Deserialization

        /// <summary>
        /// Deserializes a JSON string to an <see cref="AgentPreToolUsePayload"/> with strongly-typed <see cref="AgentToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static AgentPreToolUsePayload? DeserializeAgentPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="AskUserQuestionPreToolUsePayload"/> with strongly-typed <see cref="AskUserQuestionToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static AskUserQuestionPreToolUsePayload? DeserializeAskUserQuestionPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="BashPreToolUsePayload"/> with strongly-typed <see cref="BashToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        /// <example>
        /// <code>
        /// var payload = ClaudeHooksSerializer.DeserializeBashPreToolUse(json);
        /// Console.WriteLine(payload?.ToolInput?.Command);
        /// </code>
        /// </example>
        public static BashPreToolUsePayload? DeserializeBashPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ConfigPreToolUsePayload"/> with strongly-typed <see cref="ConfigToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ConfigPreToolUsePayload? DeserializeConfigPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="EditPreToolUsePayload"/> with strongly-typed <see cref="EditToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EditPreToolUsePayload? DeserializeEditPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="EnterWorktreePreToolUsePayload"/> with strongly-typed <see cref="EnterWorktreeToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EnterWorktreePreToolUsePayload? DeserializeEnterWorktreePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ExitPlanModePreToolUsePayload"/> with strongly-typed <see cref="ExitPlanModeToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ExitPlanModePreToolUsePayload? DeserializeExitPlanModePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ExitWorktreePreToolUsePayload"/> with strongly-typed <see cref="ExitWorktreeToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ExitWorktreePreToolUsePayload? DeserializeExitWorktreePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="GlobPreToolUsePayload"/> with strongly-typed <see cref="GlobToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static GlobPreToolUsePayload? DeserializeGlobPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="GrepPreToolUsePayload"/> with strongly-typed <see cref="GrepToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static GrepPreToolUsePayload? DeserializeGrepPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="KillShellPreToolUsePayload"/> with strongly-typed <see cref="KillShellToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static KillShellPreToolUsePayload? DeserializeKillShellPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ListMcpResourcesPreToolUsePayload"/> with strongly-typed <see cref="ListMcpResourcesToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ListMcpResourcesPreToolUsePayload? DeserializeListMcpResourcesPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="McpPreToolUsePayload"/> with strongly-typed <see cref="McpToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static McpPreToolUsePayload? DeserializeMcpPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="NotebookEditPreToolUsePayload"/> with strongly-typed <see cref="NotebookEditToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static NotebookEditPreToolUsePayload? DeserializeNotebookEditPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadPreToolUsePayload"/> with strongly-typed <see cref="ReadToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadPreToolUsePayload? DeserializeReadPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadMcpResourcePreToolUsePayload"/> with strongly-typed <see cref="ReadMcpResourceToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadMcpResourcePreToolUsePayload? DeserializeReadMcpResourcePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourcePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskPreToolUsePayload"/> with strongly-typed <see cref="TaskToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskPreToolUsePayload? DeserializeTaskPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskOutputPreToolUsePayload"/> with strongly-typed <see cref="TaskOutputToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskOutputPreToolUsePayload? DeserializeTaskOutputPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskOutputPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskStopPreToolUsePayload"/> with strongly-typed <see cref="TaskStopToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskStopPreToolUsePayload? DeserializeTaskStopPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TodoWritePreToolUsePayload"/> with strongly-typed <see cref="TodoWriteToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TodoWritePreToolUsePayload? DeserializeTodoWritePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WebFetchPreToolUsePayload"/> with strongly-typed <see cref="WebFetchToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WebFetchPreToolUsePayload? DeserializeWebFetchPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WebSearchPreToolUsePayload"/> with strongly-typed <see cref="WebSearchToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WebSearchPreToolUsePayload? DeserializeWebSearchPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WritePreToolUsePayload"/> with strongly-typed <see cref="WriteToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WritePreToolUsePayload? DeserializeWritePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePreToolUsePayload);
        }

        #endregion

        #region PostToolUse Deserialization

        /// <summary>
        /// Deserializes a JSON string to an <see cref="AgentPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static AgentPostToolUsePayload? DeserializeAgentPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AgentPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="AskUserQuestionPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static AskUserQuestionPostToolUsePayload? DeserializeAskUserQuestionPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="BashPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        /// <example>
        /// <code>
        /// var payload = ClaudeHooksSerializer.DeserializeBashPostToolUse(json);
        /// Console.WriteLine(payload?.ToolResponse?.Stdout);
        /// </code>
        /// </example>
        public static BashPostToolUsePayload? DeserializeBashPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.BashPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ConfigPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ConfigPostToolUsePayload? DeserializeConfigPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="EditPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EditPostToolUsePayload? DeserializeEditPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EditPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="EnterWorktreePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EnterWorktreePostToolUsePayload? DeserializeEnterWorktreePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterWorktreePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ExitPlanModePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ExitPlanModePostToolUsePayload? DeserializeExitPlanModePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ExitWorktreePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ExitWorktreePostToolUsePayload? DeserializeExitWorktreePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitWorktreePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="GlobPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static GlobPostToolUsePayload? DeserializeGlobPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GlobPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="GrepPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static GrepPostToolUsePayload? DeserializeGrepPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GrepPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="KillShellPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static KillShellPostToolUsePayload? DeserializeKillShellPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.KillShellPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ListMcpResourcesPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ListMcpResourcesPostToolUsePayload? DeserializeListMcpResourcesPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ListMcpResourcesPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="McpPostToolUsePayload"/> with strongly-typed input and dynamic response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static McpPostToolUsePayload? DeserializeMcpPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.McpPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="NotebookEditPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static NotebookEditPostToolUsePayload? DeserializeNotebookEditPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.NotebookEditPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadPostToolUsePayload? DeserializeReadPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadMcpResourcePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadMcpResourcePostToolUsePayload? DeserializeReadMcpResourcePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourcePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskPostToolUsePayload? DeserializeTaskPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskOutputPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskOutputPostToolUsePayload? DeserializeTaskOutputPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskOutputPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskStopPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskStopPostToolUsePayload? DeserializeTaskStopPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskStopPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TodoWritePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TodoWritePostToolUsePayload? DeserializeTodoWritePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WebFetchPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WebFetchPostToolUsePayload? DeserializeWebFetchPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebFetchPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WebSearchPostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WebSearchPostToolUsePayload? DeserializeWebSearchPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WebSearchPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WritePostToolUsePayload"/> with strongly-typed input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WritePostToolUsePayload? DeserializeWritePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WritePostToolUsePayload);
        }

        #endregion

        #region Output Serialization

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with dynamic tool input to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePreToolUseOutput(PreToolUseHookOutput<object> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PreToolUseHookOutputObject);
        }

        /// <summary>
        /// Serializes a strongly-typed PreToolUse hook output to JSON using the specified type info.
        /// </summary>
        /// <typeparam name="T">The output type.</typeparam>
        /// <param name="output">The hook output to serialize.</param>
        /// <param name="typeInfo">The JSON type info for the output type.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePreToolUseOutput<T>(T output, JsonTypeInfo<T> typeInfo)
            where T : class
        {
            return JsonSerializer.Serialize(output, typeInfo);
        }

        /// <summary>
        /// Serializes a <see cref="PermissionRequestHookOutput{TToolInput}"/> with dynamic tool input to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePermissionRequestOutput(PermissionRequestHookOutput<object> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PermissionRequestHookOutputObject);
        }

        /// <summary>
        /// Serializes a <see cref="PostToolUseHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePostToolUseOutput(PostToolUseHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PostToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="NotificationHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeNotificationOutput(NotificationHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.NotificationHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreCompactHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePreCompactOutput(PreCompactHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PreCompactHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="SessionEndHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSessionEndOutput(SessionEndHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SessionEndHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="SessionStartHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSessionStartOutput(SessionStartHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SessionStartHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="StopHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeStopOutput(StopHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.StopHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="SubagentStopHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSubagentStopOutput(SubagentStopHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SubagentStopHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="UserPromptSubmitHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeUserPromptSubmitOutput(UserPromptSubmitHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.UserPromptSubmitHookOutput);
        }

        #endregion

        #region PreToolUse Output Serialization

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="AgentToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeAgentPreToolUseOutput(PreToolUseHookOutput<AgentToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.AgentPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="AskUserQuestionToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeAskUserQuestionPreToolUseOutput(PreToolUseHookOutput<AskUserQuestionToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.AskUserQuestionPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="BashToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        /// <example>
        /// <code>
        /// var output = new PreToolUseHookOutput&lt;BashToolInput&gt;
        /// {
        ///     Continue = true,
        ///     HookSpecificOutput = new PreToolUseSpecificOutput&lt;BashToolInput&gt;
        ///     {
        ///         PermissionDecision = PermissionDecision.Allow,
        ///         UpdatedInput = new BashToolInput { Command = "echo safe" }
        ///     }
        /// };
        /// var json = ClaudeHooksSerializer.SerializeBashPreToolUseOutput(output);
        /// </code>
        /// </example>
        public static string SerializeBashPreToolUseOutput(PreToolUseHookOutput<BashToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.BashPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ConfigToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeConfigPreToolUseOutput(PreToolUseHookOutput<ConfigToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ConfigPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="EditToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeEditPreToolUseOutput(PreToolUseHookOutput<EditToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.EditPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="EnterWorktreeToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeEnterWorktreePreToolUseOutput(PreToolUseHookOutput<EnterWorktreeToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.EnterWorktreePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ExitPlanModeToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeExitPlanModePreToolUseOutput(PreToolUseHookOutput<ExitPlanModeToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ExitPlanModePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ExitWorktreeToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeExitWorktreePreToolUseOutput(PreToolUseHookOutput<ExitWorktreeToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ExitWorktreePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="GlobToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeGlobPreToolUseOutput(PreToolUseHookOutput<GlobToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.GlobPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="GrepToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeGrepPreToolUseOutput(PreToolUseHookOutput<GrepToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.GrepPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="KillShellToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeKillShellPreToolUseOutput(PreToolUseHookOutput<KillShellToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.KillShellPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ListMcpResourcesToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeListMcpResourcesPreToolUseOutput(PreToolUseHookOutput<ListMcpResourcesToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ListMcpResourcesPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="McpToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeMcpPreToolUseOutput(PreToolUseHookOutput<McpToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.McpPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="NotebookEditToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeNotebookEditPreToolUseOutput(PreToolUseHookOutput<NotebookEditToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.NotebookEditPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ReadToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeReadPreToolUseOutput(PreToolUseHookOutput<ReadToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ReadPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ReadMcpResourceToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeReadMcpResourcePreToolUseOutput(PreToolUseHookOutput<ReadMcpResourceToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ReadMcpResourcePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskPreToolUseOutput(PreToolUseHookOutput<TaskToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskOutputToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskOutputPreToolUseOutput(PreToolUseHookOutput<TaskOutputToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskOutputPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskStopToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskStopPreToolUseOutput(PreToolUseHookOutput<TaskStopToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskStopPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TodoWriteToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTodoWritePreToolUseOutput(PreToolUseHookOutput<TodoWriteToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TodoWritePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="WebFetchToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWebFetchPreToolUseOutput(PreToolUseHookOutput<WebFetchToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WebFetchPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="WebSearchToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWebSearchPreToolUseOutput(PreToolUseHookOutput<WebSearchToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WebSearchPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="WriteToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWritePreToolUseOutput(PreToolUseHookOutput<WriteToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WritePreToolUseHookOutput);
        }

        #endregion

    }

}
