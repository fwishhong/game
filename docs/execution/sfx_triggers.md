# SFX触发器表 / SFX Triggers Table

**文档版本**: v1.0
**最后更新**: 2025-11-05
**关联文档**: `audio/audio_sfx.md`, `audio/audio_music.md`, `narrative/dialogue_system.md`

---

## 文档说明 / Document Overview

本文档详细规定了游戏中所有音效的触发条件、播放参数、优先级系统和混音设置。所有音效文件引用来自 `audio/audio_sfx.md`。

This document specifies all sound effect triggers, playback parameters, priority systems, and audio mixing settings. All SFX file references are from `audio/audio_sfx.md`.

---

## 一、环境音效触发器 / Ambient Sound Triggers

### 1.1 1937年场景环境音 / 1937 Scene Ambience

| 场景ID | 音效文件 | 触发条件 | 音量 | 淡入/淡出 | 空间设置 | 变体 | 备注 |
|-------|---------|---------|------|----------|---------|------|------|
| **安全区白天** | AMB_1937_SAFETYZONE_DAY | 时段=06:00-18:00 且 场景=安全区 | 60% | 淡入:2s / 淡出:3s | 2D循环 | Layer 1,2,3 | 持续播放，根据人数动态调整Layer2音量 |
| **安全区夜晚** | AMB_1937_SAFETYZONE_NIGHT | 时段=18:00-06:00 且 场景=安全区 | 50% | 淡入:3s / 淡出:3s | 2D循环 | Layer 1,2,3 | Layer3偶发事件减少50% |
| **街道废墟** | AMB_1937_STREET_RUINS | 场景=户外废墟 | 70% | 淡入:2s / 淡出:2s | 2D循环 | 3个变体 | 远处枪声Layer3随机间隔30-90秒 |
| **冬日寒风** | AMB_1937_WINTER_WIND | 季节=冬季 且 场景=户外 | 65% | 淡入:2s / 淡出:2s | 2D循环 | 2个变体 | 温度<0°C时音量+10% |
| **秦淮河** | AMB_1937_RIVER | 场景=河边 或 距离河流<50m | 55% | 淡入:3s / 淡出:4s | 3D循环 (50m衰减) | 1个 | 象征意义重要，混响设置:长混响 |
| **雨天** | AMB_1937_RAIN | 天气=雨 | 60% | 淡入:1s / 淡出:2s | 2D循环 | 轻雨/大雨 | 叠加在其他环境音上 |
| **雪天** | AMB_1937_SNOW | 天气=雪 | 30% | 淡入:3s / 淡出:3s | 2D循环 | 1个 | 极静，压低其他环境音-20% |

**实现示例**:
```csharp
// 环境音管理器
void UpdateAmbience() {
    if (currentScene == "SafetyZone") {
        if (TimeOfDay >= 6 && TimeOfDay < 18) {
            CrossfadeAmbience("AMB_1937_SAFETYZONE_DAY", 2f);
        } else {
            CrossfadeAmbience("AMB_1937_SAFETYZONE_NIGHT", 3f);
        }
    }

    // 天气叠加
    if (weather == Weather.Rain) {
        PlayLayeredAmbience("AMB_1937_RAIN", 0.6f);
    }
}
```

### 1.2 2024年场景环境音 / 2024 Scene Ambience

| 场景ID | 音效文件 | 触发条件 | 音量 | 淡入/淡出 | 空间设置 | 备注 |
|-------|---------|---------|------|----------|---------|------|
| **纪念馆** | AMB_2024_MUSEUM | 场景=纪念馆 | 45% | 淡入:4s / 淡出:4s | 2D循环 | 肃穆氛围，脚步回声明显 |
| **现代街道** | AMB_2024_CITY | 场景=南京街道2024 | 70% | 淡入:2s / 淡出:2s | 2D循环 | 对比1937废墟，形成冲击 |
| **出租屋** | AMB_2024_APARTMENT | 场景=陈默公寓 | 40% | 淡入:3s / 淡出:2s | 2D循环 | 现代生活音，略显孤独感 |

---

## 二、对话/情感音效触发器 / Dialogue & Emotional SFX Triggers

### 2.1 情绪呼声 / Emotional Vocals

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 变体 | 优先级 | 备注 |
|---------|---------|------|------|---------|------|-------|------|
| SFX_VOICE_SIGH | 对话情绪标签="叹息" | 50% | 0ms | 3D (5m衰减) | 5个 | 中 | 叹息声，根据角色年龄选择变体 |
| SFX_VOICE_GASP | 惊吓事件触发 或 对话情绪标签="惊吓" | 60% | 0ms | 3D (5m) | 4个 | 高 | 倒吸气，QTE失败时也触发 |
| SFX_VOICE_WHIMPER | 角色健康<30% 或 对话情绪标签="呜咽" | 45% | 0ms | 3D (5m) | 6个 | 中 | 压抑哭泣，随机间隔播放 |
| SFX_VOICE_LAUGH_CHILD | 关系事件="儿童欢笑" | 55% | 0ms | 3D (10m) | 3个 | 低 | 罕见温暖时刻，触发成就提示 |
| SFX_CRY_CHILD | 儿童角色痛苦值>60 | 60% | 200ms | 3D (15m) | 5个 | 高 | 远近不同变体，情绪渲染 |
| SFX_CRY_ADULT | 成人角色痛苦值>70 | 55% | 300ms | 3D (8m) | 4个 | 中 | 压抑的成人哭泣 |
| SFX_COUGH | 角色健康<40% 或 疾病状态=生病 | 40% | 随机0-2s | 3D (8m) | 8个 | 低 | 背景层随机播放，不打断对话 |

**对话系统集成**:
```csharp
// 对话管理器中调用
void PlayDialogueLine(DialogueNode node) {
    // 显示文本
    ShowDialogueText(node.text);

    // 播放配音
    if (node.hasVoice) {
        PlayVoice(node.voiceID);
    }

    // 播放情绪音效
    if (node.emotion == "sigh") {
        PlaySFX3D("SFX_VOICE_SIGH", speakerPosition, 0.5f);
    }
}
```

### 2.2 人群声音 / Crowd Sounds

| 音效文件 | 触发条件 | 音量 | 空间设置 | 循环 | 优先级 | 备注 |
|---------|---------|------|---------|------|-------|------|
| SFX_CROWD_MURMUR | 人数>30 且 场景=安全区 | 55% | 2D循环 | 是 | 低 | 作为环境音Layer2使用 |
| SFX_CROWD_PANIC | 事件类型="搜查" 或 "空袭" | 80% | 2D | 否 | 最高 | 紧急事件，压制其他音效 |
| SFX_CROWD_PRAY | 事件类型="祈祷仪式" | 50% | 2D循环 | 是 | 中 | 低声祷告，肃穆氛围 |
| SFX_CROWD_READING | 场景=教室 且 活动=上课 | 45% | 3D (20m) | 是 | 低 | 孩子们朗读声，温暖时刻 |

---

## 三、UI音效触发器 / UI Sound Triggers

### 3.1 菜单交互音效 / Menu Interaction SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 时代区分 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_UI_BUTTON_HOVER | 鼠标悬停按钮 | 30% | 0ms | 1937:纸张沙沙 / 2024:咔哒 | 最低 | 防止过于频繁，添加100ms冷却 |
| SFX_UI_BUTTON_CLICK | 点击按钮 | 50% | 0ms | 1937:鼓声 / 2024:清脆音 | 中 | 不同按钮类型音高略有不同 |
| SFX_UI_PAGE_TURN | 翻页操作 (日记/菜单) | 45% | 50ms | 统一:纸张翻动 | 低 | 延迟50ms模拟翻页感 |
| SFX_UI_CONFIRM | 确认重要操作 | 60% | 0ms | 1937:盖章声 / 2024:确认音 | 中 | 带混响，增加分量感 |
| SFX_UI_CANCEL | 取消操作 | 40% | 0ms | 统一:擦除声 | 低 | 轻柔，不刺激 |
| SFX_UI_ERROR | 无效操作 | 55% | 0ms | 统一:不和谐音 | 中 | 负面反馈，不要太刺耳 |
| SFX_UI_NOTIFICATION | 新消息/提示 | 50% | 0ms | 1937:轻柔铃声 / 2024:现代提示音 | 中 | 根据重要性有3种音高变体 |

**防止音效轰炸**:
```csharp
// UI音效管理器
private float lastHoverTime = 0f;
private const float hoverCooldown = 0.1f;

void OnButtonHover() {
    if (Time.time - lastHoverTime > hoverCooldown) {
        PlayUISFX("SFX_UI_BUTTON_HOVER", 0.3f);
        lastHoverTime = Time.time;
    }
}
```

### 3.2 游戏系统音效 / Game System SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_RESOURCE_GET | 获得资源 | 55% | 0ms | 2D | 中 | 不同资源不同音效：食物/水/药品/燃料 |
| SFX_RESOURCE_LOSE | 失去资源 | 50% | 0ms | 2D | 中 | 负面提示音，略低沉 |
| SFX_RELATIONSHIP_UP | 关系值提升 | 60% | 200ms | 2D | 低 | 温暖音效，延迟200ms配合UI动画 |
| SFX_HEALTH_DOWN | 健康值下降>10% | 70% | 0ms | 2D | 高 | 心跳+低沉音，警示玩家 |
| SFX_ACHIEVEMENT | 成就解锁 | 65% | 0ms | 2D | 中 | 古琴音符，满足感强 |
| SFX_DIARY_WRITE | 打开日记界面 | 50% | 0ms | 2D | 低 | 毛笔沙沙声，配合书写动画 |
| SFX_DIARY_INK | 日记文字出现 | 35% | 与文字同步 | 2D | 最低 | 水墨扩散声，循环播放至文字结束 |

**资源音效映射**:
```csharp
Dictionary<ResourceType, string> resourceSFXMap = new() {
    { ResourceType.Food, "SFX_ITEM_BOWL" },
    { ResourceType.Water, "SFX_POUR_WATER" },
    { ResourceType.Medicine, "SFX_ITEM_MEDICINE_BOTTLE" },
    { ResourceType.Fuel, "SFX_ITEM_WOOD" }
};

void OnResourceGained(ResourceType type, int amount) {
    // 播放获取音效
    PlayUISFX("SFX_RESOURCE_GET");
    // 延迟播放对应物品音效
    StartCoroutine(PlayDelayed(resourceSFXMap[type], 0.2f));
}
```

---

## 四、角色动作音效触发器 / Character Action SFX Triggers

### 4.1 脚步声系统 / Footstep System

| 音效文件 | 触发条件 | 音量 | 空间设置 | 变体数 | 备注 |
|---------|---------|------|---------|-------|------|
| SFX_FOOTSTEP_STONE | 地面类型=石板路 且 移动速度>0 | 40% | 3D (10m衰减) | 8 | 根据移动速度调整播放频率 |
| SFX_FOOTSTEP_WOOD | 地面类型=木地板 | 45% | 3D (8m) | 6 | 室内木质，略响 |
| SFX_FOOTSTEP_SNOW | 地面类型=雪地 | 50% | 3D (6m) | 8 | 冬季特有，软糯感 |
| SFX_FOOTSTEP_MUD | 地面类型=泥地 | 45% | 3D (8m) | 6 | 雨后泥泞，粘稠感 |
| SFX_FOOTSTEP_TILE | 地面类型=瓷砖 (2024) | 40% | 3D (10m) | 6 | 现代场景，清脆回声 |

**脚步声触发系统**:
```csharp
// 角色控制器
private float stepDistance = 0f;
private float stepInterval = 1.5f; // 米

void Update() {
    if (isMoving) {
        stepDistance += velocity * Time.deltaTime;

        if (stepDistance >= stepInterval) {
            PlayFootstep();
            stepDistance = 0f;
        }
    }
}

void PlayFootstep() {
    // 检测地面类型
    string groundType = DetectGroundType();
    string sfxID = $"SFX_FOOTSTEP_{groundType}";

    // 随机选择变体
    int variant = Random.Range(0, GetVariantCount(sfxID));

    // 播放3D音效
    PlaySFX3D($"{sfxID}_{variant}", transform.position, 0.4f);
}
```

### 4.2 物品交互音效 / Item Interaction SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_DOOR_WOOD_OPEN | 开门交互 | 60% | 0ms | 3D (15m) | 中 | 吱嘎声，老旧木门 |
| SFX_DOOR_WOOD_CLOSE | 关门交互 | 55% | 300ms | 3D (15m) | 中 | 延迟300ms配合门动画 |
| SFX_PICKUP_FOOD | 拾取食物 | 50% | 0ms | 3D (5m) | 低 | 布料摩擦声 |
| SFX_PICKUP_MEDICINE | 拾取药品 | 50% | 0ms | 3D (5m) | 低 | 瓶子轻响 |
| SFX_POUR_WATER | 倒水动作 | 55% | 0ms | 3D (8m) | 低 | 水流声，循环至动作结束 |
| SFX_EAT | 吃东西 | 30% | 0ms | 3D (3m) | 最低 | 咀嚼声，低音量避免不适 |
| SFX_ITEM_BOWL | 使用碗 | 45% | 0ms | 3D (5m) | 低 | 瓷器碰撞 |
| SFX_ITEM_FIRE | 生火/火炉 | 40% | 0ms | 3D (20m) 循环 | 低 | 燃烧声，持续循环 |

---

## 五、事件音效触发器 / Event SFX Triggers

### 5.1 危险事件音效 / Danger Event SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_GUNSHOT_DISTANT | 事件="远处枪声" | 70% | 0ms | 3D (200m衰减) | 中 | 空旷回声，随机方向 |
| SFX_GUNSHOT_CLOSE | 事件="近处枪声" | 90% | 0ms | 3D (50m) | 最高 | 震撼感，触发屏幕震动 |
| SFX_EXPLOSION | 事件="炮击" | 95% | 0ms | 3D (300m) | 最高 | 爆炸，低频冲击，压制所有音效 |
| SFX_SIREN | 事件="空袭警报" | 85% | 0ms | 2D 循环 | 最高 | 历史录音风格，持续至事件结束 |
| SFX_SOLDIERS_MARCH | 事件="日军巡逻" | 65% | 0ms | 3D (50m) | 高 | 整齐脚步声，逐渐靠近/远离 |
| SFX_BAYONET | 事件="武器威胁" | 60% | 0ms | 3D (10m) | 高 | 金属摩擦，紧张感 |
| SFX_SCREAM_DISTANT | 事件="远处惨叫" | 55% | 随机0-1s | 3D (100m) | 中 | 暗示性，不直接展示暴力 |

**危险事件音效管理**:
```csharp
// 事件管理器
void TriggerDangerEvent(string eventType) {
    switch(eventType) {
        case "AirRaid":
            // 空袭警报
            PlaySFX2D("SFX_SIREN", 0.85f, loop: true);
            // 淡出音乐
            audioMixer.SetFloat("MusicVolume", -20f);
            // 随机炮击
            StartCoroutine(RandomExplosions());
            break;

        case "Patrol":
            // 巡逻脚步声逐渐靠近
            GameObject soldiers = SpawnAudioSource("SFX_SOLDIERS_MARCH", patrolStartPos);
            soldiers.GetComponent<AudioSource>().SetScheduledEndTime(patrolDuration);
            MoveAudioSource(soldiers, patrolStartPos, patrolEndPos, patrolDuration);
            break;
    }
}
```

### 5.2 QTE音效 / QTE SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_QTE_APPEAR | QTE出现 | 75% | 0ms | 2D | 最高 | 紧张提示音，吸引注意力 |
| SFX_QTE_SUCCESS | QTE成功 | 70% | 0ms | 2D | 高 | 松一口气的音效 |
| SFX_QTE_FAIL | QTE失败 | 80% | 0ms | 2D | 最高 | 沉重音效，配合屏幕效果 |
| SFX_QTE_HEARTBEAT | QTE进行中 | 60% | 0ms | 2D 循环 | 高 | 心跳声，加速制造紧张感 |

**QTE音效系统**:
```csharp
void StartQTE() {
    // 出现音效
    PlayUISFX("SFX_QTE_APPEAR", 0.75f);

    // 开始心跳循环，逐渐加速
    StartCoroutine(PlayHeartbeat());

    // 淡出环境音
    audioMixer.SetFloat("AmbienceVolume", -15f);
}

IEnumerator PlayHeartbeat() {
    float interval = 1.0f; // 初始间隔
    while (qteActive) {
        PlayUISFX("SFX_QTE_HEARTBEAT", 0.6f);
        yield return new WaitForSeconds(interval);
        interval = Mathf.Max(0.3f, interval * 0.95f); // 逐渐加速
    }
}

void EndQTE(bool success) {
    if (success) {
        PlayUISFX("SFX_QTE_SUCCESS", 0.7f);
    } else {
        PlayUISFX("SFX_QTE_FAIL", 0.8f);
        // 触发失败后果音效
    }

    // 恢复环境音
    audioMixer.SetFloat("AmbienceVolume", 0f);
}
```

### 5.3 特殊事件音效 / Special Event SFX

| 音效文件 | 触发条件 | 音量 | 延迟 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|------|---------|-------|------|
| SFX_TIMETRAVEL_START | 穿越开始 | 80% | 0ms | 2D | 最高 | 时间扭曲感，低频轰鸣 |
| SFX_TIMETRAVEL_VORTEX | 穿越过程 | 85% | 500ms | 2D | 最高 | 时空漩涡，持续3-5秒 |
| SFX_TIMETRAVEL_LAND | 穿越降落 | 75% | 0ms | 2D | 高 | 沉重着陆，低频冲击 |
| SFX_EVENT_LOTTERY_DRUM | 抽签事件 | 70% | 0ms | 3D (20m) | 高 | 沉重木质声，慢节奏 |
| SFX_EVENT_EXECUTION | 处决场景 | 60% | 0ms | 3D (30m) | 高 | 暗示性音效，不直接 |
| SFX_EVENT_NEWYEAR_FIRECRACKERS | 除夕夜 | 50% | 0ms | 3D (100m) | 低 | 远处微弱，对比惨烈现实 |

---

## 六、天气与自然音效 / Weather & Nature SFX

| 音效文件 | 触发条件 | 音量 | 淡入/淡出 | 空间设置 | 优先级 | 备注 |
|---------|---------|------|----------|---------|-------|------|
| SFX_WEATHER_RAIN_LIGHT | 天气=小雨 | 50% | 淡入:2s / 淡出:3s | 2D循环 | 低 | 叠加在环境音上 |
| SFX_WEATHER_RAIN_HEAVY | 天气=大雨 | 70% | 淡入:1s / 淡出:2s | 2D循环 | 中 | 压低其他环境音-15dB |
| SFX_WEATHER_THUNDER | 天气=雷暴 | 80% | 0ms | 3D (500m) | 高 | 随机触发，远近不同 |
| SFX_WEATHER_WIND_LIGHT | 风力等级=1-2 | 40% | 淡入:3s / 淡出:3s | 2D循环 | 最低 | 微风，树叶沙沙 |
| SFX_WEATHER_WIND_STRONG | 风力等级=3-5 | 65% | 淡入:2s / 淡出:2s | 2D循环 | 中 | 大风呼啸，紧张感 |
| SFX_WEATHER_SNOW_FALL | 天气=雪 | 25% | 淡入:4s / 淡出:4s | 2D循环 | 最低 | 极轻微，营造寂静 |

---

## 七、音效优先级系统 / SFX Priority System

### 7.1 优先级定义 / Priority Levels

| 优先级 | 级别 | 说明 | 最大同时播放数 | 示例 |
|-------|------|------|---------------|------|
| **最高 (Critical)** | 100 | 关键剧情/紧急事件，打断一切 | 1 | 爆炸、空袭警报、QTE |
| **高 (High)** | 80 | 重要事件，可打断中低优先级 | 3 | 枪声、危险音效、角色受伤 |
| **中 (Medium)** | 60 | 一般游戏音效 | 8 | UI交互、对话情绪、物品拾取 |
| **低 (Low)** | 40 | 背景层音效 | 15 | 脚步声、环境细节、人群低语 |
| **最低 (Ambient)** | 20 | 环境音循环 | 无限制 | 环境音、天气音效 |

### 7.2 打断规则 / Interruption Rules

```csharp
// 音效优先级管理器
public class SFXPriorityManager {
    private Dictionary<int, List<AudioSource>> activeSFX = new();

    public bool CanPlaySFX(int priority) {
        // 检查是否超过该优先级的最大播放数
        int maxCount = GetMaxCountForPriority(priority);
        int currentCount = activeSFX[priority].Count;

        if (currentCount >= maxCount) {
            // 停止最老的音效
            StopOldestSFX(priority);
        }

        return true;
    }

    public void PlaySFXWithPriority(string sfxID, int priority, float volume) {
        // 检查是否需要打断低优先级音效
        if (priority >= 80) {
            // 高优先级音效，降低低优先级音效音量
            DuckLowerPriority(priority);
        }

        if (priority == 100) {
            // 最高优先级，暂停所有其他音效
            PauseAllExcept(sfxID);
        }

        // 播放音效
        PlaySFX(sfxID, volume);
    }

    private void DuckLowerPriority(int priority) {
        // 降低低优先级音效音量（Ducking）
        foreach (var kvp in activeSFX) {
            if (kvp.Key < priority) {
                foreach (var source in kvp.Value) {
                    source.volume *= 0.3f; // 降低到30%
                }
            }
        }
    }
}
```

### 7.3 音效分组 / Audio Mixing Groups

```
Master (0dB)
├─ Music (-6dB)
│  ├─ 1937_Music
│  └─ 2024_Music
├─ SFX (0dB)
│  ├─ UI_SFX (-3dB)
│  │  ├─ 1937_UI
│  │  └─ 2024_UI
│  ├─ Ambience (-6dB)
│  │  ├─ Amb_1937
│  │  └─ Amb_2024
│  ├─ Foley (-3dB)
│  │  ├─ Footsteps
│  │  └─ Actions
│  └─ Events (0dB)
│     ├─ Danger_Events
│     ├─ QTE_Events
│     └─ Special_Events
└─ Voice (-3dB)
   ├─ Dialogue
   └─ Vocal_SFX
```

---

## 八、3D音效空间设置 / 3D Audio Spatial Settings

### 8.1 衰减曲线 / Attenuation Curves

| 音效类型 | 最小距离 | 最大距离 | 衰减模式 | 备注 |
|---------|---------|---------|---------|------|
| **对话/呼声** | 1m | 5m | 对数衰减 | 快速衰减，营造亲近感 |
| **脚步声** | 0.5m | 10m | 对数衰减 | 中等范围 |
| **物品交互** | 0.5m | 5m | 对数衰减 | 小范围 |
| **枪声** | 5m | 200m | 线性衰减 | 远距离可听 |
| **爆炸** | 10m | 300m | 对数衰减 | 超远距离，冲击感强 |
| **环境音（河流）** | 5m | 50m | 对数衰减 | 中等范围环境 |
| **火焰** | 1m | 20m | 对数衰减 | 温暖源点 |

### 8.2 混响设置 / Reverb Settings

| 场景类型 | 混响预设 | Room Size | Decay Time | 干湿比 | 备注 |
|---------|---------|-----------|-----------|-------|------|
| **室内小房间** | Room | 小 | 0.8s | 30% Wet | 安全区小隔间 |
| **大厅** | Hall | 大 | 2.0s | 40% Wet | 安全区大厅、教堂 |
| **街道** | City | 中 | 1.2s | 25% Wet | 户外街道、废墟 |
| **空旷废墟** | Canyon | 大 | 3.0s | 50% Wet | 强烈回声，孤独感 |
| **现代室内** | Generic | 小 | 0.5s | 20% Wet | 2024年场景 |
| **博物馆** | Stone Corridor | 中 | 1.5s | 35% Wet | 纪念馆肃穆感 |

**Unity混响区域实现**:
```csharp
// 在场景中放置AudioReverbZone组件
AudioReverbZone reverbZone = gameObject.AddComponent<AudioReverbZone>();
reverbZone.reverbPreset = AudioReverbPreset.Hall;
reverbZone.minDistance = 5f;
reverbZone.maxDistance = 30f;
```

---

## 九、音效混音指南 / Audio Mixing Guidelines

### 9.1 音量平衡 / Volume Balance

| 音频层 | 相对音量 | dB | 说明 |
|-------|---------|-----|------|
| 对话/配音 | 100% | 0dB | 最重要，绝对清晰 |
| 音乐 | 60% | -6dB | 烘托氛围，不压过对话 |
| UI音效 | 70% | -3dB | 清晰但不突兀 |
| 环境音 | 50% | -6dB | 背景层，营造氛围 |
| 角色动作 | 70% | -3dB | 沉浸感重要 |
| 事件音效 | 90% | -1dB | 重要事件需突出 |

### 9.2 对话优先（Ducking）/ Dialogue Ducking

当对话播放时，自动降低其他音频：
```csharp
// 对话Ducking系统
void OnDialogueStart() {
    // 音乐降低到40%
    audioMixer.SetFloat("MusicVolume", -10f);
    // 环境音降低到30%
    audioMixer.SetFloat("AmbienceVolume", -12f);
    // UI音效降低到50%
    audioMixer.SetFloat("UIVolume", -6f);
}

void OnDialogueEnd() {
    // 恢复原音量（平滑过渡）
    StartCoroutine(FadeVolumeBack("MusicVolume", -6f, 1f));
    StartCoroutine(FadeVolumeBack("AmbienceVolume", -6f, 1f));
    StartCoroutine(FadeVolumeBack("UIVolume", -3f, 0.5f));
}
```

### 9.3 紧张场景混音 / Tension Scene Mixing

危险事件发生时：
1. **淡出音乐**（1-2秒）
2. **提高危险音效**至90%音量
3. **降低环境音**至30%
4. **保持对话**清晰（如有）
5. **心跳音效**叠加（60%音量循环）

```csharp
void EnterTensionMode() {
    audioMixer.SetFloat("MusicVolume", -20f);      // 几乎静音
    audioMixer.SetFloat("AmbienceVolume", -12f);   // 降低环境音
    audioMixer.SetFloat("EventsVolume", -1f);      // 提高事件音效

    // 播放心跳
    PlayLoopingSFX("SFX_QTE_HEARTBEAT", 0.6f);
}
```

---

## 十、性能优化建议 / Performance Optimization

### 10.1 音效池管理 / Audio Pool Management

```csharp
// 音效对象池
public class SFXPoolManager {
    private Dictionary<string, Queue<AudioSource>> sfxPool = new();
    private const int poolSize = 20;

    public AudioSource GetSFXSource() {
        // 从池中获取或创建新的AudioSource
        if (sfxPool["general"].Count > 0) {
            return sfxPool["general"].Dequeue();
        } else {
            return CreateNewAudioSource();
        }
    }

    public void ReturnSFXSource(AudioSource source) {
        // 归还到池中
        source.Stop();
        source.clip = null;
        sfxPool["general"].Enqueue(source);
    }
}
```

### 10.2 音效资源加载策略 / Asset Loading Strategy

| 音效类型 | 加载方式 | 压缩格式 | 优先级 | 备注 |
|---------|---------|---------|-------|------|
| UI音效 | 预加载 | WAV | 高 | 常用，保持在内存 |
| 环境音循环 | 流式加载 | OGG Vorbis | 中 | 长音频，节省内存 |
| 对话配音 | 按需加载 | OGG Vorbis | 中 | 用完释放 |
| 事件音效 | 预加载（池） | WAV | 高 | 快速响应 |
| 背景层音效 | 延迟加载 | OGG Vorbis | 低 | 不紧急 |

### 10.3 音效限制 / Audio Limits

- **同时播放音效上限**: 32个AudioSource
- **3D音效最大距离**: 300m（超过不播放）
- **音效优先级队列**: 超过上限时停止最低优先级
- **音效去重**: 同一音效100ms内不重复播放（防止音效轰炸）

---

## 十一、调试与测试工具 / Debug & Testing Tools

### 11.1 音效调试面板 / SFX Debug Panel

```csharp
// 开发者调试工具
#if UNITY_EDITOR
public class SFXDebugPanel : MonoBehaviour {
    private void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 300, 500));

        GUILayout.Label("=== SFX Debug Panel ===");
        GUILayout.Label($"Active SFX: {GetActiveSFXCount()}");
        GUILayout.Label($"Master Volume: {GetMasterVolume()}");

        // 显示所有正在播放的音效
        foreach (var sfx in GetAllActiveSFX()) {
            GUILayout.Label($"{sfx.name} | Vol: {sfx.volume:F2} | Priority: {sfx.priority}");
        }

        // 测试按钮
        if (GUILayout.Button("Test Gunshot")) {
            PlaySFX3D("SFX_GUNSHOT_CLOSE", Camera.main.transform.position, 0.9f);
        }

        if (GUILayout.Button("Test QTE")) {
            TriggerQTESequence();
        }

        GUILayout.EndArea();
    }
}
#endif
```

### 11.2 音效检查清单 / SFX Testing Checklist

**基础测试**:
- [ ] 所有音效文件存在且无损坏
- [ ] 音量平衡合理，无爆音
- [ ] 3D音效空间定位正确
- [ ] 淡入淡出平滑无爆音
- [ ] 循环音效无缝衔接

**场景测试**:
- [ ] 环境音在场景切换时正确切换
- [ ] 对话时其他音效正确降低
- [ ] 危险事件音效优先级正确
- [ ] QTE音效反馈及时清晰
- [ ] UI音效不过于频繁

**性能测试**:
- [ ] 30+音效同时播放时无性能问题
- [ ] 内存占用在合理范围
- [ ] 音效池正常工作无内存泄漏
- [ ] 移动端/主机性能达标

**本地化测试**:
- [ ] 不同语言UI音效正确
- [ ] 对话配音与字幕同步
- [ ] 文化适配音效合理

---

## 十二、实现示例代码 / Implementation Code Examples

### 12.1 完整音效管理器 / Complete SFX Manager

```csharp
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour {
    public static SFXManager Instance { get; private set; }

    [Header("Audio Mixer")]
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup uiMixer;
    public AudioMixerGroup ambienceMixer;

    [Header("Settings")]
    public int maxSimultaneousSFX = 32;
    public float minSFXInterval = 0.1f; // 防止音效轰炸

    private Dictionary<string, AudioClip> sfxCache = new();
    private List<AudioSource> activeSources = new();
    private Dictionary<string, float> lastPlayTime = new();

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // 播放2D音效
    public void PlaySFX2D(string sfxID, float volume = 1f, bool loop = false) {
        if (!CanPlaySFX(sfxID)) return;

        AudioClip clip = LoadSFXClip(sfxID);
        if (clip == null) {
            Debug.LogError($"SFX not found: {sfxID}");
            return;
        }

        AudioSource source = GetAvailableSource();
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.spatialBlend = 0f; // 2D
        source.outputAudioMixerGroup = uiMixer;
        source.Play();

        lastPlayTime[sfxID] = Time.time;

        if (!loop) {
            StartCoroutine(ReturnSourceWhenDone(source, clip.length));
        }
    }

    // 播放3D音效
    public void PlaySFX3D(string sfxID, Vector3 position, float volume = 1f,
                          float minDistance = 1f, float maxDistance = 50f) {
        if (!CanPlaySFX(sfxID)) return;

        AudioClip clip = LoadSFXClip(sfxID);
        if (clip == null) return;

        AudioSource source = GetAvailableSource();
        source.transform.position = position;
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = 1f; // 3D
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = AudioRolloffMode.Logarithmic;
        source.outputAudioMixerGroup = sfxMixer;
        source.Play();

        lastPlayTime[sfxID] = Time.time;

        StartCoroutine(ReturnSourceWhenDone(source, clip.length));
    }

    // 检查是否可以播放（防止音效轰炸）
    private bool CanPlaySFX(string sfxID) {
        if (lastPlayTime.ContainsKey(sfxID)) {
            float timeSinceLastPlay = Time.time - lastPlayTime[sfxID];
            if (timeSinceLastPlay < minSFXInterval) {
                return false;
            }
        }
        return true;
    }

    // 加载音效
    private AudioClip LoadSFXClip(string sfxID) {
        if (sfxCache.ContainsKey(sfxID)) {
            return sfxCache[sfxID];
        }

        AudioClip clip = Resources.Load<AudioClip>($"Audio/SFX/{sfxID}");
        if (clip != null) {
            sfxCache[sfxID] = clip;
        }
        return clip;
    }

    // 获取可用的AudioSource
    private AudioSource GetAvailableSource() {
        // 寻找未激活的source
        foreach (var source in activeSources) {
            if (!source.isPlaying) {
                return source;
            }
        }

        // 如果都在播放，停止最老的
        if (activeSources.Count >= maxSimultaneousSFX) {
            AudioSource oldest = activeSources[0];
            oldest.Stop();
            return oldest;
        }

        // 创建新的
        GameObject sourceObj = new GameObject("SFX_Source");
        sourceObj.transform.SetParent(transform);
        AudioSource newSource = sourceObj.AddComponent<AudioSource>();
        activeSources.Add(newSource);
        return newSource;
    }

    // 归还AudioSource
    private IEnumerator ReturnSourceWhenDone(AudioSource source, float duration) {
        yield return new WaitForSeconds(duration);
        source.Stop();
        source.clip = null;
    }

    // 停止所有音效
    public void StopAllSFX() {
        foreach (var source in activeSources) {
            source.Stop();
        }
    }

    // 设置主音量
    public void SetMasterVolume(float volume) {
        masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }
}
```

---

## 十三、常见问题与解决方案 / FAQ & Solutions

**Q1: 音效播放延迟？**
A: 确保使用预加载的音效池，避免运行时加载。UI音效使用WAV格式。

**Q2: 音效重叠造成轰炸？**
A: 使用minSFXInterval防抖动，设置合理的优先级和最大播放数。

**Q3: 3D音效定位不准？**
A: 检查AudioListener位置，确认AudioSource空间混合设置为1.0，调整衰减曲线。

**Q4: 环境音循环有间隙？**
A: 确保音频文件首尾无缝，使用loop标记，避免代码重新播放。

**Q5: 音效在不同场景不一致？**
A: 统一使用AudioMixer，避免直接修改AudioSource.volume。

---

## 十四、资源文件命名规范 / Asset Naming Convention

### 14.1 音效文件命名

```
格式: [类别]_[具体描述]_[变体].wav/ogg

示例:
SFX_FOOTSTEP_STONE_01.wav
SFX_FOOTSTEP_STONE_02.wav
AMB_1937_SAFETYZONE_DAY.ogg
UI_BUTTON_CLICK_1937.wav
EVENT_GUNSHOT_DISTANT_01.wav
```

### 14.2 文件夹结构

```
Assets/Audio/SFX/
├── Ambience/
│   ├── 1937/
│   └── 2024/
├── UI/
│   ├── 1937/
│   └── 2024/
├── Foley/
│   ├── Footsteps/
│   ├── Actions/
│   └── Items/
├── Events/
│   ├── Danger/
│   ├── QTE/
│   └── Special/
└── Vocal/
    ├── Emotions/
    └── Crowd/
```

---

## 附录A：完整音效清单 / Appendix A: Complete SFX List

参考 `audio/audio_sfx.md` 获取完整音效资源列表（100+音效，215+变体）。

---

## 附录B：Unity AudioMixer配置 / Appendix B: Unity AudioMixer Setup

```
推荐Mixer设置:
Master Volume: 0dB
├─ Music: -6dB
│  └─ Send to Master (0dB)
├─ SFX: 0dB
│  ├─ UI: -3dB (Send to Ducking Sidechain)
│  ├─ Ambience: -6dB (Receive Ducking)
│  ├─ Foley: -3dB
│  └─ Events: 0dB
└─ Voice: -3dB (Send to Ducking Sidechain)
   └─ Ducking Target: All Other Groups (-12dB when active)
```

---

**文档状态**: 初稿完成
**下一步**: 与音频团队对齐，开始实现与测试
**维护者**: 音频程序员 + 音效设计师

**版本历史**:
- v1.0 (2025-11-05): 初始版本，完整触发器表
