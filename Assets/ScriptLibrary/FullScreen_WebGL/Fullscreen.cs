
/*****************************************************
* 版权声明：上海卓越睿新数码科技有限公司，保留所有版权
* 文件名称：Fullscreen.cs
* 文件版本：1.0
* 创建时间：2022/08/19 03:30:48
* 作者名称：NoName
* 文件描述：

*****************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace WisdomTree.NoName.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class Fullscreen : MonoBehaviour
    {
        //[Tooltip("窗口模式屏幕宽度")]
        //public int windowedScreenWidth = 960;
        //[Tooltip("窗口模式屏幕高度")]
        //public int windowedScreenHeight = 540;
        [Tooltip("全屏状态下的普通图标，即退出全屏")]
        public Sprite windowedNormalSprite;//窗口化普通图片
        [Tooltip("全屏状态下的悬浮图标，即退出全屏")]
        public Sprite windowedHoverSprite;//窗口化全屏图片
        [Tooltip("窗口状态下的普通图标，即进入全屏")]
        public Sprite fullscreenNormalSprite;//全屏普通状态
        [Tooltip("窗口状态下的悬浮图标，即进入全屏")]
        public Sprite fullscreenHoverSprite;//全屏悬浮状态

        private Image image;
        private Button btn;

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            btn = GetComponent<Button>();

            //初始检测并改变图标，当全屏时重新加载场景可避免图标不准确的问题
            ChangeIcon(Screen.fullScreen);
        }

        /// <summary>
        /// 全屏切换
        /// </summary>
        public void Switch()
        {
            //改变图标，因为改变分辨率后Screen.fullScreen并不能立刻改变，所以只能通过传入!Screen.fullScreen改变图标，不能在里面直接判断
            ChangeIcon(!Screen.fullScreen);

            //切换屏幕分辨率
            //因为WebGL的输入框问题，所以不能用自带的设置分辨率
            //Screen.SetResolution(Screen.fullScreen ? windowedScreenWidth : Screen.currentResolution.width, Screen.fullScreen ? windowedScreenHeight : Screen.currentResolution.height, !Screen.fullScreen);        
            if (Screen.fullScreen)
            {
                WebGLFullScreen.Windowed();
            }
            else
            {
                WebGLFullScreen.FullScreen();
            }
        }

        /// <summary>
        /// 改变图标
        /// </summary>
        private void ChangeIcon(bool fullScreen)
        {
            //当前是全屏状态，则切换为退出全屏图标
            if (fullScreen)
            {
                image.sprite = windowedNormalSprite;

                SpriteState state = new SpriteState();
                state.highlightedSprite = windowedHoverSprite;
                state.pressedSprite = windowedHoverSprite;
                state.disabledSprite = windowedHoverSprite;
                btn.spriteState = state;
            }
            //当前是窗口状态，则切换为全屏图标
            else
            {
                image.sprite = fullscreenNormalSprite;

                SpriteState state = new SpriteState();
                state.highlightedSprite = fullscreenHoverSprite;
                state.pressedSprite = fullscreenHoverSprite;
                state.disabledSprite = fullscreenHoverSprite;
                btn.spriteState = state;
            }
        }
    }
}