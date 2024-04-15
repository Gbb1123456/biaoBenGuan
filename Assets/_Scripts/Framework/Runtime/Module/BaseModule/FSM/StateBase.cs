using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public abstract class StateBase
    {
        protected IFSM _StateMachine = null;

        public virtual void Init(IFSM stateMachine)
        {
            _StateMachine = stateMachine;
        }

        public virtual void OnEnter(params object[] obj)
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
