using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using KILROY.Base;
using KILROY.Tool;

namespace KILROY.Controller
{
    public class NetController : BaseControllerBehaviour<NetController>
    {
        #region Parameter

        private List<int> NetList = new List<int>(); // 请求列表

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 创建请求
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="mode">模式</param>
        /// <returns>请求</returns>
        private UnityWebRequest CreateRequest(string route, string mode)
        {
            UnityWebRequest request = new UnityWebRequest(route, mode);
            request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            request.SetRequestHeader("Authorization", string.Empty);
            request.SetRequestHeader("Client-Version", string.Empty);
            request.SetRequestHeader("Client-Platform", string.Empty);
            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>数据</returns>
        private UploadHandlerRaw CreateData(object data = null)
        {
            if (data == null) return null;

            string jsonData = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

            return new UploadHandlerRaw(bodyRaw);
        }

        /// <summary>
        /// 结果处理
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="data">数据</param>
        /// <param name="request">请求</param>
        /// <param name="callback">回调</param>
        private void HandleResult<T>(string route, object data, UnityWebRequest request, Action<T> callback = null)
        {
            string title = "成功";

            if (request.result == UnityWebRequest.Result.ProtocolError) title = "失败：协议错误"; // 协议错误
            if (request.result == UnityWebRequest.Result.ConnectionError) title = "失败：链接错误"; // 链接错误

            FN.Log("//////////");
            FN.Log("请求" + title);
            FN.Log("路由：" + route);
            FN.Log("数据：" + JsonUtility.ToJson(data));
            FN.Log("//////////");

            if (callback != null) callback(JsonUtility.FromJson<T>(request.downloadHandler.text));
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="data">数据</param>
        /// <param name="callback">回调</param>
        /// <returns>请求ID</returns>
        public int Send<T>(string route, object data, Action<T> callback = null)
        {
            UnityWebRequest request = CreateRequest(route, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = CreateData(data);
            int collaboration = 0;

            collaboration = AsyncController.Instance.StartCollaboration
            (
                request.SendWebRequest(),
                () =>
                {
                    NetList.Remove(collaboration);
                    HandleResult(route, data, request, callback);
                }
            );
            NetList.Add(collaboration);
            return collaboration;
        }

        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="id">请求ID</param>
        public void Clean(int id = 0)
        {
            if (id == 0)
            {
                NetList.ForEach((int item) => { });
                NetList.Clear();
                return;
            }

            NetList.ForEach
            (
                (int item) =>
                {
                    if (id != item) return;

                    AsyncController.Instance.StopCollaboration(item);
                    NetList.Remove(item);
                }
            );
        }
    }
}