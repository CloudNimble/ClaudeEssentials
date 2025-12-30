using System.Text;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

namespace CloudNimble.ClaudeEssentials.Samples.HookProcessor.Handlers
{

    /// <summary>
    /// Handles SessionStart hooks to inject project context into new sessions.
    /// </summary>
    public static class SessionStartHandler
    {

        /// <summary>
        /// Processes a SessionStart hook input and returns context for the session.
        /// </summary>
        /// <param name="input">The hook input from Claude Code.</param>
        /// <returns>The hook output with project context.</returns>
        public static SessionStartHookOutput Handle(SessionStartHookInput input)
        {
            var context = new StringBuilder();

            // Detect project type and add relevant context
            var projectInfo = DetectProjectType(input.CurrentWorkingDirectory);

            if (projectInfo is not null)
            {
                context.AppendLine($"## Project Information");
                context.AppendLine($"- **Type**: {projectInfo.Type}");
                context.AppendLine($"- **Framework**: {projectInfo.Framework}");

                if (projectInfo.HasTests)
                {
                    context.AppendLine($"- **Test Framework**: MSTest/xUnit detected");
                }

                context.AppendLine();
            }

            // Check for common configuration files and add notes
            var configNotes = GetConfigurationNotes(input.CurrentWorkingDirectory);

            if (!string.IsNullOrEmpty(configNotes))
            {
                context.AppendLine("## Configuration Notes");
                context.AppendLine(configNotes);
                context.AppendLine();
            }

            // Add any team-specific guidelines from .claude directory
            var teamGuidelines = GetTeamGuidelines(input.CurrentWorkingDirectory);

            if (!string.IsNullOrEmpty(teamGuidelines))
            {
                context.AppendLine("## Team Guidelines");
                context.AppendLine(teamGuidelines);
            }

            var additionalContext = context.ToString().Trim();

            if (string.IsNullOrEmpty(additionalContext))
            {
                return new SessionStartHookOutput
                {
                    Continue = true
                };
            }

            return new SessionStartHookOutput
            {
                Continue = true,
                HookSpecificOutput = new SessionStartSpecificOutput
                {
                    AdditionalContext = additionalContext
                }
            };
        }

        /// <summary>
        /// Detects the project type based on files in the directory.
        /// </summary>
        private static ProjectInfo? DetectProjectType(string workingDirectory)
        {
            if (string.IsNullOrEmpty(workingDirectory) || !Directory.Exists(workingDirectory))
            {
                return null;
            }

            try
            {
                // Check for .NET projects
                var csprojFiles = Directory.GetFiles(workingDirectory, "*.csproj", SearchOption.AllDirectories);

                if (csprojFiles.Length > 0)
                {
                    var framework = DetectDotNetFramework(csprojFiles[0]);
                    var hasTests = csprojFiles.Any(f => f.Contains("Test", StringComparison.OrdinalIgnoreCase));

                    return new ProjectInfo
                    {
                        Type = ".NET",
                        Framework = framework,
                        HasTests = hasTests
                    };
                }

                // Check for Node.js projects
                var packageJson = Path.Combine(workingDirectory, "package.json");

                if (File.Exists(packageJson))
                {
                    return new ProjectInfo
                    {
                        Type = "Node.js",
                        Framework = DetectNodeFramework(packageJson),
                        HasTests = File.ReadAllText(packageJson).Contains("\"test\"")
                    };
                }

                // Check for Python projects
                var pyprojectToml = Path.Combine(workingDirectory, "pyproject.toml");
                var requirementsTxt = Path.Combine(workingDirectory, "requirements.txt");

                if (File.Exists(pyprojectToml) || File.Exists(requirementsTxt))
                {
                    return new ProjectInfo
                    {
                        Type = "Python",
                        Framework = File.Exists(pyprojectToml) ? "Modern (pyproject.toml)" : "Classic (requirements.txt)",
                        HasTests = Directory.Exists(Path.Combine(workingDirectory, "tests"))
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Detects the .NET framework version from a csproj file.
        /// </summary>
        private static string DetectDotNetFramework(string csprojPath)
        {
            try
            {
                var content = File.ReadAllText(csprojPath);

                if (content.Contains("net10.0"))
                {
                    return ".NET 10";
                }

                if (content.Contains("net9.0"))
                {
                    return ".NET 9";
                }

                if (content.Contains("net8.0"))
                {
                    return ".NET 8";
                }

                if (content.Contains("netstandard"))
                {
                    return ".NET Standard";
                }

                return ".NET (version unknown)";
            }
            catch
            {
                return ".NET";
            }
        }

        /// <summary>
        /// Detects the Node.js framework from package.json.
        /// </summary>
        private static string DetectNodeFramework(string packageJsonPath)
        {
            try
            {
                var content = File.ReadAllText(packageJsonPath);

                if (content.Contains("\"next\""))
                {
                    return "Next.js";
                }

                if (content.Contains("\"react\""))
                {
                    return "React";
                }

                if (content.Contains("\"vue\""))
                {
                    return "Vue.js";
                }

                if (content.Contains("\"express\""))
                {
                    return "Express";
                }

                return "Node.js";
            }
            catch
            {
                return "Node.js";
            }
        }

        /// <summary>
        /// Gets configuration notes based on files present.
        /// </summary>
        private static string GetConfigurationNotes(string workingDirectory)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                return string.Empty;
            }

            var notes = new StringBuilder();

            try
            {
                // Check for EditorConfig
                if (File.Exists(Path.Combine(workingDirectory, ".editorconfig")))
                {
                    notes.AppendLine("- EditorConfig present: Follow code style settings");
                }

                // Check for ESLint/Prettier
                if (File.Exists(Path.Combine(workingDirectory, ".eslintrc.json")) ||
                    File.Exists(Path.Combine(workingDirectory, ".eslintrc.js")))
                {
                    notes.AppendLine("- ESLint configured: Run linting before committing");
                }

                // Check for pre-commit hooks
                if (Directory.Exists(Path.Combine(workingDirectory, ".husky")))
                {
                    notes.AppendLine("- Husky hooks installed: Pre-commit checks will run automatically");
                }

                // Check for CI/CD
                if (Directory.Exists(Path.Combine(workingDirectory, ".github", "workflows")))
                {
                    notes.AppendLine("- GitHub Actions configured: Ensure CI passes before merging");
                }
            }
            catch
            {
                // Ignore errors
            }

            return notes.ToString().TrimEnd();
        }

        /// <summary>
        /// Gets team guidelines from .claude directory.
        /// </summary>
        private static string GetTeamGuidelines(string workingDirectory)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                return string.Empty;
            }

            try
            {
                var claudeMdPath = Path.Combine(workingDirectory, ".claude", "CLAUDE.md");

                if (File.Exists(claudeMdPath))
                {
                    // Just note that guidelines exist; Claude will read them separately
                    return "Team-specific CLAUDE.md found in .claude directory.";
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Holds detected project information.
        /// </summary>
        private sealed class ProjectInfo
        {

            public required string Type { get; init; }

            public required string Framework { get; init; }

            public bool HasTests { get; init; }

        }

    }

}
