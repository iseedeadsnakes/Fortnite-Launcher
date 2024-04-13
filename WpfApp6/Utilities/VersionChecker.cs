using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WpfApp5.Utilities
{
    internal class VersionChecker
    {
        private static List<int> Search(byte[] src, byte[] pattern)
        {
            List<int> indices = new List<int>();

            int srcLength = src.Length;
            int patternLength = pattern.Length;
            int maxSearchIndex = srcLength - patternLength;

            for (int i = 0; i <= maxSearchIndex; i++)
            {
                if (src[i] != pattern[0])
                    continue;

                bool found = true;
                for (int j = 1; j < patternLength; j++)
                {
                    if (src[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    indices.Add(i);
                }
            }

            return indices;
        }

        public async static Task<string> GetBuildVersion(string exePath)
        {
            string targetFileName = "FortniteClient-Win64-Shipping.exe";

            string targetFilePath = Path.Combine(exePath, "FortniteGame", "Binaries", "Win64", targetFileName);

            if (!File.Exists(targetFilePath))
            {
                Logs.Log($"File '{targetFileName}' not found at '{targetFilePath}'.");
                return "ERROR: File not found";
            }

            try
            {
                Logs.Log($"Trying To Get Build Version for file '{targetFileName}'.");

                string result = "";
                byte[] pattern = Encoding.Unicode.GetBytes("++Fortnite+Release-");

                using (BinaryReader binaryReader = new BinaryReader(new FileStream(targetFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    long fileSize = binaryReader.BaseStream.Length;
                    long position = 0;
                    int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];

                    while (position < fileSize)
                    {
                        int bytesRead = await binaryReader.BaseStream.ReadAsync(buffer, 0, bufferSize);
                        if (bytesRead == 0)
                            break;

                        List<int> indices = Search(buffer, pattern);
                        foreach (int num in indices)
                        {
                            string chunkText = Encoding.Unicode.GetString(buffer, num, bytesRead - num);

                            Match match = Regex.Match(chunkText, "\\+\\+Fortnite\\+Release-((\\d{1,2})\\.(\\d{1,2})|Live|Next|Cert)[-CL]*(\\d*)", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                result = match.Value;
                                Logs.Log($"Build version found: {result}");
                                goto FoundBuildVersion;
                            }
                        }

                        position += bytesRead;
                        if (position >= fileSize)
                            break;

                        binaryReader.BaseStream.Position -= pattern.Length - 1;
                    }
                }

FoundBuildVersion:
                Logs.Log($"Final result: {result}");

                if (result.Contains("-CL"))
                {
                    result = result.Substring(0, result.LastIndexOf("-CL"));
                }
                Logs.Log($"Modified result: {result}");
                result = result.Remove(0, 19);

                return result;
            }
            catch (Exception ex)
            {
                Logs.Log($"Error while getting build version: {ex.Message}");
                Logs.Log($"Stack Trace: {ex.StackTrace}");
                return "ERROR";
            }
        }

    }
}
