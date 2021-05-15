namespace WinVisible.NCSUpdateService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;
    using WinVisible.Properties;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "4.0.30319.33440"), DebuggerStepThrough, WebServiceBinding(Name="UpdateSoap", Namespace="http://www.neptunecentury.com/")]
    public class Update : SoapHttpClientProtocol
    {
        private SendOrPostCallback CheckForUpdatesOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;
        private CheckForUpdatesCompletedEventHandler CheckForUpdatesCompleted;

        public event CheckForUpdatesCompletedEventHandler CheckForUpdatesCompletedBase
        {
            add
            {
                CheckForUpdatesCompletedEventHandler checkForUpdatesCompleted = this.CheckForUpdatesCompleted;
                while (true)
                {
                    CheckForUpdatesCompletedEventHandler comparand = checkForUpdatesCompleted;
                    CheckForUpdatesCompletedEventHandler handler3 = comparand + value;
                    checkForUpdatesCompleted = Interlocked.CompareExchange<CheckForUpdatesCompletedEventHandler>(ref this.CheckForUpdatesCompleted, handler3, comparand);
                    if (ReferenceEquals(checkForUpdatesCompleted, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                CheckForUpdatesCompletedEventHandler checkForUpdatesCompleted = this.CheckForUpdatesCompleted;
                while (true)
                {
                    CheckForUpdatesCompletedEventHandler comparand = checkForUpdatesCompleted;
                    CheckForUpdatesCompletedEventHandler handler3 = comparand - value;
                    checkForUpdatesCompleted = Interlocked.CompareExchange<CheckForUpdatesCompletedEventHandler>(ref this.CheckForUpdatesCompleted, handler3, comparand);
                    if (ReferenceEquals(checkForUpdatesCompleted, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public Update()
        {
            this.Url = Settings.Default.WinVisible_NCSUpdateService_Update;
            if (!this.IsLocalFileSystemWebService(this.Url))
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
            else
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
        }

        public void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        [SoapDocumentMethod("http://www.neptunecentury.com/CheckForUpdates", RequestNamespace="http://www.neptunecentury.com/", ResponseNamespace="http://www.neptunecentury.com/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public VersionInfo CheckForUpdates(Guid appGuid, string appVersion)
        {
            object[] parameters = new object[] { appGuid, appVersion };
            return (VersionInfo) base.Invoke("CheckForUpdates", parameters)[0];
        }

        public void CheckForUpdatesAsync(Guid appGuid, string appVersion)
        {
            this.CheckForUpdatesAsync(appGuid, appVersion, null);
        }

        public void CheckForUpdatesAsync(Guid appGuid, string appVersion, object userState)
        {
            this.CheckForUpdatesOperationCompleted ??= new SendOrPostCallback(this.OnCheckForUpdatesOperationCompleted);
            object[] parameters = new object[] { appGuid, appVersion };
            base.InvokeAsync("CheckForUpdates", parameters, this.CheckForUpdatesOperationCompleted, userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        private void OnCheckForUpdatesOperationCompleted(object arg)
        {
            if (this.CheckForUpdatesCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.CheckForUpdatesCompleted(this, new CheckForUpdatesCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        public string Url
        {
            get => 
                base.Url;
            set
            {
                if (this.IsLocalFileSystemWebService(base.Url) && (!this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get => 
                base.UseDefaultCredentials;
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

