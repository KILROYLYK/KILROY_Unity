using System.Collections.Generic;
using UnityEngine;
using XLua;
using DG.Tweening;

namespace KILROY.Base
{
    [Hotfix]
    public abstract class BaseBehaviour : MonoBehaviour
    {
        #region Parameter

        private List<Sequence> TweenList = new List<Sequence>(); // 动效列表
        private Dictionary<string, Sequence> TweenOnlyList = new Dictionary<string, Sequence>(); // 唯一动效列表

        protected Dictionary<string, bool> SwitchList = new Dictionary<string, bool>(); // 开关列表
        protected Dictionary<string, float> FloatList = new Dictionary<string, float>(); // 数值列表

        #endregion

        #region Cycle

        //---------- 重置 Start ----------//
        /// <summary>
        /// 重置为默认值时
        /// </summary>
        // public void Reset() { }
        //---------- 重置 End ----------//

        //---------- 启动 Start ----------//
        /// <summary>
        /// 加载脚本实例时
        /// </summary>
        // public void Awake() { }

        /// <summary>
        /// 当对象启用并激活时
        /// </summary>
        // public void OnEnable() { }

        /// <summary>
        /// 使用并进行初始化时
        /// </summary>
        // public void Start() { }
        //---------- 启动 End ----------//

        //---------- 更新 Start ----------//
        /// <summary>
        /// 每个固定帧率帧调用时（与物理相关事件）
        /// </summary>
        // public void FixedUpdate() { }

        /// <summary>
        /// 每帧调用一次更新时（其他事件如按钮）
        /// </summary>
        // public void Update() { }

        /// <summary>
        /// 每帧更新之后（相机更新事件）
        /// </summary>
        // public void LateUpdate() { }
        //---------- 更新 End ----------//

        //---------- 场景渲染 Start ----------//
        /// <summary>
        /// 渲染物体时
        /// </summary>
        // public void OnWillRenderObject() { }

        /// <summary>
        /// 预选时
        /// </summary>
        // public void OnPreCull() { }

        /// <summary>
        /// 成为可见时
        /// </summary>
        // public void OnBecameVisible() { }

        /// <summary>
        /// 成为隐藏时
        /// </summary>
        // public void OnBecameInvisible() { }

        /// <summary>
        /// 预渲染时
        /// </summary>
        // public void OnPreRender() { }

        /// <summary>
        /// 渲染对象时
        /// </summary>
        // public void OnRenderObject() { }

        /// <summary>
        /// 后期渲染时
        /// </summary>
        // public void OnPostRender() { }

        /// <summary>
        /// 渲染图片时
        /// </summary>
        // public void OnRenderImage() { }
        //---------- 场景渲染 End ----------//

        //---------- GUI Start ----------//
        /// <summary>
        /// GUI绘制时
        /// </summary>
        // public void OnGUI() { }
        //---------- GUI End ----------//

        //---------- 销毁 Start ----------//
        /// <summary>
        /// 禁用或非活跃状态时
        /// </summary>
        // public void OnDisable() { }

        /// <summary>
        /// 销毁时
        /// </summary>
        // public void OnDestroy() { }
        //---------- 销毁 End ----------//

        //---------- 应用 Start ----------//
        /// <summary>
        /// 应用获取焦点时
        /// </summary>
        /// <param name="focus">焦点数据</param>
        // public void OnApplicationFocus(bool focus) { }

        /// <summary>
        /// 应用暂停时
        /// </summary>
        /// <param name="pause">暂停数据</param>
        // public void OnApplicationPause(bool pause) { }

        /// <summary>
        /// 应用退出时
        /// </summary>
        // public void OnApplicationQuit() { }
        //---------- 应用 End ----------//

        #endregion

        #region Tween

        /// <summary>
        /// 重载-添加动效
        /// </summary>
        /// <param name="tween">动效</param>
        protected void AddTween(Sequence tween) { TweenList.Add(tween); }

        /// <summary>
        /// 重载-添加动效
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="tween">动效</param>
        protected void AddTween(string id, Sequence tween) { TweenOnlyList.Add(id, tween); }

        /// <summary>
        /// 重载-移除动效
        /// </summary>
        /// <param name="tween">动效</param>
        protected void RemoveTween(Sequence tween) { TweenList.Remove(tween); }

        /// <summary>
        /// 重载-移除动效
        /// </summary>
        /// <param name="id">标识</param>
        protected void RemoveTween(string id) { TweenOnlyList.Remove(id); }

        /// <summary>
        /// 播放动效
        /// </summary>
        /// <param name="id">标识</param>
        protected void PlayTween(string id = "")
        {
            if (id != string.Empty)
            {
                foreach (KeyValuePair<string, Sequence> item in TweenOnlyList)
                {
                    if (id == item.Key) item.Value?.Play();
                }

                return;
            }

            TweenList.ForEach((Sequence item) => { item?.Play(); });
            foreach (KeyValuePair<string, Sequence> item in TweenOnlyList) item.Value?.Play();
        }

        /// <summary>
        /// 暂停动效
        /// </summary>
        /// <param name="id">标识</param>
        protected void PauseTween(string id = "")
        {
            if (id != string.Empty)
            {
                foreach (KeyValuePair<string, Sequence> item in TweenOnlyList)
                {
                    if (id == item.Key) item.Value?.Pause();
                }

                return;
            }

            TweenList.ForEach((Sequence item) => { item?.Pause(); });
            foreach (KeyValuePair<string, Sequence> item in TweenOnlyList) item.Value?.Pause();
        }

        /// <summary>
        /// 销毁动效
        /// </summary>
        /// <param name="id">标识</param>
        protected void KillTween(string id = "")
        {
            if (id != string.Empty)
            {
                foreach (KeyValuePair<string, Sequence> item in TweenOnlyList)
                {
                    if (id == item.Key) item.Value?.Kill();
                }

                RemoveTween(id);
                return;
            }

            TweenList.ForEach((Sequence item) => { item?.Kill(); });
            TweenList.Clear();

            foreach (KeyValuePair<string, Sequence> item in TweenOnlyList) item.Value?.Kill();
            TweenOnlyList.Clear();
        }

        #endregion
    }
}