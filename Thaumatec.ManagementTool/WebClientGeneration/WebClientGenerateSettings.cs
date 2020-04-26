using System.IO;

namespace Thaumatec.ManagementTool.WebClientGeneration
{
    internal class WebClientGenerateSettings
    {
        public string NswagConfigPath { get; }
        public string NswagConfigDir => Path.GetDirectoryName(NswagConfigPath);

        public WebClientGenerateSettings(Solution solution)
        {
            var solutionRoot = solution.SolutionRoot;
            NswagConfigPath = Path.Combine(solutionRoot, "Thaumatec.Web", "Frontend", "src", "api-clients", "api_generation_config.nswag");
        }
    }
}
