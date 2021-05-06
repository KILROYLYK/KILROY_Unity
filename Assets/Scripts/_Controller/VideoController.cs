using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using KILROY.Base;
using KILROY.Constant.Text;
using KILROY.Constant.Resource;
using KILROY.Tool;
using KILROY.Manager;

namespace KILROY.Controller
{
    public class VideoController : BaseControllerBehaviour<VideoController>
    {
        #region Parameter

        private Dictionary<VideoName, VideoPlayer> VideoList = new Dictionary<VideoName, VideoPlayer>(); // 视频播放器列表

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否创建</returns>
        private bool ErrorHandle(VideoName name)
        {
            bool isCreate = VideoList.ContainsKey(name);
            if (!isCreate) FN.Log(ErrorText.NoVideoPlayer + "：" + name.ToString());
            return isCreate;
        }

        /// <summary>
        /// 获取播放器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>播放器</returns>
        private VideoPlayer GetPlayer(VideoName name)
        {
            if (!VideoList.ContainsKey(name))
            {
                GameObject prefab = Instantiate(ResourceManager.GetPrefab(PrefabName.VideoPlayer));
                prefab.name = "Video_" + name.ToString();
                prefab.transform.SetParent(transform, false);

                VideoPlayer player = prefab.GetComponent<VideoPlayer>();
                player.clip = ResourceManager.GetVideo(name);
                player.Prepare(); // 开始预加载
                VideoList.Add(name, player);
            }

            return VideoList[name];
        }

        /// <summary>
        /// 获取纹理
        /// </summary>
        /// <param name="player">播放器</param>
        /// <returns>纹理</returns>
        private RenderTexture GetTexture(VideoPlayer player)
        {
            RenderTexture texture = new RenderTexture((int)player.clip.width, (int)player.clip.height, 0);
            texture.name = player.name;
            texture.antiAliasing = 2;
            return texture;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="target">目标对象</param>
        /// <param name="image">初始图像</param>
        /// <param name="startCB">开始回调</param>
        /// <param name="finishCB">完成回调</param>
        public void Create(VideoName name, RawImage target, Sprite image = null, Action<VideoPlayer> startCB = null, Action<VideoPlayer> finishCB = null)
        {
            VideoPlayer player = GetPlayer(name);
            RenderTexture texture = GetTexture(player);

            if (image != null) target.texture = UIFN.Texture2DToRender(image.texture);
            player.targetTexture = texture;

            player.started += (VideoPlayer source) => // 开始播放
            {
                if (startCB != null) startCB(source);
                AsyncController.Instance.StartCollaboration
                ( // 等待1s显示纹理，防止黑屏
                    new WaitForSeconds(1),
                    () => { target.texture = texture; }
                );
            };

            if (finishCB != null) player.loopPointReached += (VideoPlayer source) => { finishCB(source); }; // 播放完成
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="name">名称</param>
        public void Play(VideoName name)
        {
            if (!ErrorHandle(name)) return;
            VideoPlayer player = GetPlayer(name);
            player.Play();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="name">名称</param>
        public void Pause(VideoName name)
        {
            if (!ErrorHandle(name)) return;
            VideoPlayer player = GetPlayer(name);
            player.Pause();
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="name">名称</param>
        public void Stop(VideoName name)
        {
            if (!ErrorHandle(name)) return;
            VideoPlayer player = GetPlayer(name);
            player.Pause();
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clean()
        {
            foreach (KeyValuePair<VideoName, VideoPlayer> item in VideoList)
            {
                item.Value.Stop();
                Destroy(item.Value);
            }

            VideoList.Clear();
        }
    }
}