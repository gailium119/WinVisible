namespace NCS
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using WinVisible.NCSUpdateService;

    internal class UpdateService
    {
        private string _currentVersion;
        private AsyncStates _state = AsyncStates.Idle;
        private VersionInfo _versionInfo = null;
        private Stack<Exception> _exceptions;

        public UpdateService(string currentVersion)
        {
            this._currentVersion = currentVersion;
            this._exceptions = new Stack<Exception>();
        }

        public void CheckForUpdate()
        {
            try
            {
                this._state = AsyncStates.Working;
                VersionInfo info = new Update().CheckForUpdates(GetAppGuid(), this.CurrentVersion);
                this._versionInfo = info;
                this._state = AsyncStates.Finished;
            }
            catch (Exception exception)
            {
                this._versionInfo = null;
                this._state = AsyncStates.Error;
                Exception item = new Exception("There was an error while trying to get version information.", exception);
                this._exceptions.Push(item);
            }
        }

        public void CheckForUpdateAsync()
        {
            Thread thread = new Thread(new ThreadStart(this.CheckForUpdate));
            this._state = AsyncStates.Working;
            thread.Start();
        }

        public static Guid GetAppGuid()
        {
            Guid empty = Guid.Empty;
            object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(GuidAttribute), true);
            if (customAttributes.Length > 0)
            {
                empty = new Guid(((GuidAttribute) customAttributes[0]).Value);
            }
            return empty;
        }

        public AsyncStates AsyncState =>
            this._state;

        public VersionInfo NewVersionInfo =>
            this._versionInfo;

        public string CurrentVersion
        {
            get => 
                this._currentVersion;
            set => 
                this._currentVersion = value;
        }

        public Stack<Exception> Exceptions =>
            this._exceptions;

        public enum AsyncStates
        {
            Working,
            Idle,
            Finished,
            Error
        }

        public enum UpdateStatus
        {
            NewerAvailable,
            Current,
            DevRelease,
            Error
        }
    }
}

