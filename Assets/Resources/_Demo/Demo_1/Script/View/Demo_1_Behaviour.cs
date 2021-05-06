using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using KILROY.Constant;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Controller;
using KILROY.Project.View;

namespace KILROY.Project.Demo
{
    partial class SnakeData // 蛇数据
    {
        public bool IsActive = true; // 是否活动
        public int Score = 0; // 得分
        public int Length = 0; // 长度
        public float Direction = 0; // 方向
        public Transform Box = null; // 身体容器
        public Transform Head = null; // 头
        public Transform Tail = null; // 尾
        public Transform Perspective = null; // 视角
    }

    public class Demo_1_Behaviour : ProjectBehaviour
    {
        #region Parameter

        [SerializeField] private RawImage Image = null; // 地图图片

        private KeyboardData Keyboard = InputController.Keyboard; // 键盘数据
        private Camera CameraSnake = null; // 蛇相机
        private SnakeData Role = null; // 主角
        private Dictionary<string, GameObject> PrefabList = new Dictionary<string, GameObject>(); // 预制件列表
        private Dictionary<string, SnakeData> SnakeList = new Dictionary<string, SnakeData>(); // 蛇列表

        #endregion

        #region Cycle

        public void Awake()
        {
            SwitchList.Add("CameraFly", true); // 相机

            FloatList.Add("CameraHeight", -80); // 相机顶部高度
            FloatList.Add("CameraSpeed", 10); // 相机速度
            FloatList.Add("SnakeInit", 0); // 初始长度
            FloatList.Add("MoveSpeed", 3); // 移动速度
            FloatList.Add("RotationSpeed", 2); // 旋转速度
            FloatList.Add("FollowSpeed", 3); // 跟随速度

            if (ApplicationData.Mode == AppMode.DevelopSelf)
            {
                FN.SendNotification(Notification.InitController);
                FN.SendNotification(Notification.InitScene);

                InputController.Instance.OpenInput();
            }

            FN.SendNotification(Notification.InitSceneDemo_1, new NotificationData() { Data = this });
        }

        public void Start()
        {
            InitResource();
            ShowSnake();
            CreateSnake();

            Sequence tween = DOTween.Sequence();
            tween.AppendInterval(3);
            tween.AppendCallback(() => { AddSnakeBody(Role); });
            tween.SetLoops(-1);
        }

        public void Update()
        {
            FollowSnake();

            foreach (KeyValuePair<string, SnakeData> snake in SnakeList)
            {
                SnakeData data = snake.Value;

                if (!data.IsActive) continue;

                UpdateSnake(data);
            }

            if (Keyboard.Move)
            {
                Role.Direction =
                    (SwitchList["CameraFly"] ? 0 : CameraSnake.transform.localEulerAngles.y) +
                    UIFN.GetAxiaDirection(Keyboard.AxisX, Keyboard.AxisY);
            }

            SwitchList["CameraFly"] = !Keyboard.Space;
        }

        public void OnDestroy() { FN.SendNotification(Notification.CleanSceneDemo_1); }

        #endregion

        /// <summary>
        /// 初始化资源
        /// </summary>
        private void InitResource()
        {
            CameraSnake = Demo_1_Data.Camera[Demo_1_Camera.Snake];
            PrefabList.Add("Box", Resources.Load<GameObject>("_Demo/Demo_1/Prefab/Role/Snake/SnakeBox"));
            PrefabList.Add("Body", Resources.Load<GameObject>("_Demo/Demo_1/Prefab/Role/Snake/SnakeBody"));
        }

        /// <summary>
        /// 显示蛇
        /// </summary>
        private void ShowSnake()
        {
            RenderTexture texture = new RenderTexture(Screen.width, Screen.height, 0);

            CameraSnake.transform.localPosition = new Vector3(0, FloatList["CameraHeight"], 0);
            CameraSnake.targetTexture = texture;
            Image.texture = texture;
        }

        /// <summary>
        /// 跟随蛇
        /// </summary>
        private void FollowSnake()
        {
            if (Role == null) return;

            Transform camera = CameraSnake.transform;
            Vector3 position = Role.Head.position;
            Vector3 rotation = Role.Head.rotation.eulerAngles;

            if (SwitchList["CameraFly"])
            {
                UIFN.MoveTween(camera, new Vector3(position.x, FloatList["CameraHeight"], position.z), FloatList["CameraSpeed"]);
                UIFN.RotateTween(camera, new Vector3(90, 0, 0), FloatList["CameraSpeed"]);
            }
            else
            {
                UIFN.MoveTween(camera, Role.Perspective.position, FloatList["CameraSpeed"]);
                UIFN.RotateTween(camera, new Vector3(10, rotation.y, 0), FloatList["CameraSpeed"]);
            }
        }

        /// <summary>
        /// 创建蛇
        /// </summary>
        private void CreateSnake()
        {
            Transform box = Instantiate<GameObject>(PrefabList["Box"]).transform;
            SnakeData data = new SnakeData();
            int length = SnakeList.Count;
            string name = "Snake_" + length;

            box.name = name;
            box.localPosition = new Vector3(0, -99, 0);
            box.SetParent(Demo_1_Data.Container.BoxRole);
            data.Box = box;
            data.Head = box.Find("SnakeHead");
            data.Tail = box.Find("SnakeTail_1");
            data.Perspective = data.Head.Find("Perspective");
            SnakeList.Add(name, data);

            ResetSnake(data);

            if (length == 0) Role = data;
        }

        /// <summary>
        /// 添加蛇节点
        /// </summary>
        /// <param name="add">添加节点个数</param>
        private void AddSnakeBody(SnakeData data, int add = 1)
        {
            for (int i = 0; i < add; i++)
            {
                Transform body = Instantiate<GameObject>(PrefabList["Body"]).transform;
                body.position = data.Tail.position;
                body.rotation = data.Tail.rotation;
                body.SetParent(data.Box);
                body.SetSiblingIndex(data.Tail.GetSiblingIndex());
                data.Length++;
            }
        }

        /// <summary>
        /// 清除蛇节点
        /// </summary>
        /// <param name="data"></param>
        private void RemoveSnakeBody(SnakeData data)
        {
            foreach (Transform item in data.Box)
            {
                if (item.name.IndexOf("Body") > -1) Destroy(item);
            }
        }

        /// <summary>
        /// 重置蛇
        /// </summary>
        /// <param name="data">蛇数据</param>
        /// <param name="isArrange">是否整理</param>
        private void ResetSnake(SnakeData data)
        {
            int init = (int)FloatList["SnakeInit"];

            data.Length = init;
            data.Direction = FN.Random(0, 360);

            RemoveSnakeBody(data);
            AddSnakeBody(data, init);

            Vector3 position = new Vector3();
            Quaternion rotation = Quaternion.Euler(new Vector3(0, data.Direction, 0));
            foreach (Transform item in data.Box)
            {
                item.localPosition = position;
                item.localRotation = rotation;
            }
        }

        /// <summary>
        /// 更新蛇
        /// </summary>
        /// <param name="data">蛇数据</param>
        private void UpdateSnake(SnakeData data)
        {
            List<Vector3> position = new List<Vector3>();
            List<Quaternion> rotation = new List<Quaternion>();
            int i = 0;

            foreach (Transform item in data.Box)
            {
                position.Add(item.position);
                rotation.Add(item.rotation);

                if (i == 0)
                {
                    UIFN.MoveForward(item, FloatList["MoveSpeed"]);
                    UIFN.RotateTween(item, new Vector3(0, data.Direction, 0), FloatList["RotationSpeed"]);
                }
                else
                {
                    UIFN.MoveTween(
                        item,
                        position[i - 1],
                        i > data.Length
                            ? (1 - (data.Length - i) * 0.4f) * FloatList["FollowSpeed"]
                            : FloatList["FollowSpeed"]
                    );
                    UIFN.RotateTween(item, new Vector3(0, rotation[i - 1].eulerAngles.y, 0), FloatList["FollowSpeed"]);
                }

                i++;
            }
        }
    }
}