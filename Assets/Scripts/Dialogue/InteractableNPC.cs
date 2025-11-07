using UnityEngine;

namespace QinhuaiOldDreams.Dialogue
{
    /// <summary>
    /// 可交互NPC - 改进版
    /// 将这个脚本挂载到NPC物体上，玩家靠近按E就能对话
    /// </summary>
    public class InteractableNPC : MonoBehaviour, Core.IInteractable
    {
        [Header("NPC设置")]
        [SerializeField] private string npcName = "王掌柜";
        [Tooltip("对应的Yarn对话节点名称，例如：Start")]
        [SerializeField] private string yarnNodeName = "Start";

        [Header("UI设置")]
        [SerializeField] private GameObject dialoguePanel; // 对话面板（需要在Inspector中设置）

        [Header("可交互设置")]
        [SerializeField] private bool canInteract = true;
        [SerializeField] private bool oneTimeOnly = false;
        private bool hasInteracted = false;

        #region Unity生命周期
        private void Start()
        {
            // 如果没有手动设置对话面板，尝试自动查找
            if (dialoguePanel == null)
            {
                dialoguePanel = GameObject.Find("DialoguePanel");
                if (dialoguePanel != null)
                {
                    Debug.Log($"{npcName}: 自动找到对话面板");
                }
            }
        }
        #endregion

        #region IInteractable接口实现
        public void Interact(Core.PlayerController player)
        {
            if (!canInteract) return;

            if (oneTimeOnly && hasInteracted)
            {
                Debug.Log($"{npcName}: 已经交互过了");
                return;
            }

            StartDialogue();

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

            // 显示对话面板
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }

            // 启动对话
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(yarnNodeName);
            }
            else
            {
                Debug.LogError("找不到DialogueManager！");
            }

            Debug.Log($"与 {npcName} 开始对话");
        }
        #endregion

        #region 触发器检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"玩家靠近了 {npcName}，按E交互");
                // TODO: 显示"按E交互"提示
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"玩家离开了 {npcName}");
                // TODO: 隐藏交互提示
            }
        }
        #endregion

        #region 公共方法
        public void SetCanInteract(bool value)
        {
            canInteract = value;
        }

        public void SetDialogueNode(string newNodeName)
        {
            yarnNodeName = newNodeName;
            Debug.Log($"{npcName} 对话节点更新为: {newNodeName}");
        }

        public void SetDialoguePanel(GameObject panel)
        {
            dialoguePanel = panel;
        }
        #endregion
    }
}
