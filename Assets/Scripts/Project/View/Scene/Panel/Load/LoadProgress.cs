using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KILROY.Base;

namespace KILROY.Project.View
{
    public class LoadProgress : BaseBehaviour
    {
        #region Parameter

        private Transform Progress = null; // 进度
        private RectTransform ProgressRect = null; // 进度形状
        private RectTransform ProgressBarRect = null; // 进度条形状
        private Text TextPercentage = null; // 文案-进度
        private Text TextLoad = null; // 文案-加载
        private Text TextTotal = null; // 文案-总数
        private Dictionary<AsyncOperation, Action> AsyncList = new Dictionary<AsyncOperation, Action>(); // 异步

        #endregion

        #region Cycle

        public void Awake()
        {
            FloatList.Add("Fade", 1); // 显隐时间
            FloatList.Add("Delay", 1); // 等待时间
            FloatList.Add("DelayFlag", 1); // 累积等待时间

            Progress = transform.Find("Progress");
            ProgressRect = Progress.GetComponent<RectTransform>();
            ProgressBarRect = Progress.Find("ProgressBar").GetComponent<RectTransform>();
            TextPercentage = Progress.Find("BoxText/TextPercentage").GetComponent<Text>();
            TextLoad = Progress.Find("BoxText/TextLoad").GetComponent<Text>();
            TextTotal = Progress.Find("BoxText/TextTotal").GetComponent<Text>();
        }

        // public void Start() { }

        public void Update()
        {
            if (FloatList["DelayFlag"] < FloatList["Delay"]) FloatList["DelayFlag"] += Time.deltaTime;
            if (FloatList["DelayFlag"] >= FloatList["Delay"] && AsyncList.Count > 0) UpdateProgress(AsyncList.Keys.First(), AsyncList.Values.First());
        }

        public void OnDestroy() { KillTween(); }

        #endregion

        /// <summary>
        /// 初始化进度
        /// </summary>
        private void InitProgress()
        {
            FloatList["DelayFlag"] = 0;
            ProgressBarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            TextPercentage.text = "0%";
            TextLoad.text = "正在加载";
            TextTotal.text = "（0/0）";
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="async"></param>
        /// <param name="callback"></param>
        private void UpdateProgress(AsyncOperation async, Action callback = null)
        {
            ProgressBarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ProgressRect.rect.width * async.progress);

            TextPercentage.text = async.progress * 100 + "%";
            TextLoad.text = "正在加载";
            TextTotal.text = "（0/0）";

            if (async.isDone)
            {
                HideProgress();
                callback?.Invoke();
            }
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="async">异步</param>
        /// <param name="callback">回调</param>
        public void ShowProgress(AsyncOperation async, Action callback = null)
        {
            InitProgress();

            Progress.gameObject.SetActive(true);
            AsyncList.Add(async, callback);
        }

        /// <summary>
        /// 隐藏进度
        /// </summary>
        private void HideProgress()
        {
            Progress.gameObject.SetActive(false);
            AsyncList.Remove(AsyncList.Keys.First());
        }
    }
}