using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KILROY.Base;
using KILROY.Constant.Name;

namespace KILROY.Controller
{
    public class SceneController : BaseControllerBehaviour<SceneController>
    {
        #region Parameter

        private Dictionary<SceneName, Scene> SceneList = new Dictionary<SceneName, Scene>(); // 场景列表
        private AsyncOperation Operation; // 异步操作

        #endregion

        #region Cycle

        public void Awake() { SceneList.Add(SceneName.Application, GetScene(SceneName.Application)); }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>字符串名称</returns>
        private string GetName(SceneName name) { return "Scene" + name.ToString(); }

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>场景</returns>
        public Scene GetScene(SceneName name) { return SceneManager.GetSceneByName(GetName(name)); }

        /// <summary>
        /// 显示场景
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="callback">回调</param>
        /// <param name="mode">模式</param>
        public AsyncOperation ShowScene(SceneName name, Action callback = null, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if (SceneList.ContainsKey(name)) // 场景已存在
            {
                callback?.Invoke();
                return null;
            }

            foreach (KeyValuePair<SceneName, Scene> item in SceneList)
            {
                if (item.Key != SceneName.Panel) HideScene(item.Key); // 隐藏其他场景
            }

            AsyncOperation async = SceneManager.LoadSceneAsync(GetName(name), mode);
            // async.allowSceneActivation = false;
            AsyncController.Instance.StartCollaboration
            (
                async,
                () =>
                {
                    Scene scene = GetScene(name);

                    SceneList.Add(name, scene);
                    SceneManager.SetActiveScene(scene);

                    callback?.Invoke();
                }
            );

            return async;
        }

        /// <summary>
        /// 隐藏场景
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="callback">回调</param>
        public AsyncOperation HideScene(SceneName name, Action callback = null)
        {
            if (!SceneList.ContainsKey(name)) // 场景不存在
            {
                callback?.Invoke();
                return null;
            }

            AsyncOperation async = SceneManager.UnloadSceneAsync(GetName(name));
            // async.allowSceneActivation = false;
            AsyncController.Instance.StartCollaboration
            (
                async,
                () =>
                {
                    SceneList.Remove(name);
                    if (SceneList.Count > 0) SceneManager.SetActiveScene(SceneList.Values.Last());

                    callback?.Invoke();
                }
            );

            return async;
        }
    }
}