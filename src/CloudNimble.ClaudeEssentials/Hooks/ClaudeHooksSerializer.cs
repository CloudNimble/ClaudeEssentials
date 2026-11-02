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

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ConfigChangeHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static ConfigChangeHookInput? DeserializeConfigChangeInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ConfigChangeHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CwdChangedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static CwdChangedHookInput? DeserializeCwdChangedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CwdChangedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ElicitationHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static ElicitationHookInput? DeserializeElicitationInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ElicitationHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="ElicitationResultHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static ElicitationResultHookInput? DeserializeElicitationResultInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ElicitationResultHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="FileChangedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static FileChangedHookInput? DeserializeFileChangedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.FileChangedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="InstructionsLoadedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static InstructionsLoadedHookInput? DeserializeInstructionsLoadedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.InstructionsLoadedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PermissionDeniedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PermissionDeniedHookInput? DeserializePermissionDeniedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PermissionDeniedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PostCompactHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostCompactHookInput? DeserializePostCompactInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PostCompactHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PostToolUseFailureHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostToolUseFailureHookInput? DeserializePostToolUseFailureInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PostToolUseFailureHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="StopFailureHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static StopFailureHookInput? DeserializeStopFailureInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.StopFailureHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SubagentStartHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SubagentStartHookInput? DeserializeSubagentStartInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SubagentStartHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskCompletedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static TaskCompletedHookInput? DeserializeTaskCompletedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskCompletedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskCreatedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static TaskCreatedHookInput? DeserializeTaskCreatedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskCreatedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TeammateIdleHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static TeammateIdleHookInput? DeserializeTeammateIdleInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TeammateIdleHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WorktreeCreateHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static WorktreeCreateHookInput? DeserializeWorktreeCreateInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WorktreeCreateHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="WorktreeRemoveHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static WorktreeRemoveHookInput? DeserializeWorktreeRemoveInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WorktreeRemoveHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="DirectoryAddedHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static DirectoryAddedHookInput? DeserializeDirectoryAddedInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.DirectoryAddedHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="MessageDisplayHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static MessageDisplayHookInput? DeserializeMessageDisplayInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.MessageDisplayHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PostModelSwitchHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostModelSwitchHookInput? DeserializePostModelSwitchInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PostModelSwitchHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PostToolBatchHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostToolBatchHookInput? DeserializePostToolBatchInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PostToolBatchHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PreModelSwitchHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PreModelSwitchHookInput? DeserializePreModelSwitchInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PreModelSwitchHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SetupHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SetupHookInput? DeserializeSetupInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SetupHookInput);
        }

        /// <summary>
        /// Deserializes a JSON string to an <see cref="UserPromptExpansionHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static UserPromptExpansionHookInput? DeserializeUserPromptExpansionInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.UserPromptExpansionHookInput);
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
        /// Deserializes a JSON string to an <see cref="ArtifactPreToolUsePayload"/> with strongly-typed <see cref="ArtifactToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ArtifactPreToolUsePayload? DeserializeArtifactPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ArtifactPreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="ClaudeDesignPreToolUsePayload"/> with strongly-typed <see cref="ClaudeDesignToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ClaudeDesignPreToolUsePayload? DeserializeClaudeDesignPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ClaudeDesignPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronCreatePreToolUsePayload"/> with strongly-typed <see cref="CronCreateToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronCreatePreToolUsePayload? DeserializeCronCreatePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronCreatePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronDeletePreToolUsePayload"/> with strongly-typed <see cref="CronDeleteToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronDeletePreToolUsePayload? DeserializeCronDeletePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronDeletePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronListPreToolUsePayload"/> with strongly-typed <see cref="CronListToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronListPreToolUsePayload? DeserializeCronListPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronListPreToolUsePayload);
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
        /// Deserializes a JSON string to an <see cref="EnterPlanModePreToolUsePayload"/> with strongly-typed <see cref="EnterPlanModeToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EnterPlanModePreToolUsePayload? DeserializeEnterPlanModePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterPlanModePreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="MonitorPreToolUsePayload"/> with strongly-typed <see cref="MonitorToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static MonitorPreToolUsePayload? DeserializeMonitorPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.MonitorPreToolUsePayload);
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
        /// Deserializes a JSON string to an <see cref="OfferChromeSetupPreToolUsePayload"/> with strongly-typed <see cref="OfferChromeSetupToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static OfferChromeSetupPreToolUsePayload? DeserializeOfferChromeSetupPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.OfferChromeSetupPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProjectsPreToolUsePayload"/> with strongly-typed <see cref="ProjectsToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProjectsPreToolUsePayload? DeserializeProjectsPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProjectsPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProposeGoalPreToolUsePayload"/> with strongly-typed <see cref="ProposeGoalToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProposeGoalPreToolUsePayload? DeserializeProposeGoalPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProposeGoalPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProposeSkillsPreToolUsePayload"/> with strongly-typed <see cref="ProposeSkillsToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProposeSkillsPreToolUsePayload? DeserializeProposeSkillsPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProposeSkillsPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PushNotificationPreToolUsePayload"/> with strongly-typed <see cref="PushNotificationToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static PushNotificationPreToolUsePayload? DeserializePushNotificationPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PushNotificationPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadMcpResourceDirPreToolUsePayload"/> with strongly-typed <see cref="ReadMcpResourceDirToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadMcpResourceDirPreToolUsePayload? DeserializeReadMcpResourceDirPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourceDirPreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="ReadNotificationsPreToolUsePayload"/> with strongly-typed <see cref="ReadNotificationsToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadNotificationsPreToolUsePayload? DeserializeReadNotificationsPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadNotificationsPreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="RefreshMcpToolsPreToolUsePayload"/> with strongly-typed <see cref="RefreshMcpToolsToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static RefreshMcpToolsPreToolUsePayload? DeserializeRefreshMcpToolsPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.RefreshMcpToolsPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="RemoteTriggerPreToolUsePayload"/> with strongly-typed <see cref="RemoteTriggerToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static RemoteTriggerPreToolUsePayload? DeserializeRemoteTriggerPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.RemoteTriggerPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReportFindingsPreToolUsePayload"/> with strongly-typed <see cref="ReportFindingsToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReportFindingsPreToolUsePayload? DeserializeReportFindingsPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReportFindingsPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ScheduleWakeupPreToolUsePayload"/> with strongly-typed <see cref="ScheduleWakeupToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ScheduleWakeupPreToolUsePayload? DeserializeScheduleWakeupPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ScheduleWakeupPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SendFeedbackPreToolUsePayload"/> with strongly-typed <see cref="SendFeedbackToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static SendFeedbackPreToolUsePayload? DeserializeSendFeedbackPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SendFeedbackPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ShowOnboardingRolePickerPreToolUsePayload"/> with strongly-typed <see cref="ShowOnboardingRolePickerToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ShowOnboardingRolePickerPreToolUsePayload? DeserializeShowOnboardingRolePickerPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ShowOnboardingRolePickerPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskCreatePreToolUsePayload"/> with strongly-typed <see cref="TaskCreateToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskCreatePreToolUsePayload? DeserializeTaskCreatePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskCreatePreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskGetPreToolUsePayload"/> with strongly-typed <see cref="TaskGetToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskGetPreToolUsePayload? DeserializeTaskGetPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskGetPreToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskListPreToolUsePayload"/> with strongly-typed <see cref="TaskListToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskListPreToolUsePayload? DeserializeTaskListPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskListPreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="TaskUpdatePreToolUsePayload"/> with strongly-typed <see cref="TaskUpdateToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskUpdatePreToolUsePayload? DeserializeTaskUpdatePreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskUpdatePreToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="WorkflowPreToolUsePayload"/> with strongly-typed <see cref="WorkflowToolInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WorkflowPreToolUsePayload? DeserializeWorkflowPreToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WorkflowPreToolUsePayload);
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
        /// Deserializes a JSON string to an <see cref="ArtifactPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ArtifactPostToolUsePayload? DeserializeArtifactPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ArtifactPostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="ClaudeDesignPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ClaudeDesignPostToolUsePayload? DeserializeClaudeDesignPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ClaudeDesignPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronCreatePostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronCreatePostToolUsePayload? DeserializeCronCreatePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronCreatePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronDeletePostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronDeletePostToolUsePayload? DeserializeCronDeletePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronDeletePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="CronListPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static CronListPostToolUsePayload? DeserializeCronListPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.CronListPostToolUsePayload);
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
        /// Deserializes a JSON string to an <see cref="EnterPlanModePostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static EnterPlanModePostToolUsePayload? DeserializeEnterPlanModePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.EnterPlanModePostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="MonitorPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static MonitorPostToolUsePayload? DeserializeMonitorPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.MonitorPostToolUsePayload);
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
        /// Deserializes a JSON string to an <see cref="OfferChromeSetupPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static OfferChromeSetupPostToolUsePayload? DeserializeOfferChromeSetupPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.OfferChromeSetupPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProjectsPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProjectsPostToolUsePayload? DeserializeProjectsPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProjectsPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProposeGoalPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProposeGoalPostToolUsePayload? DeserializeProposeGoalPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProposeGoalPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ProposeSkillsPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ProposeSkillsPostToolUsePayload? DeserializeProposeSkillsPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ProposeSkillsPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="PushNotificationPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static PushNotificationPostToolUsePayload? DeserializePushNotificationPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PushNotificationPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReadMcpResourceDirPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadMcpResourceDirPostToolUsePayload? DeserializeReadMcpResourceDirPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadMcpResourceDirPostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="ReadNotificationsPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReadNotificationsPostToolUsePayload? DeserializeReadNotificationsPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReadNotificationsPostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="RefreshMcpToolsPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static RefreshMcpToolsPostToolUsePayload? DeserializeRefreshMcpToolsPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.RefreshMcpToolsPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="RemoteTriggerPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static RemoteTriggerPostToolUsePayload? DeserializeRemoteTriggerPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.RemoteTriggerPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ReportFindingsPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ReportFindingsPostToolUsePayload? DeserializeReportFindingsPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ReportFindingsPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ScheduleWakeupPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ScheduleWakeupPostToolUsePayload? DeserializeScheduleWakeupPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ScheduleWakeupPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="SendFeedbackPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static SendFeedbackPostToolUsePayload? DeserializeSendFeedbackPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SendFeedbackPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="ShowOnboardingRolePickerPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static ShowOnboardingRolePickerPostToolUsePayload? DeserializeShowOnboardingRolePickerPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ShowOnboardingRolePickerPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskCreatePostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskCreatePostToolUsePayload? DeserializeTaskCreatePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskCreatePostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskGetPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskGetPostToolUsePayload? DeserializeTaskGetPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskGetPostToolUsePayload);
        }

        /// <summary>
        /// Deserializes a JSON string to a <see cref="TaskListPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskListPostToolUsePayload? DeserializeTaskListPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskListPostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="TaskUpdatePostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static TaskUpdatePostToolUsePayload? DeserializeTaskUpdatePostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TaskUpdatePostToolUsePayload);
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
        /// Deserializes a JSON string to a <see cref="WorkflowPostToolUsePayload"/> with a strongly-typed tool input and response.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized payload, or null if deserialization fails.</returns>
        public static WorkflowPostToolUsePayload? DeserializeWorkflowPostToolUse(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.WorkflowPostToolUsePayload);
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

        /// <summary>
        /// Serializes a <see cref="ConfigChangeHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeConfigChangeOutput(ConfigChangeHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ConfigChangeHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="CwdChangedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeCwdChangedOutput(CwdChangedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.CwdChangedHookOutput);
        }

        /// <summary>
        /// Serializes an <see cref="ElicitationHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeElicitationOutput(ElicitationHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ElicitationHookOutput);
        }

        /// <summary>
        /// Serializes an <see cref="ElicitationResultHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeElicitationResultOutput(ElicitationResultHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ElicitationResultHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="FileChangedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeFileChangedOutput(FileChangedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.FileChangedHookOutput);
        }

        /// <summary>
        /// Serializes an <see cref="InstructionsLoadedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeInstructionsLoadedOutput(InstructionsLoadedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.InstructionsLoadedHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PermissionDeniedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePermissionDeniedOutput(PermissionDeniedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PermissionDeniedHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PostCompactHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePostCompactOutput(PostCompactHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PostCompactHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PostToolUseFailureHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePostToolUseFailureOutput(PostToolUseFailureHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PostToolUseFailureHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="StopFailureHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeStopFailureOutput(StopFailureHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.StopFailureHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="SubagentStartHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSubagentStartOutput(SubagentStartHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SubagentStartHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="TaskCompletedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskCompletedOutput(TaskCompletedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskCompletedHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="TaskCreatedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskCreatedOutput(TaskCreatedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskCreatedHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="TeammateIdleHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTeammateIdleOutput(TeammateIdleHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TeammateIdleHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="WorktreeCreateHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWorktreeCreateOutput(WorktreeCreateHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WorktreeCreateHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="WorktreeRemoveHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWorktreeRemoveOutput(WorktreeRemoveHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WorktreeRemoveHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="DirectoryAddedHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeDirectoryAddedOutput(DirectoryAddedHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.DirectoryAddedHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="MessageDisplayHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeMessageDisplayOutput(MessageDisplayHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.MessageDisplayHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PostModelSwitchHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePostModelSwitchOutput(PostModelSwitchHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PostModelSwitchHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PostToolBatchHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePostToolBatchOutput(PostToolBatchHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PostToolBatchHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreModelSwitchHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePreModelSwitchOutput(PreModelSwitchHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PreModelSwitchHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="SetupHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSetupOutput(SetupHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SetupHookOutput);
        }

        /// <summary>
        /// Serializes an <see cref="UserPromptExpansionHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeUserPromptExpansionOutput(UserPromptExpansionHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.UserPromptExpansionHookOutput);
        }

        /// <summary>
        /// Serializes an <see cref="AsyncHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeAsyncOutput(AsyncHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.AsyncHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ArtifactToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeArtifactPreToolUseOutput(PreToolUseHookOutput<ArtifactToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ArtifactPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ClaudeDesignToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeClaudeDesignPreToolUseOutput(PreToolUseHookOutput<ClaudeDesignToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ClaudeDesignPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="CronCreateToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeCronCreatePreToolUseOutput(PreToolUseHookOutput<CronCreateToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.CronCreatePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="CronDeleteToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeCronDeletePreToolUseOutput(PreToolUseHookOutput<CronDeleteToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.CronDeletePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="CronListToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeCronListPreToolUseOutput(PreToolUseHookOutput<CronListToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.CronListPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="EnterPlanModeToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeEnterPlanModePreToolUseOutput(PreToolUseHookOutput<EnterPlanModeToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.EnterPlanModePreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="MonitorToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeMonitorPreToolUseOutput(PreToolUseHookOutput<MonitorToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.MonitorPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="OfferChromeSetupToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeOfferChromeSetupPreToolUseOutput(PreToolUseHookOutput<OfferChromeSetupToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.OfferChromeSetupPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ProjectsToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeProjectsPreToolUseOutput(PreToolUseHookOutput<ProjectsToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ProjectsPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ProposeGoalToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeProposeGoalPreToolUseOutput(PreToolUseHookOutput<ProposeGoalToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ProposeGoalPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ProposeSkillsToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeProposeSkillsPreToolUseOutput(PreToolUseHookOutput<ProposeSkillsToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ProposeSkillsPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="PushNotificationToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePushNotificationPreToolUseOutput(PreToolUseHookOutput<PushNotificationToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.PushNotificationPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ReadMcpResourceDirToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeReadMcpResourceDirPreToolUseOutput(PreToolUseHookOutput<ReadMcpResourceDirToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ReadMcpResourceDirPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ReadNotificationsToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeReadNotificationsPreToolUseOutput(PreToolUseHookOutput<ReadNotificationsToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ReadNotificationsPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="RefreshMcpToolsToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeRefreshMcpToolsPreToolUseOutput(PreToolUseHookOutput<RefreshMcpToolsToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.RefreshMcpToolsPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="RemoteTriggerToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeRemoteTriggerPreToolUseOutput(PreToolUseHookOutput<RemoteTriggerToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.RemoteTriggerPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ReportFindingsToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeReportFindingsPreToolUseOutput(PreToolUseHookOutput<ReportFindingsToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ReportFindingsPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ScheduleWakeupToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeScheduleWakeupPreToolUseOutput(PreToolUseHookOutput<ScheduleWakeupToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ScheduleWakeupPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="SendFeedbackToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSendFeedbackPreToolUseOutput(PreToolUseHookOutput<SendFeedbackToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SendFeedbackPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="ShowOnboardingRolePickerToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeShowOnboardingRolePickerPreToolUseOutput(PreToolUseHookOutput<ShowOnboardingRolePickerToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.ShowOnboardingRolePickerPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskCreateToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskCreatePreToolUseOutput(PreToolUseHookOutput<TaskCreateToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskCreatePreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskGetToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskGetPreToolUseOutput(PreToolUseHookOutput<TaskGetToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskGetPreToolUseHookOutput);
        }

        /// <summary>
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskListToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskListPreToolUseOutput(PreToolUseHookOutput<TaskListToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskListPreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="TaskUpdateToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeTaskUpdatePreToolUseOutput(PreToolUseHookOutput<TaskUpdateToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.TaskUpdatePreToolUseHookOutput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with strongly-typed <see cref="WorkflowToolInput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeWorkflowPreToolUseOutput(PreToolUseHookOutput<WorkflowToolInput> output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.WorkflowPreToolUseHookOutput);
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
