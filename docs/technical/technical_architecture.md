# 技术架构文档

**文档版本**: v1.0
**最后更新**: 2025-11-05

---

## 一、技术栈选择

### 推荐引擎：Unity 2022 LTS

**理由**:
- 2D游戏支持完善
- 跨平台（PC/Console/移动）
- 资产商店资源丰富
- C#开发效率高
- 大量中文资料和社区支持

**替代方案**:
- Unreal Engine 5（如果需要更高画质）
- Godot（如果预算极低）

---

## 二、系统架构设计

### 模块划分

```
Game Application
├─ Core Systems（核心系统）
│  ├─ GameManager（游戏管理器）
│  ├─ SceneManager（场景管理）
│  ├─ SaveSystem（存档系统）
│  └─ EventSystem（事件系统）
│
├─ Gameplay Systems（玩法系统）
│  ├─ ResourceManager（资源管理）
│  ├─ RelationshipManager（关系系统）
│  ├─ DiaryManager（日记系统）
│  ├─ CharacterManager（角色管理）
│  └─ WorkManager（工作分配）
│
├─ UI Systems（UI系统）
│  ├─ UIManager（UI管理器）
│  ├─ DialogueSystem（对话系统）
│  ├─ MenuSystem（菜单系统）
│  └─ HUDSystem（HUD系统）
│
├─ Audio Systems（音频系统）
│  ├─ MusicManager（音乐管理）
│  ├─ SFXManager（音效管理）
│  └─ VoiceManager（配音管理）
│
└─ Data Systems（数据系统）
   ├─ GameData（游戏数据）
   ├─ NPCData（NPC数据）
   └─ EventData（事件数据）
```

---

## 三、核心系统设计

### GameManager（单例模式）

```csharp
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    // 游戏状态
    public GameState CurrentState { get; private set; }
    public int CurrentDay { get; private set; } = 1;
    public string CurrentChapter { get; private set; }

    // 核心管理器引用
    public ResourceManager Resources { get; private set; }
    public RelationshipManager Relationships { get; private set; }
    public DiaryManager Diary { get; private set; }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    void Initialize()
    {
        Resources = GetComponent<ResourceManager>();
        Relationships = GetComponent<RelationshipManager>();
        Diary = GetComponent<DiaryManager>();
        // ... 初始化其他系统
    }

    // 推进时间
    public void AdvanceDay()
    {
        CurrentDay++;
        OnDayAdvanced?.Invoke(CurrentDay);
    }

    public event System.Action<int> OnDayAdvanced;
}
```

---

## 四、数据管理

### 数据存储格式：JSON

**优点**:
- 人类可读
- 易于调试
- 跨平台兼容
- Unity内置支持

### 数据结构示例

```json
{
  "gameData": {
    "currentDay": 15,
    "currentChapter": "chapter_05",
    "resources": {
      "food": 5,
      "water": 8,
      "medicine": 2,
      "fuel": 3,
      "cloth": 4
    },
    "characters": [
      {
        "id": "wang_fugui",
        "health": 60,
        "hunger": 40,
        "morale": 55,
        "isAlive": true,
        "isSick": true,
        "sickness": "fever"
      }
    ],
    "relationships": {
      "wang_fugui": 75,
      "xiaomei": 60,
      "vautrin": 85
    },
    "flags": {
      "wang_saved": true,
      "chen_alt_dead": true
    },
    "diary_entries": 15,
    "history_completeness": 72
  }
}
```

---

## 五、关键系统实现

### 事件系统（Event Bus）

```csharp
public class EventBus
{
    private static Dictionary<string, List<System.Action<object>>> events =
        new Dictionary<string, List<System.Action<object>>>();

    // 订阅事件
    public static void Subscribe(string eventName, System.Action<object> callback)
    {
        if (!events.ContainsKey(eventName))
            events[eventName] = new List<System.Action<object>>();

        events[eventName].Add(callback);
    }

    // 发布事件
    public static void Publish(string eventName, object data = null)
    {
        if (events.ContainsKey(eventName))
        {
            foreach (var callback in events[eventName])
            {
                callback?.Invoke(data);
            }
        }
    }

    // 取消订阅
    public static void Unsubscribe(string eventName, System.Action<object> callback)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].Remove(callback);
        }
    }
}

// 使用示例
EventBus.Subscribe("resource_changed", OnResourceChanged);
EventBus.Publish("resource_changed", new { resource = "food", amount = -2 });
```

### 对话系统（基于Yarn Spinner）

```csharp
public class DialogueManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public DialogueUI dialogueUI;

    // 开始对话
    public void StartDialogue(string nodeName)
    {
        dialogueRunner.StartDialogue(nodeName);
    }

    // 处理选项
    [YarnCommand("choice_effect")]
    public void ProcessChoiceEffect(string effectType, int value)
    {
        switch (effectType)
        {
            case "relationship":
                GameManager.Instance.Relationships.ModifyRelationship(
                    currentNPCId, value);
                break;
            case "resource":
                GameManager.Instance.Resources.ModifyResource(
                    resourceType, value);
                break;
        }
    }
}
```

---

## 六、性能优化策略

### 对象池（Object Pooling）

```csharp
public class ObjectPool<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            return obj;
        }

        T pooledObj = pool.Dequeue();
        pooledObj.gameObject.SetActive(true);
        return pooledObj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
```

### LOD（Level of Detail）

- 远处NPC降低动画帧率
- 视野外NPC停止更新
- UI元素按需加载

---

## 七、多平台支持

### 平台适配

| 平台 | 分辨率 | 输入 | 特殊处理 |
|------|--------|------|---------|
| PC (Windows) | 1080p-4K | 键鼠 | 基准平台 |
| PC (Mac) | 1080p-5K | 键鼠 | Metal渲染 |
| PlayStation | 1080p/4K | 手柄 | Trophy系统 |
| Xbox | 1080p/4K | 手柄 | Achievement |
| Switch | 720p/1080p | 手柄/触屏 | 性能优化 |

### 输入系统（Unity Input System）

```csharp
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        // 处理移动
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 触发互动
        }
    }
}
```

---

## 八、本地化架构

### 多语言管理

```csharp
public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, string> currentLanguage;
    public SystemLanguage CurrentLanguage { get; private set; }

    public void LoadLanguage(SystemLanguage language)
    {
        string path = $"Localization/{language}";
        TextAsset jsonFile = Resources.Load<TextAsset>(path);
        currentLanguage = JsonUtility.FromJson<LocalizationData>(
            jsonFile.text).entries;
        CurrentLanguage = language;
    }

    public string GetText(string key)
    {
        if (currentLanguage.ContainsKey(key))
            return currentLanguage[key];
        return $"[MISSING: {key}]";
    }
}
```

---

## 九、测试策略

### 单元测试（Unity Test Framework）

```csharp
[Test]
public void ResourceManager_ConsumeFood_ReducesFood()
{
    ResourceManager rm = new ResourceManager();
    rm.SetResource("food", 10);
    rm.ConsumeResource("food", 3);
    Assert.AreEqual(7, rm.GetResource("food"));
}
```

### 自动化测试

- 存档/读档测试
- 关系系统逻辑测试
- 事件触发测试

---

## 十、开发工具链

### 版本控制

- **Git** + **GitHub**
- **LFS**（Large File Storage）用于大文件

### 协作工具

- **Jira**：任务管理
- **Confluence**：文档协作
- **Slack/Discord**：团队沟通

### CI/CD

- **Unity Cloud Build**
- 自动构建测试版本
- 自动化测试运行

---

## 十一、技术风险与应对

| 风险 | 概率 | 影响 | 应对方案 |
|------|------|------|---------|
| 性能问题（100+ NPC） | 中 | 高 | LOD、对象池、分层渲染 |
| 存档损坏 | 低 | 高 | 多重备份、版本管理 |
| 平台兼容性 | 中 | 中 | 早期多平台测试 |
| 本地化bug | 高 | 中 | 字符串外部化、回归测试 |

---

## 十二、开发时间线

### Phase 1: 原型（3个月）
- [ ] 核心系统搭建
- [ ] 基础UI
- [ ] 第一章可玩

### Phase 2: 垂直切片（3个月）
- [ ] 完整一个章节（第五章）
- [ ] 所有系统集成
- [ ] 美术/音频集成

### Phase 3: 全内容制作（9个月）
- [ ] 所有章节
- [ ] 完整美术资源
- [ ] 音频完成

### Phase 4: 打磨与测试（3个月）
- [ ] Beta测试
- [ ] Bug修复
- [ ] 优化
- [ ] 本地化

**总计**: 18个月

---

**文档状态**: 初稿完成
**持续更新**: 开发过程中根据实际情况调整
