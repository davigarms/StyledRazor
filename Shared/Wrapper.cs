using System;

namespace BlazorApp.Shared
{
    public class Wrapper
    {
        public static string GenerateUniqueClassName(string className = "wrapper") => $"{className}_{Guid.NewGuid().ToString()[..8]}";
    }
}