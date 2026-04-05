namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by an ElicitationResult hook.
    /// This hook runs when the user responds to an elicitation dialog.
    /// </summary>
    /// <remarks>
    /// The specific fields in the elicitation result depend on the MCP server
    /// that initiated the elicitation and the user's response.
    /// </remarks>
    public class ElicitationResultHookInput : HookInputBase
    {

    }

}
