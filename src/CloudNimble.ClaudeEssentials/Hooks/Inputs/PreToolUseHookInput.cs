namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PreToolUse hook.
    /// This hook runs before tool calls are executed and can block or modify them.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PreToolUseHookInput<TToolInput> : ToolHookInputBase<TToolInput>
        where TToolInput : class
    {

    }

}
