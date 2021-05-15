namespace WinVisible.NCSUpdateService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, DesignerCategory("code"), XmlType(Namespace="http://www.neptunecentury.com/"), GeneratedCode("System.Xml", "4.0.30319.33440"), DebuggerStepThrough]
    public class VersionInfo
    {
        private string versionField;
        private string downloadUrlField;
        private UpdateResult statusField;

        public string Version
        {
            get => 
                this.versionField;
            set => 
                this.versionField = value;
        }

        public string DownloadUrl
        {
            get => 
                this.downloadUrlField;
            set => 
                this.downloadUrlField = value;
        }

        public UpdateResult Status
        {
            get => 
                this.statusField;
            set => 
                this.statusField = value;
        }
    }
}

