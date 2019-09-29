namespace TestRESTService.Helpers
{
    public struct PluginInfo
    {
        public string ProjectName { get; set; }

        /// <summary>
        /// Relative path relative to the root of solution.
        /// If the project is at the root of the assembly, then you don't need to change this value.
        /// </summary>
        public string RelativePath { get; set; }
    }
}