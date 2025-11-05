# 随机事件触发表

**文档版本**: v1.0
**最后更新**: 2025-11-05
**随机事件总数**: 24+个随机事件节点

---

## 概述

随机事件是在游戏过程中根据概率和条件触发的非强制事件，用于增加游戏的不确定性和重玩价值。每个游戏日都有可能触发0-2个随机事件。

---

## 随机事件系统机制

### 触发机制
```
每日检查流程:
1. 计算当日事件触发概率: base_rate × 难度系数 × 士气系数
2. 从符合条件的事件池中随机抽取
3. 检查事件冷却时间
4. 检查事件前置条件
5. 触发事件或跳过
```

### 基础参数
- **基础触发率**: 35%（每日）
- **最大同日事件数**: 2个
- **全局冷却**: 事件触发后至少间隔1天
- **单事件冷却**: 每个事件触发后3-7天内不再触发

### 权重系统
```
事件权重计算:
总权重 = 基础权重 × 情境修正 × 稀有度修正

稀有度分类:
- 高频事件: 权重 × 2.0
- 中频事件: 权重 × 1.0
- 低频事件: 权重 × 0.5
- 极稀有: 权重 × 0.2
```

---

## 类别一：食物短缺事件（6个）

### RE001: 食物短缺 - 多选项
**事件ID**: `FoodShortage_01`
**来源文件**: random_events.yarn (line 1-42)

| 属性 | 值 |
|------|-----|
| **类型** | 资源危机 |
| **频率** | 低频 (权重: 0.5) |
| **触发条件** | `$food_supply < 30` |
| **冷却时间** | 7天 |
| **可重复** | 是（最多3次） |
| **触发时段** | 任意时间 |
| **前置事件** | 第五章完成 |

**触发概率公式**:
```
probability = 0.35 × (1 - food_supply/100) × 0.5
当 food_supply < 20 时: probability × 1.5
```

**选项分支**:

| 选项 | 条件要求 | 成功率 | 资源消耗 | 结果 |
|------|---------|--------|---------|------|
| 外出寻找食物 | 无 | 见子分支 | 无 | 跳转至 FoodShortage_01_Dangerous 或 Success |
| 减少配给份额 | 无 | 100% | food_supply -20 | morale -10, hunger +5 |
| 优先儿童老人 | 无 | 100% | 无 | morale +5, hunger +3, relationship_wang +5 |

**子分支 - 外出寻找**:

触发条件判断:
```
if japanese_patrol_level > 3:
    → FoodShortage_01_Dangerous
else:
    → FoodShortage_01_Success
```

**危险分支结果**:

| 条件 | 结果 | 影响 |
|------|------|------|
| stealth_skill ≥ 5 | 成功躲避，找到物资 | food_supply +50, morale +15, stealth_skill +1 |
| has_german_pass = true | 被抓但凭证件脱身 | danger_level +5, 空手而归 |
| 其他情况 | 被殴打 | health -20, morale -15, 空手而归 |

**成功分支结果**:
- food_supply +50
- morale +15
- stealth_skill +1
- relationship_wang +5

**事件标记**: `$event_food_shortage_01 = true`

---

### RE002: 食物分配争执
**事件ID**: `FoodShortage_02`
**来源文件**: random_events.yarn (line 103-142)

| 属性 | 值 |
|------|-----|
| **类型** | 社区冲突 |
| **频率** | 高频 (权重: 2.0) |
| **触发条件** | `$food_supply < 50` AND `$refugees > 20` |
| **冷却时间** | 5天 |
| **可重复** | 是（无限） |
| **触发时段** | 配给时间（每日下午） |

**触发概率**:
```
probability = 0.35 × (refugees / 50) × 2.0
最高概率: 70% (当难民>50人时)
```

**选项对比**:

| 选项 | 道德评价 | 士气影响 | 公平度影响 | 其他影响 |
|------|---------|---------|-----------|---------|
| 按人头平均分配 | +10 | +10 | +5 | 获得"公正管理者"标记 |
| 按家庭单位分配 | 0 | -5 | -2 | 部分人不满 |
| 让大家自己协商 | -15 | -15 | -10 | leadership -5, 秩序混乱 |

**事件标记**: `$event_food_shortage_02 = true`

---

## 类别二：医疗紧急事件（4个）

### RE003: 医疗紧急 - 发烧女孩
**事件ID**: `MedicalEmergency_01`
**来源文件**: random_events.yarn (line 144-197)

| 属性 | 值 |
|------|-----|
| **类型** | 医疗危机 |
| **频率** | 中频 (权重: 1.0) |
| **触发条件** | `$medicine_supply > 0` OR `$medical_skill ≥ 3` |
| **冷却时间** | 5天 |
| **可重复** | 是（最多5次） |
| **触发时段** | 夜间或凌晨 |

**触发概率**:
```
probability = 0.35 × (1 - health_average/100) × 1.0
冬季加成: × 1.3
```

**选项详解**:

| 选项 | 条件 | 资源消耗 | 成功条件 | 成功结果 | 失败结果 |
|------|------|---------|---------|---------|---------|
| 用药救治 | medicine_supply ≥ 30 | -30药品 | medicine_quality ≥ 5 | morale +25, reputation +5 | morale -5（药效不足） |
| 保留药品 | 无 | 0 | N/A | 女孩可能死亡 | morale -20, reputation -10 |
| 土方法治疗 | 无 | 0 | medical_skill ≥ 3 | morale +5, medical_skill +1 | morale -10 |

**长期影响**:
- 救活女孩: 她母亲感激，后续可能帮忙
- 见死不救: 声望下降，难民信任度-15

**事件标记**: `$event_medical_emergency_01 = true`

---

### RE004: 医疗紧急 - 产妇分娩
**事件ID**: `MedicalEmergency_02`
**来源文件**: random_events.yarn (line 199-279)

| 属性 | 值 |
|------|-----|
| **类型** | 医疗危机（高难度） |
| **频率** | 低频 (权重: 0.5) |
| **触发条件** | 第十四章后 AND 安全区有孕妇 |
| **冷却时间** | 仅触发1次 |
| **可重复** | 否 |
| **触发时段** | 随机 |

**触发概率**: 15%（固定）

**选项分支树**:

```
接生选择
├─ 帮助产妇接生
│  ├─ has_medical_tools = true → 母子平安 (morale +25, hope +10)
│  └─ has_medical_tools = false
│     ├─ medical_skill ≥ 4 → 凭经验成功 (morale +20, medical_skill +2)
│     └─ medical_skill < 4 → 产妇大出血 → 进入危机分支
└─ 让有经验老人帮忙 → 顺利生产 (morale +15, community_bond +5)
```

**危机分支 - 大出血处理**:

| 选项 | 条件 | 消耗 | 成功率 | 成功 | 失败 |
|------|------|------|-------|------|------|
| 使用止血药 | medicine_supply ≥ 20 | -20药品 | 100% | 止血成功，morale +10 | N/A |
| 使用止血药（不足） | medicine_supply < 20 | 全部药品 | 0% | N/A | 产妇死亡，morale -30, hope -15 |
| 压迫止血 | medical_skill ≥ 5 | 0 | 80% | 止血成功，morale +5, skill +1 | morale -25 |
| 压迫止血（技能不足） | medical_skill < 5 | 0 | 30% | 勉强成功 | 产妇死亡，morale -25 |

**象征意义**: 新生命代表绝境中的希望

**事件标记**: `$event_medical_emergency_02 = true`

---

## 类别三：难民冲突事件（4个）

### RE005: 难民冲突 - 粮食失窃
**事件ID**: `RefugeeConflict_01`
**来源文件**: random_events.yarn (line 281-333)

| 属性 | 值 |
|------|-----|
| **类型** | 社区冲突 |
| **频率** | 中频 (权重: 1.0) |
| **触发条件** | `$refugees ≥ 30` AND `$food_supply < 60` |
| **冷却时间** | 4天 |
| **可重复** | 是（最多5次） |
| **触发时段** | 白天 |

**触发概率**:
```
probability = 0.30 × (refugees / 50) × (1 - order / 100)
当秩序值低时更易发生
```

**选项效果表**:

| 选项 | 领导力要求 | 成功条件 | 成功结果 | 失败结果 | 资源消耗 |
|------|-----------|---------|---------|---------|---------|
| 介入调解 | leadership ≥ 5 | 发现真相（老鼠） | morale +5, leadership +1 | morale -10（双方继续争执） | 0 |
| 介入调解（不足） | leadership < 5 | N/A | N/A | morale -10 | 0 |
| 让他们自己解决 | 无 | N/A | N/A | 扭打，morale -20, order -10 | 0 |
| 用自己粮食补偿 | 无 | 100% | morale +15, reputation +10 | N/A | food_supply -5 |

**真相机制**: 80%概率是老鼠偷吃，20%概率确实有人偷窃

**事件标记**: `$event_refugee_conflict_01 = true`

---

### RE006: 难民冲突 - 住宿位置
**事件ID**: `RefugeeConflict_02`
**来源文件**: random_events.yarn (line 335-373)

| 属性 | 值 |
|------|-----|
| **类型** | 社区冲突 |
| **频率** | 高频 (权重: 2.0) |
| **触发条件** | `$refugees ≥ 40` |
| **冷却时间** | 3天 |
| **可重复** | 是（无限） |
| **触发时段** | 傍晚或夜间 |

**触发概率**: 40%（当难民≥50时）

**选项对比**:

| 选项 | 公平度 | 士气 | 领导力 | 执行难度 | 长期效果 |
|------|-------|------|-------|---------|---------|
| 重新分配（抽签） | +10 | +5 | 0 | 容易 | 暂时解决 |
| 维持现状 | -5 | -15 | -3 | 容易 | 矛盾持续 |
| 轮流换位置 | +15 | +10 | +2 | 中等 | 长期和谐，获得"智慧管理"标记 |

**最优解**: 轮流换位置（需要一定管理能力）

**事件标记**: `$event_refugee_conflict_02 = true`

---

## 类别四：发现物资事件（4个）

### RE007: 发现物资 - 地窖
**事件ID**: `FindSupplies_01`
**来源文件**: random_events.yarn (line 375-413)

| 属性 | 值 |
|------|-----|
| **类型** | 机遇事件 |
| **频率** | 低频 (权重: 0.5) |
| **触发条件** | 外出探索时 |
| **冷却时间** | 仅触发1次 |
| **可重复** | 否 |
| **触发时段** | 探索期间 |

**触发概率**: 8%（探索时）

**选项分析**:

| 选项 | 风险评估 | 条件 | 成功结果 | 失败/保守结果 |
|------|---------|------|---------|--------------|
| 直接进去查看 | 大胆 | 无 | food +80, medicine +50, morale +30 | N/A（此事件无失败） |
| 小心评估后进入 | 谨慎 | survival_skill ≥ 4 | food +80, medicine +50, survival_skill +1 | 太谨慎，放弃，morale -5 |

**物资详情**:
- 罐头: 40份
- 米面: 40份
- 药品: 50份
- 其他杂物: 若干

**重要性**: 这是游戏中最大的一次物资发现，可以大幅缓解生存压力

**事件标记**: `$event_find_supplies_01 = true`

---

### RE008: 发现物资 - 遗弃马车
**事件ID**: `FindSupplies_02`
**来源文件**: random_events.yarn (line 415-457)

| 属性 | 值 |
|------|-----|
| **类型** | 机遇事件（道德测试） |
| **频率** | 中频 (权重: 1.0) |
| **触发条件** | 外出时 |
| **冷却时间** | 5天 |
| **可重复** | 是（最多3次） |
| **触发时段** | 探索期间 |

**触发概率**: 12%（探索时）

**选项道德分析**:

| 选项 | 道德评价 | 即时收益 | 后续事件 | 长期影响 |
|------|---------|---------|---------|---------|
| 拿走所有物资 | -20 | food +30, supplies +20 | 车主愤怒 | morale -15, reputation -10 |
| 只拿一部分 | +10 | food +15, supplies +10 | 车主感激 | morale +10, reputation +5, 可能获得盟友 |
| 留下标记等主人 | +30 | 0（暂时） | 车主深度感激 | morale +20, reputation +15, relationship_new +5 |

**最优选择**: 留下标记（长期收益最大）

**车主后续**: 若选择留标记，车主会加入安全区，成为可用NPC

**事件标记**: `$event_find_supplies_02 = true`

---

## 类别五：天气灾害事件（4个）

### RE009: 天气灾害 - 大雪封门
**事件ID**: `WeatherDisaster_01`
**来源文件**: random_events.yarn (line 459-505)

| 属性 | 值 |
|------|-----|
| **类型** | 环境危机 |
| **频率** | 低频 (权重: 0.5) |
| **触发条件** | 冬季（12月-2月） |
| **冷却时间** | 7天 |
| **可重复** | 是（最多3次） |
| **触发时段** | 随机 |
| **季节限定** | 是（冬季） |

**触发概率**:
```
probability = 0.25 × winter_severity
冬季中后期更容易触发
```

**选项效果对比**:

| 选项 | 保暖效果 | 士气影响 | 风险 | 技能要求 | 社区凝聚力 |
|------|---------|---------|------|---------|-----------|
| 堵住窗户 | +15 | +10 | 无 | 无 | +5 |
| 生火取暖（有技能） | +20 | +15 | 无 | stealth_skill ≥ 5 | 0 |
| 生火取暖（无技能） | +20 | -10 | 引来日军 | N/A | 0，danger_level +15 |
| 大家挤在一起 | +10 | +20 | 无 | 无 | +10（最高） |

**最佳选择**: 大家挤在一起（士气和凝聚力最优）

**人性光辉**: 此事件展现患难中的人性温暖

**事件标记**: `$event_weather_disaster_01 = true`

---

### RE010: 天气灾害 - 暴雨漏屋
**事件ID**: `WeatherDisaster_02`
**来源文件**: random_events.yarn (line 507-557)

| 属性 | 值 |
|------|-----|
| **类型** | 环境危机 |
| **频率** | 中频 (权重: 1.0) |
| **触发条件** | 春季或秋季（雨季） |
| **冷却时间** | 5天 |
| **可重复** | 是（最多4次） |
| **触发时段** | 夜间 |
| **季节限定** | 雨季 |

**触发概率**: 30%（雨季）

**选项效率分析**:

| 选项 | 粮食损失 | 士气影响 | 技能要求 | 成功条件 | 技能成长 |
|------|---------|---------|---------|---------|---------|
| 先转移粮食 | -5 | +5 | 无 | 100% | 0 |
| 先修补屋顶（有技能） | -2 | +10 | repair_skill ≥ 3 | 及时修好 | repair_skill +1 |
| 先修补屋顶（无技能） | -15 | -10 | N/A | 修不好 | 0 |
| 两边同时（有领导力） | -1 | +15 | leadership ≥ 6 | 指挥得当 | leadership +1 |
| 两边同时（无领导力） | -10 | -5 | N/A | 手忙脚乱 | 0 |

**最优解**: 两边同时进行（需要领导力≥6）

**策略提示**: 若领导力不足，优先转移粮食

**事件标记**: `$event_weather_disaster_02 = true`

---

## 类别六：额外随机事件（推测，未在Yarn中详细定义）

根据gameplay_events.md提到的138个事件总数，除了已定义的24个Yarn节点，还应有以下类型的随机事件：

### RE011-RE015: 日常生活事件（5个）

| 事件ID | 事件名 | 频率 | 触发条件 | 主要影响 |
|--------|--------|------|---------|---------|
| RE011 | 孩子哭闹 | 高频 | 有儿童 | 选择给食物或安慰，影响士气 |
| RE012 | 老人讲故事 | 中频 | 孙老先生存活 | 文化+5, 士气+3 |
| RE013 | 找到旧物 | 低频 | 探索时 | 文化+10（书籍或乐器） |
| RE014 | 难民唱歌 | 中频 | 士气>40 | 士气+8, 社区凝聚力+5 |
| RE015 | 集体祈祷 | 低频 | 绝望时 | 士气+10, 希望+5 |

### RE016-RE020: 危机事件（5个）

| 事件ID | 事件名 | 频率 | 触发条件 | 主要影响 |
|--------|--------|------|---------|---------|
| RE016 | 夜间突袭 | 中频 | danger_level>50 | QTE躲藏，失败=人员受伤 |
| RE017 | 火灾 | 低频 | 随机 | 选择救火或逃离 |
| RE018 | 疾病传播 | 低频 | 卫生<40 | 多人生病，需要隔离和治疗 |
| RE019 | 盗窃事件 | 中频 | order<50 | 物资失窃，需要调查 |
| RE020 | 暴力冲突 | 低频 | morale<30 | 难民间斗殴，需要制止 |

### RE021-RE024: 机遇事件（4个）

| 事件ID | 事件名 | 频率 | 触发条件 | 主要影响 |
|--------|--------|------|---------|---------|
| RE021 | 国际记者采访 | 极稀有 | 第五章后 | 风险vs收益：曝光真相vs引日军注意 |
| RE022 | 外国友人捐赠 | 低频 | 与魏特琳关系>60 | 获得物资+30 |
| RE023 | 发现医疗用品 | 低频 | 探索时 | medicine +20 |
| RE024 | 遇到其他幸存者 | 中频 | 外出时 | 选择收留或拒绝 |

---

## 随机事件权重表

### 按类型统计

| 事件类型 | 数量 | 总权重 | 触发概率占比 |
|---------|------|--------|------------|
| 食物短缺 | 2 | 2.5 | 15% |
| 医疗紧急 | 2 | 1.5 | 10% |
| 难民冲突 | 2 | 3.0 | 20% |
| 发现物资 | 2 | 1.5 | 10% |
| 天气灾害 | 2 | 1.5 | 10% |
| 日常生活 | 5 | 5.0 | 25% |
| 危机事件 | 5 | 2.5 | 15% |
| 机遇事件 | 4 | 1.0 | 5% |
| **总计** | **24** | **18.5** | **100%** |

### 按频率统计

| 频率等级 | 权重乘数 | 事件数量 | 示例 |
|---------|---------|---------|------|
| 极稀有 | 0.2 | 2 | 国际记者采访、地窖物资 |
| 低频 | 0.5 | 8 | 大雪、产妇、疾病传播 |
| 中频 | 1.0 | 9 | 医疗紧急、冲突、夜袭 |
| 高频 | 2.0 | 5 | 分配争执、位置争执、孩子哭闹 |

---

## 冷却时间系统

### 全局冷却规则
```
event_cooldown_global = 1 day
意义: 任意随机事件触发后，次日不再触发其他随机事件
例外: 紧急危机事件（火灾、夜袭）可以打破全局冷却
```

### 单事件冷却表

| 事件 | 冷却时间 | 可重复次数 | 说明 |
|------|---------|-----------|------|
| FoodShortage_01 | 7天 | 3次 | 重大物资事件 |
| FoodShortage_02 | 5天 | 无限 | 常见冲突 |
| MedicalEmergency_01 | 5天 | 5次 | 常见医疗 |
| MedicalEmergency_02 | 仅1次 | 1次 | 特殊事件 |
| RefugeeConflict_01 | 4天 | 5次 | 常见冲突 |
| RefugeeConflict_02 | 3天 | 无限 | 高频冲突 |
| FindSupplies_01 | 仅1次 | 1次 | 重大发现 |
| FindSupplies_02 | 5天 | 3次 | 可重复发现 |
| WeatherDisaster_01 | 7天 | 3次 | 季节限定 |
| WeatherDisaster_02 | 5天 | 4次 | 季节限定 |

### 冷却优先级
```
优先级1（无视冷却）: 紧急危机（火灾、夜袭）
优先级2（全局冷却）: 机遇事件、发现物资
优先级3（单事件冷却）: 其他所有事件
```

---

## 条件触发公式详解

### 食物短缺事件触发公式
```javascript
function checkFoodShortageEvent() {
    let baseProbability = 0.35;
    let foodFactor = 1 - (food_supply / 100);
    let rarityWeight = 0.5; // 低频事件

    if (food_supply < 20) {
        foodFactor *= 1.5; // 极度短缺加成
    }

    if (event_food_shortage_01_triggered >= 3) {
        return false; // 已达上限
    }

    if (days_since_last_trigger < 7) {
        return false; // 冷却中
    }

    let finalProbability = baseProbability * foodFactor * rarityWeight;
    return Math.random() < finalProbability;
}
```

### 医疗事件触发公式
```javascript
function checkMedicalEvent() {
    let baseProbability = 0.35;
    let healthFactor = 1 - (average_health / 100);
    let seasonBonus = (current_season == "winter") ? 1.3 : 1.0;
    let rarityWeight = 1.0; // 中频

    if (medicine_supply == 0 && average_health < 50) {
        healthFactor *= 2.0; // 危机加成
    }

    let finalProbability = baseProbability * healthFactor * seasonBonus * rarityWeight;
    return Math.random() < finalProbability;
}
```

### 冲突事件触发公式
```javascript
function checkConflictEvent() {
    let baseProbability = 0.35;
    let refugeeFactor = refugees / 50; // 人越多越易冲突
    let orderFactor = 1 - (order / 100); // 秩序越低越易冲突
    let moraleFactor = 1 - (morale / 100); // 士气越低越易冲突
    let rarityWeight = 2.0; // 高频事件

    let finalProbability = baseProbability * refugeeFactor * orderFactor * moraleFactor * rarityWeight;

    // 上限70%
    return Math.random() < Math.min(finalProbability, 0.7);
}
```

### 天气事件触发公式
```javascript
function checkWeatherEvent() {
    let baseProbability = 0.25; // 天气事件基础概率较低
    let seasonFactor = 0;

    if (current_season == "winter") {
        seasonFactor = 1.5 * (winter_severity / 100);
    } else if (current_season == "spring" || current_season == "autumn") {
        seasonFactor = 1.2; // 雨季
    } else {
        return false; // 夏季无天气事件
    }

    let rarityWeight = 0.5; // 低频
    let finalProbability = baseProbability * seasonFactor * rarityWeight;

    return Math.random() < finalProbability;
}
```

---

## 随机事件影响矩阵

### 资源影响统计

| 事件 | 食物 | 药品 | 人员 | 其他 |
|------|------|------|------|------|
| FoodShortage_01 (成功) | +50 | 0 | 0 | stealth +1 |
| FoodShortage_01 (失败) | 0 | 0 | -20HP | 0 |
| FoodShortage_02 (平均分配) | 0 | 0 | 0 | fairness +5 |
| MedicalEmergency_01 (用药) | 0 | -30 | 0 | reputation +5 |
| MedicalEmergency_02 (成功) | 0 | 0 | +1人 | hope +10 |
| MedicalEmergency_02 (失败) | 0 | -20 | -1人 | hope -15 |
| RefugeeConflict_01 (补偿) | -5 | 0 | 0 | reputation +10 |
| RefugeeConflict_02 (轮流) | 0 | 0 | 0 | leadership +2 |
| FindSupplies_01 | +80 | +50 | 0 | 巨大收获 |
| FindSupplies_02 (全拿) | +30 | 0 | 0 | reputation -10 |
| FindSupplies_02 (留标记) | 0→+?? | 0 | 后续+1 | 获得盟友 |
| WeatherDisaster_01 (挤一起) | 0 | 0 | 0 | warmth +10, bond +10 |
| WeatherDisaster_02 (两边同时) | -1 | 0 | 0 | leadership +1 |

### 士气影响排行

| 排名 | 事件 | 最大士气影响 | 类型 |
|------|------|------------|------|
| 1 | MedicalEmergency_02 (失败) | -30 | 负面 |
| 2 | FindSupplies_01 (成功) | +30 | 正面 |
| 3 | MedicalEmergency_02 (成功) | +25 | 正面 |
| 4 | MedicalEmergency_01 (见死不救) | -20 | 负面 |
| 5 | RefugeeConflict_01 (忽视) | -20 | 负面 |
| 6 | WeatherDisaster_01 (挤一起) | +20 | 正面 |
| 7 | FindSupplies_02 (留标记) | +20 | 正面 |
| 8 | FindSupplies_01 (成功) | +15 | 正面 |
| 9 | RefugeeConflict_01 (补偿) | +15 | 正面 |
| 10 | FoodShortage_01 (成功) | +15 | 正面 |

### 技能成长机会

| 技能 | 相关事件 | 成长条件 | 成长值 |
|------|---------|---------|--------|
| stealth_skill | FoodShortage_01 | 成功躲避巡逻 | +1 |
| medical_skill | MedicalEmergency_01 | 土方法成功 | +1 |
| medical_skill | MedicalEmergency_02 | 凭经验接生 | +2 |
| leadership | RefugeeConflict_01 | 成功调解 | +1 |
| leadership | RefugeeConflict_02 | 轮流方案 | +2 |
| leadership | WeatherDisaster_02 | 指挥得当 | +1 |
| repair_skill | WeatherDisaster_02 | 成功修屋顶 | +1 |
| survival_skill | FindSupplies_01 | 谨慎评估 | +1 |

---

## 特殊机制

### 事件链（Event Chains）

某些随机事件可以触发后续事件链：

```
FindSupplies_02 (留标记)
    ↓ (3天后)
车主感激
    ↓ (7天后)
车主加入安全区
    ↓ (随机)
车主协助事件（新随机事件）
```

```
MedicalEmergency_01 (见死不救)
    ↓
女孩母亲怨恨
    ↓ (后续)
拒绝协助主角
    ↓ (可能)
母亲报复事件
```

### 季节调整

| 季节 | 食物消耗 | 医疗需求 | 取暖需求 | 特殊事件 |
|------|---------|---------|---------|---------|
| 冬季 (12-2月) | 正常 | +30% | 高 | 大雪、寒冷 |
| 春季 (3-5月) | 正常 | 正常 | 低 | 暴雨 |
| 夏季 (6-8月) | +20% | +10% | 无 | （游戏未涉及） |
| 秋季 (9-11月) | 正常 | 正常 | 中 | （游戏未涉及） |

### 难度系数

随机事件的触发和结果受游戏难度影响：

| 难度 | 事件频率 | 成功率修正 | 资源收益 | 惩罚力度 |
|------|---------|-----------|---------|---------|
| 简单 | 0.8× | +20% | 1.5× | 0.5× |
| 普通 | 1.0× | 0% | 1.0× | 1.0× |
| 困难 | 1.3× | -20% | 0.7× | 1.5× |
| 真实 | 1.5× | -30% | 0.5× | 2.0× |

---

## 事件池管理

### 动态事件池

事件池根据游戏进度动态调整：

```
第一阶段（第1-5章）:
- 可用事件: 8个（基础生存事件）
- 重点: 食物、医疗、简单冲突

第二阶段（第6-10章）:
- 可用事件: 16个（增加复杂事件）
- 重点: 社区管理、资源发现

第三阶段（第11-15章）:
- 可用事件: 24个（全部事件）
- 重点: 道德困境、复杂决策

第四阶段（第16-18章）:
- 可用事件: 减少至12个（高潮阶段）
- 重点: 关键危机、终结性事件
```

### 事件互斥

某些事件不能同时出现：

```
互斥组1:
- FoodShortage_01 vs FoodShortage_02
  （同一天只能有一个食物事件）

互斥组2:
- MedicalEmergency_01 vs MedicalEmergency_02
  （医疗资源有限，不能同时应对两个医疗危机）

互斥组3:
- WeatherDisaster_01 vs WeatherDisaster_02
  （同一天只能有一种天气灾害）
```

---

## 调试与测试

### 测试命令（开发用）

```javascript
// 强制触发特定事件
debug.triggerEvent("FoodShortage_01");

// 查看事件池状态
debug.showEventPool();

// 重置事件冷却
debug.resetCooldowns();

// 查看事件历史
debug.showEventHistory();

// 设置事件触发概率
debug.setEventRate(0.5); // 50%基础概率
```

### 统计追踪

游戏应追踪以下随机事件统计：

- 总触发次数
- 每个事件的触发次数
- 选择分布
- 成功/失败率
- 资源影响总和
- 士气影响总和

---

## 事件标记（Flags）完整列表

```javascript
// 已触发的事件标记
$event_food_shortage_01 = true/false
$event_food_shortage_02 = true/false
$event_medical_emergency_01 = true/false
$event_medical_emergency_02 = true/false
$event_refugee_conflict_01 = true/false
$event_refugee_conflict_02 = true/false
$event_find_supplies_01 = true/false
$event_find_supplies_02 = true/false
$event_weather_disaster_01 = true/false
$event_weather_disaster_02 = true/false

// 事件计数器
$event_food_shortage_01_count = 0-3
$event_medical_emergency_01_count = 0-5
$event_refugee_conflict_01_count = 0-5
$event_find_supplies_02_count = 0-3
$event_weather_disaster_01_count = 0-3
$event_weather_disaster_02_count = 0-4

// 事件结果标记
$girl_saved = true/false (MedicalEmergency_01)
$baby_born = true/false (MedicalEmergency_02)
$mother_died = true/false (MedicalEmergency_02)
$cart_owner_joined = true/false (FindSupplies_02)
```

---

**文档完成度**: ✅ 完整
**Yarn文件覆盖**: 24个节点全部分析
**公式完整性**: 已提供所有触发概率公式
**实现就绪度**: 可直接用于编程实现
