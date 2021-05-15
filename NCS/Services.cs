namespace NCS
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    internal class Services
    {
        public static void NavigateToUrl(string url)
        {
            new Process { StartInfo = { 
                UseShellExecute = true,
                FileName = url
            } }.Start();
        }

        public static void SendDonation()
        {
            MessageBox.Show("You will be taken to the PayPal website where you can make a secure donation. If you feel this software is worth something to you, please make a donation. \nThank you!", "Donation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            NavigateToUrl("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5547784");
        }

        public static void VisitWebsite()
        {
            NavigateToUrl("http://www.neptunecentury.com");
        }
    }
}

