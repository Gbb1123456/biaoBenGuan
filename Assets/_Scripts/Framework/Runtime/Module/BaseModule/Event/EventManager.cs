using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class EventManager : IEventManager
    {
        private Dictionary<System.Type, EventHandler> _delegates = new Dictionary<System.Type, EventHandler>();
        private Dictionary<System.Delegate, EventHandler> _delegateLookup = new Dictionary<System.Delegate, EventHandler>();

        public void AddListener<T>(EventHandler<T> del) where T : IGameEvent
        {
            if (_delegateLookup.ContainsKey(del))
                return;
            EventHandler internalDelegate = (e) => del((T)e);
            _delegateLookup[del] = internalDelegate;
            EventHandler tempDel;
            if (_delegates.TryGetValue(typeof(T), out tempDel))
                _delegates[typeof(T)] = tempDel += internalDelegate;
            else
                _delegates[typeof(T)] = internalDelegate;
        }

        public void RemoveListener<T>(EventHandler<T> del) where T : IGameEvent
        {
            EventHandler internalDelegate;
            if (_delegateLookup.TryGetValue(del, out internalDelegate))
            {
                EventHandler tempDel;
                if (_delegates.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                        _delegates.Remove(typeof(T));
                    else
                        _delegates[typeof(T)] = tempDel;
                }
                _delegateLookup.Remove(del);
            }
        }

        public void Raise(IGameEvent e)
        {
            EventHandler del;
            if (_delegates.TryGetValue(e.GetType(), out del))
            {
                System.Delegate[] delegate_array = del.GetInvocationList();
                int count = delegate_array.Length;
                Debug.Log("[GameEventManager.Raise] delegate array Count : " + count);
                for (int i = 0; i < count; i++)
                {
                    var del_item = delegate_array[i];
                    try
                    {
                        if (del_item != null)
                            del_item.DynamicInvoke(e);
                    }
                    catch (System.Exception ex)
                    {
                        string msg = ex.Message;
                        string stack_trace = ex.StackTrace;

                        if (ex.InnerException != null)
                        {
                            msg = ex.InnerException.Message;
                            stack_trace = ex.InnerException.StackTrace;
                        }
                        Debug.Log("[GameEventManager]Raise: {0}\nStack Trace: {1}\nThis event listener will be removed because of this exception!" + msg + stack_trace);
                        var evt_type = e.GetType();
                        var item_evt_handler = del_item as EventHandler;
                        if (item_evt_handler != null)
                            del -= item_evt_handler;
                        if (del == null)
                            _delegates.Remove(evt_type);
                        else
                            _delegates[evt_type] = del;
                        var etor = _delegateLookup.GetEnumerator();
                        while (etor.MoveNext())
                        {
                            if (etor.Current.Value == item_evt_handler)
                            {
                                _delegateLookup.Remove(etor.Current.Key);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            _delegates.Clear();
            _delegateLookup.Clear();
        }
    }
}