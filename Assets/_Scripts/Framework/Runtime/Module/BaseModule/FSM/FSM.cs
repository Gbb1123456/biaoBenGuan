using System.Collections;
using System.Collections.Generic;

namespace ZXKFramework
{
    public class FSM : IFSM
    {
        public string CurStateName { get; set; }
        public StateBase CurState { get; set; }
        public bool isCanRunRepeatState { get; set; } = true;//是否可以运行相同状态
        private Dictionary<string, StateBase> allState { get; set; } = new Dictionary<string, StateBase>();

        public void AddState<T>() where T : StateBase, new()
        {
            string tempName = typeof(T).Name;
            if (!allState.ContainsKey(tempName))
            {
                T tempT = new T();
                tempT.Init(this);
                allState.Add(tempName, tempT);
            }
        }

        public void ChangeState<T>(params object[] obj) where T : StateBase, new()
        {
            string tempName = typeof(T).Name;
            AddState<T>();
            if (!allState.ContainsKey(tempName)) return;
            if (CurStateName == tempName)
            {
                if (!isCanRunRepeatState) return;
            }
            if (null != CurState)
            {
                CurState.OnExit();
                CurState = null;
            }
            CurStateName = tempName;
            CurState = allState[CurStateName];
            CurState.OnEnter(obj);
        }

        public void Update()
        {
            CurState?.OnUpdate();
        }

        public void Clean()
        {
            CurState?.OnExit();
            allState.Clear();
        }

        public void Destroy()
        {
            CurState?.Destroy();
        }
    }
}
