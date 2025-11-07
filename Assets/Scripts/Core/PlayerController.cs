using UnityEngine;

namespace QinhuaiOldDreams.Core
{
    /// <summary>
    /// 玩家控制器
    /// 负责：角色移动、交互、动画控制
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("移动设置")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private bool canMove = true;

        [Header("交互设置")]
        [SerializeField] private float interactRange = 2f;
        [SerializeField] private LayerMask interactableLayer;

        [Header("组件引用")]
        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        // 移动输入
        private Vector2 moveInput;
        private Vector2 lastMoveDirection = Vector2.down;

        // 交互目标
        private GameObject nearestInteractable;

        #region Unity生命周期
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            // 设置Rigidbody2D
            rb.gravityScale = 0; // 2D俯视角不需要重力
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 禁止旋转
        }

        private void Update()
        {
            if (!canMove) return;

            // 获取输入
            GetInput();

            // 检测可交互物体
            CheckForInteractables();

            // 更新动画
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (!canMove)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            // 移动
            Move();
        }
        #endregion

        #region 输入处理
        private void GetInput()
        {
            // WASD 或 方向键
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            moveInput = new Vector2(horizontal, vertical).normalized;

            // 记录最后的移动方向（用于交互时的朝向）
            if (moveInput != Vector2.zero)
            {
                lastMoveDirection = moveInput;
            }

            // 交互按键（E键或空格）
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                TryInteract();
            }
        }
        #endregion

        #region 移动
        private void Move()
        {
            // 使用Rigidbody2D移动（物理移动，可以碰撞）
            rb.velocity = moveInput * moveSpeed;

            // 翻转精灵（面向移动方向）
            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true; // 向左
            }
            else if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false; // 向右
            }
        }

        /// <summary>
        /// 设置是否可以移动
        /// </summary>
        public void SetCanMove(bool value)
        {
            canMove = value;
            if (!canMove)
            {
                rb.velocity = Vector2.zero;
                moveInput = Vector2.zero;
            }
        }
        #endregion

        #region 动画
        private void UpdateAnimation()
        {
            if (animator == null) return;

            // 计算移动速度
            float speed = moveInput.magnitude;

            // 设置Animator参数
            animator.SetFloat("Speed", speed);
            animator.SetFloat("Horizontal", lastMoveDirection.x);
            animator.SetFloat("Vertical", lastMoveDirection.y);

            // 设置是否在移动
            animator.SetBool("IsMoving", speed > 0.01f);
        }
        #endregion

        #region 交互系统
        private void CheckForInteractables()
        {
            // 在玩家前方检测可交互物体
            Vector2 rayOrigin = transform.position;
            Vector2 rayDirection = lastMoveDirection;

            // 使用圆形检测（更宽容）
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                rayOrigin + rayDirection * interactRange * 0.5f,
                interactRange,
                interactableLayer
            );

            // 找到最近的可交互物体
            float nearestDistance = float.MaxValue;
            nearestInteractable = null;

            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = hit.gameObject;
                }
            }

            // TODO: 显示交互提示UI（当有可交互物体时）
            if (nearestInteractable != null)
            {
                // 显示 "按E交互" 提示
            }
        }

        private void TryInteract()
        {
            if (nearestInteractable == null) return;

            // 尝试获取可交互接口
            IInteractable interactable = nearestInteractable.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact(this);
                Debug.Log($"与 {nearestInteractable.name} 交互");
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 传送到指定位置
        /// </summary>
        public void TeleportTo(Vector3 position)
        {
            transform.position = position;
            rb.velocity = Vector2.zero;
        }

        /// <summary>
        /// 获取当前朝向
        /// </summary>
        public Vector2 GetFacingDirection()
        {
            return lastMoveDirection;
        }
        #endregion

        #region Debug绘制
        private void OnDrawGizmos()
        {
            // 绘制交互范围
            Gizmos.color = Color.yellow;
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = lastMoveDirection;

            Gizmos.DrawWireSphere(
                rayOrigin + rayDirection * interactRange * 0.5f,
                interactRange
            );
        }
        #endregion
    }

    /// <summary>
    /// 可交互接口 - 所有可交互物体都需要实现这个接口
    /// </summary>
    public interface IInteractable
    {
        void Interact(PlayerController player);
    }
}
