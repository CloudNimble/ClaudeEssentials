using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

namespace CloudNimble.ClaudeEssentials.Samples.HookProcessor.Handlers
{

    /// <summary>
    /// Handles PreToolUse hooks to auto-approve safe operations and block dangerous ones.
    /// </summary>
    public static class PreToolUseHandler
    {

        /// <summary>
        /// Tools that are always safe to auto-approve.
        /// </summary>
        private static readonly HashSet<string> SafeTools =
        [
            "Read",
            "Glob",
            "Grep",
            "Task"
        ];

        /// <summary>
        /// Dangerous patterns that should be blocked in Bash commands.
        /// </summary>
        private static readonly string[] DangerousPatterns =
        [
            "rm -rf /",
            "rm -rf ~",
            "format c:",
            ":(){:|:&};:",
            "dd if=/dev/zero",
            "mkfs.",
            "> /dev/sda"
        ];

        /// <summary>
        /// Processes a PreToolUse hook input and returns the appropriate output.
        /// </summary>
        /// <param name="input">The hook input from Claude Code.</param>
        /// <returns>The hook output indicating whether to allow, deny, or ask for the tool use.</returns>
        public static PreToolUseHookOutput<object> Handle(PreToolUseHookInput<object> input)
        {
            // Auto-approve safe read-only tools
            if (SafeTools.Contains(input.ToolName))
            {
                return new PreToolUseHookOutput<object>
                {
                    Continue = true,
                    HookSpecificOutput = new PreToolUseSpecificOutput<object>
                    {
                        PermissionDecision = PermissionDecision.Allow,
                        PermissionDecisionReason = $"Tool '{input.ToolName}' is pre-approved as a safe read-only operation."
                    }
                };
            }

            // Check Bash commands for dangerous patterns
            if (input.ToolName == "Bash" && input.ToolInput is not null)
            {
                var toolInputJson = input.ToolInput.ToString() ?? string.Empty;

                foreach (var pattern in DangerousPatterns)
                {
                    if (toolInputJson.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        return new PreToolUseHookOutput<object>
                        {
                            Continue = true,
                            HookSpecificOutput = new PreToolUseSpecificOutput<object>
                            {
                                PermissionDecision = PermissionDecision.Deny,
                                PermissionDecisionReason = $"Command blocked: Contains dangerous pattern '{pattern}'."
                            }
                        };
                    }
                }
            }

            // For Write/Edit operations, add a warning but allow user to decide
            if (input.ToolName is "Write" or "Edit")
            {
                return new PreToolUseHookOutput<object>
                {
                    Continue = true,
                    SystemMessage = "Note: This operation will modify files. Review the changes carefully.",
                    HookSpecificOutput = new PreToolUseSpecificOutput<object>
                    {
                        PermissionDecision = PermissionDecision.Ask,
                        PermissionDecisionReason = "File modification requires user approval."
                    }
                };
            }

            // Default: let the normal permission flow handle it
            return new PreToolUseHookOutput<object>
            {
                Continue = true
            };
        }

    }

}
