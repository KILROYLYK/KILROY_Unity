using UnityEngine;
using KILROY.Base;
using KILROY.Constant.Resource;
using KILROY.Model;
using KILROY.Manager;

namespace KILROY.Controller
{
    public class AudioController : BaseControllerBehaviour<AudioController>
    {
        #region Parameter

        private AudioSource PlayerMusic = null; // 音乐播放器
        private AudioSource PlayerSound = null; // 音效播放器
        private AudioSource PlayerDialog = null; // 对白播放器

        #endregion

        #region Cycle

        public void Awake()
        {
            CreatePlayer();
            UpdateMuteStatus();
        }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 创建播放器
        /// </summary>
        private void CreatePlayer()
        {
            int distance = 50;

            // 音乐
            PlayerMusic = gameObject.AddComponent<AudioSource>();
            PlayerMusic.name = "PlayerMusic";
            PlayerMusic.loop = true;
            PlayerMusic.maxDistance = distance;

            // 音效
            PlayerSound = gameObject.AddComponent<AudioSource>();
            PlayerSound.name = "PlayerSound";
            PlayerSound.loop = true;
            PlayerSound.maxDistance = distance;

            // 对白
            PlayerDialog = gameObject.AddComponent<AudioSource>();
            PlayerDialog.name = "PlayerDialog";
            PlayerDialog.loop = true;
            PlayerDialog.maxDistance = distance;
        }

        /// <summary>
        /// 更新静音状态
        /// </summary>
        public void UpdateMuteStatus()
        {
            float music = ConfigData.AudioMusic;
            float sound = ConfigData.AudioSound;
            float dialog = ConfigData.AudioDialog;

            // 音乐
            if (music > 0) // 播放
            {
                PlayerMusic.mute = false;
                PlayerMusic.volume = music;
            }
            else // 静音
            {
                PlayerMusic.mute = true;
            }

            // 音效
            if (sound > 0) // 播放
            {
                PlayerSound.mute = false;
                PlayerSound.volume = sound;
            }
            else // 静音
            {
                PlayerSound.mute = true;
            }

            // 对白
            if (dialog > 0) // 播放
            {
                PlayerDialog.mute = false;
                PlayerDialog.volume = dialog;
            }
            else // 静音
            {
                PlayerDialog.mute = true;
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="isReset">是否重置</param>
        public void Stop(bool isReset = false)
        {
            StopMusic(isReset);
            StopSound(isReset);
            StopDialog(isReset);
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clean()
        {
            Stop(true);
            PlayerMusic.clip = null;
            PlayerSound.clip = null;
            PlayerDialog.clip = null;
        }

        #region Music

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayMusic(AudioName name, bool isLoop = false)
        {
            if (PlayerMusic.mute) return; // 静音

            AudioClip clip = ResourceManager.GetAudio(name);

            if (isLoop)
            {
                if (clip != PlayerSound.clip) PlayerMusic.clip = clip;
                PlayerMusic.Play();
            }
            else
            {
                PlayerMusic.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// 停止音乐
        /// </summary>
        /// <param name="isReset">是否重置</param>
        public void StopMusic(bool isReset = false)
        {
            if (isReset)
            {
                PlayerMusic.Stop();
            }
            else
            {
                PlayerMusic.Pause();
            }
        }

        #endregion

        #region Sound

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isLoop">是否循环</param>
        public void PlaySound(AudioName name, bool isLoop = false)
        {
            if (PlayerSound.mute) return; // 静音

            AudioClip clip = ResourceManager.GetAudio(name);

            if (isLoop)
            {
                if (clip != PlayerSound.clip) PlayerSound.clip = clip;
                PlayerSound.Play();
            }
            else
            {
                PlayerSound.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// 停止音效
        /// </summary>
        /// <param name="isReset">是否重置</param>
        public void StopSound(bool isReset = false)
        {
            if (isReset)
            {
                PlayerSound.Stop();
            }
            else
            {
                PlayerSound.Pause();
            }
        }

        #endregion

        #region Dialog

        /// <summary>
        /// 播放对白
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayDialog(AudioName name, bool isLoop = false)
        {
            if (PlayerDialog.mute) return; // 静音

            AudioClip clip = ResourceManager.GetAudio(name);

            if (isLoop)
            {
                if (clip != PlayerSound.clip) PlayerDialog.clip = clip;
                PlayerDialog.Play();
            }
            else
            {
                PlayerDialog.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// 停止对白
        /// </summary>
        /// <param name="isReset">是否重置</param>
        public void StopDialog(bool isReset = false)
        {
            if (isReset)
            {
                PlayerDialog.Stop();
            }
            else
            {
                PlayerDialog.Pause();
            }
        }

        #endregion
    }
}