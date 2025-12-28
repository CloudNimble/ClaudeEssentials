using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Provides static helper methods for serializing and deserializing Claude Code hook types.
    /// All methods use the AOT-compatible <see cref="ClaudeHooksJsonContext"/> for serialization.
    /// </summary>
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
        /// Deserializes a JSON string to a <see cref="PreToolUseHookInput{TToolInput}"/> with a specific tool input type.
        /// </summary>
        /// <typeparam name="TToolInput">The type of the tool input.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="typeInfo">The JSON type info for the specific input type.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PreToolUseHookInput<TToolInput>? DeserializePreToolUseInput<TToolInput>(
            string json,
            JsonTypeInfo<PreToolUseHookInput<TToolInput>> typeInfo)
            where TToolInput : class
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
        /// Deserializes a JSON string to a <see cref="PostToolUseHookInput{TToolInput, TToolResponse}"/> with specific types.
        /// </summary>
        /// <typeparam name="TToolInput">The type of the tool input.</typeparam>
        /// <typeparam name="TToolResponse">The type of the tool response.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="typeInfo">The JSON type info for the specific input type.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PostToolUseHookInput<TToolInput, TToolResponse>? DeserializePostToolUseInput<TToolInput, TToolResponse>(
            string json,
            JsonTypeInfo<PostToolUseHookInput<TToolInput, TToolResponse>> typeInfo)
            where TToolInput : class
            where TToolResponse : class
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
        /// Deserializes a JSON string to a <see cref="UserPromptSubmitHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static UserPromptSubmitHookInput? DeserializeUserPromptSubmitInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.UserPromptSubmitHookInput);
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
        /// Deserializes a JSON string to a <see cref="PreCompactHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static PreCompactHookInput? DeserializePreCompactInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PreCompactHookInput);
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
        /// Deserializes a JSON string to a <see cref="SessionEndHookInput"/>.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized hook input, or null if deserialization fails.</returns>
        public static SessionEndHookInput? DeserializeSessionEndInput(string json)
        {
            return JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.SessionEndHookInput);
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
        /// Serializes a <see cref="PreToolUseHookOutput{TToolInput}"/> with a specific tool input type to JSON.
        /// </summary>
        /// <typeparam name="TToolInput">The type of the tool input.</typeparam>
        /// <param name="output">The hook output to serialize.</param>
        /// <param name="typeInfo">The JSON type info for the specific output type.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializePreToolUseOutput<TToolInput>(
            PreToolUseHookOutput<TToolInput> output,
            JsonTypeInfo<PreToolUseHookOutput<TToolInput>> typeInfo)
            where TToolInput : class
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
        /// Serializes a <see cref="UserPromptSubmitHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeUserPromptSubmitOutput(UserPromptSubmitHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.UserPromptSubmitHookOutput);
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
        /// Serializes a <see cref="SessionStartHookOutput"/> to JSON.
        /// </summary>
        /// <param name="output">The hook output to serialize.</param>
        /// <returns>The JSON string representation.</returns>
        public static string SerializeSessionStartOutput(SessionStartHookOutput output)
        {
            return JsonSerializer.Serialize(output, ClaudeHooksJsonContext.Default.SessionStartHookOutput);
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

        #endregion

    }

}
