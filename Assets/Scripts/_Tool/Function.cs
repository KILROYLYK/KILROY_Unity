using System;
using Newtonsoft.Json;
using PureMVC.Interfaces;
using XLua;
using KILROY.Application;
using KILROY.Constant;
using KILROY.Constant.Name;
using KILROY.Constant.Enum;
using KILROY.Model;

namespace KILROY.Tool
{
    [Hotfix]
    public static class FN
    {
        #region Parameter

        private static ApplicationFacade AppFacade
        {
            get { return ApplicationFacade.Instance; }
        }

        #endregion

        #region Name

        /// <summary>
        /// 重载-获取名称-Scene
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称</returns>
        public static string GetName(SceneName name) { return "Scene_" + name.ToString(); }

        /// <summary>
        /// 重载-获取名称-Facade
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称</returns>
        public static string GetName(FacadeName name) { return "Facade_" + name.ToString(); }

        /// <summary>
        /// 重载-获取名称-Proxy
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称</returns>
        public static string GetName(ProxyName name) { return "Proxy_" + name.ToString(); }

        /// <summary>
        /// 重载-获取名称-Mediator
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称</returns>
        public static string GetName(MediatorName name) { return "Mediator_" + name.ToString(); }

        #endregion

        #region MVC

        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="name">通知名称</param>
        /// <returns>通知名称</returns>
        public static string GetNotification(Notification notification) { return "Notification_" + notification.ToString(); }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notification">通知</param>
        /// <param name="data">数据</param>
        /// <param name="type">类型</param>
        public static void SendNotification(Notification notification, NotificationData data = null, string type = "")
        {
            string n = GetNotification(notification);

            // 打印通知
            Log("----------");
            Log("通知：" + n);
            if (data != null)
            {
                if (data.Data != null) Log("数据：" + data.Data);
                if (data.Callback != null) Log("回调：" + data.Callback);
            }

            if (type != string.Empty) Log("类型：" + type);
            Log("----------");

            AppFacade.SendNotification(n, data, type);
        }

        /// <summary>
        /// 重载-注册-Proxy
        /// </summary>
        /// <param name="proxy">Proxy</param>
        public static void RegisterMVC(IProxy proxy) { AppFacade.RegisterProxy(proxy); }

        /// <summary>
        /// 重载-注册-Mediator
        /// </summary>
        /// <param name="mediator">Mediator</param>
        public static void RegisterMVC(IMediator mediator) { AppFacade.RegisterMediator(mediator); }

        /// <summary>
        /// 重载-注册-Command
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="command">Command</param>
        public static void RegisterMVC(Notification notification, Func<ICommand> command)
        {
            string n = GetNotification(notification);
            AppFacade.RegisterCommand(n, command);
        }

        /// <summary>
        /// 重载-移除-Proxy
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveMVC(ProxyName name)
        {
            string n = GetName(name);
            AppFacade.RemoveProxy(n);
        }

        /// <summary>
        /// 重载-移除-Mediator
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveMVC(MediatorName name)
        {
            string n = GetName(name);
            AppFacade.RemoveMediator(n);
        }

        /// <summary>
        /// 重载-移除-Command
        /// </summary>
        /// <param name="name">名称</param>
        public static void RemoveMVC(Notification notification)
        {
            string n = GetNotification(notification);
            AppFacade.RemoveCommand(n);
        }

        /// <summary>
        /// 重载-获取-Proxy
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>Proxy</returns>
        public static IProxy RetrieveMVC(ProxyName name)
        {
            string n = GetName(name);
            return AppFacade.RetrieveProxy(n);
        }

        /// <summary>
        /// 重载-获取-Mediator
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>Mediator</returns>
        public static IMediator RetrieveMVC(MediatorName name)
        {
            string n = GetName(name);
            return AppFacade.RetrieveMediator(n);
        }

        #endregion

        #region Object

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="data">对象</param>
        /// <returns>序列化对象</returns>
        public static string SerializeObject(object data) { return JsonConvert.SerializeObject(data); }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">字符串对象</param>
        /// <returns>对象</returns>
        public static T DeserializeObject<T>(string data) { return JsonConvert.DeserializeObject<T>(data); }

        /// <summary>
        /// 根据Key获取对象属性或字段
        /// </summary>
        /// <typeparam name="T">获取参数类型</typeparam>
        /// <param name="target">目标</param>
        /// <param name="key">属性或字段名称</param>
        /// <param name="isProperty">是否是属性</param>
        /// <returns>属性</returns>
        public static T GetObjectValue<T>(object target, string key, bool isProperty = true)
        {
            var type = target.GetType();
            return isProperty
                ? (T)type.GetProperty(key).GetValue(target, null)
                : (T)type.GetField(key).GetValue(target);
        }

        /// <summary>
        /// 根据Key设置对象属性
        /// </summary>
        /// <typeparam name="T">赋值参数类型</typeparam>
        /// <param name="target">目标</param>
        /// <param name="key">属性或字段名称</param>
        /// <param name="value">值</param>
        /// <param name="isProperty">是否是属性</param>
        public static void SetObjectValue<T>(object target, string key, T value, bool isProperty = true)
        {
            var type = target.GetType();

            if (isProperty) type.GetProperty(key).SetValue(target, value);
            else type.GetField(key).SetValue(target, value);
        }

        #endregion

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="o">对象</param>
        public static void Log(object o, LogType type = LogType.Default)
        {
            #if UNITY_EDITOR
            switch (type)
            {
                case LogType.Default:
                    UnityEngine.Debug.Log(o);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(o);
                    break;
                case LogType.Error:
                    UnityEngine.Debug.LogError(o);
                    break;
            }
            #endif
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>实例</returns>
        public static T CreateInstance<T>() { return System.Activator.CreateInstance<T>(); }

        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="callback">回调</param>
        /// <returns>回调</returns>
        public static Action Click(Action callback) { return () => { callback(); }; }

        /// <summary>
        /// 获取随机整数
        /// </summary>
        /// <param name="startInt">开始整数</param>
        /// <param name="endInt">结束整数</param>
        /// <returns>随机整数</returns>
        public static int Random(int startInt, int endInt) { return UnityEngine.Random.Range(startInt, endInt + 1); }

        /// <summary>
        /// 获取随机小数
        /// </summary>
        /// <param name="startFloat">开始整数</param>
        /// <param name="endFloat">结束整数</param>
        /// <returns>随机小数</returns>
        public static float Random(float startFloat, float endFloat) { return UnityEngine.Random.Range(startFloat, endFloat); }
    }
}