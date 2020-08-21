using System.Runtime.InteropServices;
using System.Text;

namespace Solektro.Helpers
{
    public static class WinPathHelper
    {
        private const int MAX_PATH = 255;


        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(string longPath, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder ShortPath, [MarshalAs(UnmanagedType.U4)] int bufferSize);


        public static string GetShortPath(string longPath)
        {
            var shortPath = new StringBuilder(MAX_PATH);
            GetShortPathName(longPath, shortPath, MAX_PATH);
            return shortPath.ToString();
        }
    }
}
