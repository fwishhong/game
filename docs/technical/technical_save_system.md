# 存档系统设计文档

**文档版本**: v1.0
**最后更新**: 2025-11-05

---

## 一、存档需求

### 核心需求

1. **多存档槽**: 10个手动存档 + 10个自动存档
2. **快速存取**: 存档<1秒，读档<2秒
3. **防损坏**: 多重备份机制
4. **跨平台**: PC/Console统一格式
5. **版本管理**: 支持游戏更新后的存档兼容

---

## 二、存档结构

### 存档文件组织

```
SaveData/
├─ Profiles/
│  ├─ profile_1/
│  │  ├─ save_1.json        # 手动存档槽1
│  │  ├─ save_2.json
│  │  ├─ ...
│  │  ├─ autosave_1.json    # 自动存档
│  │  ├─ autosave_2.json
│  │  └─ screenshots/
│  │     ├─ save_1.png
│  │     └─ ...
│  └─ profile_2/
│     └─ ...
├─ settings.json            # 全局设置
└─ backup/                  # 备份文件夹
   └─ ...
```

---

## 三、存档数据类

### SaveData结构

```csharp
[System.Serializable]
public class SaveData
{
    // 元数据
    public string version = "1.0";          // 存档版本
    public int slotIndex;                   // 槽位索引
    public long timestamp;                  // Unix时间戳
    public string screenshotPath;           // 截图路径

    // 游戏状态（完整的GameState序列化）
    public string gameStateJson;            // 游戏状态JSON

    // 显示信息（用于存档列表显示）
    public DisplayInfo display;
}

[System.Serializable]
public class DisplayInfo
{
    public string chapterName;      // "第五章 · 安全区的生活"
    public string gameDate;         // "民国二十六年腊月初十"
    public int playTimeSeconds;     // 游戏时长
    public string location;         // 当前位置
}
```

---

## 四、保存流程

### 手动保存

```csharp
public class SaveSystem : MonoBehaviour
{
    public void SaveGame(int slotIndex)
    {
        StartCoroutine(SaveGameCoroutine(slotIndex));
    }

    IEnumerator SaveGameCoroutine(int slotIndex)
    {
        // 1. 截图
        byte[] screenshot = CaptureScreenshot();

        // 2. 收集游戏状态
        GameState gameState = GameManager.Instance.GetCurrentState();

        // 3. 创建SaveData
        SaveData saveData = new SaveData
        {
            slotIndex = slotIndex,
            timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            gameStateJson = JsonUtility.ToJson(gameState, true),
            display = CreateDisplayInfo(gameState)
        };

        // 4. 保存截图
        string screenshotPath = SaveScreenshot(screenshot, slotIndex);
        saveData.screenshotPath = screenshotPath;

        // 5. 序列化为JSON
        string json = JsonUtility.ToJson(saveData, true);

        // 6. 写入文件
        string path = GetSavePath(slotIndex);
        File.WriteAllText(path, json);

        // 7. 创建备份
        CreateBackup(path);

        // 8. UI反馈
        ShowSaveSuccessMessage();

        yield return null;
    }

    string GetSavePath(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            $"Profiles/profile_1/save_{slot}.json"
        );
    }
}
```

### 自动保存

```csharp
public class AutoSaveSystem : MonoBehaviour
{
    const int MAX_AUTOSAVES = 10;
    int currentAutoSaveIndex = 0;

    // 章节入口自动保存
    public void AutoSaveOnChapterStart()
    {
        currentAutoSaveIndex = (currentAutoSaveIndex + 1) % MAX_AUTOSAVES;
        SaveSystem.Instance.SaveToAutoSaveSlot(currentAutoSaveIndex);
    }

    // 定时自动保存（可选）
    void Start()
    {
        // 每10分钟自动保存一次
        InvokeRepeating(nameof(PeriodicAutoSave), 600f, 600f);
    }

    void PeriodicAutoSave()
    {
        // 仅在非战斗、非对话时自动保存
        if (CanAutoSave())
        {
            currentAutoSaveIndex = (currentAutoSaveIndex + 1) % MAX_AUTOSAVES;
            SaveSystem.Instance.SaveToAutoSaveSlot(currentAutoSaveIndex);
        }
    }

    bool CanAutoSave()
    {
        return !GameManager.Instance.IsInDialogue &&
               !GameManager.Instance.IsInEvent &&
               !GameManager.Instance.IsPaused;
    }
}
```

---

## 五、读取流程

### 读档系统

```csharp
public class LoadSystem : MonoBehaviour
{
    public void LoadGame(int slotIndex)
    {
        StartCoroutine(LoadGameCoroutine(slotIndex));
    }

    IEnumerator LoadGameCoroutine(int slotIndex)
    {
        // 1. 读取存档文件
        string path = GetSavePath(slotIndex);

        if (!File.Exists(path))
        {
            ShowError("存档不存在");
            yield break;
        }

        // 2. 解析JSON
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        // 3. 版本检查
        if (!IsVersionCompatible(saveData.version))
        {
            if (!TryMigrateSave(saveData))
            {
                ShowError("存档版本不兼容");
                yield break;
            }
        }

        // 4. 反序列化游戏状态
        GameState gameState = JsonUtility.FromJson<GameState>(
            saveData.gameStateJson);

        // 5. 显示载入画面
        ShowLoadingScreen();
        yield return null;

        // 6. 加载场景
        yield return SceneManager.LoadSceneAsync(gameState.currentScene);

        // 7. 恢复游戏状态
        GameManager.Instance.RestoreState(gameState);

        // 8. 隐藏载入画面
        HideLoadingScreen();

        // 9. 完成
        ShowLoadSuccessMessage();
    }

    bool IsVersionCompatible(string saveVersion)
    {
        // 简单版本检查
        return saveVersion == Application.version;
    }
}
```

---

## 六、备份与恢复

### 多重备份策略

```csharp
public class BackupSystem
{
    const int MAX_BACKUPS = 3;

    public void CreateBackup(string savePath)
    {
        string backupDir = Path.Combine(
            Path.GetDirectoryName(savePath),
            "backup"
        );

        if (!Directory.Exists(backupDir))
            Directory.CreateDirectory(backupDir);

        // 轮换备份（保留最近3个）
        for (int i = MAX_BACKUPS - 1; i > 0; i--)
        {
            string oldBackup = Path.Combine(backupDir, $"backup_{i - 1}.json");
            string newBackup = Path.Combine(backupDir, $"backup_{i}.json");

            if (File.Exists(oldBackup))
            {
                if (File.Exists(newBackup))
                    File.Delete(newBackup);
                File.Move(oldBackup, newBackup);
            }
        }

        // 创建新备份
        string latestBackup = Path.Combine(backupDir, "backup_0.json");
        File.Copy(savePath, latestBackup, true);
    }

    public bool TryRestoreFromBackup(int slotIndex)
    {
        string backupPath = Path.Combine(
            GetSaveDirectory(slotIndex),
            "backup/backup_0.json"
        );

        if (File.Exists(backupPath))
        {
            File.Copy(backupPath, GetSavePath(slotIndex), true);
            return true;
        }

        return false;
    }
}
```

---

## 七、存档列表UI数据

### SaveSlotInfo

```csharp
[System.Serializable]
public class SaveSlotInfo
{
    public int slotIndex;
    public bool isEmpty;

    // 如果非空
    public Texture2D screenshot;
    public string chapterName;
    public string gameDate;
    public string realTimestamp;       // "2024-11-05 14:30"
    public string playTime;            // "2小时15分"
}

public class SaveSlotManager : MonoBehaviour
{
    public List<SaveSlotInfo> LoadSaveSlotList()
    {
        List<SaveSlotInfo> slots = new List<SaveSlotInfo>();

        for (int i = 1; i <= 10; i++)
        {
            SaveSlotInfo info = new SaveSlotInfo { slotIndex = i };

            string path = GetSavePath(i);
            if (File.Exists(path))
            {
                SaveData data = LoadSaveData(path);
                info.isEmpty = false;
                info.chapterName = data.display.chapterName;
                info.gameDate = data.display.gameDate;
                info.realTimestamp = UnixToDateTime(data.timestamp);
                info.playTime = SecondsToTimeString(data.display.playTimeSeconds);
                info.screenshot = LoadTexture(data.screenshotPath);
            }
            else
            {
                info.isEmpty = true;
            }

            slots.Add(info);
        }

        return slots;
    }

    string UnixToDateTime(long unix)
    {
        DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(unix);
        return dto.LocalDateTime.ToString("yyyy-MM-dd HH:mm");
    }

    string SecondsToTimeString(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        return $"{hours}小时{minutes}分";
    }
}
```

---

## 八、存档兼容性（版本迁移）

### 版本管理

```csharp
public class SaveMigration
{
    public static bool MigrateSave(SaveData saveData, string fromVersion, string toVersion)
    {
        // 示例：从v1.0迁移到v1.1
        if (fromVersion == "1.0" && toVersion == "1.1")
        {
            return MigrateFrom1_0To1_1(saveData);
        }

        return false;
    }

    static bool MigrateFrom1_0To1_1(SaveData saveData)
    {
        try
        {
            // 解析旧版本数据
            var oldState = JsonUtility.FromJson<GameState_v1_0>(
                saveData.gameStateJson);

            // 转换为新版本
            var newState = new GameState();
            // ... 数据迁移逻辑

            // 更新存档
            saveData.gameStateJson = JsonUtility.ToJson(newState);
            saveData.version = "1.1";

            return true;
        }
        catch
        {
            return false;
        }
    }
}
```

---

## 九、云存档（可选）

### Steam云存档集成

```csharp
#if UNITY_STANDALONE
using Steamworks;

public class SteamCloudSave : MonoBehaviour
{
    public void UploadToCloud(string localPath)
    {
        if (!SteamManager.Initialized) return;

        byte[] data = File.ReadAllBytes(localPath);
        string cloudPath = Path.GetFileName(localPath);

        SteamRemoteStorage.FileWrite(cloudPath, data, data.Length);
    }

    public bool DownloadFromCloud(string cloudPath, string localPath)
    {
        if (!SteamManager.Initialized) return false;

        if (!SteamRemoteStorage.FileExists(cloudPath))
            return false;

        int fileSize = SteamRemoteStorage.GetFileSize(cloudPath);
        byte[] data = new byte[fileSize];

        int bytesRead = SteamRemoteStorage.FileRead(cloudPath, data, fileSize);
        if (bytesRead == fileSize)
        {
            File.WriteAllBytes(localPath, data);
            return true;
        }

        return false;
    }
}
#endif
```

---

## 十、性能优化

### 异步保存

```csharp
public async Task SaveGameAsync(int slotIndex)
{
    // 在后台线程序列化
    string json = await Task.Run(() =>
    {
        GameState state = GameManager.Instance.GetCurrentState();
        return JsonUtility.ToJson(state, true);
    });

    // 回到主线程写入文件
    await Task.Run(() =>
    {
        File.WriteAllText(GetSavePath(slotIndex), json);
    });
}
```

### 增量保存（可选）

```csharp
// 仅保存变化的数据，减少IO
public class IncrementalSave
{
    GameState lastSavedState;

    public void SaveDelta(GameState currentState)
    {
        var delta = CalculateDelta(lastSavedState, currentState);
        SaveDeltaData(delta);
        lastSavedState = currentState.DeepCopy();
    }
}
```

---

## 十一、测试要点

- [ ] 保存/读取功能正常
- [ ] 多槽位互不影响
- [ ] 损坏存档能恢复
- [ ] 版本迁移正确
- [ ] 截图正确保存
- [ ] 云存档同步（Steam）
- [ ] 多平台兼容
- [ ] 性能符合要求（<1秒保存）

---

**文档状态**: 初稿完成
**实现优先级**: P0（核心功能）
