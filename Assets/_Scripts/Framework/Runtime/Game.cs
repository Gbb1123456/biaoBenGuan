using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public class Game : MonoSingleton<Game>
    {
        [Header("======Asset")]
        public ResLoadType resLoadType = ResLoadType.Resources;
        [HideInInspector]
        public IRes res = null;

        [Header("======Save")]
        public string saveDirPath = string.Empty;
        public SaveManagerType saveManagerType = SaveManagerType.PlayerPrefs;
        [HideInInspector]
        public ISave save = null;

        [Header("======Sound")]
        public string soundDirPath = string.Empty;
        public ResLoadType soundResLoadType = ResLoadType.Resources;
        public SoundType soundType = SoundType.Base;
        public bool sceneObjFindAudioClip = false;
        [HideInInspector]
        public ISound sound = null;

        [Header("======Pool")]
        public string poolDirPath = string.Empty;
        public ResLoadType poolResLoadType = ResLoadType.Resources;
        public ObjectPoolType objectPoolType = ObjectPoolType.Base;
        [HideInInspector]
        public IObjectPool objectPool = null;

        [Header("======FSM")]
        public FSMType fSMType = FSMType.Base;
        [HideInInspector]
        public IFSM fsm = null;

        [Header("======Event")]
        public EventMangerType eventManagerType = EventMangerType.Base;
        [HideInInspector]
        public IEventManager eventManager = null;

        [Header("======IEnumerator")]
        public IEnumeratorManagerType iEnumeratorManagerType = IEnumeratorManagerType.Base;
        [HideInInspector]
        public IIECoroutine IEnumeratorManager = null;

        [Header("======HTTP")]
        public HttpManagerType httpManagerType = HttpManagerType.Base;
        [HideInInspector]
        public IHttp httpManager = null;

        [Header("======UI")]
        public string uiDirPath = string.Empty;
        public GameObject canvas = null;
        public ResLoadType uiResLoadType = ResLoadType.Resources;
        public UIManagerType uiManagerType = UIManagerType.Base;
        public bool canvasTransformFindUI = true;
        [HideInInspector]
        public IUIManager uiManager = null;

        [Header("======Tip")]
        public TipManagerType tipManagerType = TipManagerType.None;
        [HideInInspector]
        public ITipManager tipManager = null;

        [Header("======Animator")]
        public GameObject animatorParent = null;
        public AnimatorManagerType animatorManagerType = AnimatorManagerType.Animator;
        [HideInInspector]
        public IAnimatorManager animatorManager = null;

        [Header("======Language")]
        public LanguageType languageType = LanguageType.Chinese;

        [Header("======Adapter")]
        public AdapterType adapterType = AdapterType.PC;
        [HideInInspector]
        public IAdapter adapterManager = null;

        [Header("======Scene")]
        public SceneManagerType sceneManagerType = SceneManagerType.Base;
        [HideInInspector]
        public IScene sceneManager = null;
        public int directLoadNextScene = 0;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            FSMInit();
            EventManagerInit();
            ResInit();
            SaveInit();
            AnimatorManagerInit();
            HttpManagerInit();
            IEManagerInit();
            SceneMangerInit();
            SoundInit();
            ObjectPoolInit();
            UIManagerInit();
            TipManagerInit();
            AdapterInit();
            if (directLoadNextScene != 0) sceneManager?.LoadLevel(directLoadNextScene);
        }

        private void ResInit()
        {
            res = GetRes(resLoadType);
        }

        public IRes GetRes(ResLoadType moduleResLoadType)
        {
            switch (moduleResLoadType)
            {
                case ResLoadType.Resources:
                    return new ResResources();
                case ResLoadType.Assetbundle:
                    return new ResAssetBundle();
                case ResLoadType.AssetbundleSimple:
                    return new ResAssetBundleSimple();
                case ResLoadType.StreamingAssets:
                    return new ResStreamingAssets();
                case ResLoadType.Url:
                    return new ResUrl();
            }
            return null;
        }

        private void AnimatorManagerInit()
        {
            switch (animatorManagerType)
            {
                case AnimatorManagerType.Animator:
                    {
                        animatorManager = new AnimatorManager();
                    }
                    break;
                case AnimatorManagerType.Animation:
                    {
                        animatorManager = new AnimationManager();
                    }
                    break;
            }
            if (animatorParent != null)
            {
                animatorManager?.Init(animatorParent);
            }
        }

        private void SoundInit()
        {
            switch (soundType)
            {
                case SoundType.Base:
                    {
                        GameObject soundManager = new GameObject("SoundManager");
                        soundManager.transform.SetParent(transform);
                        sound = soundManager.AddComponent<AudioManager>();
                        sound.SetResType(GetRes(soundResLoadType));
                        sound.Init(soundDirPath);
                    }
                    break;
            }
        }

        private void SaveInit()
        {
            switch (saveManagerType)
            {
                case SaveManagerType.PlayerPrefs:
                    {
                        save = new SavePlayerPrefs();
                    }
                    break;
                case SaveManagerType.Text:
                    {
                        save = new SaveText();                        
                    }
                    break;
            }
            save?.Init(saveDirPath);
        }

        private void SceneMangerInit()
        {
            switch (sceneManagerType)
            {
                case SceneManagerType.Base:
                    {
                        sceneManager = new SceneManager();
                    }
                    break;
            }
        }

        private void ObjectPoolInit()
        {
            switch (objectPoolType)
            {
                case ObjectPoolType.Base:
                    GameObject soundManager = new GameObject("ObjectPoolManager");
                    soundManager.transform.SetParent(transform);
                    objectPool = soundManager.AddComponent<ObjectPool>();
                    objectPool.SetResType(GetRes(poolResLoadType));
                    objectPool.Init(poolDirPath);
                    break;
            }
        }


        private void FSMInit()
        {
            switch (fSMType)
            {
                case FSMType.Base:
                    fsm = new FSM();
                    break;
            }
        }
     
        private void HttpManagerInit()
        {
            switch (httpManagerType)
            {
                case HttpManagerType.Base:
                    httpManager = new Http();
                    break;
            }
        }

        private void IEManagerInit()
        {                                                                                                                                                                                                             
            switch (iEnumeratorManagerType)
            {
                case IEnumeratorManagerType.Base:
                    IEnumeratorManager = new IECoroutine();
                    break;
            }
        }

        private void EventManagerInit()
        {
            switch (eventManagerType)
            {
                case EventMangerType.Base:
                    eventManager = new EventManager();
                    break;
            }
        }

        private void UIManagerInit()
        {
            switch (uiManagerType)
            {
                case UIManagerType.Base:
                    uiManager = new UIManager();
                    if (canvas == null) return;
                    canvas.transform.SetParent(transform);
                    uiManager.SetResType(GetRes(uiResLoadType));
                    uiManager.Init(canvas, uiDirPath);
                    break;
            }
        }

        private void TipManagerInit()
        {
            switch (tipManagerType)
            {
                case TipManagerType.Base:
                    tipManager = new TipManager();
                    if (canvas == null) return;
                    GameObject loTip = canvas.FindFirst("TipManager");
                    if (loTip == null) return;
                    tipManager.Init(loTip.transform);
                    break;
                case TipManagerType.None:
                    break;
            }
        }

        private void AdapterInit()
        {
            switch (adapterType)
            {
                case AdapterType.PC:
                    adapterManager = new AdapterPC();
                    break;
                case AdapterType.WEBGL:
                    adapterManager = new AdapterWebgl();
                    break;
                case AdapterType.IOS:
                    adapterManager = new AdapterIOS();
                    break;
                case AdapterType.ANDROID:
                    adapterManager = new AdapterAndroid();
                    break;
                case AdapterType.Other:
                    adapterManager = GetComponent<IAdapter>();
                    break;
            }
            adapterManager?.Init();
        }

        void Update()
        {
            fsm?.Update();
            uiManager?.OnUpdate();
            tipManager?.Update();
        }

        void OnDestroy()
        {
            fsm?.Destroy();
            uiManager?.Destroy();
        }

        public void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }

        public void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }

        public void OnLevelWasLoaded(int level)
        {
            FrameworkScenesArgs e = new FrameworkScenesArgs()
            {
                scnesIndex = level
            };
            Game.Instance?.SendEvent(FrameworkConsts.E_EnterScenes, e);
        }
    }
}
