using UnityEngine;

namespace QinhuaiOldDreams.Dialogue
{
    /// <summary>
    /// 可交互NPC - 示例脚本
    /// 将这个脚本挂载到NPC物体上，玩家靠近按E就能对话
    /// </summary>
    public class InteractableNPC : MonoBehaviour, Core.IInteractable
    {
        [Header("NPC设置")]
        [SerializeField] private string npcName = "王掌柜";
        [Tooltip("对应的Yarn对话节点名称，例如：wang_fugui_talk_01")]
        [SerializeField] private string yarnNodeName = "wang_fugui_talk_01";

        [Header("交互提示")]
        [SerializeField] private GameObject interactPrompt; // 可选：显示"按E交互"的UI

        [Header("可交互设置")]
        [SerializeField] private bool canInteract = true;
        [SerializeField] private bool oneTimeOnly = false; // 是否只能交互一次
        private bool hasInteracted = false;

        #region Unity生命周期
        private void Start()
        {
            // 隐藏交互提示
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
        #endregion

        #region IInteractable接口实现
        /// <summary>
        /// 当玩家按E交互时调用
        /// </summary>
        public void Interact(Core.PlayerController player)
        {
            if (!canInteract) return;

            // 如果是一次性交互，检查是否已交互过
            if (oneTimeOnly && hasInteracted)
            {
                Debug.Log($"{npcName}: 已经交互过了");
                return;
            }

            // 开始对话
            StartDialogue();

            // 标记为已交互
            if (oneTimeOnly)
            {
                hasInteracted = true;
            }
        }
        #endregion

        #region 对话控制
        private void StartDialogue()
        {
            if (string.IsNullOrEmpty(yarnNodeName))
            {
                Debug.LogWarning($"{npcName}: 未设置Yarn对话节点！");
                return;
            }

            if (DialogueManager.Instance == null)
            {
                Debug.LogError("找不到DialogueManager！");
                return;
            }

            // 朝向玩家
            TurnTowardsPlayer();

            // 开始对话
            DialogueManager.Instance.StartDialogue(yarnNodeName);

            Debug.Log($"与 {npcName} 开始对话");
        }

        private void TurnTowardsPlayer()
        {
            // TODO: 让NPC转向面对玩家
            // 如果NPC有SpriteRenderer，可以根据玩家位置翻转
            var player = FindObjectOfType<Core.PlayerController>();
            if (player != null)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;

                var spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // 玩家在左边，翻转精灵
                    spriteRenderer.flipX = directionToPlayer.x < 0;
                }
            }
        }
        #endregion

        #region 触发器检测（可选：用于显示交互提示）
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // 显示交互提示
                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // 隐藏交互提示
                if (interactPrompt != null)
                {
                    interactPrompt.SetActive(false);
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 启用/禁用交互
        /// </summary>
        public void SetCanInteract(bool value)
        {
            canInteract = value;
        }

        /// <summary>
        /// 更改对话节点（例如：根据剧情进度）
        /// </summary>
        public void SetDialogueNode(string newNodeName)
        {
            yarnNodeName = newNodeName;
            Debug.Log($"{npcName} 对话节点更新为: {newNodeName}");
        }
        #endregion
    }
}
