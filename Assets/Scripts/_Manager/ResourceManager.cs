using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using XLua;
using KILROY.Constant.Resource;

namespace KILROY.Manager
{
    [Hotfix]
    public static class ResourceManager
    {
        #region Parameter

        private static Dictionary<ImageName, Sprite> ImageList = new Dictionary<ImageName, Sprite>(); // 图片列表
        private static Dictionary<AudioName, AudioClip> AudioList = new Dictionary<AudioName, AudioClip>(); // 音频列表
        private static Dictionary<VideoName, VideoClip> VideoList = new Dictionary<VideoName, VideoClip>(); // 视频列表
        private static Dictionary<JsonName, object> JsonList = new Dictionary<JsonName, object>(); // Json列表
        private static Dictionary<ShaderName, Shader> ShaderList = new Dictionary<ShaderName, Shader>(); // 着色器列表
        private static Dictionary<MaterialName, Material> MaterialList = new Dictionary<MaterialName, Material>(); // 材质列表
        private static Dictionary<PrefabName, GameObject> PrefabList = new Dictionary<PrefabName, GameObject>(); // 预制件列表

        #endregion

        /// <summary>
        /// 清理缓存
        /// </summary>
        public static void CleanCache()
        {
            ImageList.Clear();
            AudioList.Clear();
            VideoList.Clear();
            JsonList.Clear();
            ShaderList.Clear();
            MaterialList.Clear();
            PrefabList.Clear();
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>图片</returns>
        public static Sprite GetImage(ImageName name)
        {
            if (!ImageList.ContainsKey(name))
            {
                string path = PathList.ImageList.ContainsKey(name) ? PathList.ImageList[name] : name.ToString();
                Sprite image = Resources.Load<Sprite>(path);
                ImageList.Add(name, image);
            }

            return ImageList[name];
        }

        /// <summary>
        /// 获取音频
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>音频</returns>
        public static AudioClip GetAudio(AudioName name)
        {
            if (!AudioList.ContainsKey(name))
            {
                string path = PathList.AudioList.ContainsKey(name) ? PathList.AudioList[name] : name.ToString();
                AudioClip audio = Resources.Load<AudioClip>(path);
                AudioList.Add(name, audio);
            }

            return AudioList[name];
        }

        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>视频</returns>
        public static VideoClip GetVideo(VideoName name)
        {
            if (!VideoList.ContainsKey(name))
            {
                string path = PathList.VideoList.ContainsKey(name) ? PathList.VideoList[name] : name.ToString();
                VideoClip video = Resources.Load<VideoClip>(path);
                VideoList.Add(name, video);
            }

            return VideoList[name];
        }

        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="name">名称</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>Json</returns>
        public static T GetJson<T>(JsonName name)
        {
            if (!JsonList.ContainsKey(name))
            {
                string path = PathList.JsonList.ContainsKey(name) ? PathList.JsonList[name] : name.ToString();
                TextAsset text = Resources.Load<TextAsset>(path);
                T json = JsonUtility.FromJson<T>(text.text);
                JsonList.Add(name, json);
            }

            return (T)JsonList[name];
        }

        /// <summary>
        /// 获取着色器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>着色器</returns>
        public static Shader GetShader(ShaderName name)
        {
            if (!ShaderList.ContainsKey(name))
            {
                string path = PathList.ShaderList.ContainsKey(name) ? PathList.ShaderList[name] : name.ToString();
                Shader shader = Resources.Load<Shader>(path);
                ShaderList.Add(name, shader);
            }

            return ShaderList[name];
        }

        /// <summary>
        /// 获取材质
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>材质</returns>
        public static Material GetMaterial(MaterialName name)
        {
            if (!MaterialList.ContainsKey(name))
            {
                string path = PathList.MaterialList.ContainsKey(name) ? PathList.MaterialList[name] : name.ToString();
                Material material = Resources.Load<Material>(path);
                MaterialList.Add(name, material);
            }

            return MaterialList[name];
        }

        /// <summary>
        /// 获取预制件
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>预制件</returns>
        public static GameObject GetPrefab(PrefabName name)
        {
            if (!PrefabList.ContainsKey(name))
            {
                string path = PathList.PrefabList.ContainsKey(name) ? PathList.PrefabList[name] : name.ToString();
                GameObject prefab = Resources.Load<GameObject>(path);
                PrefabList.Add(name, prefab);
            }

            return PrefabList[name];
        }
    }
}