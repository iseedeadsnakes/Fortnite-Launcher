using DiscordRPC;

namespace WpfApp5.Utilities
{
    internal class RPC
    {
        public static DiscordRpcClient client;

        public static void StartRPC()
        {
            try
            {
                Logs.Log("Starting RPC..");

                client = new DiscordRpcClient(""); //change to you bots application id
                client.Logger = new DiscordRPC.Logging.ConsoleLogger() { Level = DiscordRPC.Logging.LogLevel.Warning };

                client.OnReady += (sender, e) =>
                {
                    Logs.Log($"RPC Ready for User: {e.User.Username}");
                };

                client.OnPresenceUpdate += (sender, e) =>
                {
                    Logs.Log($"RPC Presence Update");
                };

                client.Initialize();
                UpdateRPC("", false); 
            }
            catch (Exception ex)
            {
                Logs.Log("RPC Failed to Start: " + ex.Message);
            }
        }

        public static void UpdateRPC(string details, bool bTimeStamp = false)
        {
            try
            {
                if (client == null || !client.IsInitialized)
                {
                    StartRPC();
                }

            }
            catch (Exception ex)
            {
                Logs.Log("Failed to update RPC presence: " + ex.Message);
            }
        }

        public static void StopRPC()
        {
            try
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
            catch (Exception ex)
            {
                Logs.Log("Failed to stop RPC: " + ex.Message);
            }
        }
    }
}
