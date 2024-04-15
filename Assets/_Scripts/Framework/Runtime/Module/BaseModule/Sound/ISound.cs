using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public interface ISound
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dirPath"></param>
        void Init(string dirPath);

        /// <summary>
        /// 设置加载的方式
        /// </summary>
        /// <param name="res"></param>
        void SetResType(IRes res);

        /// <summary>
        /// 是否静音
        /// </summary>
        /// <param name="_isMute"></param>
        void Mute(bool _isMute);

        /// <summary>
        /// 播放BGM
        /// </summary>
        void PlayBGM(string bgm, bool loop = true, Action<AudioClip> callBack = null);

        /// <summary>
        /// 暂停BGM
        /// </summary>
        void PauseBGM();

        /// <summary>
        /// 取消暂停BGM
        /// </summary>
        void UnPauseBGM();

        /// <summary>
        /// 停止BGM
        /// </summary>
        void StopBGM();

        /// <summary>
        /// 播放2D音效
        /// </summary>
        void PlaySound(string sound, Action<AudioClip> callBack = null);

        /// <summary>
        /// 播放3D音效
        /// </summary>
        void PlaySound(string sound, GameObject target, Action<AudioClip> callBack = null);

        /// <summary>
        /// 在世界空间中指定的位置播放3D音效
        /// </summary>
        void PlaySound(string sound, Vector3 worldPosition, Transform parent = null, Action<AudioClip> callBack = null);

        /// <summary>
        /// 播放环境音效
        /// </summary>
        void PlayBGS(string bgs, bool loop = true, Action<AudioClip> callBack = null);

        /// <summary>
        /// 暂停环境音效
        /// </summary>
        void PauseBGS();

        /// <summary>
        /// 取消暂停环境音效
        /// </summary>
        void UnPauseBGS();

        /// <summary>
        /// 停止BGS
        /// </summary>
        void StopBGS();

        /// <summary>
        /// 播放提示音效
        /// </summary>
        /// <param name="ms"></param>
        void PlayMS(string ms, Action<AudioClip> callBack = null);

        /// <summary>
        /// 播放角色语音
        /// </summary>
        void PlayVoice(string voice, Action<AudioClip> callBack = null);

        /// <summary>
        /// 停止角色语音
        /// </summary>
        void StopVoice();
    }
}

