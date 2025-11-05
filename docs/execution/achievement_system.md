# 成就系统设计文档

**文档版本**: v1.0
**最后更新**: 2025-11-05
**负责人**: 系统策划 + 程序（待定）
**状态**: 执行级文档

---

## 一、成就系统概述

### 1.1 设计目标

1. **延长游戏寿命**: 鼓励玩家多周目探索不同路线
2. **引导探索**: 通过成就提示隐藏内容和玩法
3. **情感共鸣**: 成就描述强化叙事和历史教育
4. **社交展示**: 稀有成就满足玩家炫耀需求
5. **数据追踪**: 通过成就解锁率了解玩家行为

---

### 1.2 成就分类与配额

| 类别 | 数量 | 说明 | 平均难度 |
|------|------|------|---------|
| 剧情成就 | 30 | 完成章节、触发关键剧情 | 简单-中等 |
| 关系成就 | 15 | 角色关系、特殊互动 | 中等 |
| 收集成就 | 10 | 日记、资料库、物品 | 中等-困难 |
| 挑战成就 | 15 | 特殊条件、高难度目标 | 困难-极难 |
| 隐藏成就 | 10 | 彩蛋、特殊操作 | 中等-困难 |
| **总计** | **80** | - | - |

---

### 1.3 难度评级标准

| 难度 | 符号 | 解锁率预期 | 说明 |
|------|------|-----------|------|
| 非常简单 | ★☆☆☆☆ | >80% | 正常流程必得 |
| 简单 | ★★☆☆☆ | 60-80% | 稍加注意即可 |
| 中等 | ★★★☆☆ | 30-60% | 需要特定条件 |
| 困难 | ★★★★☆ | 10-30% | 需要多次尝试 |
| 极难 | ★★★★★ | <10% | hardcore玩家专属 |

---

## 二、成就详细列表

---

## 2.1 剧情成就 (Story Achievements) - 30个

### S-001 穿越者
- **ID**: `achievement_story_001`
- **名称**: 穿越者
- **描述**: 完成序章，穿越到1937年
- **解锁条件**: `flag_completed_prologue == true`
- **解锁时机**: 序章结束
- **奖励**: 无
- **隐藏**: 否
- **难度**: ★☆☆☆☆
- **预期解锁率**: 95%

---

### S-002 初来乍到
- **ID**: `achievement_story_002`
- **名称**: 初来乍到
- **描述**: 完成第1章，与王掌柜建立联系
- **解锁条件**: `flag_completed_chapter_01 == true`
- **解锁时机**: 第1章结束
- **奖励**: 解锁历史资料"民国南京"
- **隐藏**: 否
- **难度**: ★☆☆☆☆
- **预期解锁率**: 90%

---

### S-003 风雨欲来
- **ID**: `achievement_story_003`
- **名称**: 风雨欲来
- **描述**: 完成第2章，目睹南京保卫战
- **解锁条件**: `flag_completed_chapter_02 == true`
- **解锁时机**: 第2章结束
- **奖励**: 解锁历史资料"南京保卫战"
- **隐藏**: 否
- **难度**: ★☆☆☆☆
- **预期解锁率**: 85%

---

### S-004 黑暗降临
- **ID**: `achievement_story_004`
- **名称**: 黑暗降临
- **描述**: 完成第3章，经历12月13日
- **解锁条件**: `flag_completed_chapter_03 == true AND game_date >= "1937-12-13"`
- **解锁时机**: 第3章结束
- **奖励**: 解锁历史资料"南京大屠杀"
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 80%
- **特殊**: 成就图标使用黑色基调

---

### S-005 生存者
- **ID**: `achievement_story_005`
- **名称**: 生存者
- **描述**: 完成第5章，在安全区存活下来
- **解锁条件**: `flag_completed_chapter_05 == true AND player.is_alive == true`
- **解锁时机**: 第5章结束
- **奖励**: 无
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 75%

---

### S-006 见证者
- **ID**: `achievement_story_006`
- **名称**: 见证者
- **描述**: 完成第10章，记录下所见所闻
- **解锁条件**: `flag_completed_chapter_10 == true AND diary.entry_count >= 30`
- **解锁时机**: 第10章结束
- **奖励**: 无
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 65%

---

### S-007 春回大地
- **ID**: `achievement_story_007`
- **名称**: 春回大地
- **描述**: 完成第15章，熬过寒冬
- **解锁条件**: `flag_completed_chapter_15 == true AND game_date >= "1938-03-01"`
- **解锁时机**: 第15章结束
- **奖励**: 解锁历史资料"1938年的南京"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 55%

---

### S-008 告别
- **ID**: `achievement_story_008`
- **名称**: 告别
- **描述**: 完成第18章，准备离开
- **解锁条件**: `flag_completed_chapter_18 == true`
- **解锁时机**: 第18章结束
- **奖励**: 无
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 50%

---

### S-009 传承
- **ID**: `achievement_story_009`
- **名称**: 传承
- **描述**: 完成尾声，回到2024年
- **解锁条件**: `flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 解锁隐藏章节"小梅的余生"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 50%

---

### S-010 完美结局
- **ID**: `achievement_story_010`
- **名称**: 完美结局
- **描述**: 保护所有主要角色存活至游戏结束
- **解锁条件**:
  ```
  flag_completed_epilogue == true AND
  character_wang_alive == true AND
  character_xiaomei_alive == true AND
  character_liwenbin_alive == true AND
  character_zhao_alive == true AND
  character_weitelin_alive == true
  ```
- **解锁时机**: 尾声结束
- **奖励**: 特殊结局CG
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 20%

---

### S-011 悲剧结局
- **ID**: `achievement_story_011`
- **名称**: 悲剧结局
- **描述**: 失去所有主要角色后完成游戏
- **解锁条件**:
  ```
  flag_completed_epilogue == true AND
  character_wang_alive == false AND
  character_xiaomei_alive == false AND
  character_liwenbin_alive == false
  ```
- **解锁时机**: 尾声结束
- **奖励**: 特殊结局CG
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 15%
- **特殊**: 成就描述文字灰暗

---

### S-012 历史学者
- **ID**: `achievement_story_012`
- **名称**: 历史学者
- **描述**: 成功预警5次以上历史事件
- **解锁条件**: `player.correct_predictions >= 5`
- **解锁时机**: 游戏过程中
- **奖励**: 历史知识经验+50
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 60%

---

### S-013 先知
- **ID**: `achievement_story_013`
- **名称**: 先知
- **描述**: 成功预警10次以上历史事件
- **解锁条件**: `player.correct_predictions >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 历史知识等级上限+1
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 35%

---

### S-014 改变不了的历史
- **ID**: `achievement_story_014`
- **名称**: 改变不了的历史
- **描述**: 尝试阻止12月13日的浩劫，但失败了
- **解锁条件**: `flag_tried_prevent_massacre == true AND flag_massacre_happened == true`
- **解锁时机**: 第3章
- **奖励**: 解锁隐藏对话"无力感"
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 40%

---

### S-015 安全区守护者
- **ID**: `achievement_story_015`
- **名称**: 安全区守护者
- **描述**: 协助魏特琳保护安全区至少60天
- **解锁条件**: `days_in_safety_zone >= 60 AND safety_zone_breach_count == 0`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁历史资料"魏特琳日记"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 40%

---

### S-016 日语交涉专家
- **ID**: `achievement_story_016`
- **名称**: 日语交涉专家
- **描述**: 日语技能达到10级
- **解锁条件**: `player.skills.japanese >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊对话选项
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 25%

---

### S-017 赤脚医生
- **ID**: `achievement_story_017`
- **名称**: 赤脚医生
- **描述**: 医疗知识达到10级
- **解锁条件**: `player.skills.medical >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 治疗成功率+10%
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 22%

---

### S-018 生存大师
- **ID**: `achievement_story_018`
- **名称**: 生存大师
- **描述**: 搜集技能达到10级
- **解锁条件**: `player.skills.scavenging >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 搜集效率+20%
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 28%

---

### S-019 拉贝的助手
- **ID**: `achievement_story_019`
- **名称**: 拉贝的助手
- **描述**: 与约翰·拉贝建立深厚友谊
- **解锁条件**: `relationship.john_rabe >= 80`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁历史资料"拉贝日记节选"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 35%

---

### S-020 高桥的困惑
- **ID**: `achievement_story_020`
- **名称**: 高桥的困惑
- **描述**: 触发与日本军官高桥的深度对话
- **解锁条件**:
  ```
  flag_met_takahashi == true AND
  player.skills.japanese >= 7 AND
  dialogue_takahashi_deep_unlocked == true
  ```
- **解锁时机**: 第12章
- **奖励**: 解锁特殊剧情分支
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 18%

---

### S-021 除夕夜
- **ID**: `achievement_story_021`
- **名称**: 除夕夜
- **描述**: 在安全区度过1938年春节
- **解锁条件**: `game_date == "1938-02-15" AND location == "safety_zone"`
- **解锁时机**: 第16章
- **奖励**: 观看特殊过场动画"除夕"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 45%

---

### S-022 无名英雄
- **ID**: `achievement_story_022`
- **名称**: 无名英雄
- **描述**: 暗中帮助他人50次以上
- **解锁条件**: `player.anonymous_help_count >= 50`
- **解锁时机**: 游戏过程中
- **奖励**: 群体士气+10
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 30%

---

### S-023 冒险家
- **ID**: `achievement_story_023`
- **名称**: 冒险家
- **描述**: 成功完成20次危险搜集任务
- **解锁条件**: `player.dangerous_scavenge_success >= 20`
- **解锁时机**: 游戏过程中
- **奖励**: 搜集奖励+20%
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 25%

---

### S-024 谨慎行事
- **ID**: `achievement_story_024`
- **名称**: 谨慎行事
- **描述**: 从未触发过被日本兵抓捕事件
- **解锁条件**: `flag_caught_by_japanese == false AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"幸存专家"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 38%

---

### S-025 QTE大师
- **ID**: `achievement_story_025`
- **名称**: QTE大师
- **描述**: 成功完成所有QTE挑战
- **解锁条件**: `player.qte_success_count == player.qte_total_count AND qte_total_count >= 15`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁"QTE训练模式"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 20%

---

### S-026 外交家
- **ID**: `achievement_story_026`
- **名称**: 外交家
- **描述**: 成功用谈判解决10次以上危机
- **解锁条件**: `player.negotiation_success >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 对话选项成功率+10%
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 32%

---

### S-027 沉默的记录者
- **ID**: `achievement_story_027`
- **名称**: 沉默的记录者
- **描述**: 每天都写日记，从不间断
- **解锁条件**: `diary.consecutive_days == days_survived`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊日记封面
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 40%

---

### S-028 速通者
- **ID**: `achievement_story_028`
- **名称**: 速通者
- **描述**: 在10小时内通关游戏
- **解锁条件**: `flag_completed_epilogue == true AND total_playtime <= 36000` (10小时=36000秒)
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"时间管理大师"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 15%

---

### S-029 马拉松玩家
- **ID**: `achievement_story_029`
- **名称**: 马拉松玩家
- **描述**: 游戏时长超过30小时
- **解锁条件**: `total_playtime >= 108000` (30小时)
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"深度体验者"
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 45%

---

### S-030 多周目探索者
- **ID**: `achievement_story_030`
- **名称**: 多周目探索者
- **描述**: 完成游戏3次以上
- **解锁条件**: `game_completed_count >= 3`
- **解锁时机**: 尾声结束
- **奖励**: 解锁全部章节选择
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 25%

---

## 2.2 关系成就 (Relationship Achievements) - 15个

### R-001 如父如子
- **ID**: `achievement_relationship_001`
- **名称**: 如父如子
- **描述**: 与王掌柜的关系达到100（生死之交）
- **解锁条件**: `relationship.wang_fugui >= 100`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊剧情"王掌柜的托付"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 35%

---

### R-002 姐弟情深
- **ID**: `achievement_relationship_002`
- **名称**: 姐弟情深
- **描述**: 与小梅的关系达到80（挚友）
- **解锁条件**: `relationship.xiao_mei >= 80`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊剧情"小梅的愿望"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 40%

---

### R-003 患难之交
- **ID**: `achievement_relationship_003`
- **名称**: 患难之交
- **描述**: 与李文斌的关系达到80（挚友）
- **解锁条件**: `relationship.li_wenbin >= 80`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊剧情"李文斌的理想"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 38%

---

### R-004 白衣天使
- **ID**: `achievement_relationship_004`
- **名称**: 白衣天使
- **描述**: 与赵护士的关系达到70（挚友）
- **解锁条件**: `relationship.zhao_nurse >= 70`
- **解锁时机**: 游戏过程中
- **奖励**: 学习高级医疗技能
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 42%

---

### R-005 国际友人
- **ID**: `achievement_relationship_005`
- **名称**: 国际友人
- **描述**: 与魏特琳的关系达到70（挚友）
- **解锁条件**: `relationship.weitelin >= 70`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁历史资料"魏特琳传记"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 45%

---

### R-006 保护者
- **ID**: `achievement_relationship_006`
- **名称**: 保护者
- **描述**: 成功保护小梅免受所有伤害
- **解锁条件**:
  ```
  character_xiaomei_alive == true AND
  xiaomei_injury_count == 0 AND
  flag_completed_epilogue == true
  ```
- **解锁时机**: 尾声结束
- **奖励**: 特殊结局CG"小梅的微笑"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 22%

---

### R-007 治愈者
- **ID**: `achievement_relationship_007`
- **名称**: 治愈者
- **描述**: 成功治愈王掌柜的疾病
- **解锁条件**: `flag_wang_recovered == true`
- **解锁时机**: 游戏过程中
- **奖励**: 王掌柜好感+20
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 50%

---

### R-008 红娘
- **ID**: `achievement_relationship_008`
- **名称**: 红娘
- **描述**: 促成小梅和李文斌的婚事
- **解锁条件**: `flag_xiaomei_married == true AND flag_liwenbin_married == true`
- **解锁时机**: 第17章
- **奖励**: 观看婚礼过场动画
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 35%

---

### R-009 生命的延续
- **ID**: `achievement_relationship_009`
- **名称**: 生命的延续
- **描述**: 小梅在游戏中怀孕并生下孩子
- **解锁条件**: `flag_xiaomei_pregnant == true AND flag_child_born == true`
- **解锁时机**: 尾声
- **奖励**: 特殊结局CG"新生"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 28%

---

### R-010 众望所归
- **ID**: `achievement_relationship_010`
- **名称**: 众望所归
- **描述**: 与所有主要角色关系都达到60以上
- **解锁条件**:
  ```
  relationship.wang_fugui >= 60 AND
  relationship.xiao_mei >= 60 AND
  relationship.li_wenbin >= 60 AND
  relationship.zhao_nurse >= 60 AND
  relationship.weitelin >= 60
  ```
- **解锁时机**: 游戏过程中
- **奖励**: 群体士气+15
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 20%

---

### R-011 信任危机
- **ID**: `achievement_relationship_011`
- **名称**: 信任危机
- **描述**: 被某个角色的信任度降至0
- **解锁条件**: `ANY(relationship[].trust == 0)`
- **解锁时机**: 游戏过程中
- **奖励**: 无
- **隐藏**: 否
- **难度**: ★★☆☆☆
- **预期解锁率**: 30%
- **特殊**: 负面成就

---

### R-012 背叛者
- **ID**: `achievement_relationship_012`
- **名称**: 背叛者
- **描述**: 向日本军官出卖情报
- **解锁条件**: `flag_betrayed_info == true`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊分支"背叛者之路"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 12%
- **特殊**: 负面成就，道德选择

---

### R-013 救赎
- **ID**: `achievement_relationship_013`
- **名称**: 救赎
- **描述**: 在背叛后重新赢得信任
- **解锁条件**:
  ```
  flag_betrayed_info == true AND
  flag_redeemed == true AND
  relationship.wang_fugui >= 50
  ```
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊对话"原谅"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 8%

---

### R-014 社交达人
- **ID**: `achievement_relationship_014`
- **名称**: 社交达人
- **描述**: 与20个以上NPC建立友好关系
- **解锁条件**: `COUNT(relationship[].value >= 40) >= 20`
- **解锁时机**: 游戏过程中
- **奖励**: 获得更多情报来源
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 33%

---

### R-015 孤独者
- **ID**: `achievement_relationship_015`
- **名称**: 孤独者
- **描述**: 完成游戏但所有关系值都低于30
- **解锁条件**:
  ```
  flag_completed_epilogue == true AND
  ALL(relationship[].value < 30)
  ```
- **解锁时机**: 尾声结束
- **奖励**: 特殊结局CG"孤独的归途"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 10%
- **特殊**: 负面成就

---

## 2.3 收集成就 (Collection Achievements) - 10个

### C-001 历史学家
- **ID**: `achievement_collection_001`
- **名称**: 历史学家
- **描述**: 解锁50%的历史资料库条目
- **解锁条件**: `archive.unlocked_percentage >= 0.5`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"历史爱好者"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 40%

---

### C-002 博学者
- **ID**: `achievement_collection_002`
- **名称**: 博学者
- **描述**: 解锁100%的历史资料库条目
- **解锁条件**: `archive.unlocked_percentage == 1.0`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"历史专家" + 隐藏文章
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 18%

---

### C-003 日记完整
- **ID**: `achievement_collection_003`
- **名称**: 日记完整
- **描述**: 收集所有日记条目（包括隐藏条目）
- **解锁条件**: `diary.entry_count >= 90 AND diary.hidden_entry_count >= 5`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁"完整日记"PDF导出功能
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 22%

---

### C-004 摄影师
- **ID**: `achievement_collection_004`
- **名称**: 摄影师
- **描述**: 在日记中添加30张以上照片
- **解锁条件**: `diary.photo_count >= 30`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁相册浏览模式
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 35%

---

### C-005 考古学家
- **ID**: `achievement_collection_005`
- **名称**: 考古学家
- **描述**: 找到所有隐藏物品
- **解锁条件**: `inventory.hidden_items_found == inventory.hidden_items_total` (10个)
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊展览室
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 15%

---

### C-006 收藏家
- **ID**: `achievement_collection_006`
- **名称**: 收藏家
- **描述**: 获得20个以上贵重物品
- **解锁条件**: `inventory.valuable_items_count >= 20`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁黑市特殊交易
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 28%

---

### C-007 信件收集者
- **ID**: `achievement_collection_007`
- **名称**: 信件收集者
- **描述**: 收集所有角色的私人信件
- **解锁条件**: `collection.letters_count == collection.letters_total` (15封)
- **解锁时机**: 游戏过程中
- **奖励**: 解锁"书信集"阅读模式
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 20%

---

### C-008 音乐爱好者
- **ID**: `achievement_collection_008`
- **名称**: 音乐爱好者
- **描述**: 解锁所有音乐曲目
- **解锁条件**: `music.unlocked_count == music.total_count` (30首)
- **解锁时机**: 游戏过程中
- **奖励**: 解锁音乐鉴赏模式
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 32%

---

### C-009 完美主义者
- **ID**: `achievement_collection_009`
- **名称**: 完美主义者
- **描述**: 完成所有收集类成就
- **解锁条件**:
  ```
  achievement_collection_002.unlocked == true AND
  achievement_collection_003.unlocked == true AND
  achievement_collection_005.unlocked == true AND
  achievement_collection_007.unlocked == true AND
  achievement_collection_008.unlocked == true
  ```
- **解锁时机**: 游戏过程中
- **奖励**: 白金奖杯图标
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 8%

---

### C-010 时光档案
- **ID**: `achievement_collection_010`
- **名称**: 时光档案
- **描述**: 在所有章节都保存了游戏截图
- **解锁条件**: `screenshots_per_chapter[] ALL >= 1`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁时间线回顾模式
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 25%

---

## 2.4 挑战成就 (Challenge Achievements) - 15个

### CH-001 困难模式通关
- **ID**: `achievement_challenge_001`
- **名称**: 困难模式通关
- **描述**: 在困难难度下完成游戏
- **解锁条件**: `game.difficulty == "hard" AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"硬核玩家"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 12%

---

### CH-002 零伤亡
- **ID**: `achievement_challenge_002`
- **名称**: 零伤亡
- **描述**: 完成游戏且没有任何NPC死亡
- **解锁条件**: `character_manager.death_count == 0 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"生命守护者"
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 5%

---

### CH-003 最小化牺牲
- **ID**: `achievement_challenge_003`
- **名称**: 最小化牺牲
- **描述**: 完成游戏且NPC死亡数不超过3人
- **解锁条件**: `character_manager.death_count <= 3 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"保护者"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 18%

---

### CH-004 节俭持家
- **ID**: `achievement_challenge_004`
- **名称**: 节俭持家
- **描述**: 完成游戏时库存资源超过100单位
- **解锁条件**:
  ```
  (inventory.food + inventory.water + inventory.medicine + inventory.fuel) >= 100 AND
  flag_completed_epilogue == true
  ```
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"后勤专家"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 15%

---

### CH-005 极限生存
- **ID**: `achievement_challenge_005`
- **名称**: 极限生存
- **描述**: 在资源为0的情况下存活7天
- **解锁条件**: `days_survived_with_zero_resources >= 7`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"绝境求生"
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 3%

---

### CH-006 铁人模式
- **ID**: `achievement_challenge_006`
- **名称**: 铁人模式
- **描述**: 不使用读档功能完成游戏
- **解锁条件**: `game.load_count == 0 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"铁人"
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 2%

---

### CH-007 和平主义者
- **ID**: `achievement_challenge_007`
- **名称**: 和平主义者
- **描述**: 从未选择暴力选项完成游戏
- **解锁条件**: `player.violence_choice_count == 0 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"和平使者"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 10%

---

### CH-008 道德完人
- **ID**: `achievement_challenge_008`
- **名称**: 道德完人
- **描述**: 从未做出负面道德选择
- **解锁条件**: `player.negative_moral_choices == 0 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"圣人"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 8%

---

### CH-009 必要之恶
- **ID**: `achievement_challenge_009`
- **名称**: 必要之恶
- **描述**: 为了生存做出10次以上负面道德选择
- **解锁条件**: `player.negative_moral_choices >= 10`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊剧情"黑暗之路"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 20%
- **特殊**: 负面成就，道德探讨

---

### CH-010 时间旅行者
- **ID**: `achievement_challenge_010`
- **名称**: 时间旅行者
- **描述**: 在游戏中度过100个游戏日
- **解锁条件**: `game_days_passed >= 100`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"时间见证者"
- **隐藏**: 否
- **难度**: ★★★☆☆
- **预期解锁率**: 30%

---

### CH-011 健康达人
- **ID**: `achievement_challenge_011`
- **名称**: 健康达人
- **描述**: 全程保持健康值在80以上
- **解锁条件**: `player.min_health_recorded >= 80 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"养生专家"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 12%

---

### CH-012 高士气领袖
- **ID**: `achievement_challenge_012`
- **名称**: 高士气领袖
- **描述**: 全程保持群体士气在70以上
- **解锁条件**: `group.min_morale_recorded >= 70 AND flag_completed_epilogue == true`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"精神领袖"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 10%

---

### CH-013 富裕生活
- **ID**: `achievement_challenge_013`
- **名称**: 富裕生活
- **描述**: 同时拥有食物20+、水20+、药品10+、燃料20+
- **解锁条件**:
  ```
  inventory.food >= 20 AND
  inventory.water >= 20 AND
  inventory.medicine >= 10 AND
  inventory.fuel >= 20
  ```
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"资源大亨"
- **隐藏**: 否
- **难度**: ★★★★☆
- **预期解锁率**: 14%

---

### CH-014 精准判断
- **ID**: `achievement_challenge_014`
- **名称**: 精准判断
- **描述**: 所有重大选择都获得最佳结果
- **解锁条件**: `player.optimal_choice_count == player.major_choice_count`
- **解锁时机**: 尾声结束
- **奖励**: 特殊称号"决策大师"
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 4%

---

### CH-015 全成就达成
- **ID**: `achievement_challenge_015`
- **名称**: 全成就达成
- **描述**: 解锁所有其他79个成就
- **解锁条件**: `achievement_manager.unlocked_count >= 79`
- **解锁时机**: 解锁最后一个成就时
- **奖励**: 白金奖杯 + 特殊称号"成就大师"
- **隐藏**: 否
- **难度**: ★★★★★
- **预期解锁率**: 1%
- **特殊**: 终极成就

---

## 2.5 隐藏成就 (Hidden Achievements) - 10个

### H-001 时间悖论
- **ID**: `achievement_hidden_001`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 时间悖论
- **真实描述**: 尝试告诉2024年的自己不要穿越
- **解锁条件**:
  ```
  flag_completed_epilogue == true AND
  flag_tried_prevent_time_travel == true
  ```
- **解锁时机**: 尾声
- **奖励**: 特殊对话"时间的警告"
- **隐藏**: 是
- **难度**: ★★★☆☆
- **预期解锁率**: 15%

---

### H-002 白色圣诞
- **ID**: `achievement_hidden_002`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 白色圣诞
- **真实描述**: 在1937年12月25日送礼物给所有角色
- **解锁条件**:
  ```
  game_date == "1937-12-25" AND
  gift_given_wang == true AND
  gift_given_xiaomei == true AND
  gift_given_liwenbin == true AND
  gift_given_zhao == true AND
  gift_given_weitelin == true
  ```
- **解锁时机**: 12月25日
- **奖励**: 观看特殊过场动画"圣诞之夜"
- **隐藏**: 是
- **难度**: ★★★☆☆
- **预期解锁率**: 12%

---

### H-003 彩蛋猎人
- **ID**: `achievement_hidden_003`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 彩蛋猎人
- **真实描述**: 找到所有隐藏彩蛋（10个）
- **解锁条件**: `easter_eggs_found == 10`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁彩蛋博物馆
- **隐藏**: 是
- **难度**: ★★★★☆
- **预期解锁率**: 8%

---

### H-004 第四面墙
- **ID**: `achievement_hidden_004`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 第四面墙
- **真实描述**: 发现开发者留言
- **解锁条件**: `flag_found_dev_message == true`
- **解锁时机**: 游戏过程中
- **奖励**: 阅读开发者感言
- **隐藏**: 是
- **难度**: ★★★☆☆
- **预期解锁率**: 10%

---

### H-005 不死之身
- **ID**: `achievement_hidden_005`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 不死之身
- **真实描述**: 触发10次Game Over但从未真正死亡
- **解锁条件**: `player.game_over_count >= 10 AND player.total_deaths == 0`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊称号"不死者"
- **隐藏**: 是
- **难度**: ★★★★☆
- **预期解锁率**: 6%

---

### H-006 蝴蝶效应
- **ID**: `achievement_hidden_006`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 蝴蝶效应
- **真实描述**: 一个微小的选择改变了一个角色的命运
- **解锁条件**: `flag_butterfly_effect_triggered == true`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁特殊剧情分支
- **隐藏**: 是
- **难度**: ★★★☆☆
- **预期解锁率**: 18%

---

### H-007 平行世界
- **ID**: `achievement_hidden_007`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 平行世界
- **真实描述**: 在同一存档点读取5次以上，选择不同分支
- **解锁条件**: `save_reload_branches >= 5 FROM same_save_point`
- **解锁时机**: 游戏过程中
- **奖励**: 特殊对话"平行的可能"
- **隐藏**: 是
- **难度**: ★★☆☆☆
- **预期解锁率**: 22%

---

### H-008 历史的回响
- **ID**: `achievement_hidden_008`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 历史的回响
- **真实描述**: 在尾声中遇到小梅的后代
- **解锁条件**:
  ```
  flag_completed_epilogue == true AND
  character_xiaomei_alive == true AND
  flag_child_born == true AND
  flag_met_descendants == true
  ```
- **解锁时机**: 尾声
- **奖励**: 特殊结局CG"代代相传"
- **隐藏**: 是
- **难度**: ★★★★☆
- **预期解锁率**: 12%

---

### H-009 开发者的问候
- **ID**: `achievement_hidden_009`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 开发者的问候
- **真实描述**: 在制作名单界面停留5分钟以上
- **解锁条件**: `credits_view_time >= 300` (秒)
- **解锁时机**: 制作名单界面
- **奖励**: 解锁幕后花絮视频
- **隐藏**: 是
- **难度**: ★☆☆☆☆
- **预期解锁率**: 25%

---

### H-010 真·历史学家
- **ID**: `achievement_hidden_010`
- **名称**: ？？？
- **描述**: ？？？
- **真实名称**: 真·历史学家
- **真实描述**: 找到所有隐藏的历史注释（50个）
- **解锁条件**: `hidden_historical_notes_found == 50`
- **解锁时机**: 游戏过程中
- **奖励**: 解锁学术论文"南京1937"
- **隐藏**: 是
- **难度**: ★★★★★
- **预期解锁率**: 5%

---

## 三、成就系统技术实现

### 3.1 成就数据结构

```csharp
// C# 示例
public class Achievement {
    public string id;                    // 成就ID
    public string name;                  // 成就名称
    public string description;           // 成就描述
    public AchievementCategory category; // 类别
    public int difficulty;               // 难度 (1-5星)
    public bool hidden;                  // 是否隐藏
    public bool unlocked;                // 是否已解锁
    public DateTime unlockTime;          // 解锁时间
    public string iconPath;              // 图标路径
    public string iconPathLocked;        // 未解锁图标路径
    public AchievementReward reward;     // 奖励

    // 解锁条件检查函数
    public delegate bool UnlockCondition();
    public UnlockCondition checkCondition;

    // 解锁进度 (用于渐进式成就)
    public float progress;               // 0.0 - 1.0
    public string progressText;          // "12/20"
}

public enum AchievementCategory {
    Story,
    Relationship,
    Collection,
    Challenge,
    Hidden
}

public class AchievementReward {
    public string rewardType;            // "item", "unlock", "title", "none"
    public string rewardId;              // 奖励物品/功能ID
    public string rewardDescription;     // 奖励描述
}
```

---

### 3.2 成就管理器

```csharp
public class AchievementManager : MonoBehaviour {
    private List<Achievement> achievements;
    private Dictionary<string, Achievement> achievementDict;

    // Steam集成
    private SteamAchievementHandler steamHandler;

    // 初始化
    public void Initialize() {
        LoadAchievementData();
        LoadPlayerProgress();

        // 初始化Steam成就系统
        if (SteamManager.Initialized) {
            steamHandler = new SteamAchievementHandler();
        }
    }

    // 从JSON加载成就数据
    private void LoadAchievementData() {
        TextAsset jsonData = Resources.Load<TextAsset>("Data/achievements");
        AchievementData data = JsonUtility.FromJson<AchievementData>(jsonData.text);
        achievements = data.achievements;

        // 构建字典便于查询
        achievementDict = new Dictionary<string, Achievement>();
        foreach (var ach in achievements) {
            achievementDict[ach.id] = ach;
        }
    }

    // 加载玩家进度
    private void LoadPlayerProgress() {
        string savePath = Application.persistentDataPath + "/achievement_progress.json";
        if (File.Exists(savePath)) {
            string json = File.ReadAllText(savePath);
            AchievementProgress progress = JsonUtility.FromJson<AchievementProgress>(json);
            ApplyProgress(progress);
        }
    }

    // 保存玩家进度
    public void SaveProgress() {
        AchievementProgress progress = new AchievementProgress();
        progress.unlockedAchievements = new List<string>();

        foreach (var ach in achievements) {
            if (ach.unlocked) {
                progress.unlockedAchievements.Add(ach.id);
            }
        }

        string json = JsonUtility.ToJson(progress, true);
        string savePath = Application.persistentDataPath + "/achievement_progress.json";
        File.WriteAllText(savePath, json);
    }

    // 检查并解锁成就
    public void CheckAchievement(string achievementId) {
        if (!achievementDict.ContainsKey(achievementId)) {
            Debug.LogError($"Achievement {achievementId} not found!");
            return;
        }

        Achievement ach = achievementDict[achievementId];

        // 已解锁则跳过
        if (ach.unlocked) return;

        // 检查解锁条件
        if (ach.checkCondition != null && ach.checkCondition()) {
            UnlockAchievement(achievementId);
        }
    }

    // 解锁成就
    public void UnlockAchievement(string achievementId) {
        Achievement ach = achievementDict[achievementId];

        if (ach.unlocked) return; // 防止重复解锁

        ach.unlocked = true;
        ach.unlockTime = DateTime.Now;
        ach.progress = 1.0f;

        // 显示解锁通知
        ShowAchievementNotification(ach);

        // 应用奖励
        ApplyReward(ach.reward);

        // 同步到Steam
        if (SteamManager.Initialized) {
            steamHandler.UnlockSteamAchievement(achievementId);
        }

        // 保存进度
        SaveProgress();

        // 触发事件
        OnAchievementUnlocked?.Invoke(ach);

        Debug.Log($"Achievement Unlocked: {ach.name}");
    }

    // 更新渐进式成就进度
    public void UpdateAchievementProgress(string achievementId, float progress) {
        Achievement ach = achievementDict[achievementId];
        ach.progress = Mathf.Clamp01(progress);

        // 自动解锁
        if (ach.progress >= 1.0f && !ach.unlocked) {
            UnlockAchievement(achievementId);
        }
    }

    // 显示成就解锁通知
    private void ShowAchievementNotification(Achievement ach) {
        GameObject notification = Instantiate(achievementNotificationPrefab);
        AchievementNotificationUI ui = notification.GetComponent<AchievementNotificationUI>();
        ui.ShowAchievement(ach);

        // 播放音效
        AudioManager.Instance.PlaySFX("achievement_unlock");
    }

    // 应用奖励
    private void ApplyReward(AchievementReward reward) {
        if (reward == null) return;

        switch (reward.rewardType) {
            case "item":
                InventoryManager.Instance.AddItem(reward.rewardId);
                break;
            case "unlock":
                UnlockManager.Instance.Unlock(reward.rewardId);
                break;
            case "title":
                PlayerProfile.Instance.AddTitle(reward.rewardId);
                break;
        }
    }

    // 获取统计数据
    public AchievementStats GetStats() {
        int total = achievements.Count;
        int unlocked = achievements.Count(a => a.unlocked);
        float percentage = (float)unlocked / total * 100f;

        return new AchievementStats {
            totalCount = total,
            unlockedCount = unlocked,
            percentage = percentage
        };
    }

    // 获取各类别统计
    public Dictionary<AchievementCategory, int> GetCategoryStats() {
        var stats = new Dictionary<AchievementCategory, int>();

        foreach (AchievementCategory category in Enum.GetValues(typeof(AchievementCategory))) {
            stats[category] = achievements.Count(a => a.category == category && a.unlocked);
        }

        return stats;
    }

    // 事件
    public event Action<Achievement> OnAchievementUnlocked;
}
```

---

### 3.3 成就解锁条件示例

```csharp
// 在游戏各系统中检查成就
public class GameStateManager : MonoBehaviour {
    private AchievementManager achievementManager;

    void Start() {
        achievementManager = AchievementManager.Instance;

        // 注册成就检查条件
        RegisterAchievementConditions();
    }

    void RegisterAchievementConditions() {
        // S-001: 穿越者
        Achievement s001 = achievementManager.GetAchievement("achievement_story_001");
        s001.checkCondition = () => {
            return GameFlags.GetFlag("flag_completed_prologue");
        };

        // S-010: 完美结局
        Achievement s010 = achievementManager.GetAchievement("achievement_story_010");
        s010.checkCondition = () => {
            return GameFlags.GetFlag("flag_completed_epilogue") &&
                   CharacterManager.Instance.IsAlive("wang_fugui") &&
                   CharacterManager.Instance.IsAlive("xiao_mei") &&
                   CharacterManager.Instance.IsAlive("li_wenbin") &&
                   CharacterManager.Instance.IsAlive("zhao_nurse") &&
                   CharacterManager.Instance.IsAlive("weitelin");
        };

        // R-001: 如父如子
        Achievement r001 = achievementManager.GetAchievement("achievement_relationship_001");
        r001.checkCondition = () => {
            return RelationshipManager.Instance.GetRelationship("wang_fugui") >= 100;
        };

        // C-001: 历史学家
        Achievement c001 = achievementManager.GetAchievement("achievement_collection_001");
        c001.checkCondition = () => {
            return ArchiveManager.Instance.GetUnlockedPercentage() >= 0.5f;
        };

        // CH-002: 零伤亡
        Achievement ch002 = achievementManager.GetAchievement("achievement_challenge_002");
        ch002.checkCondition = () => {
            return CharacterManager.Instance.GetDeathCount() == 0 &&
                   GameFlags.GetFlag("flag_completed_epilogue");
        };
    }

    // 在关键时刻检查成就
    public void OnChapterComplete(int chapterIndex) {
        // 检查章节成就
        achievementManager.CheckAchievement($"achievement_story_{chapterIndex:D3}");

        // 检查其他可能解锁的成就
        achievementManager.CheckAllAchievements();
    }

    public void OnRelationshipChanged(string npcId, int newValue) {
        // 检查关系成就
        achievementManager.CheckAchievements(AchievementCategory.Relationship);
    }
}
```

---

### 3.4 Steam成就集成

```csharp
// Steam成就处理器
public class SteamAchievementHandler {
    private Callback<UserStatsReceived_t> userStatsReceived;
    private bool statsValid = false;

    public SteamAchievementHandler() {
        // 请求用户统计数据
        SteamUserStats.RequestCurrentStats();

        // 注册回调
        userStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
    }

    private void OnUserStatsReceived(UserStatsReceived_t callback) {
        if (callback.m_eResult == EResult.k_EResultOK) {
            statsValid = true;
            Debug.Log("Steam stats received successfully");
        }
    }

    // 解锁Steam成就
    public void UnlockSteamAchievement(string achievementId) {
        if (!statsValid) return;

        // 获取Steam成就API名称 (通常与游戏内ID相同)
        string steamAchievementName = achievementId;

        // 设置成就
        bool success = SteamUserStats.SetAchievement(steamAchievementName);

        if (success) {
            // 上传统计数据
            SteamUserStats.StoreStats();
            Debug.Log($"Steam achievement unlocked: {steamAchievementName}");
        } else {
            Debug.LogError($"Failed to unlock Steam achievement: {steamAchievementName}");
        }
    }

    // 检查Steam成就是否已解锁
    public bool IsSteamAchievementUnlocked(string achievementId) {
        if (!statsValid) return false;

        bool achieved = false;
        SteamUserStats.GetAchievement(achievementId, out achieved);
        return achieved;
    }

    // 同步Steam成就到游戏内
    public void SyncSteamAchievements() {
        if (!statsValid) return;

        AchievementManager achManager = AchievementManager.Instance;

        foreach (var ach in achManager.GetAllAchievements()) {
            if (IsSteamAchievementUnlocked(ach.id) && !ach.unlocked) {
                // Steam已解锁但游戏内未解锁，同步
                achManager.UnlockAchievement(ach.id);
            }
        }
    }
}
```

---

### 3.5 成就通知UI

```csharp
// 成就解锁通知UI
public class AchievementNotificationUI : MonoBehaviour {
    public Image iconImage;
    public Text nameText;
    public Text descriptionText;
    public CanvasGroup canvasGroup;

    private float displayDuration = 5f;
    private float fadeInDuration = 0.5f;
    private float fadeOutDuration = 0.5f;

    public void ShowAchievement(Achievement achievement) {
        // 设置UI内容
        iconImage.sprite = Resources.Load<Sprite>(achievement.iconPath);
        nameText.text = achievement.name;
        descriptionText.text = achievement.description;

        // 播放动画
        StartCoroutine(AnimateNotification());
    }

    private IEnumerator AnimateNotification() {
        // 淡入
        float elapsed = 0f;
        while (elapsed < fadeInDuration) {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeInDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 显示
        yield return new WaitForSeconds(displayDuration);

        // 淡出
        elapsed = 0f;
        while (elapsed < fadeOutDuration) {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeOutDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // 销毁
        Destroy(gameObject);
    }
}
```

---

## 四、成就UI界面设计

### 4.1 成就列表界面

```
┌──────────────────────────────────────────────────────────┐
│                    成就系统                    [X 关闭] │
├────────────────────┬─────────────────────────────────────┤
│ [分类筛选]         │  [成就列表 - 滚动区域]              │
│                    │                                     │
│ • 全部 (35/80)     │  ┌─────────────────────────────┐  │
│ • 剧情 (18/30)     │  │ [图标] 穿越者         ✓    │  │
│ • 关系 (8/15)      │  │  完成序章，穿越到1937年    │  │
│ • 收集 (5/10)      │  │  解锁时间: 2024-11-05      │  │
│ • 挑战 (3/15)      │  │  难度: ★☆☆☆☆              │  │
│ • 隐藏 (1/10)      │  └─────────────────────────────┘  │
│                    │                                     │
│ [排序方式]         │  ┌─────────────────────────────┐  │
│ • 解锁时间         │  │ [图标] 完美结局      🔒    │  │
│ • 稀有度           │  │  保护所有主要角色存活      │  │
│ • 难度             │  │  进度: 3/5 角色存活        │  │
│                    │  │  难度: ★★★★☆              │  │
│ [显示选项]         │  │  预期解锁率: 20%           │  │
│ ☑ 显示已解锁       │  └─────────────────────────────┘  │
│ ☑ 显示未解锁       │                                     │
│ ☑ 显示隐藏成就     │  ┌─────────────────────────────┐  │
│                    │  │ [?] ？？？          🔒     │  │
│                    │  │  ？？？                    │  │
│                    │  │  这是一个隐藏成就          │  │
│                    │  │  难度: ★★★☆☆              │  │
│                    │  └─────────────────────────────┘  │
│                    │                                     │
│                    │  [更多成就...]                      │
├────────────────────┴─────────────────────────────────────┤
│  总进度: ████████░░░░░░░░░░ 35/80 (43.75%)             │
│  稀有成就: 5  •  完成度排名: 全球前 25%                │
└──────────────────────────────────────────────────────────┘
```

---

### 4.2 成就详情界面

```
点击成就条目 → 打开详情面板
┌──────────────────────────────────────────────┐
│          [成就大图标]                        │
│                                              │
│              完美结局                        │
│         ★★★★☆ 困难                         │
│                                              │
│  保护所有主要角色存活至游戏结束              │
│                                              │
│  解锁条件:                                   │
│  • 完成游戏 ✓                               │
│  • 王掌柜存活 ✓                             │
│  • 小梅存活 ✓                               │
│  • 李文斌存活 ✓                             │
│  • 赵护士存活 🔒                            │
│  • 魏特琳存活 🔒                            │
│                                              │
│  奖励: 特殊结局CG                            │
│                                              │
│  全球解锁率: 20.5%                           │
│  你的好友中: 3/15 已解锁                     │
│                                              │
│              [返回列表]                      │
└──────────────────────────────────────────────┘
```

---

## 五、成就美术资源

### 5.1 成就图标规格

**尺寸**: 256×256 像素 (PNG格式, Alpha通道)

**风格**:
- 1937年相关成就: 水墨画风格, 黑白为主
- 2024年相关成就: 现代扁平风格, 彩色
- 隐藏成就: 神秘符号, 解锁后显示真实图标

**状态**:
- 已解锁: 全彩色, 清晰
- 未解锁: 灰度处理 + 剪影效果
- 隐藏未解锁: 问号图标

---

### 5.2 成就图标列表

| 成就ID | 图标描述 | 颜色方案 | 优先级 |
|--------|---------|---------|--------|
| S-001 | 时空漩涡 + 人物剪影 | 蓝紫渐变 | P0 |
| S-004 | 12月13日日历撕页 + 黑色基调 | 黑红 | P0 |
| S-010 | 五个角色剪影手牵手 | 金色 | P0 |
| R-001 | 父子握手 | 暖色调 | P1 |
| R-009 | 婴儿手印 | 粉色 | P1 |
| C-001 | 书本 + 放大镜 | 棕色 | P1 |
| C-003 | 日记本 + 钢笔 | 米黄色 | P1 |
| CH-002 | 盾牌 + 心形 | 绿色 | P0 |
| CH-006 | 铁人图标 | 金属灰 | P1 |
| CH-015 | 白金奖杯 | 白金色 | P0 |
| H-001 | 时钟 + 悖论符号 | 神秘紫 | P2 |
| H-003 | 彩蛋 + 放大镜 | 彩虹色 | P2 |

**总数**: 80个图标
**制作工时**: 约20人日 (平均每个图标3小时)

---

### 5.3 成就背景与特效

- **稀有成就**: 金色光芒特效
- **困难成就**: 红色边框
- **隐藏成就**: 神秘粒子效果
- **白金成就**: 白金光柱动画

---

## 六、成就追踪与分析

### 6.1 数据追踪

通过成就解锁率分析玩家行为:

```
SELECT
    achievement_id,
    COUNT(*) as unlock_count,
    COUNT(*) * 100.0 / (SELECT COUNT(DISTINCT player_id) FROM players) as unlock_rate
FROM achievement_unlocks
GROUP BY achievement_id
ORDER BY unlock_rate DESC;
```

**关键指标**:
- 完成率: 各章节成就解锁率 → 了解流失点
- 分支率: 不同结局成就比例 → 分析玩家选择倾向
- 难度验证: 挑战成就解锁率 → 验证难度设计合理性

---

### 6.2 成就热力图

可视化玩家在游戏中的成就解锁路径:

```
章节1  章节2  章节3  ...  尾声
 95%    90%    80%         50%  (剧情成就解锁率)
  ↓      ↓      ↓           ↓
分析流失原因
```

---

## 七、本地化要求

### 7.1 文本本地化

所有成就名称和描述需翻译为:
- 简体中文 (默认)
- 繁体中文
- English
- 日本語
- 한국어

**本地化表格式** (achievements_loc.json):
```json
{
  "achievement_story_001": {
    "zh-CN": {
      "name": "穿越者",
      "description": "完成序章，穿越到1937年"
    },
    "en-US": {
      "name": "Time Traveler",
      "description": "Complete the prologue and travel to 1937"
    },
    "ja-JP": {
      "name": "タイムトラベラー",
      "description": "プロローグを完了し、1937年にタイムスリップする"
    }
  }
}
```

---

### 7.2 文化适配

- 某些成就名称需考虑文化敏感性
- 图标避免使用特定文化禁忌符号
- Steam成就描述限制150字符 (英文)

---

## 八、测试检查清单

### 成就系统测试

- [ ] 所有80个成就都能正常解锁
- [ ] 解锁条件逻辑正确无误
- [ ] 解锁通知正常显示
- [ ] 成就图标正确加载
- [ ] 隐藏成就在解锁前正确隐藏
- [ ] 成就进度正确追踪
- [ ] 存档系统正确保存成就进度
- [ ] Steam成就正确同步
- [ ] 多语言文本正确显示
- [ ] 成就列表界面正常排序筛选
- [ ] 成就详情界面信息完整
- [ ] 全成就达成触发正确
- [ ] 成就奖励正确发放
- [ ] 成就音效正常播放

### 边界情况测试

- [ ] 在离线状态下解锁成就
- [ ] Steam API不可用时的fallback
- [ ] 存档损坏时成就数据处理
- [ ] 快速连续解锁多个成就
- [ ] 成就数据与游戏版本兼容性

---

## 九、开发优先级与里程碑

### Phase 1: 核心系统 (2周)
- [ ] 成就数据结构设计
- [ ] 成就管理器实现
- [ ] 基础解锁逻辑
- [ ] 成就UI框架

### Phase 2: 内容填充 (3周)
- [ ] 所有80个成就数据录入
- [ ] 解锁条件编写
- [ ] 成就图标制作 (20个核心图标)
- [ ] Steam集成测试

### Phase 3: UI与反馈 (2周)
- [ ] 成就列表界面完善
- [ ] 解锁通知动画
- [ ] 音效配置
- [ ] 特效实现

### Phase 4: 测试与调整 (2周)
- [ ] 全面测试所有成就
- [ ] 解锁率数据收集
- [ ] 难度平衡调整
- [ ] Bug修复

**总工期**: 约9周

---

## 十、附录：快速参考

### 成就ID命名规范

```
achievement_{category}_{number}

category:
- story: 剧情成就
- relationship: 关系成就
- collection: 收集成就
- challenge: 挑战成就
- hidden: 隐藏成就

number: 001-030 (三位数)

示例: achievement_story_001
```

---

### 常用函数速查

```csharp
// 解锁成就
AchievementManager.Instance.UnlockAchievement("achievement_story_001");

// 检查成就
AchievementManager.Instance.CheckAchievement("achievement_story_001");

// 更新进度
AchievementManager.Instance.UpdateProgress("achievement_collection_001", 0.5f);

// 获取统计
var stats = AchievementManager.Instance.GetStats();
Debug.Log($"已解锁: {stats.unlockedCount}/{stats.totalCount}");

// 检查是否已解锁
bool unlocked = AchievementManager.Instance.IsUnlocked("achievement_story_001");
```

---

**文档版本**: v1.0
**完成日期**: 2025-11-05
**审核状态**: 待审核
**下一步**: 开始成就系统实现，制作成就图标

---

**关联文档**:
- `/docs/execution/ui_interaction_flow.md` - UI交互流程 (配套文档)
- `/docs/gameplay/gameplay_numerical.md` - 数值系统
- `/docs/narrative/narrative_chapters.md` - 章节设计
- `/docs/technical/technical_data_structure.md` - 数据结构设计
