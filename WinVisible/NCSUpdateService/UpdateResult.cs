namespace WinVisible.NCSUpdateService
{
    using System;
    using System.CodeDom.Compiler;
    using System.Xml.Serialization;

    [Serializable, XmlType(Namespace="http://www.neptunecentury.com/"), GeneratedCode("System.Xml", "4.0.30319.33440")]
    public enum UpdateResult
    {
        CurrentVersion,
        NewerAvailable,
        DevRelease,
        Error
    }
}

