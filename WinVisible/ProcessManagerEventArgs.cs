namespace WinVisible
{
    using System;

    internal class ProcessManagerEventArgs : EventArgs
    {
        public ProcessItem Item;

        public ProcessManagerEventArgs(ProcessItem item)
        {
            this.Item = item;
        }
    }
}

