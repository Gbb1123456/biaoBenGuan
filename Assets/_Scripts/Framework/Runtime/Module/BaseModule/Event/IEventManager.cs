using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public delegate void EventHandler<T>(T e) where T : IGameEvent;
    public delegate void EventHandler(IGameEvent e);

    public interface IEventManager
    {
        void AddListener<T>(EventHandler<T> del) where T : IGameEvent;
        void RemoveListener<T>(EventHandler<T> del) where T : IGameEvent;
        void Raise(IGameEvent e);
        void Clear();
    }
}
