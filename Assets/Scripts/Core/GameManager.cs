using UnityEngine;
using UnityEngine.SceneManagement;

namespace QinhuaiOldDreams.Core
{
    /// <summary>
    /// 游戏核心管理器 - 单例模式
    /// 负责：游戏状态管理、场景切换、全局数据访问
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 单例模式
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region 游戏状态
        public enum GameState
        {
            MainMenu,      // 主菜单
            Playing,       // 游戏中
            Dialogue,      // 对话中
            Paused,        // 暂停
            Inventory,     // 物品界面
            Diary          // 日记界面
        }

        [Header("游戏状态")]
        [SerializeField] private GameState currentState = GameState.MainMenu;
        public GameState CurrentState => currentState;
        #endregion

        #region 时间线管理
        [Header("时间线设置")]
        [SerializeField] private TimelineType currentTimeline = TimelineType.Modern2024;

        public enum TimelineType
        {
            Modern2024,    // 2024年现代
            Historical1937 // 1937年历史
        }

        public TimelineType CurrentTimeline => currentTimeline;

        /// <summary>
        /// 切换时间线（重要的游戏机制）
        /// </summary>
        public void SwitchTimeline(TimelineType newTimeline)
        {
            currentTimeline = newTimeline;
            Debug.Log($"时间线切换至：{newTimeline}");
            // TODO: 触发时间线切换的视觉效果
            // TODO: 切换音乐
            // TODO: 切换场景氛围
        }
        #endregion

        #region Unity生命周期
        private void Awake()
        {
            // 单例检查
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeGame();
        }

        private void Start()
        {
            Debug.Log("《秦淮旧梦》游戏管理器已启动");
        }

        private void Update()
        {
            // 全局按键检测
            HandleGlobalInput();
        }
        #endregion

        #region 初始化
        private void InitializeGame()
        {
            // 设置目标帧率
            Application.targetFrameRate = 60;

            // 设置屏幕不休眠
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Debug.Log("游戏初始化完成");
        }
        #endregion

        #region 状态管理
        /// <summary>
        /// 改变游戏状态
        /// </summary>
        public void ChangeState(GameState newState)
        {
            if (currentState == newState) return;

            Debug.Log($"游戏状态: {currentState} → {newState}");

            GameState previousState = currentState;
            currentState = newState;

            // 根据状态变化执行相应操作
            OnStateChanged(previousState, newState);
        }

        private void OnStateChanged(GameState from, GameState to)
        {
            switch (to)
            {
                case GameState.Playing:
                    Time.timeScale = 1f;
                    break;

                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;

                case GameState.Dialogue:
                    // 对话时禁用玩家移动
                    Time.timeScale = 1f;
                    break;

                case GameState.Inventory:
                case GameState.Diary:
                    Time.timeScale = 0f; // 打开界面时暂停游戏
                    break;
            }
        }
        #endregion

        #region 场景管理
        /// <summary>
        /// 加载场景
        /// </summary>
        public void LoadScene(string sceneName)
        {
            Debug.Log($"加载场景: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// 异步加载场景（带加载界面）
        /// </summary>
        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }

        private System.Collections.IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            // TODO: 显示加载界面

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                Debug.Log($"加载进度: {progress * 100}%");
                // TODO: 更新加载界面进度条
                yield return null;
            }

            // TODO: 隐藏加载界面
        }
        #endregion

        #region 全局输入
        private void HandleGlobalInput()
        {
            // ESC键：暂停/返回
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentState == GameState.Playing)
                {
                    PauseGame();
                }
                else if (currentState == GameState.Paused)
                {
                    ResumeGame();
                }
            }

            // Tab键：打开日记
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (currentState == GameState.Playing)
                {
                    OpenDiary();
                }
                else if (currentState == GameState.Diary)
                {
                    CloseDiary();
                }
            }

            // I键：打开物品栏
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (currentState == GameState.Playing)
                {
                    OpenInventory();
                }
                else if (currentState == GameState.Inventory)
                {
                    CloseInventory();
                }
            }
        }
        #endregion

        #region 游戏控制
        public void PauseGame()
        {
            ChangeState(GameState.Paused);
            Debug.Log("游戏已暂停");
            // TODO: 显示暂停菜单
        }

        public void ResumeGame()
        {
            ChangeState(GameState.Playing);
            Debug.Log("游戏已继续");
            // TODO: 隐藏暂停菜单
        }

        public void OpenDiary()
        {
            ChangeState(GameState.Diary);
            // TODO: 打开日记UI
        }

        public void CloseDiary()
        {
            ChangeState(GameState.Playing);
            // TODO: 关闭日记UI
        }

        public void OpenInventory()
        {
            ChangeState(GameState.Inventory);
            // TODO: 打开物品栏UI
        }

        public void CloseInventory()
        {
            ChangeState(GameState.Playing);
            // TODO: 关闭物品栏UI
        }

        public void QuitGame()
        {
            Debug.Log("退出游戏");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        #endregion

        #region 数据访问（供其他脚本使用）
        // 这里提供全局数据的访问接口
        // 例如：当前章节、当前日期、玩家数据等

        [Header("当前章节信息")]
        public int currentChapter = 0;
        public int currentDay = 0;

        public void SetChapter(int chapter)
        {
            currentChapter = chapter;
            Debug.Log($"当前章节：第{chapter}章");
        }

        public void SetDay(int day)
        {
            currentDay = day;
            Debug.Log($"当前日期：第{day}天");
        }
        #endregion

        #region Debug功能
        // 在Unity编辑器中显示调试信息
        private void OnGUI()
        {
            if (Debug.isDebugBuild)
            {
                GUILayout.BeginArea(new Rect(10, 10, 300, 200));
                GUILayout.Label($"游戏状态: {currentState}");
                GUILayout.Label($"时间线: {currentTimeline}");
                GUILayout.Label($"章节: {currentChapter}");
                GUILayout.Label($"天数: {currentDay}");
                GUILayout.EndArea();
            }
        }
        #endregion
    }
}
