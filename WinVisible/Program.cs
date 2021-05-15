namespace WinVisible
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex mutex = null;
            try
            {
                Mutex.OpenExisting("WINVISIBLE").Close();
                MessageBox.Show("WinVisible已在运行！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            catch (Exception)
            {
                mutex = new Mutex(true, "WINVISIBLE");
            }
            Application.Run(new frmMain());
        }
    }
}

