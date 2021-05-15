namespace WinVisible.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new System.Resources.ResourceManager("WinVisible.Properties.Resources", typeof(Resources).Assembly);
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get =>
                resourceCulture;
            set =>
                resourceCulture = value;
        }

        internal static Bitmap about32x32 =>
            (Bitmap)ResourceManager.GetObject("about32x32", resourceCulture);

        internal static Bitmap add16x16 =>
            (Bitmap)ResourceManager.GetObject("add16x16", resourceCulture);

        internal static Bitmap copy24x24 =>
            (Bitmap)ResourceManager.GetObject("copy24x24", resourceCulture);

        internal static Bitmap delete16x16 =>
            (Bitmap)ResourceManager.GetObject("delete16x16", resourceCulture);

        internal static Bitmap donate32x32 =>
            (Bitmap)ResourceManager.GetObject("donate32x32", resourceCulture);

        internal static Bitmap donate32x321 =>
            (Bitmap)ResourceManager.GetObject("donate32x321", resourceCulture);

        internal static Bitmap error48x48 =>
            (Bitmap)ResourceManager.GetObject("error48x48", resourceCulture);

        internal static Bitmap exit32x32 =>
            (Bitmap)ResourceManager.GetObject("exit32x32", resourceCulture);

        internal static Bitmap hide32x32 =>
            (Bitmap)ResourceManager.GetObject("hide32x32", resourceCulture);

        internal static Bitmap icon =>
            (Bitmap)ResourceManager.GetObject("icon", resourceCulture);

        internal static Bitmap internet32x32 =>
            (Bitmap)ResourceManager.GetObject("internet32x32", resourceCulture);

        internal static Bitmap internet32x321 =>
            (Bitmap)ResourceManager.GetObject("internet32x321", resourceCulture);

        internal static Bitmap keyboard32x32 =>
            (Bitmap)ResourceManager.GetObject("keyboard32x32", resourceCulture);

        internal static Bitmap kill32x32 =>
            (Bitmap)ResourceManager.GetObject("kill32x32", resourceCulture);

        internal static Bitmap options32x32 =>
            (Bitmap)ResourceManager.GetObject("options32x32", resourceCulture);

        internal static Bitmap refresh32x32 =>
            (Bitmap)ResourceManager.GetObject("refresh32x32", resourceCulture);

        internal static Bitmap search24x24 =>
            (Bitmap)ResourceManager.GetObject("search24x24", resourceCulture);

        internal static Bitmap show32x32 =>
            (Bitmap)ResourceManager.GetObject("show32x32", resourceCulture);

        internal static Bitmap web32x32 =>
            (Bitmap)ResourceManager.GetObject("web32x32", resourceCulture);
    }
}

