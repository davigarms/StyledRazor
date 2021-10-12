using System;

namespace BlazorApp.Shared
{
    public class Wrapper
    {
        public static string UseId() => $"wrapper_{Guid.NewGuid().ToString()[..8]}";
    }
}