using System.Collections.Generic;

namespace KILROY.Constant.Resource
{
    #region Name

    /// <summary>
    /// 图片名称
    /// </summary>
    public enum ImageName
    {
        NONE,

        #if KILROY_TEST
        TestVideo,
        #endif
    }

    /// <summary>
    /// 音频名称
    /// </summary>
    public enum AudioName
    {
        NONE,
    }

    /// <summary>
    /// 视频名称
    /// </summary>
    public enum VideoName
    {
        NONE,
        #if KILROY_TEST
        TestVideo,
        #endif
    }

    /// <summary>
    /// Json名称
    /// </summary>
    public enum JsonName
    {
        NONE,
    }

    /// <summary>
    /// 着色器名称
    /// </summary>
    public enum ShaderName
    {
        None,
        DepthLight,
    }

    /// <summary>
    /// 材质名称
    /// </summary>
    public enum MaterialName
    {
        NONE,
        Footprint,
    }

    /// <summary>
    /// 预制件名称
    /// </summary>
    public enum PrefabName
    {
        NONE,
        Sphere,
        Cube,
        Button,
        VideoPlayer,
        Live2D,
    }

    #endregion

    /// <summary>
    /// 资源地址列表
    /// </summary>
    public static class PathList
    {
        public static readonly Dictionary<ImageName, string> ImageList = new Dictionary<ImageName, string>() // 图片列表
        {
            #if KILROY_TEST
            { ImageName.TestVideo, "_Test/Prefab/Video/TestVideo" },
            #endif
        };

        public static readonly Dictionary<AudioName, string> AudioList = new Dictionary<AudioName, string>() // 音频列表
        {
        };

        public static readonly Dictionary<VideoName, string> VideoList = new Dictionary<VideoName, string>() // 视频列表
        {
            #if KILROY_TEST
            { VideoName.TestVideo, "_Test/Prefab/Video/TestVideo" },
            #endif
        };

        public static readonly Dictionary<JsonName, string> JsonList = new Dictionary<JsonName, string>() // Json列表
        {
        };

        public static readonly Dictionary<ShaderName, string> ShaderList = new Dictionary<ShaderName, string>() // 着色器列表
        {
            { ShaderName.DepthLight, "Shader/Depth/Light/DepthLight" },
        };

        public static readonly Dictionary<MaterialName, string> MaterialList = new Dictionary<MaterialName, string>() // 材质列表
        {
            { MaterialName.Footprint, "Image/Material/Footprint/Footprint" },
        };

        public static readonly Dictionary<PrefabName, string> PrefabList = new Dictionary<PrefabName, string>() // 预制件列表
        {
            { PrefabName.Sphere, "Prefab/Tool/Base/Sphere" },
            { PrefabName.Cube, "Prefab/Tool/Base/Cube" },
            { PrefabName.Button, "Prefab/Tool/Base/Button" },
            { PrefabName.VideoPlayer, "Prefab/Tool/VideoPlayer" },
            { PrefabName.Live2D, "Prefab/Tool/BoxLive2D" },
        };
    }
}