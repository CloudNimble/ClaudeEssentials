using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a ConfigChange hook.
    /// Used to optionally block configuration changes (except policy_settings).
    /// </summary>
    public class ConfigChangeHookOutput : HookOutputBase
    {

    }

}
