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
                Mutex.OpenExisting("NCSWINVISIBLE").Close();
                MessageBox.Show("Only one instance of NCS WinVisible can run at a time.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            catch (Exception)
            {
                mutex = new Mutex(true, "NCSWINVISIBLE");
            }
            Application.Run(new frmMain());
        }
    }
}

