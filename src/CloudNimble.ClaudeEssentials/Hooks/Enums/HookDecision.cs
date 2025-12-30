namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a hook's decision to block or allow an operation.
    /// </summary>
    public enum HookDecision
    {

        /// <summary>
        /// Allow the operation to proceed normally.
        /// </summary>
        Allow,

        /// <summary>
        /// Block the operation from proceeding.
        /// </summary>
        Block

    }

}
