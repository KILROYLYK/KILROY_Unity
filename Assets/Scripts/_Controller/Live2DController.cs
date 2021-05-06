using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Core;
using Live2D.Cubism.Rendering;
using Live2D.Cubism.Framework.Json;
using KILROY.Base;
using KILROY.Constant.Text;
using KILROY.Constant.Resource;
using KILROY.Tool;
using KILROY.Manager;

namespace KILROY.Controller
{
    /// <summary>
    /// Live2D名称
    /// </summary>
    public enum Live2DName
    {
        #if KILROY_TEST
        TestKing,
        #endif
    }

    public class Live2DController : BaseControllerBehaviour<Live2DController>
    {
        #region Parameter

        private Camera ModelCamera = null; // 模型相机
        private Transform BoxModel = null; // 模型盒子
        private RenderTexture ModelTexture = null; // 模型纹理 

        private Dictionary<Live2DName, string> PathList = new Dictionary<Live2DName, string>() // Live2D列表
        {
            #if KILROY_TEST
            { Live2DName.TestKing, "_Test/Prefab/Live2D/King/king" }
            #endif
        };

        private Dictionary<Live2DName, CubismModel> ModelList = new Dictionary<Live2DName, CubismModel>(); // 模型列表

        #endregion

        #region Cycle

        public void Awake() { CreateBox(); }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 获取Live2D
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>Json</returns>
        private CubismModel3Json GetLive2D(Live2DName name)
        {
            CubismModel3Json json = CubismModel3Json.LoadAtPath
            (
                "Assets/Resources/" + PathList[name] + ".model3.json",
                (Type assetType, string absolutePath) =>
                {
                    if (assetType == typeof(byte[])) return File.ReadAllBytes(absolutePath);
                    else if (assetType == typeof(string)) return File.ReadAllText(absolutePath);
                    else if (assetType == typeof(Texture2D))
                    {
                        Texture2D texture = new Texture2D(1, 1);
                        texture.LoadImage(File.ReadAllBytes(absolutePath));
                        return texture;
                    }

                    throw new NotSupportedException();
                }
            );
            return json;
        }

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>是否创建</returns>
        private bool ErrorHandle(Live2DName name)
        {
            bool isCreate = ModelList.ContainsKey(name);
            if (!isCreate) FN.Log(ErrorText.NoVideoPlayer + "：" + name.ToString());
            return isCreate;
        }

        /// <summary>
        /// 创建盒子
        /// </summary>
        private void CreateBox()
        {
            GameObject prefab = ResourceManager.GetPrefab(PrefabName.Live2D);
            GameObject box = UnityEngine.Object.Instantiate(prefab);
            box.name = "BoxLive2D";
            box.transform.SetParent(transform);

            ModelCamera = box.transform.Find("Camera").GetComponent<Camera>();
            ModelCamera.targetTexture = ModelTexture = GetTexture();

            BoxModel = box.transform.Find("BoxModel");
        }

        /// <summary>
        /// 设置显示图片
        /// </summary>
        /// <param name="image">图片对象</param>
        public void SetShowImage(RawImage image) { image.texture = ModelTexture; }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>播放器</returns>
        private CubismModel GetModel(Live2DName name)
        {
            if (!ModelList.ContainsKey(name))
            {
                CubismModel model = GetLive2D(name).ToModel();
                model.name = "Live" + name.ToString();
                model.transform.SetParent(BoxModel, false);

                foreach (Transform tran in model.GetComponentsInChildren<Transform>()) tran.gameObject.layer = 7;

                CubismRenderController controller = model.GetComponent<CubismRenderController>();
                controller.Opacity = 0;

                ModelList.Add(name, model);
            }

            return ModelList[name];
        }

        /// <summary>
        /// 获取纹理
        /// </summary>
        /// <returns>纹理</returns>
        private RenderTexture GetTexture()
        {
            RenderTexture texture = new RenderTexture(500, 1000, 0);
            texture.name = "Live2D";
            texture.antiAliasing = 2;
            return texture;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="path">控制器地址</param>
        public void Create(Live2DName name, string path)
        {
            CubismModel model = GetModel(name);

            Animator animator = model.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);
        }

        /// <summary>
        /// 获取动画控制器
        /// </summary>
        /// <param name="name">名称</param>
        public Animator Get(Live2DName name)
        {
            if (!ErrorHandle(name)) return null;

            CubismModel model = GetModel(name);
            return model.GetComponent<Animator>();
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="name"></param>
        public void Show(Live2DName name)
        {
            if (!ErrorHandle(name)) return;

            Hide();

            CubismRenderController controller = ModelList[name].GetComponent<CubismRenderController>();
            controller.Opacity = 1;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            foreach (KeyValuePair<Live2DName, CubismModel> item in ModelList)
            {
                CubismRenderController controller = item.Value.GetComponent<CubismRenderController>();
                controller.Opacity = 0;
            }
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clean()
        {
            foreach (KeyValuePair<Live2DName, CubismModel> item in ModelList) Destroy(item.Value);
            ModelList.Clear();
        }

        /// <summary>
        /// 切换动画
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="animation">动画</param>
        public void Animation(Live2DName name, string animation)
        {
            if (!ErrorHandle(name)) return;

            CubismModel model = GetModel(name);
            Animator animator = model.GetComponent<Animator>();
            animator.CrossFade(animation, 0.5f);
        }
    }
}