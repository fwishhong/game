# 数据结构设计文档

**文档版本**: v1.0
**最后更新**: 2025-11-05

---

## 一、核心数据类

### GameState（游戏状态）

```csharp
[System.Serializable]
public class GameState
{
    public int currentDay;              // 当前天数（1-90）
    public string currentChapter;       // 当前章节ID
    public string currentDate;          // 游戏内日期（1937-12-01）
    public int playTime;                // 游戏时长（秒）

    public ResourceData resources;      // 资源数据
    public List<CharacterData> characters;  // 角色数据
    public RelationshipData relationships;  // 关系数据
    public List<string> completedEvents;    // 已完成事件
    public Dictionary<string, bool> flags;  // 游戏标志

    public int historicalCompleteness; // 历史完整度（0-100）
    public int culturalPreservation;   // 文化保存度（0-100）
    public int moralScore;             // 道德分数

    public List<DiaryEntry> diaryEntries;  // 日记条目
}
```

---

## 二、资源系统数据

### ResourceData

```csharp
[System.Serializable]
public class ResourceData
{
    public int food;      // 食物
    public int water;     // 饮用水
    public int medicine;  // 药品
    public int fuel;      // 燃料
    public int cloth;     // 布料

    // 资源修改
    public void Modify(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "food": food += amount; break;
            case "water": water += amount; break;
            case "medicine": medicine += amount; break;
            case "fuel": fuel += amount; break;
            case "cloth": cloth += amount; break;
        }

        // 确保不为负
        ClampAll();
    }

    void ClampAll()
    {
        food = Mathf.Max(0, food);
        water = Mathf.Max(0, water);
        medicine = Mathf.Max(0, medicine);
        fuel = Mathf.Max(0, fuel);
        cloth = Mathf.Max(0, cloth);
    }
}
```

---

## 三、角色系统数据

### CharacterData

```csharp
[System.Serializable]
public class CharacterData
{
    public string characterID;      // 角色唯一ID
    public string characterName;    // 姓名
    public int age;                 // 年龄
    public CharacterRole role;      // 角色（主角/NPC/敌人）

    // 状态属性
    public int health;       // 健康度（0-100）
    public int hunger;       // 饥饿度（0-100）
    public int morale;       // 士气（0-100）
    public int warmth;       // 温暖度（0-100，冬季）

    // 特殊状态
    public bool isAlive;
    public bool isSick;
    public string sickness;  // 病症类型
    public bool isPregnant;  // 是否怀孕（女性）
    public int pregnancyDays;

    // 位置
    public string currentLocation;

    // 每日更新
    public void DailyUpdate()
    {
        // 饥饿度自然增加
        hunger += GetHungerRate();

        // 温暖度（冬季）
        if (GameManager.Instance.IsWinter())
            warmth -= 30;

        // 健康度影响
        if (hunger > 70) health -= 5;
        if (warmth < 30) health -= 5;
        if (isSick) health -= GetSicknessImpact();

        // 死亡判定
        if (health <= 0) isAlive = false;

        ClampValues();
    }

    int GetHungerRate()
    {
        if (age < 12) return 25;      // 儿童
        if (isPregnant) return 50;    // 孕妇
        return 40;                     // 成人
    }

    void ClampValues()
    {
        health = Mathf.Clamp(health, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
        morale = Mathf.Clamp(morale, 0, 100);
        warmth = Mathf.Clamp(warmth, 0, 100);
    }
}

public enum CharacterRole
{
    Player,
    MainNPC,
    SecondaryNPC,
    BackgroundNPC,
    Enemy
}
```

---

## 四、关系系统数据

### RelationshipData

```csharp
[System.Serializable]
public class RelationshipData
{
    public Dictionary<string, int> relationships =
        new Dictionary<string, int>();

    // 获取关系值
    public int GetRelationship(string npcID)
    {
        if (relationships.ContainsKey(npcID))
            return relationships[npcID];
        return 0;
    }

    // 修改关系
    public void ModifyRelationship(string npcID, int amount)
    {
        if (!relationships.ContainsKey(npcID))
            relationships[npcID] = 0;

        relationships[npcID] += amount;
        relationships[npcID] = Mathf.Clamp(relationships[npcID], 0, 100);

        // 触发关系等级变化事件
        EventBus.Publish("relationship_changed", new
        {
            npcID = npcID,
            newValue = relationships[npcID]
        });
    }

    // 获取关系等级
    public RelationshipLevel GetLevel(string npcID)
    {
        int value = GetRelationship(npcID);
        if (value >= 81) return RelationshipLevel.LifeAndDeath;
        if (value >= 61) return RelationshipLevel.BestFriend;
        if (value >= 41) return RelationshipLevel.Friend;
        if (value >= 21) return RelationshipLevel.Acquaintance;
        return RelationshipLevel.Stranger;
    }
}

public enum RelationshipLevel
{
    Stranger,          // 陌生人（0-20）
    Acquaintance,      // 相识（21-40）
    Friend,            // 朋友（41-60）
    BestFriend,        // 挚友（61-80）
    LifeAndDeath       // 生死之交（81-100）
}
```

---

## 五、日记系统数据

### DiaryEntry

```csharp
[System.Serializable]
public class DiaryEntry
{
    public int day;                  // 第几天
    public string date;              // 日期（民国xx年）
    public string weather;           // 天气
    public DiaryFocus focusType;    // 重点类型
    public string focusTarget;       // 重点对象（人物ID/事件ID）
    public string content;           // 日记内容
    public bool playerEdited;        // 是否玩家修改

    public List<string> relatedCharacters;  // 相关人物
    public List<string> relatedEvents;      // 相关事件
    public List<string> unlocks;            // 解锁的内容
}

public enum DiaryFocus
{
    Character,    // 记录人物
    History,      // 记录历史
    Feeling       // 记录感受
}
```

---

## 六、事件系统数据

### EventData

```csharp
[System.Serializable]
public class EventData
{
    public string eventID;           // 事件唯一ID
    public string eventName;         // 事件名称
    public EventType type;           // 事件类型
    public int chapter;              // 所属章节
    public string date;              // 触发日期

    public EventTriggerType triggerType;  // 触发类型
    public string triggerCondition;       // 触发条件

    public string description;       // 事件描述
    public List<EventChoice> choices;// 选项

    public bool isCompleted;         // 是否完成
    public string playerChoice;      // 玩家选择
}

public enum EventType
{
    StoryForced,      // 强制剧情
    RandomDaily,      // 随机日常
    ConditionalTrigger, // 条件触发
    RelationshipEvent, // 关系事件
    MoralDilemma      // 道德困境
}

public enum EventTriggerType
{
    DateBased,        // 日期触发
    ConditionalFlag,  // 条件标志
    RelationshipBased, // 关系触发
    ResourceBased,    // 资源触发
    Random            // 随机
}

[System.Serializable]
public class EventChoice
{
    public string choiceID;
    public string text;              // 选项文字
    public string[] tags;            // 标签（[日语][道德]等）
    public Dictionary<string, object> requirements; // 需求
    public Dictionary<string, object> effects;      // 效果
    public string nextEventID;       // 后续事件
}
```

---

## 七、配置数据（ScriptableObject）

### NPCConfig

```csharp
[CreateAssetMenu(fileName = "NPC_", menuName = "Game/NPC Config")]
public class NPCConfig : ScriptableObject
{
    public string npcID;
    public string npcName;
    public int initialAge;

    [Header("初始属性")]
    public int initialHealth = 80;
    public int initialHunger = 30;
    public int initialMorale = 60;

    [Header("特殊属性")]
    public bool isEssential;         // 是否核心角色
    public bool canDie;              // 是否可死亡

    [Header("外观")]
    public Sprite portrait;          // 头像
    public Sprite fullBody;          // 全身立绘

    [Header("对话")]
    public string dialogueNodePrefix; // Yarn对话节点前缀
}
```

---

## 八、存档数据结构

### SaveData

```csharp
[System.Serializable]
public class SaveData
{
    public int slotIndex;            // 存档槽位（1-10）
    public string timestamp;         // 保存时间
    public byte[] screenshot;        // 截图（压缩）

    // 游戏数据
    public GameState gameState;

    // 元数据
    public string chapterName;       // 章节名（用于显示）
    public int playTime;             // 游戏时长（秒）
    public int saveCount;            // 保存次数

    // 统计数据
    public int totalDeaths;          // 总死亡人数
    public int totalSaved;           // 总救助人数
    public int diaryCount;           // 日记条目数
}
```

### 序列化/反序列化

```csharp
public static class SaveSystem
{
    static string SavePath(int slot) =>
        Path.Combine(Application.persistentDataPath, $"save_{slot}.json");

    public static void Save(SaveData data, int slot)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath(slot), json);
    }

    public static SaveData Load(int slot)
    {
        if (!File.Exists(SavePath(slot)))
            return null;

        string json = File.ReadAllText(SavePath(slot));
        return JsonUtility.FromJson<SaveData>(json);
    }
}
```

---

## 九、配置表（CSV/JSON）

### items.json（物品配置）

```json
{
  "items": [
    {
      "id": "food_rice",
      "name": "米饭",
      "type": "food",
      "restoreHunger": 50,
      "description": "简单的米饭"
    },
    {
      "id": "medicine_basic",
      "name": "基础药品",
      "type": "medicine",
      "restoreHealth": 30,
      "treatsSickness": ["fever", "cold"],
      "description": "治疗常见病"
    }
  ]
}
```

### events.json（事件配置）

```json
{
  "events": [
    {
      "id": "E001",
      "name": "穿越触发",
      "type": "story_forced",
      "chapter": 0,
      "date": "2024-12-13",
      "dialogueNode": "序章_穿越"
    }
  ]
}
```

---

## 十、数据流图

```
玩家输入
    ↓
GameManager.ProcessInput()
    ↓
修改GameState
    ↓
触发EventBus事件
    ↓
相关Manager响应（ResourceManager/RelationshipManager等）
    ↓
更新UI显示
    ↓
保存到SaveData（自动/手动）
```

---

**文档状态**: 数据结构完成
**下一步**: 实现各Manager类
