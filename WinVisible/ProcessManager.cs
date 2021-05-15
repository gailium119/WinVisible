namespace WinVisible
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    internal class ProcessManager
    {
        private static ProcessCollection _processList = new ProcessCollection();
        private static EventHandler<ProcessManagerEventArgs> ProcessItemAdded;
        private static EventHandler<ProcessManagerEventArgs> ProcessItemRemoved;
        private static EventHandler<ProcessManagerEventArgs> ProcessItemUpdate;

        public static event EventHandler<ProcessManagerEventArgs> ProcessItemAddedBase
        {
            add
            {
                EventHandler<ProcessManagerEventArgs> processItemAdded = ProcessItemAdded;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemAdded;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand + value;
                    processItemAdded = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemAdded, handler3, comparand);
                    if (ReferenceEquals(processItemAdded, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<ProcessManagerEventArgs> processItemAdded = ProcessItemAdded;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemAdded;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand - value;
                    processItemAdded = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemAdded, handler3, comparand);
                    if (ReferenceEquals(processItemAdded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event EventHandler<ProcessManagerEventArgs> ProcessItemRemovedBase
        {
            add
            {
                EventHandler<ProcessManagerEventArgs> processItemRemoved = ProcessItemRemoved;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemRemoved;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand + value;
                    processItemRemoved = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemRemoved, handler3, comparand);
                    if (ReferenceEquals(processItemRemoved, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<ProcessManagerEventArgs> processItemRemoved = ProcessItemRemoved;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemRemoved;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand - value;
                    processItemRemoved = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemRemoved, handler3, comparand);
                    if (ReferenceEquals(processItemRemoved, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event EventHandler<ProcessManagerEventArgs> ProcessItemUpdateBase
        {
            add
            {
                EventHandler<ProcessManagerEventArgs> processItemUpdate = ProcessItemUpdate;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemUpdate;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand + value;
                    processItemUpdate = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemUpdate, handler3, comparand);
                    if (ReferenceEquals(processItemUpdate, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<ProcessManagerEventArgs> processItemUpdate = ProcessItemUpdate;
                while (true)
                {
                    EventHandler<ProcessManagerEventArgs> comparand = processItemUpdate;
                    EventHandler<ProcessManagerEventArgs> handler3 = comparand - value;
                    processItemUpdate = Interlocked.CompareExchange<EventHandler<ProcessManagerEventArgs>>(ref ProcessItemUpdate, handler3, comparand);
                    if (ReferenceEquals(processItemUpdate, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static bool IsProcessIDInCollection(int id)
        {
            bool flag;
            using (List<ProcessItem>.Enumerator enumerator = _processList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ProcessItem current = enumerator.Current;
                        if (current.Process.Id != id)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private static bool IsProcessIDInList(int id)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RefreshProcessCollection()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (!IsProcessIDInCollection(process.Id) && (process.MainWindowHandle != IntPtr.Zero))
                {
                    ProcessItem item = new ProcessItem(process);
                    _processList.Add(item);
                    if (ProcessItemAdded != null)
                    {
                        ProcessItemAdded(null, new ProcessManagerEventArgs(item));
                    }
                }
            }
            for (int i = 0; i < _processList.Count; i++)
            {
                ProcessItem item = _processList[i];
                try
                {
                    item.Process.Refresh();
                    if (item.Process.HasExited)
                    {
                        _processList.Remove(item);
                        if (ProcessItemRemoved != null)
                        {
                            ProcessItemRemoved(null, new ProcessManagerEventArgs(item));
                        }
                        item.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static ProcessCollection Processes =>
            _processList;
    }
}

