/************************************************************
  FileName: AnimatorExtension.cs
  Author: 万剑飞
  Version : 1.1
  Date: 2019.10.9
  Description: 动画控制器的扩展
  ======================================
  ChangeLog：
  2020.6.10 v1.1
  1.修改命名空间
************************************************************/

using UnityEngine;

namespace WJF_CodeLibrary.Extension
{
	public static class AnimatorExtension 
	{
        /// <summary>
        /// 停在动画的指定时间上
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <param name="normalizedTime"></param>
        public static void FreezeAtNormalizedTime(this Animator anim, string clipName, float normalizedTime)
        {
            anim.Play(clipName, 0, normalizedTime);
            anim.SetSpeed(0f);
        }

        /// <summary>
        /// 获取动画的时长
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <returns></returns>
		public static float GetClipLength(this Animator anim, string clipName)
        {
            AnimationClip clip = anim.GetClip(clipName);

            if (clip == null)
                return 0f;

            return clip.length;
        }

        /// <summary>
        /// 设置动画速度
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="speed"></param>
        public static void SetSpeed(this Animator anim, float speed)
        {
            anim.speed = speed;
        }

        /// <summary>
        /// 获取动画总帧数
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <returns></returns>
        public static float GetTotalFrame(this Animator anim, string clipName)
        {
            AnimationClip clip = anim.GetClip(clipName);
            
            if (clip == null)
                return 0f;

            return clip.frameRate * clip.length;
        }

        /// <summary>
        /// 获取动画指定帧的时长，即[0, frame]的时长
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static float GetClipLengthByFrame(this Animator anim, string clipName, int frame)
        {
            return anim.GetClipLength(clipName) * frame / anim.GetTotalFrame(clipName);
        }

        /// <summary>
        /// 获取动画
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <returns></returns>
        public static AnimationClip GetClip(this Animator anim, string clipName)
        {
            if (anim == null || string.IsNullOrEmpty(clipName) || anim.runtimeAnimatorController == null)
                return null;

            RuntimeAnimatorController ac = anim.runtimeAnimatorController;
            AnimationClip[] clips = ac.animationClips;
            
            if (clips == null || clips.Length <= 0)
                return null;

            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i].name == clipName)
                    return clips[i];
            }

            return null;
        }
	}
}