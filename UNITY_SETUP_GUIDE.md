# Unity项目搭建指南

## 🎮 欢迎来到《秦淮旧梦》Unity开发！

我已经为你准备好了所有Unity项目需要的文件和代码。请按照以下步骤操作：

---

## 📂 方案A：在当前目录直接打开Unity（推荐）

### 步骤1：用Unity打开这个项目

1. 打开 **Unity Hub**
2. 点击右上角 **"Add"**（添加）按钮
3. 导航到这个文件夹：`/home/user/game`
4. 选择这个文件夹，点击 **"Open"**（打开）

Unity会识别这是一个Unity项目，并自动生成必要的配置文件。

### 步骤2：等待Unity加载
- 第一次打开会比较慢（5-10分钟）
- Unity会自动导入所有资源
- 不要关闭Unity，等它完全加载完成

---

## 📂 方案B：复制文件到你的Unity项目

如果你已经在 `/Users/hongliang/dream` 创建了Unity项目：

### 步骤1：复制Assets文件夹
```bash
# 在终端执行（或手动复制）
cp -r /home/user/game/Assets/* /Users/hongliang/dream/Assets/
```

### 步骤2：在Unity中刷新
- 返回Unity编辑器
- 按 **Cmd+R**（Mac）刷新资源
- 或者在Project窗口右键 → Refresh

---

## 📦 必须安装的Package

### 安装Yarn Spinner（对话系统）

1. 在Unity中，打开 **Window → Package Manager**
2. 点击左上角 **"+"** → **"Add package from git URL..."**
3. 输入：`https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.4.2`
4. 点击 **"Add"**
5. 等待安装完成（1-2分钟）

### 安装TextMesh Pro（中文字体支持）

1. 仍在Package Manager中
2. 左上角选择 **"Unity Registry"**
3. 搜索 **"TextMesh Pro"**
4. 点击 **"Install"**
5. 安装完成后会弹窗，点击 **"Import TMP Essentials"**

---

## 📁 项目结构说明

```
Assets/
├── Scenes/              # 场景文件（稍后创建）
├── Scripts/             # C#脚本（已准备好）
│   ├── Core/           # 核心系统
│   ├── Dialogue/       # 对话系统
│   ├── Items/          # 物品系统
│   ├── Diary/          # 日记系统
│   ├── UI/             # UI脚本
│   └── Managers/       # 游戏管理器
├── Prefabs/            # 预制体
├── Dialogues/          # Yarn对话文件（已准备好）
├── Sprites/            # 精灵图片（待添加）
│   ├── Characters/    # 角色立绘
│   ├── Scenes/        # 场景背景
│   ├── UI/            # UI素材
│   └── Items/         # 物品图标
├── Audio/              # 音频文件（待添加）
│   ├── Music/         # 音乐
│   ├── SFX/           # 音效
│   └── Voice/         # 语音
├── Data/               # 数据文件（ScriptableObjects）
├── Materials/          # 材质
└── Fonts/              # 字体文件
```

---

## ✅ 安装完成检查清单

完成以下步骤后，在这里打勾：

- [ ] Unity成功打开项目
- [ ] Assets文件夹中能看到所有子文件夹
- [ ] Yarn Spinner已安装（Package Manager中能看到）
- [ ] TextMesh Pro已安装
- [ ] Console窗口没有红色错误（黄色警告可以忽略）

---

## 🆘 如果遇到问题

### 问题1：Unity无法打开项目
**解决**：确保Unity版本是 2022.3.x LTS

### 问题2：Yarn Spinner安装失败
**解决**：
1. 检查网络连接
2. 或者手动下载：https://github.com/YarnSpinnerTool/YarnSpinner-Unity/releases
3. 解压到 `Assets/YarnSpinner/` 文件夹

### 问题3：脚本报错
**解决**：
1. 先确保Yarn Spinner已安装
2. 关闭Unity重新打开
3. 清理Library文件夹（关闭Unity后删除，重新打开会自动生成）

---

## 🎯 下一步

完成上述步骤后，告诉我，我会继续创建：
1. 第一个场景（2024纪念馆）
2. 玩家控制器
3. 对话系统测试场景
4. 可玩的Demo

**准备好了吗？让我们开始吧！** 🚀
