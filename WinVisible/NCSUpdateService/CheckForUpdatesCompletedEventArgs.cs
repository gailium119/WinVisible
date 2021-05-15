namespace WinVisible.NCSUpdateService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "4.0.30319.33440"), DebuggerStepThrough]
    public class CheckForUpdatesCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal CheckForUpdatesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public VersionInfo Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (VersionInfo) this.results[0];
            }
        }
    }
}

