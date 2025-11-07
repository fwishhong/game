# 🔧 修复 Yarn Spinner 设置问题

## 问题症状
1. ❌ 找不到 GameDialogues.yarnproject 文件
2. ❌ 找不到 "In Memory Variable Storage" 组件

## 根本原因
Yarn Spinner 可能没有正确安装，或者 Unity 没有识别资源文件。

---

## 解决方案A：在Unity中手动创建（推荐）

这是最简单、最可靠的方法。

### 第1步：创建 Yarn Project 文件

1. **在Unity中，在Project窗口导航到 Assets/Dialogues 文件夹**
2. **在Dialogues文件夹中右键 → Create → Yarn Spinner → Yarn Project**
3. **命名为 "GameDialogues"**
4. **选中新创建的 GameDialogues**
5. **在Inspector中设置**：
   - Default Language: `zh-Hans`（简体中文）

### 第2步：创建测试对话文件

1. **在Dialogues文件夹中右键 → Create → Yarn Spinner → Yarn Script**
2. **命名为 "TestDialogue"**
3. **双击打开，粘贴以下内容**：

```yarn
title: Start
---
这是第一个测试对话！

欢迎来到《秦淮旧梦》。

-> 继续
    很好，对话系统正常工作！
    你可以按任意键关闭对话。
-> 测试选项
    你选择了第二个选项。
    选项系统也正常工作！
===
```

4. **保存文件（Ctrl+S）**

### 第3步：将Yarn Script添加到Yarn Project

1. **选中 GameDialogues**
2. **在Inspector中找到 "Source Scripts" 列表**
3. **点击 + 号**
4. **拖动 TestDialogue 到新添加的槽位**

---

## 解决方案B：刷新Unity资源

如果你想使用已存在的文件，试试刷新：

1. **在Unity菜单栏：Assets → Refresh**（或按 Ctrl+R）
2. **等待Unity重新导入资源**
3. **检查 Assets/Dialogues 文件夹是否出现 GameDialogues**

---

## 解决方案C：重新安装 Yarn Spinner

如果上面的方法都不行，可能需要重新安装 Yarn Spinner。

### 方法1：通过 Package Manager（Git URL）

1. **Window → Package Manager**
2. **点击左上角 + 号 → Add package from git URL**
3. **输入**：`https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.4.2`
4. **点击 Add**
5. **等待安装完成（可能需要几分钟）**

### 方法2：检查是否已安装

1. **Window → Package Manager**
2. **左上角下拉菜单选择 "In Project"**
3. **查看列表中是否有 "Yarn Spinner"**
4. **如果有，检查版本是否为 2.4.x**

---

## 关于 "In Memory Variable Storage" 组件

### 如果Yarn Spinner正确安装了，这个组件应该这样添加：

1. **选中Hierarchy中的 DialogueSystem 对象**
2. **在Inspector底部点击 "Add Component"**
3. **搜索框输入：`memory`**（只输入这几个字母）
4. **应该看到：`In Memory Variable Storage`**
5. **点击它添加**

### 如果还是找不到，试试完整路径：

搜索：`Yarn.Unity.InMemoryVariableStorage`

### 如果还是不行：

说明Yarn Spinner没有正确安装。请按照"解决方案C"重新安装。

---

## ✅ 验证安装成功

安装成功后，你应该能：

1. ✅ 在 Create 菜单中看到 "Yarn Spinner" 选项
2. ✅ 能创建 Yarn Project 和 Yarn Script 文件
3. ✅ 能在 Add Component 中搜索到 "In Memory Variable Storage"
4. ✅ 能在 Add Component 中搜索到 "Dialogue Runner"

---

## 🆘 如果还是不行

告诉我以下信息：

1. **Unity版本**：你的Unity完整版本号（例如：2022.3.6f1）
2. **Package Manager状态**：
   - 打开 Window → Package Manager
   - 左上角选择 "In Project"
   - 截图或列出所有已安装的包
3. **Console错误**：
   - 打开 Console 窗口
   - 把所有红色错误信息复制给我
4. **Create菜单**：
   - 右键Project窗口 → Create
   - 你能看到 "Yarn Spinner" 选项吗？

---

## 🎯 推荐路径（最快）

我强烈建议你使用**解决方案A**：

1. 在Unity中手动创建Yarn Project（Create → Yarn Spinner → Yarn Project）
2. 在Unity中手动创建Yarn Script（Create → Yarn Spinner → Yarn Script）
3. 这样Unity会自动处理所有元数据和导入设置

这比试图让Unity识别外部创建的文件要可靠得多！

---

按照上面的步骤操作，然后告诉我进展如何！💪
