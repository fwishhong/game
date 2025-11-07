# 🎮 对话系统测试指南

## ✅ 你已经完成的准备工作

1. ✅ 创建了 TestDialogue.yarn 对话文件
2. ✅ 创建了 TestNPC 游戏对象
3. ✅ 创建了对话UI（Canvas、DialoguePanel、TextMeshPro）
4. ✅ 设置了 DialogueSystem（DialogueRunner、Line View、Variable Storage）
5. ✅ InteractableNPC 脚本已更新（会自动重新编译）

---

## 📝 现在开始测试！

### 第1步：等待脚本重新编译

1. **回到Unity编辑器**
2. **等待右下角进度条完成**（"Compiling..."）
   - 通常需要5-10秒
3. **检查Console窗口**（Window → General → Console）
   - 如果没有红色错误 → ✅ 继续
   - 如果有红色错误 → ❌ 把错误信息告诉我

---

### 第2步：（可选）配置TestNPC

这一步是可选的，因为脚本会自动查找DialoguePanel。但手动设置更可靠。

1. **在Hierarchy中选中 TestNPC**
2. **在Inspector中找到 "Interactable NPC" 组件**
3. **找到 "UI设置" 部分**
4. **拖动 Canvas → DialoguePanel 到 "Dialogue Panel" 字段**

你应该看到：
```
UI设置
  Dialogue Panel: DialoguePanel (GameObject)
```

---

### 第3步：确保对话面板初始状态是隐藏的

1. **在Hierarchy中选中 Canvas → DialoguePanel**
2. **在Inspector顶部**，确保 **左边的复选框是未勾选的**（面板应该默认隐藏）
3. **如果是勾选的，点击取消勾选**

---

### 第4步：开始测试！🎉

1. **点击Unity顶部的Play按钮（▶️）**
2. **使用WASD移动玩家到TestNPC附近**
   - 玩家：白色方块
   - NPC：绿色/黄色方块
3. **当玩家靠近NPC时，按E键**

---

## 🎯 预期结果

### 应该发生的事情：

1. ✅ **对话面板出现**（底部深灰色框）
2. ✅ **显示对话文本**："这是第一个测试对话！"
3. ✅ **显示选项按钮**：
   - "继续"
   - "测试选项"
4. ✅ **玩家无法移动**（对话进行中）
5. ✅ **点击选项后对话继续**
6. ✅ **对话结束后，面板消失，玩家可以继续移动**

### Console窗口应该显示的日志：

```
玩家靠近了 TestNPC，按E交互
与 TestNPC 开始对话
开始对话: Start
```

---

## ❌ 常见问题排查

### 问题1：按E键没反应

**可能原因**：
- TestNPC的Collider2D没有勾选"Is Trigger"
- TestNPC的Layer不是"Interactable"
- Player的Tag不是"Player"

**解决方法**：
1. 选中TestNPC
2. 检查Box Collider 2D组件
3. 确保"Is Trigger"已勾选 ✅

---

### 问题2：对话面板不出现

**可能原因**：
- DialoguePanel没有正确连接到TestNPC
- Line View配置不正确

**解决方法**：
1. 选中TestNPC，检查Inspector中的"Dialogue Panel"字段是否已设置
2. 选中DialogueSystem，检查DialogueRunner的"Line View"是否已设置为"Line View (Line View)"

---

### 问题3：对话文本不显示

**可能原因**：
- Line View的Text组件没有连接
- Yarn文件路径不正确

**解决方法**：
1. 选中Canvas → DialoguePanel → Line View
2. 检查Inspector中的"Text"字段是否连接到TextMeshPro组件
3. 选中DialogueSystem → DialogueRunner
4. 检查"Source Text"列表中是否包含TestDialogue

---

### 问题4：有错误信息

**解决方法**：
1. 打开Console窗口
2. 双击红色错误查看详情
3. 把错误信息复制给我
4. 我会帮你解决！

---

## 📸 测试完成后告诉我

测试完成后，请告诉我：

1. **成功了**？
   - 描述一下你看到了什么
   - 我们继续下一步：添加更多NPC、改进UI样式

2. **失败了**？
   - 发生了什么？（例如：对话面板没出现、有错误等）
   - Console窗口有什么错误信息？
   - 我会立即帮你解决

3. **部分成功**？
   - 哪些部分工作了？
   - 哪些部分有问题？

---

## 🎉 如果成功了，下一步是什么？

如果对话系统工作正常，我们可以：

**A. 改进对话UI样式**
- 添加打字机效果
- 添加角色头像
- 美化对话框

**B. 添加更多NPC**
- 创建多个NPC
- 每个NPC有不同的对话
- 测试切换对话

**C. 集成你的33个Yarn文件**
- 把所有对话文件导入Unity
- 创建对应的NPC
- 开始构建完整场景

**D. 添加其他系统**
- 物品系统
- 日记系统
- 场景切换

你想先做哪个？告诉我测试结果，然后我们继续！💪
