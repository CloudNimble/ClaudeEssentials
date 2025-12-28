using System.Diagnostics;
using CloudNimble.ClaudeEssentials.Hooks.Enums;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

namespace CloudNimble.ClaudeEssentials.Samples.HookProcessor.Handlers
{

    /// <summary>
    /// Handles Stop hooks to enforce policies before Claude ends a session.
    /// </summary>
    public static class StopHandler
    {

        /// <summary>
        /// Processes a Stop hook input and returns the appropriate output.
        /// </summary>
        /// <param name="input">The hook input from Claude Code.</param>
        /// <returns>The hook output indicating whether to allow or block the stop.</returns>
        /// <remarks>
        /// This handler demonstrates checking that tests pass before allowing Claude to stop.
        /// In a real scenario, you might check for uncommitted changes, pending builds, etc.
        /// </remarks>
        public static StopHookOutput Handle(StopHookInput input)
        {
            // Only enforce checks if stop hook is active
            if (!input.StopHookActive)
            {
                return new StopHookOutput
                {
                    Continue = true
                };
            }

            // Check if we're in a project directory with tests
            var testsExist = CheckForTestProject(input.CurrentWorkingDirectory);

            if (testsExist)
            {
                // Run tests and check if they pass
                var testsPass = RunTestsAndCheckResult(input.CurrentWorkingDirectory);

                if (!testsPass)
                {
                    return new StopHookOutput
                    {
                        Continue = true,
                        Decision = HookDecision.Block,
                        Reason = "Tests are failing. Please fix the failing tests before stopping."
                    };
                }
            }

            // Check for uncommitted git changes
            var hasUncommittedChanges = CheckForUncommittedChanges(input.CurrentWorkingDirectory);

            if (hasUncommittedChanges)
            {
                return new StopHookOutput
                {
                    Continue = true,
                    SystemMessage = "⚠️ You have uncommitted changes. Consider committing your work before ending the session."
                };
            }

            return new StopHookOutput
            {
                Continue = true
            };
        }

        /// <summary>
        /// Checks if the directory contains a test project.
        /// </summary>
        private static bool CheckForTestProject(string workingDirectory)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                return false;
            }

            try
            {
                // Look for test project indicators
                var csprojFiles = Directory.GetFiles(workingDirectory, "*.Tests.csproj", SearchOption.AllDirectories);
                return csprojFiles.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Runs tests and returns whether they pass.
        /// </summary>
        private static bool RunTestsAndCheckResult(string workingDirectory)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "test --no-build --verbosity quiet",
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(startInfo);

                if (process is null)
                {
                    return true; // Can't verify, assume OK
                }

                process.WaitForExit(60000);

                return process.ExitCode == 0;
            }
            catch
            {
                // If we can't run tests, don't block
                return true;
            }
        }

        /// <summary>
        /// Checks for uncommitted git changes.
        /// </summary>
        private static bool CheckForUncommittedChanges(string workingDirectory)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                return false;
            }

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "status --porcelain",
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(startInfo);

                if (process is null)
                {
                    return false;
                }

                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit(5000);

                return !string.IsNullOrWhiteSpace(output);
            }
            catch
            {
                return false;
            }
        }

    }

}
