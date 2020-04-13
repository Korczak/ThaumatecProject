using System.IO;

namespace Thaumatec.ManagementTool
{
    internal class Solution
    {
        public string SolutionRoot { get; }
        public bool Found => !string.IsNullOrEmpty(SolutionRoot);

        public Solution()
        {
            SolutionRoot = GetSolutionRoot();
        }

        private static string GetSolutionRoot(int tryLimit = 5)
        {
            string path = Directory.GetCurrentDirectory();
            
            int iteration = 0;

            while(iteration < tryLimit)
            {
                string tryPath = Path.Combine(path, "Web");

                if(Directory.Exists(tryPath))
                {
                    return path;
                }
                else
                {
                    path = Path.Combine(path, "..");
                }

                ++iteration;
            }

            return null;
        }
    }
}
