# 本地化指南 / Localization Guide
## 《秦淮旧梦》翻译手册 / "Qinhuai Old Dream" Translation Handbook

**文档版本**: v1.0
**最后更新**: 2025-11-05
**关联文档**: `narrative/dialogue_scripts.md`, `audio/audio_voice.md`, `ui/ui_overview.md`

---

## 目录 / Table of Contents

1. [概述 / Overview](#一概述--overview)
2. [支持语言 / Supported Languages](#二支持语言--supported-languages)
3. [翻译原则 / Translation Principles](#三翻译原则--translation-principles)
4. [特殊内容处理 / Special Content Handling](#四特殊内容处理--special-content-handling)
5. [UI文本长度限制 / UI Text Length Limits](#五ui文本长度限制--ui-text-length-limits)
6. [变量文本模式 / Variable Text Patterns](#六变量文本模式--variable-text-patterns)
7. [术语表 / Glossary](#七术语表--glossary)
8. [文件格式规范 / File Format Specifications](#八文件格式规范--file-format-specifications)
9. [QA检查清单 / QA Checklist](#九qa检查清单--qa-checklist)
10. [文化敏感性指南 / Cultural Sensitivity Guide](#十文化敏感性指南--cultural-sensitivity-guide)

---

## 一、概述 / Overview

### 1.1 游戏背景 / Game Background

**游戏名称**: 秦淮旧梦 / Qinhuai Old Dream
**类型**: 叙事驱动冒险游戏 / Narrative-Driven Adventure
**主题**: 南京大屠杀历史 / Nanjing Massacre History
**核心价值**: 铭记历史、和平反思 / Remember History, Reflect on Peace

**翻译使命** / Translation Mission:
本游戏承载着沉重的历史责任。翻译不仅是语言转换，更是文化传递和历史教育。每一句台词都需要准确传达情感、历史事实和人性光辉。

This game carries significant historical responsibility. Translation is not merely language conversion, but cultural transmission and historical education. Every line must accurately convey emotion, historical facts, and humanity's light.

### 1.2 翻译团队结构 / Translation Team Structure

| 角色 | 职责 | 要求 |
|------|------|------|
| **主翻译** | 核心文本翻译 | 母语水平+历史知识 |
| **历史顾问** | 史实核查 | 历史学专业背景 |
| **文化顾问** | 文化适配 | 了解目标文化 |
| **校对编辑** | 质量把控 | 语言+游戏经验 |
| **本地化测试员** | 游戏内测试 | 目标语言玩家 |

---

## 二、支持语言 / Supported Languages

### 2.1 语言列表 / Language List

| 语言 | 代码 | 优先级 | 字数统计 | 预计工期 | 配音 | 状态 |
|------|------|--------|---------|---------|------|------|
| **简体中文** | zh-CN | P0 (源语言) | ~80,000字 | - | 全配音 | 开发中 |
| **English** | en-US | P0 | ~60,000 words | 8周 | 主要角色配音 | 计划中 |
| **日本語** | ja-JP | P1 | ~70,000字 | 10周 | 日本角色配音 | 计划中 |
| **繁體中文** | zh-TW | P1 | ~80,000字 | 4周 | 复用简中配音 | 计划中 |
| **한국어** | ko-KR | P2 | ~65,000자 | 8周 | 仅字幕 | 待定 |

### 2.2 字体支持 / Font Support

| 语言 | 字体 | 字号范围 | 备注 |
|------|------|---------|------|
| 简体中文 | 思源黑体 CN, 仿宋GB2312 | 24-48px | 1937年用仿宋 |
| English | Noto Sans, Crimson Text | 20-42px | 衬线字体用于日记 |
| 日本語 | 思源黑体 JP, 源ノ明朝 | 24-48px | 明朝体用于历史文本 |
| 繁體中文 | 思源黑体 TW, 標楷體 | 24-48px | 复用简中风格 |
| 한국어 | Noto Sans KR | 22-46px | 字号略小 |

---

## 三、翻译原则 / Translation Principles

### 3.1 核心原则 / Core Principles

#### 1. **历史准确性 > 文学美感** / Historical Accuracy > Literary Beauty

```
原则：历史事实不可更改，人名地名必须准确。
Principle: Historical facts are unchangeable; names and places must be accurate.

错误示例 (Wrong):
"魏特琳女士" → "Miss Weiting" ❌
正确 (Correct):
"魏特琳女士" → "Miss Minnie Vautrin" ✓

理由：魏特琳是历史人物，必须使用真实英文名。
Reason: Minnie Vautrin is a historical figure; her real English name must be used.
```

#### 2. **情感传达 > 字面翻译** / Emotional Conveyance > Literal Translation

```
原文："爹，你别走……"（小梅哭喊）
字面翻译 (Literal): "Father, don't leave..."
情感翻译 (Emotional): "Papa, please... don't leave me..." ✓

理由：增加"please"和"me"增强情感冲击力。
Reason: Adding "please" and "me" enhances emotional impact.
```

#### 3. **文化适配 vs 文化保留** / Cultural Adaptation vs Preservation

**需要适配** (Adapt):
- 俗语、歇后语 → 目标语言的等效表达
- 文化特定幽默 → 调整为可理解的幽默

**需要保留** (Preserve):
- 历史名词 → 保留中文拼音+注释
- 古诗词 → 保留原文+翻译+注释
- 称呼系统 → 保留"大哥""掌柜"等，加括号说明

```
示例 (Example):
"王掌柜" → "Shopkeeper Wang" (老王, Lao Wang) ✓
保留"掌柜"概念，加注释说明身份。
Preserve the "Shopkeeper" concept with contextual explanation.
```

#### 4. **语境一致性** / Contextual Consistency

**同一角色的翻译必须保持一致的语气和用词**。
**The same character's translation must maintain consistent tone and vocabulary.**

```
王掌柜说话风格：憨厚、本地化、略带口音
Shopkeeper Wang's speech style: Honest, localized, slight accent

一致的翻译风格:
"哎呦" → "Oh my" (consistently)
"小陈啊" → "Little Chen" (not "Young Chen" or "Chen, my boy")
```

### 3.2 翻译流程 / Translation Workflow

```
1. 初译 (First Draft)
   ↓
2. 历史顾问审核 (Historical Review)
   ↓
3. 文化适配 (Cultural Adaptation)
   ↓
4. 游戏内测试 (In-Game Testing)
   ↓
5. 校对修改 (Proofreading)
   ↓
6. 最终审核 (Final Review)
```

---

## 四、特殊内容处理 / Special Content Handling

### 4.1 方言与口音 / Dialect & Accent

#### 王福生（王掌柜）的南京口音 / Shopkeeper Wang's Nanjing Accent

**简体中文源文本** (Original Chinese):
```
"哎呦，小陈啊，这大冷天的，快进屋暖和暖和。"
```

**英文翻译建议** (English Translation):
```
选项A (推荐): 使用略显口语化的语法，体现市井感
"Oh my, Little Chen! It's freezing out there, come on in and warm yourself up!"

选项B: 标准英语但保留亲切感
"Oh, Little Chen! It's so cold today. Please, come inside and warm up."

❌ 避免：过度方言化（如Cockney口音），会造成混淆
```

**日文翻译建议** (Japanese Translation):
```
推荐：使用庶民的温暖语气，略带老年人口吻
"おやおや、陳さん、こんな寒い日に...さあさあ、中に入って温まってください。"

注意：避免使用太重的方言（如関西弁），保持南京本地的平实感
```

**处理原则** (Principles):
1. 不要使用目标语言的具体方言（避免文化错位）
2. 通过口语化、语气词、句式体现"本地人"感觉
3. 保持亲切、憨厚的人物性格

### 4.2 古典诗词与文学引用 / Classical Poetry & Literary References

游戏中出现的古诗词翻译策略：

#### 示例1：孙老先生教学场景

**原文** (Original):
```
孙老先生："岁寒，然后知松柏之后凋也。"——《论语·子罕》
```

**英文翻译** (English):
```
格式：原文拼音 + 英译 + 出处 + 注释

Old Master Sun: "Sui han, ran hou zhi song bai zhi hou diao ye."
[Translation: "When the cold season comes, only then do we know that the pine and cypress are the last to wither."]
— The Analects of Confucius, Chapter Zihan

[Note: A metaphor for integrity in adversity—true character is revealed in hardship.]
```

**日文翻译** (Japanese):
```
孫先生：「歳寒くして、然る後に松柏の後に凋むを知る。」——『論語·子罕』
[訳：厳しい寒さが来て初めて、松や柏が最後まで枯れないことが分かる]

[注：逆境においても節操を守る者の喩え。真の人格は困難の中で現れる]
```

**翻译原则**:
- ✓ 保留中文原文（拼音或汉字）
- ✓ 提供准确翻译
- ✓ 注明出处
- ✓ 加文化注释
- ✓ 在UI中提供可选[查看详情]按钮

#### 示例2：秦淮河诗句

**原文**:
```
"烟笼寒水月笼沙，夜泊秦淮近酒家。" ——杜牧《泊秦淮》
```

**英文翻译**:
```
"Mist veils the cold river, moonlight shrouds the sand,
At night, our boat moors by Qinhuai, near the tavern stand."
— Du Mu, "Mooring on the Qinhuai River"

[Historical Context: This Tang dynasty poem reflects on the fall of dynasties and the passage of time, deeply connected to Nanjing's history.]
```

**注意**：诗词翻译可以略带韵律，但准确性优先于押韵。

### 4.3 历史术语 / Historical Terms

#### 关键术语统一翻译表

| 中文 | English | 日本語 | 说明 / Notes |
|------|---------|--------|--------------|
| 南京大屠杀 | Nanjing Massacre | 南京大虐殺 | 官方历史术语，不可更改 |
| 安全区 | Safety Zone | 安全区 | 国际安全区，历史专有名词 |
| 金陵女子学院 | Ginling College / Ginling Women's College | 金陵女子大学 | 历史真实机构名 |
| 魏特琳女士 | Miss Minnie Vautrin | ミニー・ヴォートリン | 历史人物真名 |
| 国际红十字会 | International Red Cross | 国際赤十字 | 国际组织标准译名 |
| 中华门 | Zhonghua Gate | 中華門 | 南京城门，地名保留拼音 |
| 难民 | Refugees | 難民 | - |
| 抽签处决 | Lottery Execution | くじ引き処刑 | 历史事件，慎重翻译 |

**翻译注意事项**:
1. 历史人名、地名使用官方英文拼写（如Minnie Vautrin，而非"Wei Telin"）
2. 历史事件名称参考学术文献标准译法
3. 日文翻译需注意：日本国内对该历史的不同表述，游戏使用国际通用译法

### 4.4 角色称呼系统 / Character Naming System

#### 中文称呼的复杂性

中文具有复杂的称呼系统，体现亲疏关系：

| 中文称呼 | 关系阶段 | English (建议) | 日本語 (建议) | 说明 |
|---------|---------|---------------|--------------|------|
| 陈先生 | 陌生/正式 | Mr. Chen | 陳さん | 初次见面 |
| 小陈 / 陈默 | 熟悉 | Chen / Little Chen | 陳さん / 陳くん | 关系发展 |
| 默哥 | 亲近 | Brother Chen | 陳兄さん | 好友阶段 |
| 老陈 | 平辈亲昵 | Old Chen | 陳さん (親しげに) | 同龄亲密 |

**王掌柜对陈默的称呼变化** (Progression):
```
第1章: "陈先生" → "Mr. Chen" (formal)
第3章: "小陈" → "Little Chen" (friendly)
第8章: "小陈啊" → "Little Chen, my boy" (paternal)
第11章 (如果救活): "默儿" → "Mo'er, my boy" (intimate, like family)
```

**翻译原则**:
- 保留称呼变化以体现关系深化
- 英文可用语气词和句式变化（如从"Mr. Chen"到"Chen, my boy"）
- 日文利用敬语变化和语气词（さん→くん→ちゃん）

### 4.5 数字与日期 / Numbers & Dates

#### 日期格式

| 语言 | 格式 | 示例 | 备注 |
|------|------|------|------|
| 简体中文 | 民国xx年x月x日 | 民国二十六年十二月十三日 | 历史真实日期格式 |
| English | Month Day, Year (ROC) | December 13, Year 26 of the Republic of China (1937) | 加公历年份注释 |
| 日本語 | 民国xx年x月x日 | 民国26年12月13日 | 保留"民国"概念 |
| 繁體中文 | 民國xx年x月x日 | 民國二十六年十二月十三日 | 繁体字 |

**重要日期标注**:
```
中文: 民国二十六年十二月十三日 (1937年12月13日)
English: December 13, Year 26 of the Republic of China (December 13, 1937)
日本語: 民国26年12月13日（1937年12月13日）

UI显示: 自动根据玩家语言设置切换格式
```

#### 数字与资源

```
中文: 食物 ×5
English: Food ×5
日本語: 食料 ×5
한국어: 식량 ×5

保持统一符号：× (乘号) 表示数量
```

---

## 五、UI文本长度限制 / UI Text Length Limits

### 5.1 各UI元素字符限制 / Character Limits by UI Element

| UI元素 | 简体中文 | English | 日本語 | 备注 |
|--------|---------|---------|--------|------|
| **按钮文本** | 4-6字 | 8-12 chars | 4-8字 | 如"确认""取消" |
| **选项按钮** | 12-20字 | 40-60 chars | 15-25字 | 对话选项 |
| **物品名称** | 4-8字 | 12-20 chars | 6-10字 | "红薯""棉衣" |
| **物品描述** | 20-40字 | 60-100 chars | 25-50字 | 悬停提示 |
| **角色名字** | 2-4字 | 8-15 chars | 3-6字 | "王掌柜" |
| **章节标题** | 6-12字 | 15-30 chars | 8-15字 | "第一章：初遇" |
| **成就标题** | 6-10字 | 20-35 chars | 8-15字 | - |
| **成就描述** | 20-35字 | 60-100 chars | 25-45字 | - |
| **日记标题** | 8-16字 | 20-40 chars | 10-20字 | - |
| **对话单行** | 30-45字 | 80-120 chars | 35-55字 | 对话框每行 |

### 5.2 超长文本处理策略 / Overflow Text Handling

#### 策略1: 缩写 (Abbreviation)
```
原文: "国际红十字会南京委员会"
英文全称: "International Committee of the Red Cross Nanjing Branch"
UI显示: "ICRC Nanjing" (with tooltip for full name)
```

#### 策略2: 分行 (Line Break)
```
如果UI允许2行：
原文: "这是一个非常重要的道德选择"
English:
"This is a very important
moral decision"
```

#### 策略3: 重写 (Rewrite)
```
原文: "你决定要不要帮助这个陌生人"
字面翻译: "You decide whether or not to help this stranger" (52 chars) ❌
精简重写: "Help this stranger?" (20 chars) ✓
```

#### 策略4: 动态UI (Dynamic UI)
```csharp
// UI自动适应文本长度
if (textLength > maxLength) {
    fontSize *= 0.9f; // 缩小字号
    // 或者启用滚动条
}
```

### 5.3 实际UI示例 / Real UI Examples

#### 对话选项框

```
┌──────────────────────────────────────────────────┐
│ 中文 (30字限制):                                   │
│ ► "我可以帮你，但你要告诉我真相。" (15字) ✓         │
│                                                   │
│ English (80字限制):                               │
│ ► "I can help you, but you must tell me the      │
│    truth." (48 chars) ✓                          │
│                                                   │
│ 日本語 (35字限制):                                 │
│ ► "手伝えますが、真実を話してください。" (19字) ✓   │
└──────────────────────────────────────────────────┘
```

#### 物品悬停提示

```
┌─────────────────────────┐
│ [图标] 红薯              │ ← 物品名 (4字/12 chars)
├─────────────────────────┤
│ 珍贵的食物，可以         │ ← 描述行1
│ 恢复饥饿值 +20          │ ← 描述行2
│                          │
│ Food +20 Hunger          │ ← 英文
└─────────────────────────┘
```

---

## 六、变量文本模式 / Variable Text Patterns

### 6.1 动态文本系统 / Dynamic Text System

#### 变量插入格式 (Variable Insertion Format)

```
格式: {variable_name}

示例:
中文: "{character_name}的健康值下降到{health_value}%。"
English: "{character_name}'s health has dropped to {health_value}%."
日本語: "{character_name}の健康値が{health_value}%に低下しました。"
```

#### 复数处理 (Pluralization)

**英文复数问题**:
```json
// 错误做法 (Wrong)
"You have {count} item."

// 正确做法 (Correct) - 使用复数标记
{
  "item_count": {
    "one": "You have {count} item.",
    "other": "You have {count} items."
  }
}
```

**实现示例**:
```csharp
// Unity本地化代码
string GetLocalizedItemCount(int count) {
    if (count == 1) {
        return LocalizationManager.GetString("item_count.one", count);
    } else {
        return LocalizationManager.GetString("item_count.other", count);
    }
}
```

### 6.2 条件文本 / Conditional Text

#### 性别代词 (Gender Pronouns)

```json
{
  "dialogue_line": {
    "zh-CN": "{character_name}说：'我会保护你。'",
    "en-US": "{character_name} said: 'I will protect you.'",
    "ja-JP": "{character_name}は言った：'君を守る。'"
  }
}
```

#### 关系称呼 (Relationship-based Address)

```csharp
// 根据好感度动态改变称呼
string GetCharacterAddress(int relationshipLevel) {
    if (relationshipLevel < 30) {
        return LocalizationManager.GetString("address.formal"); // "陈先生" / "Mr. Chen"
    } else if (relationshipLevel < 70) {
        return LocalizationManager.GetString("address.friendly"); // "小陈" / "Little Chen"
    } else {
        return LocalizationManager.GetString("address.intimate"); // "默儿" / "Mo'er"
    }
}
```

### 6.3 日期时间格式化 / Date & Time Formatting

```csharp
// 根据语言自动格式化日期
public string FormatGameDate(int year, int month, int day) {
    switch (currentLanguage) {
        case "zh-CN":
            return $"民国{year - 1911}年{month}月{day}日";
        case "en-US":
            return $"{GetMonthName(month)} {day}, Year {year - 1911} of ROC ({year})";
        case "ja-JP":
            return $"民国{year - 1911}年{month}月{day}日（{year}年）";
        default:
            return $"{year}-{month:00}-{day:00}";
    }
}
```

---

## 七、术语表 / Glossary

### 7.1 核心术语统一译法 / Core Term Standardization

#### 历史与地点 / History & Locations

| 中文 | English | 日本語 | 繁體中文 | 备注 |
|------|---------|--------|---------|------|
| 南京大屠杀 | Nanjing Massacre | 南京大虐殺 | 南京大屠殺 | 历史事件 |
| 南京保卫战 | Battle of Nanjing | 南京戦 | 南京保衛戰 | - |
| 安全区 | Safety Zone / International Safety Zone | 安全区 | 安全區 | - |
| 金陵女子学院 | Ginling College | 金陵女子大学 | 金陵女子學院 | 真实机构 |
| 秦淮河 | Qinhuai River | 秦淮河 | 秦淮河 | 地名保留拼音 |
| 中华门 | Zhonghua Gate | 中華門 | 中華門 | - |
| 夫子庙 | Confucius Temple | 夫子廟 | 夫子廟 | - |
| 长江 | Yangtze River | 長江 | 長江 | - |

#### 角色称谓 / Character Titles

| 中文 | English | 日本語 | 繁體中文 | 说明 |
|------|---------|--------|---------|------|
| 掌柜 | Shopkeeper | 店主 | 掌櫃 | 职业称呼 |
| 先生 | Mr. / Mister / Sir | さん | 先生 | 尊称 |
| 女士 | Miss / Ms. | さん / 女史 | 女士 | - |
| 大哥 | Big Brother | 兄さん | 大哥 | 亲近称呼 |
| 老师 | Teacher | 先生 | 老師 | - |
| 护士 | Nurse | 看護師 | 護士 | - |

#### 资源物品 / Resources & Items

| 中文 | English | 日本語 | 繁體中文 | 图标 |
|------|---------|--------|---------|------|
| 食物 | Food | 食料 | 食物 | 🍚 |
| 水 | Water | 水 | 水 | 💧 |
| 药品 | Medicine | 薬品 | 藥品 | 💊 |
| 燃料 | Fuel | 燃料 | 燃料 | 🔥 |
| 布料 | Fabric / Cloth | 布 | 布料 | 🧵 |
| 红薯 | Sweet Potato | サツマイモ | 紅薯 | - |
| 米饭 | Rice | 米 | 米飯 | - |
| 馒头 | Steamed Bun | 饅頭 | 饅頭 | - |
| 萝卜 | Radish | 大根 | 蘿蔔 | - |
| 煤炭 | Coal | 石炭 | 煤炭 | - |
| 木柴 | Firewood | 薪 | 木柴 | - |

#### 游戏机制 / Game Mechanics

| 中文 | English | 日本語 | 繁體中文 |
|------|---------|--------|---------|
| 健康值 | Health | 健康値 | 健康值 |
| 饥饿值 | Hunger | 空腹度 | 飢餓值 |
| 士气 | Morale | 士気 | 士氣 |
| 关系值 | Relationship | 関係値 | 關係值 |
| 好感度 | Affinity | 好感度 | 好感度 |
| 技能 | Skill | スキル | 技能 |
| 成就 | Achievement | 実績 | 成就 |
| 日记 | Diary / Journal | 日記 | 日記 |
| 对话 | Dialogue | 会話 | 對話 |
| 选择 | Choice | 選択肢 | 選擇 |
| QTE快速反应 | QTE (Quick Time Event) | QTE | QTE快速反應 |

#### 技能系统 / Skill System

| 中文 | English | 日本語 | 繁體中文 |
|------|---------|--------|---------|
| 日语精通 | Japanese Proficiency | 日本語能力 | 日語精通 |
| 医疗知识 | Medical Knowledge | 医療知識 | 醫療知識 |
| 历史知识 | Historical Knowledge | 歴史知識 | 歷史知識 |
| 资源管理 | Resource Management | 資源管理 | 資源管理 |
| 人际交往 | Social Skills | 対人スキル | 人際交往 |

### 7.2 禁用术语 / Forbidden Terms

**❌ 严禁使用的翻译**:

| 错误翻译 | 语言 | 原因 | 正确翻译 |
|---------|------|------|---------|
| "Rape of Nanking" | English | 过度暴力化，非官方术语 | "Nanjing Massacre" |
| "Incident" | English | 淡化历史，日本右翼用语 | "Massacre" |
| "南京事件" | 日本語 | 否认历史的委婉语 | "南京大虐殺" |
| "慰安妇" (直译) | 任何语言 | 美化强迫性行为 | 使用"战争性奴隶" / "Wartime Sex Slaves" |

---

## 八、文件格式规范 / File Format Specifications

### 8.1 文件结构 / File Structure

```
Assets/Localization/
├── zh-CN/
│   ├── dialogues.json
│   ├── ui.json
│   ├── items.json
│   ├── achievements.json
│   └── diary.json
├── en-US/
│   ├── dialogues.json
│   ├── ui.json
│   ├── items.json
│   ├── achievements.json
│   └── diary.json
├── ja-JP/
│   └── ...
└── shared/
    ├── glossary.json (术语表)
    └── variables.json (变量定义)
```

### 8.2 JSON格式规范 / JSON Format Specification

#### 对话文件 (dialogues.json)

```json
{
  "dialogue_nodes": {
    "wang_first_meet_01": {
      "speaker": "wang_fugui",
      "text": {
        "zh-CN": "哎呦，您就是那位要租房的先生吧？",
        "en-US": "Oh my, you must be the gentleman looking to rent a room?",
        "ja-JP": "おやおや、部屋を借りたいという方ですね？",
        "zh-TW": "哎呦，您就是那位要租房的先生吧？"
      },
      "emotion": "friendly",
      "voice_file": {
        "zh-CN": "wang_01_cn.ogg",
        "en-US": "wang_01_en.ogg",
        "ja-JP": "wang_01_jp.ogg"
      },
      "context_notes": "First meeting, warm and welcoming tone"
    },
    "chen_response_01a": {
      "speaker": "chen_mo",
      "text": {
        "zh-CN": "是的，麻烦您了。",
        "en-US": "Yes, thank you for your help.",
        "ja-JP": "はい、よろしくお願いします。",
        "zh-TW": "是的，麻煩您了。"
      },
      "choices": ["choice_01a", "choice_01b", "choice_01c"],
      "effects": {
        "relationship_wang": 2
      }
    }
  }
}
```

#### UI文本文件 (ui.json)

```json
{
  "ui_strings": {
    "button_confirm": {
      "zh-CN": "确认",
      "en-US": "Confirm",
      "ja-JP": "確認",
      "zh-TW": "確認"
    },
    "button_cancel": {
      "zh-CN": "取消",
      "en-US": "Cancel",
      "ja-JP": "キャンセル",
      "zh-TW": "取消"
    },
    "resource_food": {
      "zh-CN": "食物",
      "en-US": "Food",
      "ja-JP": "食料",
      "zh-TW": "食物"
    },
    "health_warning": {
      "zh-CN": "{character_name}的健康值已降至{health}%！",
      "en-US": "{character_name}'s health has dropped to {health}%!",
      "ja-JP": "{character_name}の健康値が{health}%に低下！",
      "zh-TW": "{character_name}的健康值已降至{health}%！"
    }
  }
}
```

#### 物品文件 (items.json)

```json
{
  "items": {
    "item_sweet_potato": {
      "name": {
        "zh-CN": "红薯",
        "en-US": "Sweet Potato",
        "ja-JP": "サツマイモ",
        "zh-TW": "紅薯"
      },
      "description": {
        "zh-CN": "珍贵的食物，可以恢复饥饿值。",
        "en-US": "Precious food that restores hunger.",
        "ja-JP": "貴重な食料。空腹度を回復する。",
        "zh-TW": "珍貴的食物，可以恢復飢餓值。"
      },
      "category": "food",
      "effects": {
        "hunger": 20
      }
    }
  }
}
```

### 8.3 Yarn Spinner集成 / Yarn Spinner Integration

如果使用Yarn Spinner对话系统：

```yarn
title: WangFirstMeet
tags: chapter1
---
<<set $language = GetCurrentLanguage()>>

Wang: <<loc "wang_first_meet_01">>
  -> <<loc "chen_response_01a">>
    <<set $relationship_wang += 2>>
    Wang: <<loc "wang_first_meet_02a">>
  -> <<loc "chen_response_01b">>
    Wang: <<loc "wang_first_meet_02b">>
  -> [沉默/Silence/沈黙] <<loc "chen_response_01c">>
    <<set $relationship_wang -= 1>>
    Wang: <<loc "wang_first_meet_02c">>
===
```

**Yarn本地化函数**:
```csharp
// 自定义Yarn函数
[YarnFunction("loc")]
public static string Localize(string key) {
    return LocalizationManager.GetString(key, currentLanguage);
}
```

---

## 九、QA检查清单 / QA Checklist

### 9.1 翻译质量检查 / Translation Quality Check

#### 第一阶段：案头审核 / Phase 1: Desk Review

- [ ] **完整性检查** / Completeness Check
  - [ ] 所有文本都已翻译（无遗漏的{key}）
  - [ ] 所有变量标记保留（如{character_name}）
  - [ ] 所有格式标记完整（如`<b>`, `<color>`, `\n`）

- [ ] **准确性检查** / Accuracy Check
  - [ ] 历史人名、地名使用正确译名
  - [ ] 历史事件描述准确无误
  - [ ] 数字、日期正确（如民国26年 = 1937年）
  - [ ] 专有名词统一（查阅术语表）

- [ ] **语言质量** / Language Quality
  - [ ] 语法正确，无拼写错误
  - [ ] 句式流畅，符合母语习惯
  - [ ] 语气与角色性格一致
  - [ ] 情感表达到位，不生硬

- [ ] **文化适配** / Cultural Adaptation
  - [ ] 俗语、歇后语合理转化
  - [ ] 文化特定内容有注释（如需要）
  - [ ] 避免文化冒犯或误解
  - [ ] 幽默/讽刺可理解

#### 第二阶段：游戏内测试 / Phase 2: In-Game Testing

- [ ] **UI显示检查** / UI Display Check
  - [ ] 文本未溢出UI边界
  - [ ] 换行位置合理
  - [ ] 字体显示正常（无乱码）
  - [ ] 按钮文本居中对齐
  - [ ] 多分辨率下显示正常

- [ ] **对话流畅性** / Dialogue Flow
  - [ ] 对话逐字显示速度合理
  - [ ] 配音与字幕同步（如有）
  - [ ] 选项分支逻辑正确
  - [ ] 角色称呼变化正确体现关系

- [ ] **变量替换** / Variable Substitution
  - [ ] 所有{variable}正确替换
  - [ ] 复数形式正确（英文）
  - [ ] 数字格式正确（日期、货币等）
  - [ ] 性别代词正确（如需要）

- [ ] **音效与配音** / SFX & Voice
  - [ ] 配音文件存在且播放正常
  - [ ] 配音与文本内容匹配
  - [ ] 音效提示语言正确（如"Press E" vs "Eキーを押す"）

#### 第三阶段：玩家测试 / Phase 3: Player Testing

- [ ] **可理解性** / Comprehensibility
  - [ ] 目标语言母语玩家能完全理解剧情
  - [ ] 无歧义或困惑的表达
  - [ ] 历史背景信息充分

- [ ] **情感共鸣** / Emotional Resonance
  - [ ] 感人场景仍能打动玩家
  - [ ] 紧张场景营造紧迫感
  - [ ] 角色性格鲜明可辨

- [ ] **文化接受度** / Cultural Acceptance
  - [ ] 目标文化玩家无不适感
  - [ ] 历史叙述客观中立
  - [ ] 敏感内容处理得当

### 9.2 技术测试清单 / Technical Testing Checklist

```
□ 字体文件完整，支持所有字符
□ 文本编码统一（UTF-8）
□ JSON文件格式正确，无语法错误
□ 资源文件路径正确
□ 语言切换功能正常
□ 保存/读档时语言设置保持
□ 成就/统计系统多语言正常
□ UI自动适应文本长度
□ 换行算法正确（中文无需空格，英文需要）
□ 性能测试：加载时间、内存占用正常
```

### 9.3 本地化Bug分类 / Localization Bug Categories

| 严重性 | 类型 | 示例 | 优先级 |
|-------|------|------|-------|
| **Critical** | 文本缺失 | 显示{key}而非翻译文本 | P0 |
| **Critical** | 历史错误 | 人名地名错误 | P0 |
| **High** | UI溢出 | 按钮文字超出边界 | P1 |
| **High** | 逻辑错误 | 变量未替换 | P1 |
| **Medium** | 语法错误 | 拼写/语法问题 | P2 |
| **Medium** | 不流畅 | 机翻感，不自然 | P2 |
| **Low** | 格式问题 | 标点符号不规范 | P3 |
| **Low** | 优化建议 | 可以更好的表达 | P3 |

---

## 十、文化敏感性指南 / Cultural Sensitivity Guide

### 10.1 历史题材的责任 / Responsibility in Historical Themes

本游戏涉及南京大屠杀这一严肃历史事件，翻译团队必须：

1. **尊重历史事实** / Respect Historical Facts
   - 不淡化、不夸大、不歪曲历史
   - 使用国际学术界认可的术语和描述
   - 保持客观中立的叙述态度

2. **尊重受害者** / Respect Victims
   - 避免过度煽情或娱乐化
   - 暴力场景采用暗示手法，不直接展现
   - 保持庄重、肃穆的叙述语气

3. **文化桥梁** / Cultural Bridge
   - 帮助不同文化背景的玩家理解这段历史
   - 提供必要的文化和历史背景注释
   - 促进反战和平的普世价值

### 10.2 日文翻译特殊注意事项 / Japanese Translation Special Notes

#### 历史认知差异 / Historical Perception Differences

日本国内对南京大屠杀存在不同认知，翻译需谨慎：

**✓ 推荐做法** (Recommended):
```
使用国际通用术语：
- "南京大虐殺" (官方历史术语)
- "1937年12月13日" (明确日期)
- 引用国际史料和证言

提供客观叙述：
- 避免情绪化语言
- 让事实说话
- 注重个人故事而非宏大叙事
```

**❌ 避免** (Avoid):
```
- 使用"南京事件"等淡化术语
- 回避具体数字和事实
- 过度强调国家对立而非人性
```

#### 审查与发行考虑 / Review & Distribution Considerations

- 日本版本可能面临审查，需提前准备合规材料
- 考虑在游戏开头加警告/说明，阐明游戏教育目的
- 与历史学者合作，确保叙述的学术严谨性

### 10.3 西方玩家的历史教育 / Historical Education for Western Players

许多西方玩家对南京大屠杀了解有限，翻译需承担教育功能：

**策略** (Strategies):
1. **历史注释系统** / Historical Annotation System
   - 首次出现历史名词时提供[H]按钮
   - 点击查看详细历史背景
   - 提供外部学习资源链接

2. **类比教学** / Analogical Teaching
   ```
   英文翻译可参考西方熟悉的历史事件：
   "This event was comparable to the Holocaust in its systematic brutality..."
   （谨慎使用，避免冒犯任何群体）
   ```

3. **时间线辅助** / Timeline Aid
   - 提供1937年世界大事时间线
   - 帮助玩家建立历史语境

### 10.4 避免文化刻板印象 / Avoiding Cultural Stereotypes

**不要将所有日本角色塑造为恶人**:
- 游戏中的高桥角色是觉醒者，代表良知
- 翻译需传达"人性超越国界"的主题
- 避免煽动民族仇恨

**不要过度东方主义** (Orientalism):
- 避免将中国文化过度异域化
- 不要使用陈腐的"古老神秘"等刻板描述
- 保持现代、人性化的叙述视角

### 10.5 内容警告 / Content Warnings

**游戏开头应包含的警告**（各语言版本）:

```
简体中文:
"本游戏包含南京大屠杀的历史内容，涉及战争暴力、死亡等成人主题。
游戏的目的是铭记历史、反思战争、珍爱和平。
部分内容可能引起不适，请谨慎游玩。"

English:
"This game contains content related to the Nanjing Massacre, including themes of
war violence, death, and mature subject matter. The purpose of this game is to
remember history, reflect on war, and cherish peace. Some content may be disturbing.
Player discretion is advised."

日本語:
"このゲームは南京大虐殺に関する歴史的内容を含み、戦争暴力、死などの
成人向けテーマを扱っています。ゲームの目的は歴史を記憶し、戦争を
省み、平和を大切にすることです。一部の内容は不快感を与える可能性が
ありますので、ご注意ください。"
```

---

## 十一、工具与资源 / Tools & Resources

### 11.1 推荐工具 / Recommended Tools

| 工具 | 用途 | 价格 | 链接 |
|------|------|------|------|
| **Trados Studio** | 专业CAT工具，术语库管理 | 付费 | www.trados.com |
| **MemoQ** | 翻译记忆，协作翻译 | 付费 | www.memoq.com |
| **Unity Localization** | Unity原生本地化系统 | 免费 | Unity Asset Store |
| **I2 Localization** | 第三方Unity本地化插件 | $45 | Unity Asset Store |
| **Yarn Spinner** | 对话系统，支持本地化 | 免费 | yarnspinner.dev |
| **POEditor** | 在线协作翻译平台 | 免费/付费 | poeditor.com |
| **Google Sheets** | 术语表管理 | 免费 | sheets.google.com |

### 11.2 参考资源 / Reference Resources

#### 历史资料 / Historical References

1. **《南京大屠杀史料集》** (中英日多语言)
   - 学术权威资料
   - 历史术语标准译法

2. **南京大屠杀遇难同胞纪念馆官网**
   - 官方历史叙述
   - 多语言展览文本参考

3. **《拉贝日记》** (The Good Man of Nanking)
   - 外国人视角
   - 英文翻译参考

4. **《魏特琳日记》** (Minnie Vautrin's Diaries)
   - 一手历史文献
   - 英文原文参考

#### 游戏本地化参考 / Game Localization References

1. **《This War of Mine》** - 战争题材叙事
2. **《Valiant Hearts》** - 一战历史游戏
3. **《Papers, Please》** - 严肃主题对话设计
4. **《奥伯拉·丁的回归》** - 历史风格文本

### 11.3 译者培训材料 / Translator Training Materials

**必修内容** (Required):
- [ ] 南京大屠杀历史纪录片（2小时）
- [ ] 游戏完整试玩（中文版，8小时）
- [ ] 术语表记忆与测试
- [ ] 风格指南学习
- [ ] 与历史顾问座谈会

**推荐阅读** (Recommended):
- 《南京大屠杀》张纯如著
- 《拉贝日记》约翰·拉贝著
- 《魏特琳日记》
- 游戏设计文档（GDD）

---

## 十二、联系与支持 / Contact & Support

### 12.1 翻译团队联系方式 / Translation Team Contacts

```
本地化项目经理: localization@game-studio.com
历史顾问: history-consultant@game-studio.com
技术支持: tech-support@game-studio.com
翻译问题反馈: translation-feedback@game-studio.com
```

### 12.2 常见问题快速通道 / FAQ Quick Channel

**Q: 遇到无法理解的文化内容怎么办？**
A: 发邮件给本地化经理，抄送文化顾问，会在24小时内回复。

**Q: 发现历史错误怎么办？**
A: 立即标记，联系历史顾问，不要自行修改历史事实。

**Q: UI文本超长无法缩短怎么办？**
A: 提交给UI设计师，评估是否需要调整UI，或使用动态字号。

**Q: 术语表中没有的新词怎么翻译？**
A: 提交术语审核流程，由团队共同决定统一译法。

---

## 附录A：翻译示例对照 / Appendix A: Translation Examples

### 示例1：第一章开场对话

**原文** (Original Chinese):
```
陈默（旁白）:
"2024年12月13日，南京。第87个国家公祭日。
我作为历史学者，每年都会来这里。
但今年……有些不一样。"
```

**英文翻译** (English):
```
Chen Mo (Narration):
"December 13, 2024. Nanjing. The 87th National Memorial Day.
As a historian, I come here every year.
But this year... something feels different."
```

**日文翻译** (Japanese):
```
陳黙（ナレーション）：
「2024年12月13日、南京。第87回国家哀悼日。
歴史学者として、私は毎年ここに来る。
しかし今年は...何かが違う。」
```

### 示例2：情感高潮场景（王掌柜临终）

**原文** (Original Chinese):
```
王掌柜（虚弱）:
"小陈……答应我……照顾好小梅……
她……她还小……什么都不懂……
我这条老命……不值钱……
但……但她是我的全部啊……"

小梅（哭喊）:
"爹！爹！你别走……别丢下我……
我不要你走……呜呜呜……"
```

**英文翻译** (English):
```
Shopkeeper Wang (weakly):
"Little Chen... promise me... take care of Xiao Mei...
She's... she's still so young... knows nothing of the world...
This old life of mine... it's worth nothing...
But... but she's everything I have..."

Xiao Mei (crying out):
"Papa! Papa! Don't leave... don't leave me alone...
I don't want you to go... *sobbing*..."
```

**日文翻译** (Japanese):
```
王掌柜（弱々しく）：
「陳さん...約束してくれ...小梅を...頼む...
あの子は...まだ若くて...何も知らない...
俺のこんな命...何の価値もない...
でも...でもあの子は俺のすべてなんだ...」

小梅（泣き叫ぶ）：
「お父さん！お父さん！行かないで...私を置いていかないで...
お父さんが行っちゃ嫌だ...うっ...うっ...」
```

**翻译注释**:
- "爹" → 英文"Papa"（比"Father"更亲昵感人）
- 日文"お父さん"（标准称呼，情感真挚）
- "*sobbing*"等拟声词保留，增强情感

---

## 附录B：术语审核流程 / Appendix B: Term Approval Process

```
1. 译者提交新术语请求
   ↓
2. 本地化经理审核（2个工作日）
   ↓
3. 历史顾问/文化顾问审核（如需要）
   ↓
4. 团队讨论（每周例会）
   ↓
5. 确定统一译法
   ↓
6. 更新术语表
   ↓
7. 通知所有译者
```

---

## 附录C：版本更新记录 / Appendix C: Version History

| 版本 | 日期 | 更新内容 | 作者 |
|------|------|---------|------|
| v1.0 | 2025-11-05 | 初始版本，完整本地化指南 | Localization Team |
| v1.1 | TBD | 根据alpha测试反馈修订 | TBD |
| v2.0 | TBD | 正式版发布，整合玩家反馈 | TBD |

---

**文档状态**: 初稿完成
**审核者**: 本地化经理 + 历史顾问 + 文化顾问
**下一步**:
1. 开始核心文本翻译
2. 建立术语库
3. 启动翻译记忆系统
4. 招募本地化测试员

**维护**: 本文档为活文档，随项目进展持续更新

---

## 结语 / Conclusion

《秦淮旧梦》不仅是一款游戏，更是一座连接历史与现实的桥梁。翻译工作承载着向世界传递历史真相、促进文化理解、倡导和平价值的使命。

让我们以专业的态度、严谨的精神、人文的关怀，共同完成这项意义非凡的工作。

---

*"Those who cannot remember the past are condemned to repeat it."*
*— George Santayana*

*"忘记历史就意味着背叛。"*
*— 列宁*

*"歴史を忘れる者は、再び同じ過ちを繰り返す。"*

---

**文档结束** / End of Document
