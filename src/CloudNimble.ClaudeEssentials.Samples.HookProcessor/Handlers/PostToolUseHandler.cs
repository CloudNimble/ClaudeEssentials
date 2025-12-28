using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

namespace CloudNimble.ClaudeEssentials.Samples.HookProcessor.Handlers
{

    /// <summary>
    /// Handles PostToolUse hooks to add context or take action after tool execution.
    /// </summary>
    public static class PostToolUseHandler
    {

        /// <summary>
        /// Processes a PostToolUse hook input and returns the appropriate output.
        /// </summary>
        /// <param name="input">The hook input from Claude Code.</param>
        /// <returns>The hook output with optional additional context.</returns>
        public static PostToolUseHookOutput Handle(PostToolUseHookInput<object, object> input)
        {
            // Add context after Bash commands complete
            if (input.ToolName == "Bash")
            {
                return HandleBashResult(input);
            }

            // Add context after file writes
            if (input.ToolName == "Write")
            {
                return HandleWriteResult(input);
            }

            // Add context after edits
            if (input.ToolName == "Edit")
            {
                return HandleEditResult(input);
            }

            // Default: continue without additional context
            return new PostToolUseHookOutput
            {
                Continue = true
            };
        }

        /// <summary>
        /// Handles Bash command results, checking for common error patterns.
        /// </summary>
        private static PostToolUseHookOutput HandleBashResult(PostToolUseHookInput<object, object> input)
        {
            var responseJson = input.ToolResponse?.ToString() ?? string.Empty;

            // Check for test failures
            if (responseJson.Contains("FAILED", StringComparison.OrdinalIgnoreCase) ||
                responseJson.Contains("error", StringComparison.OrdinalIgnoreCase))
            {
                return new PostToolUseHookOutput
                {
                    Continue = true,
                    HookSpecificOutput = new PostToolUseSpecificOutput
                    {
                        AdditionalContext = "⚠️ The command output contains errors or failures. Please review the output carefully before proceeding."
                    }
                };
            }

            // Check for successful test runs
            if (responseJson.Contains("passed", StringComparison.OrdinalIgnoreCase) &&
                responseJson.Contains("Test run", StringComparison.OrdinalIgnoreCase))
            {
                return new PostToolUseHookOutput
                {
                    Continue = true,
                    HookSpecificOutput = new PostToolUseSpecificOutput
                    {
                        AdditionalContext = "✅ Tests completed successfully."
                    }
                };
            }

            return new PostToolUseHookOutput
            {
                Continue = true
            };
        }

        /// <summary>
        /// Handles Write tool results.
        /// </summary>
        private static PostToolUseHookOutput HandleWriteResult(PostToolUseHookInput<object, object> input)
        {
            var inputJson = input.ToolInput?.ToString() ?? string.Empty;

            // Check if a test file was created
            if (inputJson.Contains("Test", StringComparison.OrdinalIgnoreCase) &&
                inputJson.Contains(".cs", StringComparison.OrdinalIgnoreCase))
            {
                return new PostToolUseHookOutput
                {
                    Continue = true,
                    HookSpecificOutput = new PostToolUseSpecificOutput
                    {
                        AdditionalContext = "📝 Test file created. Remember to run tests to verify the new tests pass."
                    }
                };
            }

            return new PostToolUseHookOutput
            {
                Continue = true
            };
        }

        /// <summary>
        /// Handles Edit tool results.
        /// </summary>
        private static PostToolUseHookOutput HandleEditResult(PostToolUseHookInput<object, object> input)
        {
            return new PostToolUseHookOutput
            {
                Continue = true,
                HookSpecificOutput = new PostToolUseSpecificOutput
                {
                    AdditionalContext = "📝 File modified. Consider running relevant tests to verify the change."
                }
            };
        }

    }

}
