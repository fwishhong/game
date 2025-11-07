using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

namespace QinhuaiOldDreams.Dialogue
{
    /// <summary>
    /// 对话管理器 - 与Yarn Spinner集成
    /// 负责：对话显示、选项处理、对话控制
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        #region 单例
        private static DialogueManager _instance;
        public static DialogueManager Instance => _instance;
        #endregion

        [Header("Yarn Spinner组件")]
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private LineView lineView;
        [SerializeField] private OptionsListView optionsListView;

        [Header("UI组件")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image characterPortrait;
        [SerializeField] private GameObject continueButton;

        [Header("角色立绘")]
        [SerializeField] private Sprite[] characterPortraits; // 在Inspector中设置角色立绘

        [Header("设置")]
        [SerializeField] private float textSpeed = 50f; // 文字显示速度（字符/秒）
        [SerializeField] private bool autoAdvance = false; // 是否自动前进

        private bool isDialogueActive = false;

        #region Unity生命周期
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            // 初始化
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
        }

        private void Start()
        {
            // 注册Yarn命令
            RegisterYarnCommands();
        }
        #endregion

        #region 对话控制
        /// <summary>
        /// 开始对话
        /// </summary>
        public void StartDialogue(string nodeName)
        {
            if (dialogueRunner == null)
            {
                Debug.LogError("DialogueRunner未设置！请在Inspector中指定。");
                return;
            }

            Debug.Log($"开始对话: {nodeName}");

            // 改变游戏状态
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.ChangeState(Core.GameManager.GameState.Dialogue);
            }

            // 禁用玩家移动
            var player = FindObjectOfType<Core.PlayerController>();
            if (player != null)
            {
                player.SetCanMove(false);
            }

            // 显示对话面板
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }

            isDialogueActive = true;

            // 开始Yarn对话
            dialogueRunner.StartDialogue(nodeName);
        }

        /// <summary>
        /// 结束对话
        /// </summary>
        public void EndDialogue()
        {
            Debug.Log("对话结束");

            isDialogueActive = false;

            // 隐藏对话面板
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }

            // 恢复游戏状态
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.ChangeState(Core.GameManager.GameState.Playing);
            }

            // 恢复玩家移动
            var player = FindObjectOfType<Core.PlayerController>();
            if (player != null)
            {
                player.SetCanMove(true);
            }
        }
        #endregion

        #region Yarn命令注册
        /// <summary>
        /// 注册自定义Yarn命令
        /// 这些命令可以在Yarn脚本中调用
        /// </summary>
        private void RegisterYarnCommands()
        {
            if (dialogueRunner == null) return;

            // 设置角色立绘
            // 用法: <<setPortrait 陈默>>
            dialogueRunner.AddCommandHandler<string>(
                "setPortrait",
                SetCharacterPortrait
            );

            // 设置角色名字
            // 用法: <<setName 陈默>>
            dialogueRunner.AddCommandHandler<string>(
                "setName",
                SetCharacterName
            );

            // 等待指定时间
            // 用法: <<wait 2>>
            dialogueRunner.AddCommandHandler<float>(
                "wait",
                Wait
            );

            // 播放音效
            // 用法: <<playSFX door_open>>
            dialogueRunner.AddCommandHandler<string>(
                "playSFX",
                PlaySFX
            );

            // 切换场景
            // 用法: <<changeScene Chapter01>>
            dialogueRunner.AddCommandHandler<string>(
                "changeScene",
                ChangeScene
            );

            Debug.Log("Yarn命令已注册");
        }

        // Yarn命令实现
        private void SetCharacterPortrait(string characterName)
        {
            Debug.Log($"设置角色立绘: {characterName}");

            if (characterPortrait == null) return;

            // TODO: 根据角色名字查找对应的立绘
            // 这里需要一个字典来映射角色名→立绘Sprite
            // 暂时先留空，等添加了立绘资源后再实现

            characterPortrait.gameObject.SetActive(true);
        }

        private void SetCharacterName(string characterName)
        {
            if (nameText != null)
            {
                nameText.text = characterName;
            }
        }

        private System.Collections.IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        private void PlaySFX(string sfxName)
        {
            Debug.Log($"播放音效: {sfxName}");
            // TODO: 调用音频管理器播放音效
        }

        private void ChangeScene(string sceneName)
        {
            Debug.Log($"切换场景: {sceneName}");
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.LoadScene(sceneName);
            }
        }
        #endregion

        #region Yarn变量操作（供外部调用）
        /// <summary>
        /// 设置Yarn变量
        /// </summary>
        public void SetYarnVariable(string variableName, object value)
        {
            if (dialogueRunner == null) return;

            // 根据值的类型设置变量
            if (value is bool boolValue)
            {
                dialogueRunner.VariableStorage.SetValue(variableName, boolValue);
            }
            else if (value is float floatValue)
            {
                dialogueRunner.VariableStorage.SetValue(variableName, floatValue);
            }
            else if (value is string stringValue)
            {
                dialogueRunner.VariableStorage.SetValue(variableName, stringValue);
            }

            Debug.Log($"设置Yarn变量: {variableName} = {value}");
        }

        /// <summary>
        /// 获取Yarn变量
        /// </summary>
        public bool TryGetYarnVariable<T>(string variableName, out T value)
        {
            if (dialogueRunner == null)
            {
                value = default;
                return false;
            }

            return dialogueRunner.VariableStorage.TryGetValue(variableName, out value);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 检查对话是否正在进行
        /// </summary>
        public bool IsDialogueActive()
        {
            return isDialogueActive;
        }
        #endregion
    }
}
