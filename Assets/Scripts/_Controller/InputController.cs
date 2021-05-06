using UnityEngine;
using KILROY.Base;
using KILROY.Model;

namespace KILROY.Controller
{
    public class InputController : BaseControllerBehaviour<InputController>
    {
        #region Parameter

        public static MouseData Mouse { private set; get; } = new MouseData(); // 鼠标数据
        public static KeyboardData Keyboard { private set; get; } = new KeyboardData(); // 鼠标数据
        public static Vector2 InputAxis = Vector2.zero; // 输入向量
        private bool IsOpenInput = false; // 是否打开输入

        #endregion

        #region Cycle

        public void Awake()
        {
            // ShowMouse(false);
        }

        // public void Start() { }

        public void Update()
        {
            if (!IsOpenInput) return;

            UpdateMouse();
            UpdateKeyboard();
        }

        #endregion

        /// <summary>
        /// 打开输入
        /// </summary>
        /// <param name="isOpen"></param>
        public void OpenInput(bool isOpen = true) { IsOpenInput = isOpen; }

        #region Mouse

        /// <summary>
        /// 显示鼠标
        /// </summary>
        /// <param name="isShow">是否显示</param>
        private void ShowMouse(bool isShow = true)
        {
            Cursor.visible = isShow;
            Cursor.lockState = isShow ? CursorLockMode.None : CursorLockMode.Locked;
        }

        /// <summary>
        /// 更新鼠标
        /// </summary>
        private void UpdateMouse()
        {
            #if KILROY_PC
            Mouse.AxisX = Input.GetAxisRaw("Mouse X");
            Mouse.AxisY = Input.GetAxisRaw("Mouse Y");
            Mouse.Left = Input.GetMouseButton(0);
            Mouse.Right = Input.GetMouseButton(1);
            Mouse.Center = Input.GetMouseButton(2);

            // ShowMouse(Input.GetKey(KeyCode.LeftAlt));
            #endif
        }

        #endregion

        #region Keyboard

        /// <summary>
        /// 更新键盘
        /// </summary>
        private void UpdateKeyboard()
        {
            #if KILROY_PC
            Keyboard.AxisX = Input.GetAxisRaw("Horizontal");
            Keyboard.AxisY = Input.GetAxisRaw("Vertical");

            Keyboard.Escape = Input.GetKeyDown(KeyCode.Escape);
            Keyboard.EscapePress = Input.GetKey(KeyCode.Escape);

            Keyboard.Tab = Input.GetKeyDown(KeyCode.Tab);
            Keyboard.TabPress = Input.GetKey(KeyCode.Tab);

            Keyboard.CapsLock = Input.GetKeyDown(KeyCode.CapsLock);
            Keyboard.CapsLockPress = Input.GetKey(KeyCode.CapsLock);

            Keyboard.ShiftLeft = Input.GetKeyDown(KeyCode.LeftShift);
            Keyboard.ShiftLeftPress = Input.GetKey(KeyCode.LeftShift);

            Keyboard.ControlLeft = Input.GetKeyDown(KeyCode.LeftControl);
            Keyboard.ControlLeftPress = Input.GetKey(KeyCode.LeftControl);

            Keyboard.AltLeft = Input.GetKeyDown(KeyCode.LeftAlt);
            Keyboard.AltLeftPress = Input.GetKey(KeyCode.LeftAlt);

            Keyboard.Space = Input.GetKeyDown(KeyCode.Space);
            Keyboard.SpacePress = Input.GetKey(KeyCode.Space);
            #endif

            #if KILROY_PHONE
            Keyboard.Horizontal = InputAxis.x;
            Keyboard.Vertical = InputAxis.y;
            Keyboard.ShiftLeft = InputAxis.magnitude > 0.5f;
            #endif
        }

        #endregion
    }
}