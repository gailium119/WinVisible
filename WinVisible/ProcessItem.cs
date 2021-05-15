namespace WinVisible
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ProcessItem
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int SW_SHOWNA = 8;
        private static List<IntPtr> _handles;
        private System.Diagnostics.Process _process;
        private List<IntPtr> _windows;
        private bool _isVisible;

        public ProcessItem(System.Diagnostics.Process process)
        {
            if (process == null)
            {
                throw new Exception("Process cannot be null.");
            }
            this._process = process;
            this._windows = new List<IntPtr>();
            GetWindowHandles(this.Process.Id, this._windows);
            this._isVisible = this._windows.Count > 0;
        }

        public void Dispose()
        {
            this._process.Dispose();
        }

        [DllImport("user32.dll")]
        private static extern int EnumChildWindows(IntPtr hWnd, WindowEnumDelegate pEnumWindowCallback, int iLParam);
        private static void GetWindowHandles(int procId, List<IntPtr> handles)
        {
            _handles = handles;
            WindowEnumDelegate pEnumWindowCallback = new WindowEnumDelegate(ProcessItem.WindowEnumProc);
            EnumChildWindows(IntPtr.Zero, pEnumWindowCallback, procId);
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static unsafe bool WindowEnumProc(IntPtr hwnd, int lParam)
        {
            if (!_handles.Contains(hwnd))
            {
                int num = 0;
                GetWindowThreadProcessId(hwnd, (IntPtr) (&num));
                if ((num == lParam) && IsWindowVisible(hwnd))
                {
                    _handles.Add(hwnd);
                }
            }
            return true;
        }

        public System.Diagnostics.Process Process
        {
            get => 
                this._process;
            set => 
                this._process = value;
        }

        public bool Visible
        {
            get => 
                this._isVisible;
            set
            {
                if (this._process.HasExited)
                {
                    throw new Exception("The process has exited.");
                }
                if (!value && this._isVisible)
                {
                    GetWindowHandles(this.Process.Id, this._windows);
                    foreach (IntPtr ptr in this._windows)
                    {
                        ShowWindow(ptr, 0);
                    }
                }
                else if (value && !this._isVisible)
                {
                    foreach (IntPtr ptr2 in this._windows)
                    {
                        ShowWindow(ptr2, 8);
                    }
                }
                this._isVisible = value;
            }
        }

        public delegate bool WindowEnumDelegate(IntPtr hwnd, int lParam);
    }
}

