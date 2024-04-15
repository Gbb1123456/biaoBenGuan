using UnityEngine;

namespace ZXKFramework
{
    public abstract class UIBase : View
    {
        public IUIManager uiManager { get; set; } = null;
        protected Transform trs;
        public abstract string GroupName { get; }

        public virtual void Init(IUIManager uictrl)
        {
            uiManager = uictrl;
            trs = transform;
        }

        public void SetActive(bool isShow)
        {
            if (trs.gameObject.activeInHierarchy != isShow)
            {
                trs.gameObject.SetActive(isShow);
            }
            if (isShow)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public bool isShow()
        {
            return trs.gameObject.activeInHierarchy;
        }

        public virtual void ShowData(params object[] obj)
        {
            
        }

        public virtual void Show()
        {

        }

        public virtual void Hide()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}