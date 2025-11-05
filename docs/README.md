# 《秦淮旧梦》游戏策划文档索引

## 文档结构

本策划案按功能模块拆分为以下详细文档，每个文档都包含可执行的具体内容。

### 📁 美术资源 (art/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [art_assets_scenes.md](art/art_assets_scenes.md) | 场景美术资源清单 | ✅ |
| [art_assets_characters.md](art/art_assets_characters.md) | 角色美术资源清单 | ✅ |
| [art_assets_ui.md](art/art_assets_ui.md) | UI美术资源清单 | ✅ |
| [art_assets_vfx.md](art/art_assets_vfx.md) | 特效资源清单 | ✅ |
| [art_style_guide.md](art/art_style_guide.md) | 美术风格指南 | ✅ |

### 🎵 音频资源 (audio/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [audio_music.md](audio/audio_music.md) | 音乐资源清单 | ✅ |
| [audio_sfx.md](audio/audio_sfx.md) | 音效资源清单 | ✅ |
| [audio_voice.md](audio/audio_voice.md) | 语音资源清单（可选） | ✅ |

### 📖 剧情设计 (narrative/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [narrative_overview.md](narrative/narrative_overview.md) | 剧情概览 | ✅ |
| [narrative_chapters.md](narrative/narrative_chapters.md) | 章节详细设计 | ✅ |
| [narrative_characters.md](narrative/narrative_characters.md) | 角色设定与关系 | ✅ |
| [dialogue_system.md](narrative/dialogue_system.md) | 对话系统设计 | ✅ |
| [dialogue_scripts.md](narrative/dialogue_scripts.md) | 对话脚本 | ✅ |

### 🎮 玩法设计 (gameplay/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [gameplay_core_loop.md](gameplay/gameplay_core_loop.md) | 核心循环设计 | ✅ |
| [gameplay_systems.md](gameplay/gameplay_systems.md) | 系统设计总览 | ✅ |
| [gameplay_survival.md](gameplay/gameplay_survival.md) | 生存管理系统 | ✅ |
| [gameplay_relationship.md](gameplay/gameplay_relationship.md) | 关系系统 | ✅ |
| [gameplay_events.md](gameplay/gameplay_events.md) | 事件系统 | ✅ |
| [gameplay_diary.md](gameplay/gameplay_diary.md) | 日记系统 | ✅ |
| [gameplay_numerical.md](gameplay/gameplay_numerical.md) | 数值策划 | ✅ |
| [level_design.md](gameplay/level_design.md) | 关卡设计 | ✅ |

### 🖥️ UI/UX设计 (ui/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [ui_overview.md](ui/ui_overview.md) | UI/UX总览 | ✅ |
| [ui_hud.md](ui/ui_hud.md) | HUD设计 | ✅ |
| [ui_menus.md](ui/ui_menus.md) | 菜单界面设计 | ✅ |
| [ui_inventory.md](ui/ui_inventory.md) | 物品管理界面 | ✅ |
| [ui_dialogue.md](ui/ui_dialogue.md) | 对话界面 | ✅ |
| [ui_diary.md](ui/ui_diary.md) | 日记界面 | ✅ |

### 💻 技术文档 (technical/)

| 文档名称 | 说明 | 状态 |
|---------|------|------|
| [technical_overview.md](technical/technical_overview.md) | 技术总览 | ✅ |
| [technical_architecture.md](technical/technical_architecture.md) | 架构设计 | ✅ |
| [technical_data_structure.md](technical/technical_data_structure.md) | 数据结构设计 | ✅ |
| [technical_save_system.md](technical/technical_save_system.md) | 存档系统 | ✅ |

## 使用说明

### 文档规范

每个详细文档都遵循以下格式：

1. **概述** - 该模块的功能说明
2. **详细清单** - 具体内容列表（美术/音频资源包含格式、尺寸等技术规格）
3. **制作要求** - 具体的制作标准和要求
4. **优先级** - P0（核心）、P1（重要）、P2（次要）

### 资源命名规范

```
类型_场景/系统_具体内容_变体.扩展名

示例：
scene_safetyzone_exterior_day.psd      # 场景-安全区-外景-白天
char_wangfugui_portrait_happy.psd      # 角色-王掌柜-立绘-高兴
ui_button_confirm_normal.png           # UI-按钮-确认-正常状态
sfx_footstep_stone_01.wav             # 音效-脚步声-石板路-变体1
```

### 版本控制

- 所有文档都包含版本号和更新日期
- 每次重大修改需要更新版本号
- 保留修改记录

## 开发流程建议

### 阶段1：原型开发 (3个月)
- 专注核心玩法系统
- 需要的文档：
  - gameplay_core_loop.md
  - gameplay_survival.md
  - gameplay_numerical.md
  - technical_architecture.md
  - 基础美术和音效

### 阶段2：垂直切片 (3个月)
- 完整实现1-2个章节
- 需要的文档：
  - narrative_chapters.md（第1-2章）
  - level_design.md（第1-2章）
  - 所有相关资源文档

### 阶段3：全内容开发 (6-12个月)
- 实现所有章节
- 需要所有文档

### 阶段4：打磨与测试 (3-6个月)
- 平衡调整
- Bug修复
- 优化

## 资源统计汇总

待所有详细文档完成后，这里将显示：

- 总美术资源数量：待统计
- 总音频资源数量：待统计
- 总文本数量：待统计
- 估算工作量：待统计

---

**文档版本**: v1.0
**创建日期**: 2025-11-05
**最后更新**: 2025-11-05
**负责人**: [待填写]
