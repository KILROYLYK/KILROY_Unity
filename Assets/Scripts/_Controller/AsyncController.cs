using System;
using System.Collections;
using System.Collections.Generic;
using KILROY.Base;

namespace KILROY.Controller
{
    public class AsyncController : BaseControllerBehaviour<AsyncController>
    {
        #region Parameter

        private Dictionary<int, IEnumerator> EnumeratorList = new Dictionary<int, IEnumerator>(); // 协同程序列表
        private int Count = 0; // 协同程序总数

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        // public void Update() { }

        #endregion

        #region Collaboration

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <param name="id">协同程序ID</param>
        /// <param name="awaitable">AsyncOperation || YieldInstruction</param>
        /// <param name="callback">回调</param>
        /// <returns>枚举器</returns>
        private IEnumerator Enumerator(int id, object awaitable, Action callback)
        {
            yield return awaitable;
            EnumeratorList.Remove(id);
            callback();
        }

        /// <summary>
        /// 开始协同程序
        /// </summary>
        /// <param name="awaitable">AsyncOperation || YieldInstruction</param>
        /// <param name="callback">回调</param>
        /// <returns>协同ID</returns>
        public int StartCollaboration(object awaitable, Action callback)
        {
            Count++;

            IEnumerator enumerator = Enumerator(Count, awaitable, callback);

            EnumeratorList.Add(Count, enumerator);
            StartCoroutine(enumerator);

            return Count;
        }

        /// <summary>
        /// 停止协同程序
        /// </summary>
        /// <param name="id">协同程序ID</param>
        public void StopCollaboration(int id = 0)
        {
            if (id == 0) // 停止全部协同程序
            {
                Count = 0;
                foreach (KeyValuePair<int, IEnumerator> item in EnumeratorList) StopCoroutine(item.Value);
                EnumeratorList.Clear();
                return;
            }

            StopCoroutine(EnumeratorList[id]); // 停止单个协同程序
            EnumeratorList.Remove(id);
        }

        #endregion
    }
}