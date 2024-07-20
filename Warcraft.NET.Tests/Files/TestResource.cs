using System.Net;

namespace Warcraft.NET.Tests.Files.ADT
{
    internal class TestResource
    {
        /// <summary>
        /// Thx to Wago for hosting the CASC API <3
        /// </summary>
        private const string BASE_URL_CASC_API = "https://wago.tools/api/casc";

        internal static byte[] Download(uint fileId, string version)
        {
#pragma warning disable SYSLIB0014
            using (var client = new WebClient())
                return client.DownloadData($"{BASE_URL_CASC_API}/{fileId}?download&version={version}");
#pragma warning restore SYSLIB0014
        }
    }
}
