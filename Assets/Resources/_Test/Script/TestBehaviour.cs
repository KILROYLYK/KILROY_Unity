using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using KILROY.Constant.Resource;
using KILROY.Tool;
using KILROY.Manager;
using KILROY.Controller;
using KILROY.Util;
using KILROY.Project.View;

namespace KILROY.Test.View
{
    public class TestBehaviour : ProjectBehaviour
    {
        #if KILROY_TEST

        #region Cycle

        public void Awake()
        {
            // DebugController.Instance.Open();
            // Click();
            // Drag();
            // Video();
            Live2D();
        }

        // public void Start() { }

        // public void Update() { }

        #endregion

        #region Test

        /// <summary>
        /// 测试-点击
        /// </summary>
        private void Click()
        {
            ClickComponent click = GameObject.Find("TestLive2D/CanvasButton/_Click").GetComponent<ClickComponent>();
            click.Continuous();
            click.onClick.AddListener((PointerEventData eventData) => { FN.Log("Click"); });
            click.onDoubleClick.AddListener((PointerEventData eventData) => { FN.Log("DoubleClick"); });
            click.onLongPress.AddListener((PointerEventData eventData) => { FN.Log("LongPress"); });
        }

        /// <summary>
        /// 测试-拖拽
        /// </summary>
        private void Drag()
        {
            DragComponent drag = GameObject.Find("TestLive2D/CanvasButton/_Drag").GetComponent<DragComponent>();
            drag.onBeginDrag.AddListener((PointerEventData eventData) => { FN.Log("BeginDrag"); });
            drag.onDrag.AddListener((PointerEventData eventData) => { FN.Log("Drag"); });
            drag.onEndDrag.AddListener((PointerEventData eventData) => { FN.Log("EndDrag"); });
        }

        /// <summary>
        /// 测试-视频
        /// </summary>
        private void Video()
        {
            VideoController.Instance.Create(
                VideoName.TestVideo,
                transform.Find("TestVideoPlayer/Canvas/RawImage").GetComponent<RawImage>(),
                ResourceManager.GetImage(ImageName.TestVideo),
                (VideoPlayer player) => { },
                (VideoPlayer player) => { VideoController.Instance.Play(VideoName.TestVideo); }
            );

            VideoController.Instance.Play(VideoName.TestVideo);
        }

        /// <summary>
        /// 测试-Live2D
        /// </summary>
        private void Live2D()
        {
            Live2DController.Instance.SetShowImage(transform.Find("TestLive2D/Canvas/RawImage").GetComponent<RawImage>());
            Live2DController.Instance.Create(Live2DName.TestKing, "_Test/Prefab/Live2D/King");
            Live2DController.Instance.Show(Live2DName.TestKing);

            UIFN.ShowAnimatorButton(
                Live2DController.Instance.Get(Live2DName.TestKing),
                transform.Find("TestLive2D/Canvas/")
            );
        }

        #endregion

        #endif
    }
}