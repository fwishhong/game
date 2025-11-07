# 🎮 下一步操作指南

## ✅ 已完成的工作

我已经为你创建了：
- ✅ Unity项目文件夹结构
- ✅ 核心C#脚本（GameManager, PlayerController等）
- ✅ 对话系统脚本（DialogueManager, InteractableNPC）
- ✅ 测试用Yarn对话文件

**文件位置**：`/home/user/game/Assets/`

---

## 📋 现在你需要做的事（一步一步来）

### 第1步：在Unity中打开项目 ⭐

1. 打开 **Unity Hub**
2. 点击右上角 **"Add"**（添加）
3. **重要**：选择这个文件夹：`/home/user/game`
4. Unity会识别这是一个项目，点击 **"Open"**

**预期结果**：Unity会打开，可能需要5-10分钟导入资源。

---

### 第2步：安装Yarn Spinner ⭐⭐⭐

这是最重要的一步！因为你有33个Yarn对话文件。

#### 方法A：通过Git URL安装（推荐）

1. 在Unity中，打开菜单：**Window → Package Manager**
2. 左上角点击 **"+"** → **"Add package from git URL..."**
3. 输入：`https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.4.2`
4. 点击 **"Add"**
5. 等待安装（1-2分钟）

#### 方法B：如果方法A失败（网络问题）

我可以帮你下载Yarn Spinner的unitypackage文件，然后你导入。请告诉我如果需要。

**检查安装成功**：
- Package Manager中能看到 "Yarn Spinner"
- 没有红色错误

---

### 第3步：安装TextMesh Pro（中文字体支持）

1. 还在 **Package Manager** 中
2. 左上角下拉菜单选择 **"Unity Registry"**
3. 搜索：**"TextMesh Pro"**
4. 点击右下角 **"Install"**
5. 安装完成后会弹窗，点击 **"Import TMP Essentials"**

---

### 第4步：创建第一个场景

1. 在Project窗口，右键 **Assets/Scenes** 文件夹
2. 选择 **Create → Scene**
3. 命名为：`TestScene`
4. 双击打开这个场景

---

### 第5步：添加GameManager到场景

1. 在Hierarchy窗口，右键 → **Create Empty**
2. 命名为：`GameManager`
3. 选中这个空物体
4. 在Inspector窗口，点击 **Add Component**
5. 搜索并添加：`GameManager` 脚本

**你应该看到**：
- Inspector中显示GameManager脚本的所有参数
- 可以看到"游戏状态"、"时间线设置"等选项

---

### 第6步：创建玩家

1. 在Hierarchy窗口，右键 → **2D Object → Sprite → Square**
2. 命名为：`Player`
3. 选中Player
4. 在Inspector窗口：
   - 修改 Transform → Position 为 `(0, 0, 0)`
   - 修改 Transform → Scale 为 `(1, 1, 1)`
   - 点击 **Add Component** → 搜索并添加：`PlayerController`
   - 点击 **Add Component** → 搜索并添加：`Rigidbody 2D`（如果还没有）

5. 给Player添加Tag：
   - Inspector顶部，Tag下拉菜单 → **Add Tag...**
   - 点击 **"+"** 添加新Tag：`Player`
   - 回到Player物体，设置Tag为 `Player`

**你应该看到**：
- Scene视图中有一个白色正方形（临时的玩家角色）
- Inspector中有PlayerController脚本

---

### 第7步：添加摄像机跟随（可选但推荐）

1. 选中Main Camera
2. 在Inspector中，点击 **Add Component**
3. 添加这个简单的脚本（我会在下面创建）：`CameraFollow`

---

### 第8步：测试玩家移动

1. 点击Unity顶部的 **Play按钮**（▶️）
2. 使用 **WASD** 或 **方向键** 移动
3. 你应该能看到白色方块（玩家）在移动

**如果没有移动**：
- 检查Console窗口是否有错误
- 确保Game视图是激活的（点击一下Game标签）

---

### 第9步：设置对话系统（下一阶段）

完成上述步骤后，**告诉我**你到哪一步了，我会：
- 帮你配置Yarn Spinner
- 创建对话UI
- 添加测试NPC
- 让你能玩到第一段对话！

---

## 🆘 常见问题

### Q: Console中有很多黄色警告
**A**: 黄色警告可以暂时忽略，红色错误才需要解决。

### Q: 找不到某个脚本
**A**: 确保Assets/Scripts文件夹中有对应的.cs文件。

### Q: Yarn Spinner安装失败
**A**: 告诉我错误信息，我会帮你解决。

### Q: 我是完全新手，不知道在哪里点
**A**: 没关系！给我发截图，我会告诉你具体点击哪里。

---

## 📸 给我反馈

完成到第几步了？
- 遇到任何问题？发截图给我看
- 成功了？告诉我！我们继续下一步

**建议**：先完成1-6步，能让玩家移动就算成功！然后我们再做对话系统。

---

## 🎯 目标

**第一个里程碑**：
- 玩家能在场景中移动 ✅
- 没有红色错误 ✅

**第二个里程碑**（完成上面后）：
- 能和NPC对话
- 显示Yarn对话文本

**第三个里程碑**：
- 完整的第一个场景（纪念馆）

一步一步来，别着急！我随时在这里帮你。💪
