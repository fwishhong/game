# Yarn Spinner 免费安装完整指南

## ✅ 重要说明：Yarn Spinner 是 100% 免费的！

**官方信息**：
- 开源项目：https://github.com/YarnSpinnerTool/YarnSpinner-Unity
- 开源协议：MIT License（完全免费，可商用）
- 作者：Yarn Spinner团队（由Secret Lab维护）
- 费用：**0元，永久免费**

---

## 🎯 推荐安装方法（最简单）

### 方法1：通过Package Manager安装（推荐）⭐⭐⭐

**步骤**：

1. 打开Unity编辑器
2. 点击菜单：**Window → Package Manager**
3. 左上角点击 **"+"** 按钮
4. 选择 **"Add package from git URL..."**
5. 复制粘贴这个URL：
   ```
   https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.4.2
   ```
6. 点击 **"Add"** 按钮
7. 等待1-2分钟，安装完成！

**检查安装成功**：
- Package Manager中能看到 "Yarn Spinner"
- 版本显示：2.4.2
- 状态显示：Installed

---

## 🔧 如果上述方法失败

### 可能原因1：网络问题

**症状**：提示无法连接到GitHub

**解决方案A - 使用OpenUPM**：
1. 打开Package Manager
2. 点击 "+" → "Add package from git URL..."
3. 输入：
   ```
   com.yarnspinner.unity
   ```
4. 点击Add

**解决方案B - 手动添加到manifest.json**：
1. 关闭Unity
2. 找到项目文件夹下的 `Packages/manifest.json`
3. 在 "dependencies" 里添加：
   ```json
   {
     "dependencies": {
       "dev.yarnspinner.unity": "https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.4.2",
       ...其他依赖...
     }
   }
   ```
4. 保存文件
5. 重新打开Unity

### 可能原因2：Unity版本太旧

**要求**：Unity 2021.3 或更新版本

**你的版本**：Unity 2022.3.6 ✅ 符合要求

---

## 🆘 如果真的安装不了

### 备选方案1：我帮你手动下载

告诉我你遇到的具体错误信息，我会：
1. 帮你诊断问题
2. 提供其他下载渠道
3. 或者手动给你打包一个

### 备选方案2：使用其他对话系统

**不推荐**，因为：
- ❌ 你的33个Yarn文件需要重写
- ❌ 失去专业的对话编辑器
- ❌ 开发时间会增加很多

但如果实在不行，我可以：
- 用Unity自带方式重写对话系统
- 提供简单的文本对话方案
- 或使用其他免费对话插件

---

## ❓ 你可能看到的"付费"内容

### Asset Store上的这些是付费的（但我们不需要）：

1. **"Dialogue System for Unity"** - 另一个对话系统（$75）
   - 这**不是**Yarn Spinner
   - 我们不需要

2. **"Yarn Spinner UI Pack"** - UI模板包（可能付费）
   - 这只是UI模板
   - Yarn Spinner本身是免费的
   - 我们可以自己做UI（我会帮你）

3. **其他对话插件** - 各种付费插件
   - 都不是Yarn Spinner
   - 我们不需要

### 我们只需要：

✅ **Yarn Spinner for Unity** - GitHub开源，100%免费
- 通过Git URL安装
- 或通过Package Manager安装
- 永久免费，可商用

---

## 📸 如果你看到"付费"提示

请给我发个截图，让我看看你在哪里看到的。
可能是：
1. 点错了别的插件
2. Asset Store的广告
3. Unity的某个提示（可以忽略）

**99%的可能性**：你看到的不是Yarn Spinner本身。

---

## 💡 现在怎么办？

### 请告诉我：

1. **你在哪里看到"收费"的？**
   - Unity编辑器里？
   - Asset Store网页？
   - 其他地方？

2. **具体显示什么内容？**
   - 截图最好
   - 或者告诉我文字内容

3. **你试过Git URL方法了吗？**
   - 如果没试，现在就试试（步骤在上面）
   - 如果试了失败，告诉我错误信息

---

## ✅ 我的保证

**Yarn Spinner是免费的！**
- 官方开源项目
- MIT协议
- 无需付费
- 无需注册
- 无需订阅

如果安装过程中遇到任何问题，我会帮你解决！

---

**现在就试试Git URL方法吧！** 5分钟搞定！

有任何问题随时告诉我！
