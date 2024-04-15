
using UnityEngine;
using UnityEngine.UI;
namespace ZXKFramework
{
    public class TipBase : MonoBehaviour
    {
        private Text tipData = null;
        private Image bg = null;
        private Color baseColor = new Color();
        private bool isInit = false;
        public void Init()
        {
            if (!isInit)
            {
                tipData = transform.FindFirst<Text>("TipData");
                bg = GetComponent<Image>();
                baseColor = bg.color;
                OnAwake();
                isInit = true;
            }
        }
        public virtual void OnAwake()
        {

        }
        public virtual void ShowTip(string data)
        {
            gameObject.SetActive(true);
            tipData.text = data;
        }
        public virtual void ShowBg(bool isShow)
        {
            bg.color = isShow ? baseColor : new Color(1, 1, 1, 0);
        }
        public virtual void Close()
        {

        }
    }
}
