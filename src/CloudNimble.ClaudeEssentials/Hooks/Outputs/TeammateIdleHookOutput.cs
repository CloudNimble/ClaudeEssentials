namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a TeammateIdle hook.
    /// Use exit code 2 to prevent the teammate from going idle,
    /// or set <see cref="HookOutputBase.Continue"/> to false with a <see cref="HookOutputBase.StopReason"/>
    /// to stop the teammate entirely.
    /// </summary>
    public class TeammateIdleHookOutput : HookOutputBase
    {

    }

}
