namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a TaskCompleted hook.
    /// Use exit code 2 to block task completion with stderr feedback,
    /// or set <see cref="HookOutputBase.Continue"/> to false with a <see cref="HookOutputBase.StopReason"/>
    /// to stop the teammate entirely.
    /// </summary>
    public class TaskCompletedHookOutput : HookOutputBase
    {

    }

}
