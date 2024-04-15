
using UnityEngine;

namespace ZXKFramework
{
    public abstract class SingletonThis<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _Instance = null;

        //只有访问权限
        public static T Instance
        {
            get
            {
                return _Instance;
            }
        }

        public virtual void Init()
        {
            _Instance = this as T;
        }
    }

    public abstract class SingletonNew<T> where T : class, new()
    {
        protected static T _Instance = null;

        public static T Instance
        {
            get
            {
                if (null == _Instance)
                {
                    _Instance = new T();
                }
                return _Instance;
            }
        }

        protected SingletonNew()
        {
            if (null != _Instance)
                throw new SingletonException("This " + (typeof(T)).ToString() + " Singleton Instance is not null !!!");
            Init();
        }
        public virtual void Init() { }
    }

    public class SingletonException : System.Exception
    {
        public SingletonException(string msg) : base(msg)
        {

        }
    }

    public abstract class SingletonDdol<T> : MonoBehaviour where T : SingletonDdol<T>
    {
        protected static T _Instance = null;
        private static GameObject go;
        public static T Instance
        {
            get
            {
                if (null == _Instance)
                {
                    go = GameObject.Find("SingletonDdol");
                    if (null == go)
                    {
                        go = new GameObject("SingletonDdol");
                        DontDestroyOnLoad(go);
                    }
                    GameObject insObj = new GameObject(typeof(T).ToString());
                    DontDestroyOnLoad(insObj);
                    insObj.transform.SetParent(go.transform);
                    _Instance = insObj.AddComponent<T>();
                    _Instance.InitInstance();
                }
                return _Instance;
            }
        }

        public virtual void InitInstance()
        {

        }

        public virtual void Init()
        {

        }
    }
}




