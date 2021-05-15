namespace WinVisible.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0"), CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) Synchronized(new Settings()));

        public static Settings Default =>
            defaultInstance;
     
        [ApplicationScopedSetting, DefaultSettingValue("0"), DebuggerNonUserCode]
        public int AppCloseAction
        {
            get => 
                (int) this["AppCloseAction"];
            set => 
                this["AppCloseAction"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool WarnOnNonRecommended
        {
            get => 
                (bool) this["WarnOnNonRecommended"];
            set => 
                this["WarnOnNonRecommended"] = value;
        }

        [DefaultSettingValue("Alt+Z"), ApplicationScopedSetting, DebuggerNonUserCode]
        public Keys ShowHotkey
        {
            get => 
                (Keys) this["ShowHotkey"];
            set => 
                this["ShowHotkey"] = value;
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("Alt+Shift+H")]
        public Keys HideHotkey
        {
            get => 
                (Keys) this["HideHotkey"];
            set => 
                this["HideHotkey"] = value;
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys KillHotKey
        {
            get => 
                (Keys) this["KillHotKey"];
            set => 
                this["KillHotKey"] = value;
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool RememberProcesses
        {
            get => 
                (bool) this["RememberProcesses"];
            set => 
                this["RememberProcesses"] = value;
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool HideIcon
        {
            get => 
                (bool) this["HideIcon"];
            set => 
                this["HideIcon"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode]
        public StringCollection RememberList
        {
            get => 
                (StringCollection) this["RememberList"];
            set => 
                this["RememberList"] = value;
        }

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool AutoSelect
        {
            get => 
                (bool) this["AutoSelect"];
            set => 
                this["AutoSelect"] = value;
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool CheckUpdates
        {
            get => 
                (bool) this["CheckUpdates"];
            set => 
                this["CheckUpdates"] = value;
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool StartHidden
        {
            get => 
                (bool) this["StartHidden"];
            set => 
                this["StartHidden"] = value;
        }

        [DebuggerNonUserCode, DefaultSettingValue("None"), UserScopedSetting]
        public Keys HideCurrent
        {
            get => 
                (Keys) this["HideCurrent"];
            set => 
                this["HideCurrent"] = value;
        }

        [ApplicationScopedSetting, DefaultSettingValue("http://www.neptunecentury.com/update/Update.asmx"), DebuggerNonUserCode, SpecialSetting(SpecialSetting.WebServiceUrl)]
        public string WinVisible_NCSUpdateService_Update =>
            (string) this["WinVisible_NCSUpdateService_Update"];

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool HideWhenScreenSaverActivates
        {
            get => 
                (bool) this["HideWhenScreenSaverActivates"];
            set => 
                this["HideWhenScreenSaverActivates"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ApplicationVersion
        {
            get => 
                (string) this["ApplicationVersion"];
            set => 
                this["ApplicationVersion"] = value;
        }
    }
}

