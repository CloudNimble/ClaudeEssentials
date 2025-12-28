using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Samples.HookProcessor.Handlers;

namespace CloudNimble.ClaudeEssentials.Samples.HookProcessor
{

    /// <summary>
    /// Sample Claude Code hook processor demonstrating common hook scenarios.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This processor handles multiple hook types and can be configured in your
    /// Claude Code settings.json like this:
    /// </para>
    /// <code>
    /// {
    ///   "hooks": {
    ///     "PreToolUse": [{
    ///       "command": "path/to/CloudNimble.ClaudeEssentials.Samples.HookProcessor PreToolUse"
    ///     }],
    ///     "PostToolUse": [{
    ///       "command": "path/to/CloudNimble.ClaudeEssentials.Samples.HookProcessor PostToolUse"
    ///     }],
    ///     "Stop": [{
    ///       "command": "path/to/CloudNimble.ClaudeEssentials.Samples.HookProcessor Stop"
    ///     }],
    ///     "SessionStart": [{
    ///       "command": "path/to/CloudNimble.ClaudeEssentials.Samples.HookProcessor SessionStart"
    ///     }]
    ///   }
    /// }
    /// </code>
    /// </remarks>
    internal class Program
    {

        /// <summary>
        /// Entry point for the hook processor.
        /// </summary>
        /// <param name="args">Command line arguments. First argument should be the hook type.</param>
        /// <returns>Exit code: 0 for success, 1 for error.</returns>
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: HookProcessor <HookType>");
                Console.Error.WriteLine("Hook types: PreToolUse, PostToolUse, Stop, SessionStart");
                return 1;
            }

            var hookType = args[0];

            try
            {
                // Read JSON input from stdin
                var inputJson = Console.In.ReadToEnd();

                if (string.IsNullOrWhiteSpace(inputJson))
                {
                    Console.Error.WriteLine("No input received on stdin");
                    return 1;
                }

                // Process based on hook type
                var outputJson = hookType switch
                {
                    "PreToolUse" => ProcessPreToolUse(inputJson),
                    "PostToolUse" => ProcessPostToolUse(inputJson),
                    "Stop" => ProcessStop(inputJson),
                    "SessionStart" => ProcessSessionStart(inputJson),
                    _ => throw new ArgumentException($"Unknown hook type: {hookType}")
                };

                // Write output to stdout
                Console.WriteLine(outputJson);

                return 0;
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON parsing error: {ex.Message}");
                return 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {hookType} hook: {ex.Message}");
                return 1;
            }
        }

        /// <summary>
        /// Processes PreToolUse hooks.
        /// </summary>
        private static string ProcessPreToolUse(string inputJson)
        {
            var input = ClaudeHooksSerializer.DeserializePreToolUseInput(inputJson)
                ?? throw new InvalidOperationException("Failed to deserialize PreToolUse input");

            var output = PreToolUseHandler.Handle(input);

            return ClaudeHooksSerializer.SerializePreToolUseOutput(output);
        }

        /// <summary>
        /// Processes PostToolUse hooks.
        /// </summary>
        private static string ProcessPostToolUse(string inputJson)
        {
            var input = ClaudeHooksSerializer.DeserializePostToolUseInput(inputJson)
                ?? throw new InvalidOperationException("Failed to deserialize PostToolUse input");

            var output = PostToolUseHandler.Handle(input);

            return ClaudeHooksSerializer.SerializePostToolUseOutput(output);
        }

        /// <summary>
        /// Processes Stop hooks.
        /// </summary>
        private static string ProcessStop(string inputJson)
        {
            var input = ClaudeHooksSerializer.DeserializeStopInput(inputJson)
                ?? throw new InvalidOperationException("Failed to deserialize Stop input");

            var output = StopHandler.Handle(input);

            return ClaudeHooksSerializer.SerializeStopOutput(output);
        }

        /// <summary>
        /// Processes SessionStart hooks.
        /// </summary>
        private static string ProcessSessionStart(string inputJson)
        {
            var input = ClaudeHooksSerializer.DeserializeSessionStartInput(inputJson)
                ?? throw new InvalidOperationException("Failed to deserialize SessionStart input");

            var output = SessionStartHandler.Handle(input);

            return ClaudeHooksSerializer.SerializeSessionStartOutput(output);
        }

    }

}
