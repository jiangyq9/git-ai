using System;
using System.IO;
using System.Runtime.InteropServices;

namespace GitAiVS.Services
{
    /// <summary>
    /// Finds the git repository root for a given file path by walking up
    /// the directory tree looking for a .git directory.
    /// </summary>
    public static class GitRepoResolver
    {
        private static readonly StringComparison PathComparison =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

        public static string? FindRepoRoot(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);

            while (dir != null)
            {
                if (Directory.Exists(Path.Combine(dir, ".git")) || File.Exists(Path.Combine(dir, ".git")))
                    return dir;

                dir = Path.GetDirectoryName(dir);
            }

            return null;
        }

        /// <summary>
        /// Convert an absolute file path to a path relative to the workspace root.
        /// Uses case-insensitive comparison on Windows to handle path casing mismatches.
        /// </summary>
        internal static string ToRelativePath(string absolutePath, string workspaceRoot)
        {
            if (absolutePath.StartsWith(workspaceRoot, PathComparison))
            {
                var relative = absolutePath.Substring(workspaceRoot.Length);
                return relative.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }

            return absolutePath;
        }
    }
}
