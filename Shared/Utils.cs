using System;

namespace BlazorApp.Shared
{
    public class Utils
    {
        public static string GenerateUniqueClassName(string className = "wrapper") => $"{className}_{Guid.NewGuid().ToString()[..8]}";
    }
}