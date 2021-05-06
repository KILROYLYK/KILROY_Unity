using XLua;
using KILROY.Base;
using KILROY.Tool;

namespace KILROY.Controller
{
    public class LuaController : BaseControllerBehaviour<LuaController>
    {
        #region Parameter

        public LuaEnv LuaEnv = new LuaEnv();

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 检查
        /// </summary>
        private void Check()
        {
            FN.Log("----------");
            FN.Log("XLua-热更新启动失败");
            FN.Log("----------");
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="code">Lua代码</param>
        public void DO(string code) { LuaEnv.DoString(code); }

        /// <summary>
        /// 热更新
        /// </summary>
        public void Hotfix()
        {
            DO("require 'Main'");
            Check();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy() { LuaEnv.Dispose(); }
    }
}