using UnityEngine;
using KILROY.Base;

namespace KILROY.Project.View
{
    public class Rotate : BaseBehaviour
    {
        #region Parameter

        [SerializeField] private float Speed; // 速度

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        public void Update() { transform.Rotate(0, Speed * Time.deltaTime, 0); }

        #endregion
    }
}