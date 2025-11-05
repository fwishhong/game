# UI/UX交互流程图

**文档版本**: v1.0
**最后更新**: 2025-11-05
**负责人**: UI/UX设计师 + 程序（待定）
**状态**: 执行级文档

---

## 一、整体UI导航地图

### 1.1 全局UI结构图

```
启动游戏
    ↓
┌──────────────────────────────────────────────────────────────┐
│                        主菜单 (MainMenu)                      │
├──────────────────────────────────────────────────────────────┤
│ • 新游戏 → 难度选择 → 序章载入                               │
│ • 继续游戏 → 存档列表 → 游戏场景                             │
│ • 章节选择 → 章节列表 → 场景载入                             │
│ • 设置 → 设置菜单                                             │
│ • 历史资料库 → 历史内容浏览                                  │
│ • 制作名单 → 鸣谢界面                                         │
│ • 退出游戏 → 确认对话框 → 退出                               │
└──────────────────────────────────────────────────────────────┘
                                ↓
┌──────────────────────────────────────────────────────────────┐
│                     游戏场景 (GameScene)                      │
├──────────────────────────────────────────────────────────────┤
│ HUD显示层:                                                    │
│ ├─ 状态栏 (StatusBar)                                        │
│ ├─ 日期时间 (DateTimeDisplay)                                │
│ ├─ 资源指示器 (ResourceIndicator)                            │
│ └─ 交互提示 (InteractionHint)                                │
│                                                               │
│ 可打开界面:                                                   │
│ ├─ [ESC] 暂停菜单 (PauseMenu)                                │
│ ├─ [Tab] 资源管理界面 (InventoryPanel)                       │
│ ├─ [J] 日记界面 (DiaryPanel)                                 │
│ ├─ [R] 关系网络界面 (RelationshipPanel)                      │
│ ├─ [E触发] 对话界面 (DialoguePanel)                          │
│ ├─ [E触发] 事件选择界面 (EventPanel)                         │
│ └─ [自动] 章节转场界面 (ChapterTransition)                   │
└──────────────────────────────────────────────────────────────┘
```

---

## 二、主菜单交互流程

### 2.1 主菜单 (MainMenu)

**场景**: `scene_mainmenu`
**UI对象**: `UI_MainMenu`

```
进入主菜单
    ↓
[初始化]
├─ 播放背景视频: bg_video_memorial.mp4
├─ 播放背景音乐: bgm_main_theme.ogg (循环)
├─ 显示版本号: version_text.text = "v1.0"
├─ 读取玩家偏好: language, volume
└─ 检查存档: has_save_data → 高亮"继续游戏"按钮

[按钮状态机]
默认选中: btn_new_game (新游戏)
导航方式:
  - 键盘: 上下方向键 / W S
  - 手柄: D-Pad 上下 / 左摇杆
  - 鼠标: 悬停高亮

[按钮列表]
1. btn_new_game (新游戏)
2. btn_continue (继续游戏) [无存档时灰化]
3. btn_chapter_select (章节选择) [未解锁时灰化]
4. btn_settings (设置)
5. btn_archive (历史资料库)
6. btn_credits (制作名单)
7. btn_exit (退出游戏)
```

#### 2.1.1 [新游戏] 按钮

```
点击 btn_new_game
    ↓
检查: 是否已有存档?
    ├─ Yes → 弹出确认对话框
    │         "检测到已有存档，开始新游戏将覆盖存档。是否继续？"
    │         [确认] [取消]
    │             ↓ 确认
    └─ No  → 直接进入难度选择
                 ↓
        ┌──────────────────────┐
        │  难度选择界面         │
        ├──────────────────────┤
        │ • 故事模式            │
        │   - 资源×1.5倍       │
        │   - 消耗×0.7倍       │
        │   - 推荐新手         │
        │                      │
        │ • 历史模式 [推荐]    │
        │   - 资源×1.0倍       │
        │   - 真实难度         │
        │   - 原汁原味体验     │
        │                      │
        │ • 困难模式            │
        │   - 资源×0.7倍       │
        │   - 消耗×1.3倍       │
        │   - 高死亡率         │
        │                      │
        │ [返回]        [确认] │
        └──────────────────────┘
                 ↓ 选择难度并确认
        保存难度设置: game_data.difficulty = selected_difficulty
                 ↓
        播放过场动画: cutscene_time_travel.mp4
                 ↓
        加载序章场景: LoadScene("scene_ch00_prologue")
                 ↓
        淡入黑屏 (2秒) → 开始游戏
```

**变量名**:
- `game_data.difficulty`: int (0=故事, 1=历史, 2=困难)
- `ui_difficulty_panel.selected_index`: int
- `save_data.has_save`: bool

---

#### 2.1.2 [继续游戏] 按钮

```
点击 btn_continue
    ↓
检查: save_data.has_save?
    ├─ No  → 按钮灰化，无法点击
    │        tooltip: "当前无存档数据"
    └─ Yes → 打开存档列表界面
                 ↓
        ┌───────────────────────────────────┐
        │      存档列表 (SaveLoadPanel)     │
        ├───────────────────────────────────┤
        │ [自动存档]                        │
        │  章节: 第3章 - 生存的煎熬         │
        │  日期: 1937年12月15日             │
        │  游戏时长: 5小时32分              │
        │  保存时间: 2024-11-05 14:32       │
        │                  [载入] [删除]    │
        ├───────────────────────────────────┤
        │ [手动存档 #1]                     │
        │  章节: 第5章 - 日本兵搜查         │
        │  日期: 1937年12月20日             │
        │  游戏时长: 8小时15分              │
        │  保存时间: 2024-11-04 20:18       │
        │                  [载入] [删除]    │
        ├───────────────────────────────────┤
        │ [空存档槽]                        │
        │  - 无数据 -                       │
        │                                   │
        ├───────────────────────────────────┤
        │         [返回主菜单]              │
        └───────────────────────────────────┘
                     ↓ 点击[载入]
        播放载入动画 (loading_spinner.anim)
                     ↓
        读取存档数据:
          - save_data.load_from_slot(slot_index)
          - game_state.restore_from_save(save_data)
                     ↓
        加载对应场景: LoadScene(save_data.scene_name)
                     ↓
        恢复游戏状态:
          - player_stats.restore()
          - inventory.restore()
          - relationships.restore()
          - flags.restore()
                     ↓
        淡入场景 → 继续游戏
```

**变量名**:
- `save_manager.save_slots[]`: SaveSlot[10]
- `save_slot.chapter_name`: string
- `save_slot.in_game_date`: string
- `save_slot.playtime`: float (秒)
- `save_slot.real_time`: DateTime
- `save_slot.scene_name`: string
- `save_slot.screenshot`: Texture2D

**操作映射**:
- 上下键/摇杆: 选择存档槽
- 鼠标: 点击选择
- Enter/A键: 载入
- Delete/Y键: 删除 (需二次确认)
- ESC/B键: 返回

---

#### 2.1.3 [章节选择] 按钮

```
点击 btn_chapter_select
    ↓
检查: 是否已通关一次?
    ├─ No  → 提示对话框
    │        "需要通关游戏一次后解锁章节选择功能"
    │        [确定]
    └─ Yes → 打开章节选择界面
                 ↓
        ┌──────────────────────────────────────────┐
        │      章节选择 (ChapterSelectPanel)       │
        ├──────────────────────────────────────────┤
        │ [缩略图] 序章：穿越                      │
        │          2024年12月13日                  │
        │          已通关 ✓          [开始游玩]    │
        ├──────────────────────────────────────────┤
        │ [缩略图] 第1章：初遇                     │
        │          1937年12月1-5日                 │
        │          已通关 ✓          [开始游玩]    │
        ├──────────────────────────────────────────┤
        │ [缩略图] 第2章：陷落                     │
        │          1937年12月6-12日                │
        │          已通关 ✓          [开始游玩]    │
        ├──────────────────────────────────────────┤
        │ [缩略图] 第3章：生存的煎熬               │
        │          1937年12月13-20日               │
        │          已通关 ✓          [开始游玩]    │
        ├──────────────────────────────────────────┤
        │ ...                                      │
        │                                          │
        │              [返回主菜单]                │
        └──────────────────────────────────────────┘
                     ↓ 点击[开始游玩]
        弹出难度选择 (与新游戏相同)
                     ↓
        初始化该章节数据:
          - chapter_data.init_chapter(chapter_index)
          - player_stats.set_to_chapter_start(chapter_index)
                     ↓
        加载章节场景: LoadScene(chapter_data.start_scene)
                     ↓
        开始游玩
```

**变量名**:
- `game_progress.completed_once`: bool
- `game_progress.unlocked_chapters[]`: bool[19]
- `chapter_data[i].title`: string
- `chapter_data[i].date_range`: string
- `chapter_data[i].thumbnail`: Texture2D
- `chapter_data[i].completed`: bool

---

#### 2.1.4 [设置] 按钮

```
点击 btn_settings
    ↓
打开设置菜单 (详见 3.2 设置菜单)
```

#### 2.1.5 [历史资料库] 按钮

```
点击 btn_archive
    ↓
打开历史资料库界面
        ↓
┌─────────────────────────────────────────────┐
│      历史资料库 (ArchivePanel)              │
├─────────────────────────────────────────────┤
│ [分类标签]                                  │
│  • 全部  • 人物  • 事件  • 地点  • 物品   │
├─────────────────────────────────────────────┤
│ [列表区域 - 滚动]                           │
│                                             │
│ ┌─────────────────────────────────────┐   │
│ │ [缩略图] 南京安全区                 │   │
│ │          1937年12月设立，由欧美...  │   │
│ │          已解锁 ✓          [查看]   │   │
│ ├─────────────────────────────────────┤   │
│ │ [缩略图] 魏特琳                     │   │
│ │          美国传教士，金陵女子...    │   │
│ │          已解锁 ✓          [查看]   │   │
│ ├─────────────────────────────────────┤   │
│ │ [缩略图] 南京保卫战                 │   │
│ │          1937年12月...              │   │
│ │          未解锁 🔒                  │   │
│ └─────────────────────────────────────┘   │
│                                             │
│              [返回主菜单]                   │
└─────────────────────────────────────────────┘
            ↓ 点击[查看]
┌─────────────────────────────────────────────┐
│      资料详情 (ArchiveDetailPanel)          │
├─────────────────────────────────────────────┤
│  [大图]                                     │
│                                             │
│  标题: 南京安全区                           │
│                                             │
│  [正文内容 - 滚动区域]                      │
│  1937年12月，以德国商人约翰·拉贝...        │
│  （详细历史介绍，约500-1000字）            │
│                                             │
│  [相关图片/视频]                            │
│                                             │
│              [返回列表]                     │
└─────────────────────────────────────────────┘
```

**解锁条件** (variable: `archive_unlocked[]`):
- 每个资料条目对应一个解锁条件
- 例: archive_unlocked[0] = flag_entered_safety_zone

**变量名**:
- `archive_data[i].id`: int
- `archive_data[i].title`: string
- `archive_data[i].category`: enum (人物/事件/地点/物品)
- `archive_data[i].unlocked`: bool
- `archive_data[i].content`: string
- `archive_data[i].images[]`: Texture2D[]

---

#### 2.1.6 [制作名单] 按钮

```
点击 btn_credits
    ↓
播放制作名单滚动动画
┌─────────────────────────────────────┐
│         制作名单                    │
│                                     │
│      《秦淮旧梦》                   │
│   MEMORIES OF QINHUAI               │
│                                     │
│  [自动向上滚动]                     │
│                                     │
│  策划/制作                          │
│  某某某                             │
│                                     │
│  程序                               │
│  某某某                             │
│  ...                                │
│                                     │
│  [ESC跳过]                          │
└─────────────────────────────────────┘
    ↓ 播放完毕或按ESC
返回主菜单
```

---

#### 2.1.7 [退出游戏] 按钮

```
点击 btn_exit
    ↓
弹出确认对话框
┌──────────────────────┐
│  确认退出游戏？      │
│                      │
│  [确认]    [取消]    │
└──────────────────────┘
    ↓ 点击[确认]
淡出屏幕 (1秒)
    ↓
Application.Quit()
```

---

## 三、游戏内UI交互流程

### 3.1 游戏场景HUD (GameHUD)

**常驻显示元素**:

```
┌──────────────────────────────────────────────────────────┐
│ [左上角]                              [右上角]           │
│ ┌──────────────────┐              ┌──────────────────┐ │
│ │ 日期时间显示      │              │ 快捷资源状态     │ │
│ │ 民国26年腊月初二  │              │ 食物: 12 🍚     │ │
│ │ 1937年12月15日   │              │ 水: 8 💧        │ │
│ │ 下午3时          │              │ 药品: 3 💊      │ │
│ └──────────────────┘              │ 燃料: 5 🔥      │ │
│                                   └──────────────────┘ │
│                                                         │
│              [游戏场景主区域]                           │
│                                                         │
│ [左下角]                              [右下角]         │
│ ┌──────────────────┐              ┌──────────────────┐ │
│ │ 角色状态          │              │ 交互提示         │ │
│ │ [头像] 陈默       │              │                  │ │
│ │ 健康: ████░ 80   │              │ [E] 与王掌柜交谈 │ │
│ │ 饥饿: ██░░░ 40   │              │                  │ │
│ │ 士气: ███░░ 65   │              │                  │ │
│ └──────────────────┘              └──────────────────┘ │
│                                                         │
│ [底部中央 - 可选显示]                                   │
│ 任务提示: "寻找食物和水 (0/5)"                          │
└──────────────────────────────────────────────────────────┘
```

**UI对象与变量映射**:

```javascript
// 日期时间 (UI_DateTimeDisplay)
text_cn_date.text = game_time.GetChineseDateString()  // "民国26年腊月初二"
text_gregorian.text = game_time.GetGregorianString()  // "1937年12月15日"
text_time.text = game_time.GetTimeOfDayString()       // "下午3时"

// 资源状态 (UI_ResourceIndicator)
text_food.text = inventory.food.ToString()            // "12"
text_water.text = inventory.water.ToString()          // "8"
text_medicine.text = inventory.medicine.ToString()    // "3"
text_fuel.text = inventory.fuel.ToString()            // "5"

// 角色状态 (UI_PlayerStatus)
image_avatar.sprite = player.avatar_sprite             // 陈默头像
slider_health.value = player.health / 100f             // 0.80
text_health.text = player.health.ToString()            // "80"
slider_hunger.value = player.hunger / 100f             // 0.40
text_hunger.text = player.hunger.ToString()            // "40"
slider_morale.value = player.morale / 100f             // 0.65
text_morale.text = player.morale.ToString()            // "65"

// 交互提示 (UI_InteractionHint)
// 根据玩家距离最近的可交互物体动态显示
if (nearest_interactable != null) {
    text_hint.text = $"[{input_key}] {nearest_interactable.action_text}"
    // 示例: "[E] 与王掌柜交谈"
    panel_hint.SetActive(true)
} else {
    panel_hint.SetActive(false)
}
```

**状态条颜色编码**:
- 健康 (health):
  - 80-100: 绿色 #4CAF50
  - 50-79: 黄色 #FFC107
  - 20-49: 橙色 #FF9800
  - 0-19: 红色 #F44336
- 饥饿 (hunger):
  - 0-30: 绿色
  - 31-60: 黄色
  - 61-80: 橙色
  - 81-100: 红色 (危险)
- 士气 (morale):
  - 70-100: 金色 #FFD700
  - 40-69: 灰色 #9E9E9E
  - 0-39: 深灰 #424242

---

### 3.2 暂停菜单 (PauseMenu)

**触发方式**:
- 键盘: ESC键
- 手柄: Start按钮
- 触摸: UI暂停按钮

```
游戏中按ESC
    ↓
暂停游戏: Time.timeScale = 0
    ↓
模糊背景: 应用高斯模糊后处理
    ↓
显示暂停菜单面板
┌────────────────────────────────────┐
│           游戏已暂停               │
├────────────────────────────────────┤
│         [继续游戏]                 │
│         [保存游戏]                 │
│         [读取游戏]                 │
│         [设置]                     │
│         [返回主菜单]               │
│         [退出游戏]                 │
└────────────────────────────────────┘
```

#### 3.2.1 [继续游戏]

```
点击或按ESC
    ↓
关闭菜单面板: panel_pause.SetActive(false)
    ↓
移除背景模糊
    ↓
恢复游戏: Time.timeScale = 1
```

#### 3.2.2 [保存游戏]

```
点击保存
    ↓
打开存档槽选择界面
┌─────────────────────────────────────┐
│        选择存档槽                   │
├─────────────────────────────────────┤
│ [自动存档]                          │
│  - 系统自动管理 -                   │
│  上次保存: 12月15日 14:32           │
│                    [覆盖保存]       │
├─────────────────────────────────────┤
│ [手动存档 #1]                       │
│  第3章 - 1937年12月15日             │
│  保存时间: 11-05 14:32              │
│                    [覆盖保存]       │
├─────────────────────────────────────┤
│ [手动存档 #2]                       │
│  - 空槽位 -                         │
│                    [新建存档]       │
├─────────────────────────────────────┤
│ ...                                 │
│              [取消]                 │
└─────────────────────────────────────┘
            ↓ 点击[覆盖保存]或[新建存档]
弹出确认对话框 (如果覆盖)
"是否覆盖该存档？"
[确认] [取消]
            ↓ 确认
执行保存逻辑:
save_manager.SaveToSlot(slot_index)
  ├─ 保存游戏状态: game_state.Serialize()
  ├─ 保存玩家数据: player.Serialize()
  ├─ 保存库存: inventory.Serialize()
  ├─ 保存关系: relationships.Serialize()
  ├─ 保存标记位: flags.Serialize()
  ├─ 截取屏幕截图: screenshot = CaptureScreenshot()
  └─ 写入文件: File.Write(save_file_path, save_data)
            ↓
显示提示: "保存成功！"(2秒后消失)
            ↓
返回暂停菜单
```

**保存数据结构** (JSON格式):
```json
{
  "save_version": "1.0",
  "save_time": "2024-11-05T14:32:00",
  "game_time": {
    "year": 1937,
    "month": 12,
    "day": 15,
    "hour": 15
  },
  "chapter_index": 2,
  "scene_name": "scene_ch03_day13",
  "playtime_seconds": 19920,
  "player": {
    "health": 80,
    "hunger": 40,
    "morale": 65,
    "warmth": 50,
    "skills": {
      "history": 5,
      "japanese": 2,
      "medical": 3,
      "scavenging": 4
    }
  },
  "inventory": {
    "food": 12,
    "water": 8,
    "medicine": 3,
    "fuel": 5,
    "cloth": 2,
    "valuables": []
  },
  "relationships": {
    "wang_fugui": 75,
    "xiao_mei": 60,
    "li_wenbin": 50,
    "zhao_nurse": 45
  },
  "flags": {
    "flag_entered_safety_zone": true,
    "flag_saved_li_wenbin": true,
    "flag_wang_sick": false
  },
  "screenshot_path": "save_screenshots/save_01.jpg"
}
```

**文件路径**:
- Windows: `%APPDATA%/QinhuaiOldDream/saves/save_slot_X.json`
- macOS: `~/Library/Application Support/QinhuaiOldDream/saves/save_slot_X.json`
- Linux: `~/.config/QinhuaiOldDream/saves/save_slot_X.json`

---

#### 3.2.3 [读取游戏]

```
点击读取
    ↓
打开存档列表界面 (与主菜单"继续游戏"相同)
    ↓
选择存档并载入
    ↓
确认对话框:
"读取存档将丢失当前未保存进度，是否继续？"
[确认] [取消]
    ↓ 确认
显示载入动画
    ↓
加载场景并恢复状态 (与主菜单流程相同)
    ↓
恢复游戏: Time.timeScale = 1
```

---

#### 3.2.4 [设置]

```
点击设置
    ↓
打开设置菜单 (详见下方设置菜单详细设计)
```

---

#### 3.2.5 [返回主菜单]

```
点击返回主菜单
    ↓
弹出确认对话框:
┌──────────────────────────────────────┐
│  返回主菜单将丢失未保存进度          │
│  是否继续？                          │
│                                      │
│  [确认]         [取消]               │
└──────────────────────────────────────┘
    ↓ 点击[确认]
淡出当前场景 (1秒)
    ↓
卸载游戏场景: UnloadScene(current_scene)
    ↓
重置游戏状态: game_state.Reset()
    ↓
加载主菜单: LoadScene("scene_mainmenu")
    ↓
恢复时间: Time.timeScale = 1
```

---

#### 3.2.6 [退出游戏]

```
点击退出游戏
    ↓
弹出确认对话框:
"退出游戏将丢失未保存进度，是否继续？"
[确认] [取消]
    ↓ 点击[确认]
淡出屏幕
    ↓
Application.Quit()
```

---

### 3.3 设置菜单 (SettingsMenu)

**可从主菜单或暂停菜单打开**

```
┌──────────────────────────────────────────────────────────┐
│                        设置                              │
├──────────────────────────────────────────────────────────┤
│ [标签页]                                                 │
│  • 画面  • 音频  • 游戏  • 控制  • 语言                 │
├──────────────────────────────────────────────────────────┤
│ [当前标签内容区域]                                       │
│                                                          │
│  (根据选中标签显示不同设置项)                            │
│                                                          │
├──────────────────────────────────────────────────────────┤
│              [恢复默认]    [应用]    [返回]              │
└──────────────────────────────────────────────────────────┘
```

#### 3.3.1 画面设置 (GraphicsTab)

```
[画面设置]
├─ 分辨率: [1920x1080 ▼]
│   选项: 1280x720, 1600x900, 1920x1080, 2560x1440, 3840x2160
│   变量: settings.resolution_width, settings.resolution_height
│   应用: Screen.SetResolution(width, height, fullscreen)
│
├─ 显示模式: [全屏 ▼]
│   选项: 全屏, 无边框窗口, 窗口
│   变量: settings.fullscreen_mode (0/1/2)
│   应用: Screen.fullScreenMode = selected_mode
│
├─ 画质预设: [高 ▼]
│   选项: 低, 中, 高, 极高, 自定义
│   变量: settings.quality_preset
│   应用: QualitySettings.SetQualityLevel(level)
│
├─ 抗锯齿: [FXAA ▼]
│   选项: 关, FXAA, SMAA, TAA
│   变量: settings.anti_aliasing
│
├─ 阴影质量: [高 ▼]
│   选项: 关, 低, 中, 高
│   变量: settings.shadow_quality
│
├─ 纹理质量: [高 ▼]
│   选项: 低, 中, 高
│   变量: settings.texture_quality
│
├─ 后处理效果: [开 ▼]
│   选项: 开, 关
│   变量: settings.post_processing
│   说明: 包括景深、色差、晕影等
│
├─ 垂直同步: ☑ 开启
│   变量: settings.vsync (bool)
│   应用: QualitySettings.vSyncCount = vsync ? 1 : 0
│
└─ 帧率限制: [60 FPS ▼]
    选项: 30, 60, 120, 144, 无限制
    变量: settings.fps_limit
    应用: Application.targetFrameRate = fps_limit
```

---

#### 3.3.2 音频设置 (AudioTab)

```
[音频设置]
├─ 主音量: [████████░░] 80%
│   范围: 0-100
│   变量: settings.master_volume (float 0-1)
│   应用: AudioMixer.SetFloat("MasterVolume", volume)
│
├─ 音乐音量: [██████████] 100%
│   范围: 0-100
│   变量: settings.music_volume
│   应用: AudioMixer.SetFloat("MusicVolume", volume)
│
├─ 音效音量: [████████░░] 80%
│   范围: 0-100
│   变量: settings.sfx_volume
│   应用: AudioMixer.SetFloat("SFXVolume", volume)
│
├─ 对话音量: [█████████░] 90%
│   范围: 0-100
│   变量: settings.voice_volume
│   应用: AudioMixer.SetFloat("VoiceVolume", volume)
│
├─ 环境音量: [███████░░░] 70%
│   范围: 0-100
│   变量: settings.ambient_volume
│   应用: AudioMixer.SetFloat("AmbientVolume", volume)
│
└─ 静音: ☐ 全局静音
    变量: settings.mute_all (bool)
    应用: AudioListener.volume = mute_all ? 0 : 1
```

**实时预览**:
- 调整音量时播放测试音效
- 音乐音量: 播放主题曲片段
- 音效音量: 播放按钮点击音
- 对话音量: 播放角色对话示例

---

#### 3.3.3 游戏设置 (GameplayTab)

```
[游戏设置]
├─ 难度: [历史模式 ▼]
│   选项: 故事模式, 历史模式, 困难模式
│   变量: settings.difficulty
│   说明: 仅在新游戏时可更改
│   灰化条件: if (in_game) disable()
│
├─ 字幕: ☑ 显示字幕
│   变量: settings.show_subtitles (bool)
│
├─ 对话自动播放: ☐ 自动播放
│   变量: settings.auto_dialogue (bool)
│   说明: 对话文字显示完毕后自动继续
│
├─ 对话播放速度: [████████░░] 快
│   范围: 慢 - 中 - 快
│   变量: settings.text_speed (0.5x / 1.0x / 2.0x)
│   应用: dialogue_manager.chars_per_second = base_speed * text_speed
│
├─ QTE难度: [普通 ▼]
│   选项: 简单(2.0s), 普通(1.0s), 困难(0.5s)
│   变量: settings.qte_difficulty
│   应用: qte_window_time = base_time * difficulty_multiplier
│
├─ 自动保存: ☑ 启用
│   变量: settings.auto_save (bool)
│   说明: 章节开始时自动保存
│
├─ 历史注释: ☑ 显示
│   变量: settings.show_historical_notes (bool)
│   说明: 是否显示历史知识弹窗
│
└─ 教程提示: ☑ 显示
    变量: settings.show_tutorials (bool)
    说明: 新手引导提示
```

---

#### 3.3.4 控制设置 (ControlsTab)

```
[控制设置]
├─ 按键映射:
│   ┌────────────────────────────────────┐
│   │ 移动向上:    [W]       [重新映射]  │
│   │ 移动向下:    [S]       [重新映射]  │
│   │ 移动向左:    [A]       [重新映射]  │
│   │ 移动向右:    [D]       [重新映射]  │
│   │ 互动:        [E]       [重新映射]  │
│   │ 跳过对话:    [Space]   [重新映射]  │
│   │ 暂停:        [ESC]     [重新映射]  │
│   │ 物品栏:      [Tab]     [重新映射]  │
│   │ 日记:        [J]       [重新映射]  │
│   │ 关系网:      [R]       [重新映射]  │
│   └────────────────────────────────────┘
│   变量: settings.key_bindings[action_name]
│   应用: InputManager.RebindKey(action, new_key)
│
├─ 鼠标灵敏度: [█████░░░░░] 50%
│   范围: 10-100
│   变量: settings.mouse_sensitivity
│
├─ 手柄震动: ☑ 启用
│   变量: settings.gamepad_vibration (bool)
│
├─ 手柄灵敏度: [████████░░] 80%
│   范围: 10-100
│   变量: settings.gamepad_sensitivity
│
└─ [恢复默认按键]
    点击: 重置所有按键到默认值
```

**按键重新映射流程**:
```
点击[重新映射]
    ↓
显示提示: "请按下新按键..."
    ↓
监听输入: input = Input.GetAnyKeyDown()
    ↓
检查冲突:
if (key_already_used) {
    显示提示: "该按键已被占用，是否交换？"
    [交换] [取消]
} else {
    直接绑定
}
    ↓
更新显示: button_text = new_key_name
    ↓
保存设置: settings.key_bindings[action] = new_key
```

---

#### 3.3.5 语言设置 (LanguageTab)

```
[语言设置]
├─ 界面语言: [简体中文 ▼]
│   选项: 简体中文, 繁体中文, English, 日本語, 한국어
│   变量: settings.language
│   应用: LocalizationManager.SetLanguage(language_code)
│   说明: 更改后需要重启游戏
│
├─ 字体大小: [中 ▼]
│   选项: 小(90%), 中(100%), 大(120%)
│   变量: settings.font_scale
│   应用: UIManager.SetFontScale(scale)
│
└─ 色盲模式: [正常 ▼]
    选项: 正常, 红绿色盲, 蓝黄色盲, 全色盲
    变量: settings.colorblind_mode
    应用: ColorBlindFilter.SetMode(mode)
```

---

#### 3.3.6 设置菜单底部按钮

```
[恢复默认]
点击 → 确认对话框
"是否恢复当前标签页的所有设置为默认值？"
[确认] [取消]
    ↓ 确认
settings.RestoreDefaultsForTab(current_tab)
更新UI显示

[应用]
点击 → 保存所有设置
settings.SaveToFile()
显示提示: "设置已保存"

[返回]
点击 → 检查是否有未应用的更改
if (has_unsaved_changes) {
    确认对话框: "有未保存的更改，是否保存？"
    [保存] [不保存] [取消]
} else {
    直接返回上一界面
}
```

---

### 3.4 资源管理界面 (InventoryPanel)

**触发方式**:
- 键盘: Tab键
- 手柄: Select按钮
- 触摸: UI背包按钮

```
游戏中按Tab
    ↓
暂停游戏: Time.timeScale = 0
    ↓
显示资源管理界面
┌──────────────────────────────────────────────────────────┐
│                    物资分配                    [X 关闭] │
├────────────────────┬─────────────────────────────────────┤
│ [库存区域]         │  [人员列表区域]                     │
│                    │                                     │
│ ┌────────────────┐ │  ┌─────────────────────────────┐  │
│ │ 🍚 食物: 12    │ │  │ [头像] 陈默                 │  │
│ │  今日需求: 5   │ │  │  健康: ████░ 80             │  │
│ │  预计剩余: 7   │ │  │  饥饿: ██░░░ 40             │  │
│ └────────────────┘ │  │  士气: ███░░ 65             │  │
│                    │  │                               │  │
│ ┌────────────────┐ │  │  今日分配:                  │  │
│ │ 💧 水: 8       │ │  │  食物: [1份 ▼]             │  │
│ │  今日需求: 5   │ │  │  水:   [1份 ▼]             │  │
│ │  预计剩余: 3   │ │  └─────────────────────────────┘  │
│ └────────────────┘ │                                     │
│                    │  ┌─────────────────────────────┐  │
│ ┌────────────────┐ │  │ [头像] 王掌柜               │  │
│ │ 💊 药品: 3     │ │  │  健康: ███░░ 60             │  │
│ │  - 稀缺 -      │ │  │  饥饿: ████░ 70             │  │
│ └────────────────┘ │  │  士气: ███░░ 55             │  │
│                    │  │  状态: 🤒 发烧              │  │
│ ┌────────────────┐ │  │                               │  │
│ │ 🔥 燃料: 5     │ │  │  今日分配:                  │  │
│ │  今日需求: 2   │ │  │  食物: [1.5份 ▼] (病人加量) │  │
│ │  预计剩余: 3   │ │  │  水:   [2份 ▼]   (发烧需水) │  │
│ └────────────────┘ │  │  药品: [1个 ▼]   (治疗)    │  │
│                    │  └─────────────────────────────┘  │
│ ┌────────────────┐ │                                     │
│ │ 🧵 布料: 2     │ │  [滚动查看更多人员...]             │
│ │                │ │                                     │
│ └────────────────┘ │                                     │
│                    │                                     │
│ ┌────────────────┐ │                                     │
│ │ 💎 贵重物品: 3 │ │                                     │
│ │ • 金戒指 ×1    │ │                                     │
│ │ • 怀表 ×1      │ │                                     │
│ │ • 玉佩 ×1      │ │                                     │
│ └────────────────┘ │                                     │
├────────────────────┴─────────────────────────────────────┤
│  预警提示: ⚠️ 食物将在3天后耗尽，请尽快搜寻！           │
│                                                          │
│         [全部自动分配]         [确认分配]  [取消]       │
└──────────────────────────────────────────────────────────┘
```

**交互逻辑**:

```javascript
// 打开界面时
OnOpen() {
    // 计算总需求
    total_food_needed = 0
    total_water_needed = 0
    foreach (character in all_characters) {
        total_food_needed += character.GetFoodNeed()
        total_water_needed += character.GetWaterNeed()
    }

    // 显示预警
    days_food_left = inventory.food / total_food_needed
    if (days_food_left < 3) {
        ShowWarning("食物将在" + days_food_left + "天后耗尽")
    }

    // 默认分配方案
    AutoAssignResources()
}

// 自动分配算法
AutoAssignResources() {
    // 优先级: 病人 > 孕妇 > 儿童 > 成人
    characters_sorted = SortByPriority(all_characters)

    foreach (character in characters_sorted) {
        // 分配食物
        if (inventory.food >= character.GetFoodNeed()) {
            character.assigned_food = character.GetFoodNeed()
            inventory.food -= character.assigned_food
        } else {
            character.assigned_food = inventory.food
            inventory.food = 0
        }

        // 分配水
        if (inventory.water >= character.GetWaterNeed()) {
            character.assigned_water = character.GetWaterNeed()
            inventory.water -= character.assigned_water
        } else {
            character.assigned_water = inventory.water
            inventory.water = 0
        }

        // 分配药品 (仅病人)
        if (character.is_sick && inventory.medicine > 0) {
            character.assigned_medicine = 1
            inventory.medicine -= 1
        }
    }
}

// 手动调整
OnResourceAdjusted(character, resource_type, amount) {
    // 检查库存是否足够
    if (inventory[resource_type] < amount) {
        ShowError("库存不足")
        return
    }

    // 更新分配
    character.assigned[resource_type] = amount

    // 重新计算剩余
    UpdateRemainingResources()
}

// 确认分配
OnConfirmAssignment() {
    foreach (character in all_characters) {
        // 应用食物效果
        character.hunger -= character.assigned_food * 50
        character.hunger = Clamp(character.hunger, 0, 100)

        // 应用水效果
        if (character.assigned_water > 0) {
            character.dehydration_days = 0
        } else {
            character.dehydration_days++
            character.health -= 15 * character.dehydration_days
        }

        // 应用药品效果
        if (character.assigned_medicine > 0) {
            character.health += 30
            character.sickness_progress -= 33.3  // 3天治愈
        }

        // 关系影响
        if (character.assigned_food < character.GetFoodNeed()) {
            character.relationship -= 3  // 未满足需求，关系下降
            character.morale -= 5
        } else {
            character.relationship += 1  // 满足需求，关系微增
        }
    }

    // 消耗库存
    inventory.ApplyAssignments()

    // 保存分配记录
    diary.RecordResourceAssignment(date, assignments)

    // 关闭界面
    ClosePanel()

    // 推进时间 (如果是每日分配环节)
    if (is_daily_allocation) {
        game_time.AdvanceToNextDay()
    }
}
```

**变量名**:
- `inventory.food`: int
- `inventory.water`: int
- `inventory.medicine`: int
- `inventory.fuel`: int
- `inventory.cloth`: int
- `inventory.valuables[]`: Item[]
- `character.assigned_food`: float
- `character.assigned_water`: float
- `character.assigned_medicine`: int
- `character.food_need_base`: float (基础需求1.0, 儿童0.5, 孕妇2.0)
- `character.water_need_base`: float
- `character.is_sick`: bool
- `character.sickness_type`: enum

---

### 3.5 日记界面 (DiaryPanel)

**触发方式**:
- 键盘: J键
- 手柄: Y按钮
- 触摸: UI日记按钮

```
游戏中按J
    ↓
暂停游戏: Time.timeScale = 0
    ↓
播放日记展开动画 (翻书效果)
    ↓
显示日记界面
┌──────────────────────────────────────────────────────────┐
│                      陈默的日记                [X 关闭] │
├────────────────────┬─────────────────────────────────────┤
│ [左页 - 日期列表]  │  [右页 - 日记内容]                  │
│                    │                                     │
│ ┌────────────────┐ │  民国廿六年腊月初二                 │
│ │ 📅 12月1日     │ │  公元一九三七年十二月十五日          │
│ │   初遇王掌柜   │ │  ────────────────────────────      │
│ ├────────────────┤ │                                     │
│ │ 📅 12月2日     │ │  今天是来到1937年的第15天。         │
│ │   安全区       │ │                                     │
│ ├────────────────┤ │  上午，我与王掌柜一起分配食物。     │
│ │ 📅 12月5日     │ │  库存只剩12份，勉强够5天。王掌柜    │
│ │   预警         │ │  的病情加重了，赵护士说需要更多药。 │
│ ├────────────────┤ │                                     │
│ │ 📅 12月13日 ⚠️ │ │  下午，听到安全区外传来枪声。       │
│ │   浩劫开始     │ │  有难民试图逃进来，但被日本兵...    │
│ ├────────────────┤ │                                     │
│ │ ▶ 12月15日     │ │  （此处文字淡化，暗示不忍记录）     │
│ │   艰难的一天   │ │                                     │
│ ├────────────────┤ │  我无法改变历史，但至少要记录下来。 │
│ │ 📅 12月18日    │ │  让未来的人知道，这里曾发生了什么。 │
│ │   ...          │ │                                     │
│ └────────────────┘ │  ────────────────────────────      │
│                    │                                     │
│  [滚动查看更多]    │  [附件]                             │
│                    │  • 📷 安全区外景照片               │
│                    │  • 📋 物资分配记录                 │
│                    │                                     │
│                    │            [上一页]  [下一页]      │
├────────────────────┴─────────────────────────────────────┤
│  统计信息: 已记录 15 天  •  保护 12 人  •  失去 3 人   │
└──────────────────────────────────────────────────────────┘
```

**日记自动生成逻辑**:

```javascript
// 每日结束时自动生成日记
OnDayEnd() {
    diary_entry = new DiaryEntry()
    diary_entry.date = game_time.GetCurrentDate()
    diary_entry.title = GenerateDiaryTitle()  // 根据当天事件生成

    // 生成日记正文
    content = ""

    // 1. 天气/环境开场
    content += GetWeatherDescription()  // "今天依然寒冷..."

    // 2. 重大事件记录
    foreach (event in today_major_events) {
        content += event.diary_description
    }

    // 3. 资源状态
    if (inventory.food < 5) {
        content += "食物越来越少了，大家都在挨饿。"
    }

    // 4. 人物互动
    if (relationship_changed.Count > 0) {
        content += GetRelationshipDescription()
    }

    // 5. 主角心理
    content += GetProtagonistThoughts()  // 根据剧情节点

    // 6. 附件
    if (today_screenshots.Count > 0) {
        diary_entry.attachments = today_screenshots
    }

    // 保存日记条目
    diary_manager.AddEntry(diary_entry)
}

// 示例: 根据事件生成标题
GenerateDiaryTitle() {
    if (flag_chapter13_massacre) {
        return "浩劫开始"
    } else if (character_died_today) {
        return "失去了" + character.name
    } else if (food_crisis) {
        return "食物危机"
    } else {
        return "艰难的一天"
    }
}
```

**特殊日记条目**:
- 12月13日: 特殊排版,加粗字体,血红色日期标记
- 人物死亡日: 黑色边框,哀悼文字
- 重要决策日: 分支记录 "我选择了..." + 后果

**变量名**:
- `diary_manager.entries[]`: DiaryEntry[]
- `diary_entry.date`: DateTime
- `diary_entry.title`: string
- `diary_entry.content`: string
- `diary_entry.attachments[]`: Texture2D[] (照片)
- `diary_entry.mood`: enum (平静/悲伤/愤怒/希望)

---

### 3.6 关系网络界面 (RelationshipPanel)

**触发方式**:
- 键盘: R键
- 手柄: X按钮 (Square)
- 触摸: UI关系按钮

```
游戏中按R
    ↓
暂停游戏: Time.timeScale = 0
    ↓
显示关系网络界面
┌──────────────────────────────────────────────────────────┐
│                    人物关系网络              [X 关闭]   │
├──────────────────────────────────────────────────────────┤
│                                                          │
│                  [网络图可视化区域]                      │
│                                                          │
│            王掌柜 ───75─── 陈默 ───60─── 小梅           │
│              │              │              │             │
│             50             45             55             │
│              │              │              │             │
│            李文斌 ────────赵护士───────魏特琳            │
│                                                          │
│  连线颜色:                                               │
│  - 绿色 (80-100): 生死之交                              │
│  - 蓝色 (60-79):  挚友                                  │
│  - 黄色 (40-59):  朋友                                  │
│  - 灰色 (20-39):  相识                                  │
│  - 红色 (0-19):   陌生/敌对                             │
│                                                          │
├──────────────────────────────────────────────────────────┤
│ [选中角色详情]                                           │
│                                                          │
│ ┌──────────────────────────────────────────────────┐   │
│ │ [头像] 王福生 (王掌柜)                            │   │
│ │                                                  │   │
│ │ 关系值: ████████░ 75/100 (挚友)                 │   │
│ │ 信任度: ████████░ 80/100 (高度信任)             │   │
│ │                                                  │   │
│ │ 状态: 🤒 发烧中 (第2天)                          │   │
│ │                                                  │   │
│ │ 个人资料:                                        │   │
│ │ • 年龄: 50岁                                    │   │
│ │ • 职业: 棺材铺老板                              │   │
│ │ • 家人: 女儿小梅                                │   │
│ │                                                  │   │
│ │ 关系历史:                                        │   │
│ │ • 12月2日: 初次相遇 (+10)                       │   │
│ │ • 12月5日: 分享食物 (+5)                        │   │
│ │ • 12月10日: 深夜长谈 (+8)                       │   │
│ │ • 12月14日: 给予药品 (+10)                      │   │
│ │                                                  │   │
│ │ 特殊事件:                                        │   │
│ │ ✓ 已触发: 王掌柜的往事 (12月8日)                │   │
│ │ 🔒 未解锁: 托付遗愿 (需关系 > 80)               │   │
│ └──────────────────────────────────────────────────┘   │
│                                                          │
│            [上一个角色]           [下一个角色]          │
└──────────────────────────────────────────────────────────┘
```

**网络图交互**:
- 点击节点: 选中角色,显示详情
- 鼠标悬停: 显示简略信息tooltip
- 节点大小: 反映角色重要程度
- 节点颜色:
  - 主角: 金色
  - 主要角色: 蓝色
  - 次要角色: 灰色
  - 已死亡: 黑白 + 半透明

**变量名**:
- `relationship_manager.characters[]`: Character[]
- `relationship_manager.bonds[]`: RelationshipBond[]
- `bond.character_a`: Character
- `bond.character_b`: Character
- `bond.value`: int (0-100)
- `bond.trust`: int (0-100)
- `character.relationship_history[]`: RelationshipEvent[]
- `relationship_event.date`: DateTime
- `relationship_event.description`: string
- `relationship_event.delta`: int (+/- 关系值)

---

### 3.7 对话界面 (DialoguePanel)

**触发方式**:
- 靠近NPC,显示交互提示
- 按E键触发对话

```
按E与NPC交谈
    ↓
淡入对话界面 (0.3秒)
    ↓
显示角色立绘
    ↓
开始对话
┌──────────────────────────────────────────────────────────┐
│                                                          │
│                 [NPC立绘 - 左侧]                         │
│                                                          │
│                                                          │
├──────────────────────────────────────────────────────────┤
│  王掌柜                                  1937年12月15日  │
├──────────────────────────────────────────────────────────┤
│                                                          │
│  "小陈啊，这些天多亏了你。                               │
│   要不是你懂那些历史，咱们怕是早就……唉。"                │
│                                                          │
│  [文字逐字显示，可按Space跳过]                           │
│                                                          │
├──────────────────────────────────────────────────────────┤
│  [选项列表 - 当文字显示完毕后出现]                       │
│                                                          │
│  ▶ 1. "这都是应该做的，咱们是一家人。"  [+5关系]        │
│    2. "我也只是知道大概，细节还得看运气。"  [中立]      │
│    3. "王掌柜，你的病情怎么样了？"  [关心]              │
│    4. [沉默] (不说话)                                    │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

**对话系统流程**:

```javascript
// 触发对话
StartDialogue(npc_id) {
    // 暂停游戏
    Time.timeScale = 0

    // 显示对话UI
    dialogue_panel.SetActive(true)

    // 加载对话数据
    dialogue_data = dialogue_manager.LoadDialogue(npc_id, current_flags)

    // 显示NPC立绘
    ShowCharacterPortrait(npc_id, dialogue_data.portrait_expression)

    // 播放对话音效/语音
    if (has_voice) {
        PlayVoice(dialogue_data.voice_clip)
    }

    // 开始文字显示
    StartTextTyping(dialogue_data.text)
}

// 文字逐字显示
StartTextTyping(text) {
    displayed_text = ""
    current_char_index = 0

    // 每帧显示几个字符
    chars_per_frame = text_speed * Time.deltaTime * 50

    InvokeRepeating("TypeNextChar", 0, 0.02)  // 每0.02秒显示一个字
}

TypeNextChar() {
    if (current_char_index < full_text.Length) {
        displayed_text += full_text[current_char_index]
        text_component.text = displayed_text
        current_char_index++

        // 播放文字音效 (每个字一次)
        if (current_char_index % 2 == 0) {
            PlaySound("sfx_text_beep")
        }
    } else {
        // 文字显示完毕
        CancelInvoke("TypeNextChar")
        OnTextComplete()
    }
}

// 文字显示完毕
OnTextComplete() {
    // 显示选项列表
    if (dialogue_data.has_choices) {
        ShowChoices(dialogue_data.choices)
    } else {
        // 无选项,等待玩家按键继续
        ShowContinuePrompt()  // 显示 "按Space继续..."
    }
}

// 显示选项
ShowChoices(choices) {
    choice_list.Clear()

    for (int i = 0; i < choices.Count; i++) {
        choice = choices[i]

        // 检查选项是否可用
        if (!CheckChoiceCondition(choice.condition)) {
            continue  // 跳过不满足条件的选项
        }

        // 创建选项按钮
        choice_button = Instantiate(choice_button_prefab)
        choice_button.text = choice.text

        // 显示选项后果提示
        if (choice.relationship_delta > 0) {
            choice_button.hint = $"[+{choice.relationship_delta}关系]"
        } else if (choice.relationship_delta < 0) {
            choice_button.hint = $"[{choice.relationship_delta}关系]"
        }

        // 绑定点击事件
        choice_button.onClick.AddListener(() => OnChoiceSelected(choice))

        choice_list.Add(choice_button)
    }

    // 默认选中第一个选项
    SelectChoice(0)
}

// 选择了对话选项
OnChoiceSelected(choice) {
    // 播放确认音效
    PlaySound("sfx_dialogue_select")

    // 应用选项效果
    ApplyChoiceEffects(choice)

    // 进入下一段对话
    if (choice.next_dialogue_id != -1) {
        LoadNextDialogue(choice.next_dialogue_id)
    } else {
        // 对话结束
        EndDialogue()
    }
}

// 应用选项效果
ApplyChoiceEffects(choice) {
    // 关系值变化
    if (choice.relationship_delta != 0) {
        relationship_manager.ChangeRelationship(
            current_npc_id,
            choice.relationship_delta
        )

        // 显示关系变化提示
        ShowFloatingText($"关系 {choice.relationship_delta:+#;-#}")
    }

    // 信任度变化
    if (choice.trust_delta != 0) {
        relationship_manager.ChangeTrust(current_npc_id, choice.trust_delta)
    }

    // 设置标记位
    foreach (flag in choice.set_flags) {
        game_flags.SetFlag(flag, true)
    }

    // 获得/失去物品
    if (choice.item_rewards.Count > 0) {
        foreach (item in choice.item_rewards) {
            inventory.AddItem(item)
            ShowNotification($"获得 {item.name}")
        }
    }

    // 触发事件
    if (choice.trigger_event != null) {
        event_manager.TriggerEvent(choice.trigger_event)
    }
}

// 结束对话
EndDialogue() {
    // 淡出对话界面
    dialogue_panel.FadeOut(0.3)

    // 恢复游戏
    Time.timeScale = 1

    // 记录对话到日记
    diary_manager.RecordDialogue(current_npc_id, dialogue_summary)
}
```

**对话数据结构** (JSON格式):

```json
{
  "dialogue_id": "wang_shopkeeper_day15_01",
  "npc_id": "wang_fugui",
  "portrait": "portrait_wang_worried",
  "voice_clip": "voice_wang_15_01.ogg",
  "conditions": {
    "min_day": 15,
    "flags_required": ["flag_entered_safety_zone"],
    "flags_forbidden": ["flag_wang_dead"]
  },
  "text": "小陈啊，这些天多亏了你。要不是你懂那些历史，咱们怕是早就……唉。",
  "choices": [
    {
      "choice_id": 0,
      "text": "这都是应该做的,咱们是一家人。",
      "condition": null,
      "relationship_delta": 5,
      "trust_delta": 0,
      "set_flags": ["flag_wang_closer"],
      "next_dialogue_id": "wang_shopkeeper_day15_02a"
    },
    {
      "choice_id": 1,
      "text": "我也只是知道大概，细节还得看运气。",
      "condition": null,
      "relationship_delta": 0,
      "trust_delta": 0,
      "next_dialogue_id": "wang_shopkeeper_day15_02b"
    },
    {
      "choice_id": 2,
      "text": "王掌柜，你的病情怎么样了？",
      "condition": {"flag": "flag_wang_sick"},
      "relationship_delta": 3,
      "trust_delta": 0,
      "next_dialogue_id": "wang_shopkeeper_day15_03"
    },
    {
      "choice_id": 3,
      "text": "[沉默]",
      "condition": null,
      "relationship_delta": -2,
      "trust_delta": 0,
      "next_dialogue_id": "wang_shopkeeper_day15_02c"
    }
  ]
}
```

**变量名**:
- `dialogue_manager.current_dialogue`: DialogueData
- `dialogue_data.npc_id`: string
- `dialogue_data.text`: string
- `dialogue_data.portrait`: string (立绘sprite名)
- `dialogue_data.choices[]`: DialogueChoice[]
- `choice.relationship_delta`: int
- `choice.trust_delta`: int
- `choice.set_flags[]`: string[]
- `choice.next_dialogue_id`: string

---

### 3.8 事件选择界面 (EventPanel)

**触发方式**: 剧情自动触发

```
触发重大事件
    ↓
暂停游戏
    ↓
播放事件引入动画
    ↓
显示事件选择界面
┌──────────────────────────────────────────────────────────┐
│                    重大抉择                              │
├──────────────────────────────────────────────────────────┤
│                                                          │
│  [事件背景图 - 全屏暗化]                                 │
│                                                          │
│  ┌────────────────────────────────────────────────┐    │
│  │                                                │    │
│  │  1937年12月20日，夜晚                          │    │
│  │                                                │    │
│  │  日本兵突然闯入安全区搜查"败兵"。             │    │
│  │  李文斌因为手上有茧，被怀疑是军人。           │    │
│  │  如果不及时干预，他将被带走……              │    │
│  │                                                │    │
│  │  你必须做出选择：                              │    │
│  │                                                │    │
│  │  ┌──────────────────────────────────────┐    │    │
│  │  │ ▶ 1. 用日语与日本兵交涉               │    │    │
│  │  │    需求: 日语等级 ≥ 5                 │    │    │
│  │  │    成功率: 70%                        │    │    │
│  │  │    失败后果: 自己被怀疑               │    │    │
│  │  │    关系影响: 李文斌 +15               │    │    │
│  │  ├──────────────────────────────────────┤    │    │
│  │  │   2. 贿赂日本兵                       │    │    │
│  │  │    消耗: 贵重物品 ×2                  │    │    │
│  │  │    成功率: 90%                        │    │    │
│  │  │    失败后果: 失去贵重物品             │    │    │
│  │  │    道德影响: -10 士气                 │    │    │
│  │  ├──────────────────────────────────────┤    │    │
│  │  │   3. 什么都不做                       │    │    │
│  │  │    后果: 李文斌被带走                 │    │    │
│  │  │    关系影响: 所有人 -10               │    │    │
│  │  │    剧情分支: 李文斌死亡               │    │    │
│  │  │    [⚠️ 该角色将永久失去]             │    │    │
│  │  └──────────────────────────────────────┘    │    │
│  │                                                │    │
│  │               [确认选择]                       │    │
│  └────────────────────────────────────────────────┘    │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

**事件选择逻辑**:

```javascript
// 显示事件选择
ShowEventChoice(event_data) {
    // 暂停游戏
    Time.timeScale = 0

    // 显示事件面板
    event_panel.SetActive(true)

    // 设置背景
    event_bg.sprite = event_data.background

    // 显示事件描述
    event_title.text = event_data.title
    event_description.text = event_data.description

    // 显示选项
    foreach (choice in event_data.choices) {
        // 检查选项是否可用
        bool available = CheckChoiceAvailable(choice)

        choice_button = CreateChoiceButton(choice)

        // 如果不满足条件，灰化并显示原因
        if (!available) {
            choice_button.interactable = false
            choice_button.tooltip = choice.unavailable_reason
        }

        // 显示选项详情
        choice_button.text = choice.text
        choice_button.requirements = FormatRequirements(choice)
        choice_button.success_rate = choice.success_rate
        choice_button.consequences = FormatConsequences(choice)
    }
}

// 检查选项是否可用
CheckChoiceAvailable(choice) {
    // 检查技能需求
    if (choice.skill_requirement != null) {
        if (player.skills[choice.skill_requirement.skill] < choice.skill_requirement.level) {
            choice.unavailable_reason = $"需要 {choice.skill_requirement.skill} 等级 {choice.skill_requirement.level}"
            return false
        }
    }

    // 检查物品需求
    if (choice.item_cost != null) {
        if (inventory.GetItemCount(choice.item_cost.item_id) < choice.item_cost.count) {
            choice.unavailable_reason = $"需要 {choice.item_cost.item_name} ×{choice.item_cost.count}"
            return false
        }
    }

    // 检查关系需求
    if (choice.relationship_requirement != null) {
        if (relationship_manager.GetRelationship(choice.relationship_requirement.npc_id) < choice.relationship_requirement.min_value) {
            choice.unavailable_reason = $"关系值不足"
            return false
        }
    }

    return true
}

// 确认选择
OnEventChoiceConfirmed(choice) {
    // 播放确认音效
    PlaySound("sfx_event_confirm")

    // 扣除消耗
    if (choice.item_cost != null) {
        inventory.RemoveItem(choice.item_cost.item_id, choice.item_cost.count)
    }

    // 判断成功/失败
    float roll = Random.Range(0f, 100f)
    bool success = roll <= choice.success_rate

    // 应用结果
    if (success) {
        ApplyEventOutcome(choice.success_outcome)
        ShowEventResult(choice.success_result_text)
    } else {
        ApplyEventOutcome(choice.failure_outcome)
        ShowEventResult(choice.failure_result_text)
    }

    // 记录到日记
    diary_manager.RecordMajorChoice(event_data, choice, success)

    // 关闭事件面板
    CloseEventPanel()
}

// 应用事件结果
ApplyEventOutcome(outcome) {
    // 设置标记位
    foreach (flag in outcome.set_flags) {
        game_flags.SetFlag(flag, true)
    }

    // 关系变化
    foreach (relationship_change in outcome.relationship_changes) {
        relationship_manager.ChangeRelationship(
            relationship_change.npc_id,
            relationship_change.delta
        )
    }

    // 士气变化
    if (outcome.morale_delta != 0) {
        group_manager.ChangeMorale(outcome.morale_delta)
    }

    // 角色生死
    if (outcome.character_dies != null) {
        character_manager.KillCharacter(outcome.character_dies)
        ShowCharacterDeathCutscene(outcome.character_dies)
    }

    // 触发后续事件
    if (outcome.trigger_event != null) {
        event_manager.QueueEvent(outcome.trigger_event)
    }
}
```

**事件数据结构** (JSON):

```json
{
  "event_id": "event_save_li_wenbin",
  "title": "日本兵搜查",
  "date": "1937-12-20",
  "background": "bg_event_soldier_search",
  "description": "日本兵突然闯入安全区搜查"败兵"。李文斌因为手上有茧，被怀疑是军人。如果不及时干预，他将被带走……",
  "choices": [
    {
      "choice_id": 0,
      "text": "用日语与日本兵交涉",
      "skill_requirement": {
        "skill": "japanese",
        "level": 5
      },
      "success_rate": 70,
      "success_outcome": {
        "set_flags": ["flag_saved_li_wenbin", "flag_used_japanese"],
        "relationship_changes": [
          {"npc_id": "li_wenbin", "delta": 15}
        ]
      },
      "success_result_text": "你流利的日语让日本兵相信了你的解释，李文斌得救了。",
      "failure_outcome": {
        "set_flags": ["flag_self_suspected"],
        "relationship_changes": [
          {"npc_id": "li_wenbin", "delta": -5}
        ]
      },
      "failure_result_text": "日本兵没有相信你，反而开始怀疑你的身份……"
    },
    {
      "choice_id": 1,
      "text": "贿赂日本兵",
      "item_cost": {
        "item_id": "valuable",
        "item_name": "贵重物品",
        "count": 2
      },
      "success_rate": 90,
      "success_outcome": {
        "set_flags": ["flag_saved_li_wenbin", "flag_bribed_soldier"],
        "relationship_changes": [
          {"npc_id": "li_wenbin", "delta": 10}
        ],
        "morale_delta": -10
      },
      "success_result_text": "日本兵收下贿赂后离开了。李文斌得救了，但大家的士气下降了。"
    },
    {
      "choice_id": 2,
      "text": "什么都不做",
      "success_rate": 100,
      "success_outcome": {
        "set_flags": ["flag_li_wenbin_taken", "flag_li_wenbin_dead"],
        "character_dies": "li_wenbin",
        "relationship_changes": [
          {"npc_id": "all", "delta": -10}
        ],
        "morale_delta": -20
      },
      "success_result_text": "李文斌被日本兵带走了。几天后，有人在江边发现了他的尸体……"
    }
  ]
}
```

---

### 3.9 章节转场界面 (ChapterTransition)

**触发方式**: 章节结束时自动触发

```
章节结束条件满足
    ↓
淡出游戏画面 (2秒)
    ↓
显示章节转场界面
┌──────────────────────────────────────────────────────────┐
│                                                          │
│                                                          │
│                   第三章 完成                            │
│                                                          │
│                 生存的煎熬                               │
│                                                          │
│             1937年12月13-20日                            │
│                                                          │
│                                                          │
│              ────────────────────                        │
│                                                          │
│                  章节统计                                │
│                                                          │
│              保护人数: 12/15                             │
│              获得资源: 食物×24, 水×18                    │
│              完成事件: 8/10                              │
│              解锁成就: 2                                 │
│                                                          │
│                                                          │
│            [按任意键继续下一章]                          │
│                                                          │
└──────────────────────────────────────────────────────────┘
    ↓ 按任意键
播放下一章引入过场动画
    ↓
加载下一章场景
```

**章节统计数据**:

```javascript
// 章节结束时计算统计
OnChapterEnd(chapter_index) {
    chapter_stats = new ChapterStats()

    // 保护人数
    chapter_stats.people_saved = character_manager.GetAliveCount()
    chapter_stats.people_total = character_manager.GetTotalCount()

    // 获得资源
    chapter_stats.resources_gained = inventory.GetChapterGain(chapter_index)

    // 完成事件
    chapter_stats.events_completed = event_manager.GetCompletedCount(chapter_index)
    chapter_stats.events_total = event_manager.GetTotalCount(chapter_index)

    // 解锁成就
    chapter_stats.achievements_unlocked = achievement_manager.GetChapterUnlocked(chapter_index)

    // 显示转场界面
    ShowChapterTransition(chapter_index, chapter_stats)

    // 自动保存
    if (settings.auto_save) {
        save_manager.AutoSave()
    }
}
```

---

## 四、错误处理流程

### 4.1 存档损坏

```
尝试读取存档
    ↓
检测到存档文件损坏
    ↓
显示错误对话框:
┌──────────────────────────────┐
│  ⚠️ 存档文件已损坏            │
│                              │
│  该存档槽数据无法读取。      │
│  可能的原因:                 │
│  • 文件被意外修改           │
│  • 磁盘错误                 │
│  • 版本不兼容               │
│                              │
│  [删除该存档]  [返回]       │
└──────────────────────────────┘
```

**错误处理代码**:

```javascript
LoadSaveFile(slot_index) {
    try {
        save_data = File.ReadAllText(save_path)
        save_object = JsonConvert.DeserializeObject<SaveData>(save_data)

        // 验证存档版本
        if (save_object.save_version != current_version) {
            ShowError("存档版本不兼容，请更新游戏")
            return false
        }

        // 验证数据完整性
        if (!ValidateSaveData(save_object)) {
            ShowError("存档数据不完整或已损坏")
            return false
        }

        return true

    } catch (FileNotFoundException) {
        ShowError("存档文件不存在")
        return false
    } catch (JsonException) {
        ShowError("存档文件格式错误")
        return false
    } catch (Exception e) {
        ShowError($"未知错误: {e.Message}")
        LogError(e)
        return false
    }
}
```

---

### 4.2 场景加载失败

```
尝试加载场景
    ↓
加载失败 (资源缺失/内存不足)
    ↓
显示错误提示:
"场景加载失败，即将返回主菜单"
    ↓
自动返回主菜单
```

---

### 4.3 输入冲突

```
检测到多个相同按键绑定
    ↓
在设置菜单中高亮冲突按键
    ↓
提示用户重新映射
```

---

## 五、性能优化规范

### 5.1 UI对象池

```javascript
// UI元素复用,避免频繁创建销毁
ui_object_pool = new Dictionary<string, Queue<GameObject>>()

GetUIObject(prefab_name) {
    if (!ui_object_pool.ContainsKey(prefab_name)) {
        ui_object_pool[prefab_name] = new Queue<GameObject>()
    }

    if (ui_object_pool[prefab_name].Count > 0) {
        // 从对象池取出
        obj = ui_object_pool[prefab_name].Dequeue()
        obj.SetActive(true)
        return obj
    } else {
        // 创建新对象
        return Instantiate(ui_prefabs[prefab_name])
    }
}

ReturnUIObject(prefab_name, obj) {
    obj.SetActive(false)
    ui_object_pool[prefab_name].Enqueue(obj)
}
```

---

### 5.2 UI更新频率限制

```javascript
// 避免每帧更新UI,使用脏标记
health_dirty = false
hunger_dirty = false

UpdatePlayerHealth(new_value) {
    player.health = new_value
    health_dirty = true  // 标记为脏
}

Update() {
    // 每0.1秒更新一次UI
    if (Time.time - last_ui_update > 0.1f) {
        if (health_dirty) {
            ui_health_slider.value = player.health / 100f
            health_dirty = false
        }
        if (hunger_dirty) {
            ui_hunger_slider.value = player.hunger / 100f
            hunger_dirty = false
        }
        last_ui_update = Time.time
    }
}
```

---

### 5.3 懒加载

```javascript
// 仅在需要时加载UI资源
LoadDialoguePanel() {
    if (dialogue_panel == null) {
        dialogue_panel = Instantiate(Resources.Load("UI/DialoguePanel"))
        dialogue_panel.SetActive(false)
    }
}
```

---

## 六、输入映射总表

### 6.1 键盘+鼠标

| 功能 | 按键 | 变量名 | 说明 |
|------|------|--------|------|
| 向上移动 | W | input.move_up | 游戏场景移动 |
| 向下移动 | S | input.move_down | 游戏场景移动 |
| 向左移动 | A | input.move_left | 游戏场景移动 |
| 向右移动 | D | input.move_right | 游戏场景移动 |
| 互动 | E | input.interact | 与NPC/物体交互 |
| 跳过对话 | Space | input.skip | 快进对话文字 |
| 暂停 | ESC | input.pause | 打开暂停菜单 |
| 物品栏 | Tab | input.inventory | 打开资源管理 |
| 日记 | J | input.diary | 打开日记界面 |
| 关系网 | R | input.relationship | 打开关系界面 |
| 快速保存 | F5 | input.quick_save | 快速保存到自动槽 |
| 快速读取 | F9 | input.quick_load | 快速读取自动槽 |
| 截图 | F12 | input.screenshot | 截图到Pictures文件夹 |

---

### 6.2 手柄 (Xbox布局)

| 功能 | 按键 | 变量名 | 说明 |
|------|------|--------|------|
| 移动 | 左摇杆 | input.axis_move | 8方向移动 |
| 互动 | A | input.interact | 确认/互动 |
| 取消 | B | input.cancel | 返回/取消 |
| 物品栏 | X | input.inventory | 打开资源管理 |
| 日记 | Y | input.diary | 打开日记 |
| 暂停 | Start | input.pause | 暂停菜单 |
| 关系网 | Select | input.relationship | 关系界面 |
| UI导航 | D-Pad | input.ui_navigate | 菜单导航 |
| 快速访问 | LB/RB | input.quick_tab | 快速切换界面 |

---

### 6.3 触屏 (移动端,可选)

| 功能 | 操作 | 说明 |
|------|------|------|
| 移动 | 虚拟摇杆 | 左下角 |
| 互动 | 点击按钮 | 屏幕提示位置 |
| 对话继续 | 点击屏幕任意处 | - |
| 菜单 | UI按钮 | 右上角 |

---

## 七、状态转换图

### 7.1 游戏全局状态机

```
游戏启动
    ↓
[MainMenu State]
    ├─→ [NewGame] → [DifficultySelect] → [Loading] → [Gameplay]
    ├─→ [Continue] → [SaveSelect] → [Loading] → [Gameplay]
    ├─→ [Settings] → [SettingsMenu] → [MainMenu]
    └─→ [Exit] → 退出

[Gameplay State]
    ├─→ [Pause] → [PauseMenu]
    │       ├─→ [Resume] → [Gameplay]
    │       ├─→ [Save] → [SaveMenu] → [PauseMenu]
    │       ├─→ [Load] → [LoadMenu] → [Loading] → [Gameplay]
    │       └─→ [Quit] → [MainMenu]
    │
    ├─→ [Dialogue] → [DialoguePanel] → [Gameplay]
    ├─→ [Event] → [EventPanel] → [Gameplay]
    ├─→ [Inventory] → [InventoryPanel] → [Gameplay]
    ├─→ [Diary] → [DiaryPanel] → [Gameplay]
    ├─→ [Relationship] → [RelationshipPanel] → [Gameplay]
    └─→ [ChapterEnd] → [ChapterTransition] → [Loading] → [Gameplay]
```

**状态变量**:
- `game_state.current_state`: enum GameState
- GameState枚举:
  - MainMenu
  - Gameplay
  - Paused
  - Dialogue
  - Event
  - Inventory
  - Diary
  - Relationship
  - Loading
  - ChapterTransition

---

### 7.2 对话系统状态机

```
[Idle]
    ↓ 按E触发
[DialogueStarting]
    ├─ 淡入界面
    ├─ 加载对话数据
    └─ 显示NPC立绘
    ↓
[TextTyping]
    ├─ 逐字显示文字
    ├─ 播放音效/语音
    └─ 可按Space跳过
    ↓
[WaitingForChoice]
    ├─ 显示选项列表
    ├─ 等待玩家选择
    └─ 或等待继续按键
    ↓
[ProcessingChoice]
    ├─ 应用选项效果
    ├─ 播放反馈动画
    └─ 加载下一段对话
    ↓
[DialogueEnding]
    ├─ 淡出界面
    ├─ 记录到日记
    └─ 恢复游戏
    ↓
[Idle]
```

---

## 八、技术实现要点

### 8.1 UI框架选择

**Unity推荐**:
- uGUI (Unity UI)
- TextMeshPro (文字渲染)
- DOTween (UI动画)

**Unreal推荐**:
- UMG (Unreal Motion Graphics)
- Slate框架

---

### 8.2 本地化集成

```javascript
// 所有UI文本从本地化表读取
LocalizationManager.Init("zh-CN")  // 默认简体中文

// 获取本地化文本
string text = Localization.Get("ui.mainmenu.new_game")
// 返回: "新游戏"

// 带参数的本地化
string text = Localization.Get("ui.inventory.food_count", inventory.food)
// 模板: "食物: {0}"
// 返回: "食物: 12"
```

**本地化表格式** (JSON):

```json
{
  "zh-CN": {
    "ui.mainmenu.new_game": "新游戏",
    "ui.mainmenu.continue": "继续游戏",
    "ui.inventory.food": "食物",
    "ui.inventory.food_count": "食物: {0}"
  },
  "en-US": {
    "ui.mainmenu.new_game": "New Game",
    "ui.mainmenu.continue": "Continue",
    "ui.inventory.food": "Food",
    "ui.inventory.food_count": "Food: {0}"
  }
}
```

---

### 8.3 无障碍功能

#### 色盲模式实现

```javascript
// 应用色盲滤镜
ApplyColorBlindFilter(mode) {
    switch (mode) {
        case ColorBlindMode.Protanopia:  // 红色盲
            color_grading.colorFilter = protanopia_lut
            break
        case ColorBlindMode.Deuteranopia:  // 绿色盲
            color_grading.colorFilter = deuteranopia_lut
            break
        case ColorBlindMode.Tritanopia:  // 蓝色盲
            color_grading.colorFilter = tritanopia_lut
            break
        case ColorBlindMode.Achromatopsia:  // 全色盲
            color_grading.saturation = 0  // 去饱和度
            break
    }
}

// 使用形状+颜色双重编码
// 例如: 健康条不仅用颜色,还用图案
health_bar.pattern = health > 50 ? "solid" : "striped"
```

---

### 8.4 自适应布局

```javascript
// 根据屏幕比例调整UI
AdjustUIForAspectRatio() {
    aspect_ratio = Screen.width / Screen.height

    if (aspect_ratio > 2.0) {
        // 21:9 超宽屏
        hud_left.anchoredPosition = new Vector2(200, 0)  // 左移
        hud_right.anchoredPosition = new Vector2(-200, 0)  // 右移
    } else if (aspect_ratio < 1.5) {
        // 4:3 旧屏幕
        hud_scale = 0.9f  // 缩小HUD
    } else {
        // 16:9 标准
        // 使用默认值
    }
}
```

---

## 九、测试检查清单

### UI交互测试

- [ ] 主菜单所有按钮可点击且功能正常
- [ ] 新游戏流程完整 (难度选择→过场→游戏开始)
- [ ] 存档/读档功能正常,数据完整恢复
- [ ] 章节选择解锁逻辑正确
- [ ] 设置菜单所有选项应用生效
- [ ] 暂停菜单在游戏中随时可访问
- [ ] 资源管理界面计算正确
- [ ] 日记自动生成且内容准确
- [ ] 关系网络图正确显示关系值
- [ ] 对话系统文字显示正常,选项生效
- [ ] 事件选择界面条件判断正确
- [ ] 章节转场统计数据准确

### 输入测试

- [ ] 键盘所有按键映射正确
- [ ] 手柄所有按钮映射正确
- [ ] 按键重新映射功能正常
- [ ] 多输入设备切换无冲突
- [ ] UI导航 (Tab切换) 顺序合理

### 性能测试

- [ ] UI帧率稳定60fps
- [ ] 面板打开/关闭延迟<100ms
- [ ] 大量文本显示无卡顿
- [ ] 内存占用在合理范围

### 兼容性测试

- [ ] 1920×1080分辨率正常
- [ ] 1280×720分辨率正常
- [ ] 3840×2160 (4K) 正常
- [ ] 21:9宽屏UI适配正常
- [ ] 全屏/窗口模式切换正常
- [ ] 不同语言显示正常
- [ ] 色盲模式滤镜生效

### 错误处理测试

- [ ] 存档损坏有错误提示
- [ ] 场景加载失败有fallback
- [ ] 资源缺失有提示
- [ ] 非法输入有防护

---

## 十、附录：快速参考

### 核心变量速查表

| 变量名 | 类型 | 说明 |
|--------|------|------|
| `game_state.current_state` | enum | 游戏当前状态 |
| `game_time.current_date` | DateTime | 游戏内日期 |
| `player.health` | int (0-100) | 玩家健康值 |
| `player.hunger` | int (0-100) | 玩家饥饿度 |
| `player.morale` | int (0-100) | 玩家士气 |
| `inventory.food` | int | 食物数量 |
| `inventory.water` | int | 水数量 |
| `inventory.medicine` | int | 药品数量 |
| `save_manager.current_slot` | int | 当前存档槽 |
| `relationship_manager.bonds[]` | RelationshipBond[] | 关系数据 |
| `game_flags.flags{}` | Dictionary<string, bool> | 游戏标记位 |

---

### UI Prefab命名规范

```
UI_MainMenu               # 主菜单面板
UI_PauseMenu              # 暂停菜单面板
UI_SettingsMenu           # 设置菜单面板
UI_InventoryPanel         # 资源管理面板
UI_DiaryPanel             # 日记面板
UI_RelationshipPanel      # 关系网络面板
UI_DialoguePanel          # 对话面板
UI_EventPanel             # 事件选择面板
UI_ChapterTransition      # 章节转场面板
UI_SaveLoadPanel          # 存读档面板
UI_ConfirmDialog          # 确认对话框
UI_NotificationToast      # 通知提示
UI_LoadingScreen          # 载入画面
```

---

**文档版本**: v1.0
**完成日期**: 2025-11-05
**审核状态**: 待审核
**下一步**: 开始UI原型实现,与程序对接集成

---

**关联文档**:
- `/docs/ui/ui_overview.md` - UI设计总览
- `/docs/ui/ui_menus.md` - 菜单界面设计
- `/docs/technical/technical_save_system.md` - 存档系统技术文档
- `/docs/execution/achievement_system.md` - 成就系统 (本文档配套)
