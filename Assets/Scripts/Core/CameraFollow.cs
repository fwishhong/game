using UnityEngine;

namespace QinhuaiOldDreams.Core
{
    /// <summary>
    /// 摄像机跟随脚本
    /// 挂载到Main Camera上，让摄像机跟随玩家
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [Header("跟随目标")]
        [SerializeField] private Transform target; // 如果不设置，会自动查找Player

        [Header("跟随设置")]
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); // Z轴-10保持2D视角
        [SerializeField] private float smoothSpeed = 5f; // 平滑速度，越大越快

        [Header("边界限制（可选）")]
        [SerializeField] private bool useBounds = false;
        [SerializeField] private float minX = -10f;
        [SerializeField] private float maxX = 10f;
        [SerializeField] private float minY = -10f;
        [SerializeField] private float maxY = 10f;

        private void Start()
        {
            // 如果没有设置目标，自动查找Player
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                    Debug.Log("摄像机找到玩家：" + player.name);
                }
                else
                {
                    Debug.LogWarning("找不到Player！请确保玩家物体的Tag是'Player'");
                }
            }
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // 计算目标位置
            Vector3 desiredPosition = target.position + offset;

            // 边界限制（如果启用）
            if (useBounds)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
            }

            // 平滑移动
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );

            // 确保Z轴不变（2D游戏）
            smoothedPosition.z = offset.z;

            transform.position = smoothedPosition;
        }

        /// <summary>
        /// 立即跳转到目标位置（不平滑）
        /// </summary>
        public void SnapToTarget()
        {
            if (target == null) return;

            Vector3 targetPosition = target.position + offset;

            if (useBounds)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }

            targetPosition.z = offset.z;
            transform.position = targetPosition;
        }

        /// <summary>
        /// 设置跟随目标
        /// </summary>
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        /// <summary>
        /// 设置摄像机边界
        /// </summary>
        public void SetBounds(float minX, float maxX, float minY, float maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            useBounds = true;
        }

        // 在Scene视图中绘制边界（方便调试）
        private void OnDrawGizmosSelected()
        {
            if (!useBounds) return;

            Gizmos.color = Color.yellow;

            // 绘制边界框
            Vector3 center = new Vector3(
                (minX + maxX) / 2f,
                (minY + maxY) / 2f,
                0
            );

            Vector3 size = new Vector3(
                maxX - minX,
                maxY - minY,
                1
            );

            Gizmos.DrawWireCube(center, size);
        }
    }
}
