using UnityEngine;
using Yarn.Unity;

namespace QinhuaiOldDreams.Dialogue
{
    /// <summary>
    /// 对话管理器 - 简化版（不依赖LineView等UI组件）
    /// 负责：对话控制、Yarn命令注册
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        #region 单例
        private static DialogueManager _instance;
        public static DialogueManager Instance => _instance;
        #endregion

        [Header("Yarn Spinner组件")]
        [SerializeField] private DialogueRunner dialogueRunner;

        [Header("设置")]
        [SerializeField] private bool autoStartDialogue = false;
        [SerializeField] private string startNode = "Start";

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
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // 注册Yarn命令
            if (dialogueRunner != null)
            {
                RegisterYarnCommands();

                if (autoStartDialogue && !string.IsNullOrEmpty(startNode))
                {
                    StartDialogue(startNode);
                }
            }
            else
            {
                Debug.LogWarning("DialogueRunner未设置！对话系统将无法工作。");
            }
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
                Debug.LogError("DialogueRunner未设置！");
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

            isDialogueActive = true;

            // 开始Yarn对话
            dialogueRunner.StartDialogue(nodeName);
        }

        /// <summary>
        /// 结束对话（由DialogueRunner自动调用）
        /// </summary>
        public void OnDialogueComplete()
        {
            Debug.Log("对话结束");

            isDialogueActive = false;

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
        /// </summary>
        private void RegisterYarnCommands()
        {
            if (dialogueRunner == null) return;

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
        private System.Collections.IEnumerator Wait(float seconds)
        {
            Debug.Log($"等待 {seconds} 秒");
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

        #region Yarn变量操作
        /// <summary>
        /// 设置Yarn变量
        /// </summary>
        public void SetYarnVariable(string variableName, object value)
        {
            if (dialogueRunner == null || dialogueRunner.VariableStorage == null) return;

            try
            {
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
            catch (System.Exception e)
            {
                Debug.LogError($"设置Yarn变量失败: {e.Message}");
            }
        }

        /// <summary>
        /// 获取Yarn变量
        /// </summary>
        public bool TryGetYarnVariable<T>(string variableName, out T value)
        {
            value = default;

            if (dialogueRunner == null || dialogueRunner.VariableStorage == null)
                return false;

            try
            {
                return dialogueRunner.VariableStorage.TryGetValue(variableName, out value);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 检查对话是否正在进行
        /// </summary>
        public bool IsDialogueActive()
        {
            return isDialogueActive;
        }

        /// <summary>
        /// 设置DialogueRunner（如果需要动态设置）
        /// </summary>
        public void SetDialogueRunner(DialogueRunner runner)
        {
            dialogueRunner = runner;
            if (runner != null)
            {
                RegisterYarnCommands();
            }
        }
        #endregion
    }
}
