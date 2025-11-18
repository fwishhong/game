# CLAUDE.md - AI Assistant Guide for 《秦淮旧梦》(Old Dreams of Qinhuai)

**Last Updated**: 2025-11-18
**Project Version**: Development (35% Complete)
**Unity Version**: 2022.3.6 LTS
**Primary Language**: C# + Chinese Content

---

## 📋 Table of Contents

1. [Project Overview](#project-overview)
2. [Technology Stack](#technology-stack)
3. [Repository Structure](#repository-structure)
4. [Code Conventions](#code-conventions)
5. [Development Workflows](#development-workflows)
6. [Key Systems](#key-systems)
7. [Important Context](#important-context)
8. [Common Tasks](#common-tasks)
9. [Testing](#testing)
10. [Git Workflow](#git-workflow)
11. [Troubleshooting](#troubleshooting)

---

## 🎮 Project Overview

### What is This Project?

**《秦淮旧梦》(Old Dreams of Qinhuai)** is a narrative-driven survival adventure game focused on the 1937 Nanjing Massacre. It combines historical education with emotional gaming experience through a dual-timeline mechanic.

**Core Pillars:**
- **Historical Witness** - Player as time traveler documenting real history
- **Moral Dilemmas** - Difficult choices with no absolute right or wrong
- **Cultural Resistance** - Education and culture as spiritual weapons
- **Human Light** - Warmth and perseverance in darkness

**Game Scope:**
- **Platform**: PC (Steam primary), expandable to console
- **Length**: 12-15 hours (main story), 20+ hours (complete)
- **Content**: 102,000+ Chinese characters of dialogue (100% complete)
- **Chapters**: 16 chapters (prologue + 15 chapters + epilogue)
- **Development Status**: ~35% complete (foundation solid, needs feature expansion)

### Current State

| Component | Status | Notes |
|-----------|--------|-------|
| Narrative Content | 100% ✅ | All dialogue scripts complete (33 Yarn files) |
| Core Scripts | 100% ✅ | 5 core C# scripts functional |
| Unity Setup | 100% ✅ | Project compiles with 0 errors |
| Player Movement | 100% ✅ | WASD movement tested |
| Dialogue System | 95% ✅ | Needs end-to-end testing |
| Game Systems | 35% ⏳ | Survival, inventory, diary need implementation |
| Art Assets | 0% ⏳ | Specifications complete (~990 assets planned) |
| Audio Assets | 0% ⏳ | Specifications complete (~260 assets planned) |

---

## 🛠️ Technology Stack

### Core Technologies

```yaml
Game Engine: Unity 2022.3.6 LTS
Language: C# (.NET Standard 2.1)
Dialogue System: Yarn Spinner v2.4.2 (Open Source)
UI Framework: Unity UI + TextMeshPro
Physics: Rigidbody2D (2D Game)
Version Control: Git
```

### Key Unity Packages

- **Yarn Spinner 2.4.2** - Dialogue system (already installed)
- **TextMeshPro** - Text rendering with Chinese character support (already installed)
- **Unity UI** - UI system (built-in)

### External Dependencies

None currently. Project uses only free, open-source tools.

---

## 📂 Repository Structure

```
/home/user/game/
│
├── Assets/                              # Unity project assets
│   ├── Scenes/                          # Unity scenes (.unity files)
│   │   └── TestScene.unity              # Current test scene
│   │
│   ├── Scripts/                         # C# source code (5 core scripts)
│   │   ├── Core/
│   │   │   ├── GameManager.cs           # ⭐ Game state & lifecycle management
│   │   │   ├── PlayerController.cs      # ⭐ Player movement & interaction
│   │   │   └── CameraFollow.cs          # Camera following system
│   │   └── Dialogue/
│   │       ├── DialogueManager.cs       # ⭐ Yarn Spinner integration
│   │       └── InteractableNPC.cs       # NPC interaction triggers
│   │
│   └── Dialogues/                       # Yarn dialogue files
│       ├── GameDialogues.yarnproject    # Yarn project configuration
│       └── TestDialogue.yarn            # Test dialogue file
│
├── docs/                                # Comprehensive design documentation
│   ├── README.md                        # Documentation index
│   │
│   ├── art/                             # Art specifications (5 docs)
│   │   ├── art_assets_scenes.md         # 260+ scene resources
│   │   ├── art_assets_characters.md     # 140+ character resources
│   │   ├── art_assets_ui.md             # 400+ UI resources
│   │   ├── art_assets_vfx.md            # 190+ VFX resources
│   │   └── art_style_guide.md           # Visual style guide
│   │
│   ├── audio/                           # Audio specifications (3 docs)
│   │   ├── audio_music.md               # 32+ music tracks
│   │   ├── audio_sfx.md                 # 215+ sound effects
│   │   └── audio_voice.md               # 5000+ voice lines (optional)
│   │
│   ├── gameplay/                        # Game systems design (6 docs)
│   │   ├── gameplay_numerical.md        # Balance & numbers
│   │   ├── gameplay_survival.md         # Resource management
│   │   ├── gameplay_relationship.md     # Character relationships
│   │   ├── gameplay_events.md           # Event system (138+ events)
│   │   ├── gameplay_diary.md            # Diary/journal system
│   │   └── level_design.md              # 18-chapter level design
│   │
│   ├── narrative/                       # Story & dialogue
│   │   ├── narrative_chapters.md        # Chapter breakdown
│   │   ├── narrative_characters.md      # 30+ character profiles
│   │   ├── dialogue_system.md           # Dialogue mechanics
│   │   ├── item_descriptions.md         # 102 item descriptions
│   │   ├── environment_inspection_texts.md  # 300+ inspectable objects
│   │   ├── diary_entries.md             # 90 diary prompts
│   │   ├── historical_annotations.md    # 150+ historical notes
│   │   └── dialogues/                   # ⭐ Complete Yarn scripts (33 files)
│   │       ├── chapters/                # 19 chapter dialogues
│   │       ├── npcs/                    # 7 NPC dialogues
│   │       ├── events/                  # 3 event dialogues
│   │       └── system/                  # 3 system UI texts
│   │
│   ├── ui/                              # UI/UX design (6 docs)
│   │   ├── ui_overview.md
│   │   ├── ui_hud.md
│   │   ├── ui_menus.md
│   │   ├── ui_inventory.md
│   │   ├── ui_dialogue.md
│   │   └── ui_diary.md
│   │
│   ├── technical/                       # Architecture (3 docs)
│   │   ├── technical_architecture.md    # System architecture
│   │   ├── technical_data_structure.md  # Data models (with C# examples)
│   │   └── technical_save_system.md     # Save/load system
│   │
│   └── execution/                       # Implementation guides
│       ├── balance_tables/              # Numerical balance data
│       ├── flowcharts/                  # Chapter flow diagrams
│       ├── triggers/                    # Event trigger specs
│       ├── achievement_system.md        # 80+ achievements
│       ├── cutscene_scripts.md          # Cutscene specifications
│       └── tutorial_system.md           # Tutorial design
│
├── chapters/                            # Story content (16 markdown files)
│   ├── 00_PROLOGUE.md
│   ├── 01-15_CHAPTER_*.md
│   └── 16_EPILOGUE.md
│
├── PROJECT_SUMMARY.md                   # ⭐ Complete project overview
├── PROJECT_STATUS.md                    # ⭐ Current development status
├── UNITY_PROJECT_STATUS.md              # Unity-specific status
├── game_design_doc.md                   # ⭐ Main design document (1049 lines)
├── DIALOGUE_TEST_GUIDE.md               # Testing instructions
├── NEXT_STEPS.md                        # Development roadmap
├── UNITY_SETUP_GUIDE.md                 # Setup instructions
└── CLAUDE.md                            # This file
```

**⭐ = Critical files to read first**

---

## 📐 Code Conventions

### Namespace Organization

```csharp
namespace QinhuaiOldDreams.Core       // Core game systems
namespace QinhuaiOldDreams.Dialogue   // Dialogue systems
namespace QinhuaiOldDreams.UI         // UI systems (future)
namespace QinhuaiOldDreams.Data       // Data models (future)
```

### Naming Conventions

**C# Code:**
```csharp
// Classes: PascalCase
public class GameManager

// Private fields: _camelCase with underscore
private static GameManager _instance;

// Public properties: PascalCase
public GameState CurrentState { get; }

// Methods: PascalCase
public void SetGameState(GameState newState)

// Constants: UPPER_SNAKE_CASE
public const int MAX_INVENTORY_SIZE = 20;

// Enums: PascalCase
public enum GameState { MainMenu, Playing, Dialogue }
```

**File Naming:**
```
C# Scripts:        PascalCase.cs          (GameManager.cs)
Yarn Files:        snake_case.yarn        (chapter_01_awakening.yarn)
Unity Scenes:      PascalCase.unity       (SafetyZoneScene.unity)
Asset Files:       type_location_detail_variant.ext
                   (scene_museum_interior_day.psd)
```

### Code Structure Patterns

**Singleton Pattern** (used for managers):
```csharp
private static GameManager _instance;
public static GameManager Instance
{
    get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
            }
        }
        return _instance;
    }
}

private void Awake()
{
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
        return;
    }
    _instance = this;
    DontDestroyOnLoad(gameObject);
}
```

**Interface-Based Interaction:**
```csharp
public interface IInteractable
{
    void Interact(PlayerController player);
}

// Usage in PlayerController
if (interactable != null)
{
    interactable.Interact(this);
}
```

**Region Organization:**
```csharp
#region 单例模式
// Singleton code
#endregion

#region 游戏状态
// Game state code
#endregion

#region Unity生命周期
// Unity lifecycle methods
#endregion
```

### Documentation

```csharp
/// <summary>
/// Brief description of the class/method
/// </summary>
/// <param name="paramName">Parameter description</param>
/// <returns>Return value description</returns>
```

---

## 🔄 Development Workflows

### For Different Roles

#### As a Programmer
1. Read `docs/technical/technical_architecture.md` for system design
2. Check `docs/technical/technical_data_structure.md` for data models
3. Core systems are in `Assets/Scripts/Core/` and `Assets/Scripts/Dialogue/`
4. Follow namespace convention: `QinhuaiOldDreams.[System]`
5. Register new Yarn commands in `DialogueManager.RegisterYarnCommands()`
6. Use TODO comments for incomplete features

#### As a Game Designer
1. Reference `docs/gameplay/` for system specifications
2. Check `docs/execution/balance_tables/` for numerical values
3. Use `docs/execution/flowcharts/` for chapter design
4. Refer to `game_design_doc.md` for core mechanics

#### As a Writer/Narrative Designer
1. Complete dialogue library is in `docs/narrative/dialogues/`
2. Use Yarn Spinner syntax for all dialogue files
3. Character voices defined in `docs/narrative/narrative_characters.md`
4. Follow dialogue testing guide in `DIALOGUE_TEST_GUIDE.md`

#### As an Artist
1. Check `docs/art/art_style_guide.md` for visual standards
2. Follow naming: `type_location_detail_variant.ext`
3. Export specs in `docs/art/art_assets_*.md`
4. Reference character designs in `docs/art/art_assets_characters.md`

### Working with Unity

**Opening the Project:**
```bash
# Project is located at: /home/user/game/
# Open with Unity 2022.3.6 LTS or compatible version
```

**Before Making Changes:**
1. Check current git status
2. Ensure Unity project compiles without errors
3. Read relevant documentation in `docs/`

**After Making Changes:**
1. Test in Unity Editor
2. Check for compilation errors
3. Write clear commit messages
4. Push to feature branch (starts with `claude/`)

---

## 🎯 Key Systems

### 1. GameManager (`Assets/Scripts/Core/GameManager.cs`)

**Purpose**: Central game state and lifecycle management
**Pattern**: Singleton
**Lines**: 323

**Key Features:**
- Game state management (6 states: MainMenu, Playing, Dialogue, Paused, Inventory, Diary)
- Timeline switching (1937 ↔ 2024)
- Scene loading (sync & async)
- Global input handling (ESC, Tab, I keys)
- Chapter and day tracking

**Usage:**
```csharp
// Change game state
GameManager.Instance.SetGameState(GameState.Dialogue);

// Switch timeline
GameManager.Instance.SwitchTimeline(TimelineType.Historical1937);

// Load scene
GameManager.Instance.LoadScene("SafetyZone");
```

**TODO Items:**
- Timeline switch visual effects
- Music switching on timeline change
- Loading screen UI
- Pause menu display
- Diary/Inventory UI opening

### 2. PlayerController (`Assets/Scripts/Core/PlayerController.cs`)

**Purpose**: Player movement and interaction
**Lines**: 236

**Key Features:**
- WASD/arrow key movement (8-directional)
- Rigidbody2D physics
- Interaction detection (2-meter range)
- Animation state management
- Sprite direction flipping

**Usage:**
```csharp
// Player automatically handles movement and interaction
// Objects implement IInteractable to be interactable
```

**TODO Items:**
- Interaction hint UI display

### 3. DialogueManager (`Assets/Scripts/Dialogue/DialogueManager.cs`)

**Purpose**: Yarn Spinner integration and dialogue control
**Pattern**: Singleton
**Lines**: 245

**Key Features:**
- Yarn command registration
- Custom commands: `<<wait>>`, `<<playSFX>>`, `<<changeScene>>`
- Yarn variable management
- Game state sync during dialogue
- Player movement locking

**Custom Yarn Commands:**
```yarn
<<wait 2>>                    // Wait 2 seconds
<<playSFX footsteps>>        // Play sound effect
<<changeScene SafetyZone>>   // Load new scene
```

**TODO Items:**
- Audio system integration for playSFX

### 4. CameraFollow (`Assets/Scripts/Core/CameraFollow.cs`)

**Purpose**: Camera system following player
**Lines**: 133

**Key Features:**
- Smooth following with configurable speed
- Optional boundary constraints
- Auto-find Player by tag
- Snap-to-target functionality

### 5. InteractableNPC (`Assets/Scripts/Dialogue/InteractableNPC.cs`)

**Purpose**: NPC interaction and dialogue triggering
**Lines**: 127

**Key Features:**
- IInteractable interface implementation
- E-key or Space to interact
- Yarn node triggering
- One-time-only dialogue option

**TODO Items:**
- "Press E to interact" UI hint

---

## 🧠 Important Context

### Historical & Cultural Sensitivity

**CRITICAL**: This project deals with the 1937 Nanjing Massacre, one of the most tragic events in Chinese history.

**When working on this project:**
- Treat historical content with utmost respect and accuracy
- Reference `docs/narrative/historical_annotations.md` for historical context
- All narrative content has been carefully researched
- Avoid trivializing suffering or historical events
- This is an educational project with social value

### Dual Timeline Mechanic

**CRITICAL GAME MECHANIC:**

The game uses **two timelines**:
1. **2024 Modern Timeline** - Player starts in Nanjing Massacre Memorial Museum
2. **1937 Historical Timeline** - Player experiences past through time travel/visions

**How it works:**
- Player switches between timelines during gameplay
- 2024 = Reflection, learning, modern perspective
- 1937 = Experience, witness, emotional impact
- Choices in 1937 affect what player learns in 2024

**In Code:**
```csharp
public enum TimelineType
{
    Modern2024,      // 现代（2024年）
    Historical1937   // 历史（1937年）
}
```

### Language: Simplified Chinese

**Primary Language**: All narrative content is in Simplified Chinese (简体中文)

**Localization Plan**:
- Future support for English and Japanese
- Use Unity Localization package (future)
- TextMeshPro already configured for CJK characters

**When Working with Chinese Text:**
- Use UTF-8 encoding
- Yarn files use Chinese text directly
- UI uses TextMeshPro with Chinese font support

### Yarn Spinner Dialogue System

**All dialogue uses Yarn format:**
- **33 complete .yarn files** with 102,000+ Chinese characters
- Located in `docs/narrative/dialogues/`
- Standard Yarn Spinner v2.4.2 syntax
- Includes custom commands for game integration

**Example Yarn Syntax:**
```yarn
title: TestNode
---
王掌柜: 你好，年轻人。
-> 你好，掌柜。
    王掌柜: 欢迎来到秦淮人家。
-> 我只是路过。
    王掌柜: 外面不太平，进来歇歇吧。
===
```

**Variable System:**
```yarn
<<set $relationship_wang += 10>>
<<if $food > 0>>
    陈默: 我有食物可以分享。
<<else>>
    陈默: 我没有多余的食物了。
<<endif>>
```

---

## ✅ Common Tasks

### Adding a New Game System

1. **Create Script:**
   ```bash
   # Create in appropriate namespace folder
   Assets/Scripts/[SystemName]/[ClassName].cs
   ```

2. **Follow Pattern:**
   ```csharp
   namespace QinhuaiOldDreams.[SystemName]
   {
       public class [ClassName] : MonoBehaviour
       {
           #region Fields
           #endregion

           #region Unity Lifecycle
           private void Awake() { }
           private void Start() { }
           private void Update() { }
           #endregion

           #region Public Methods
           #endregion

           #region Private Methods
           #endregion
       }
   }
   ```

3. **Register with GameManager** (if needed):
   - Add system reference to GameManager
   - Initialize in Awake() or Start()

### Adding Yarn Dialogue

1. **Create .yarn file** in `Assets/Dialogues/`
2. **Follow naming**: `chapter_XX_name.yarn` or `npc_name_talks.yarn`
3. **Add to Yarn Project**: Select in `GameDialogues.yarnproject`
4. **Test**: Create test NPC with InteractableNPC component

### Implementing a New UI Screen

1. **Reference**: `docs/ui/ui_[screen_name].md`
2. **Create Canvas**: New UI → Canvas
3. **Add Components**: Follow UI specifications in docs
4. **Script Integration**: Create UI controller script
5. **Connect to GameManager**: Register in game state system

### Adding Custom Yarn Command

1. **Open**: `Assets/Scripts/Dialogue/DialogueManager.cs`
2. **Add in RegisterYarnCommands():**
   ```csharp
   yarnProject.AddCommandHandler("myCommand", MyCommandMethod);
   ```
3. **Implement Method:**
   ```csharp
   private void MyCommandMethod(string[] parameters)
   {
       // Implementation
   }
   ```

### Reading Documentation

**Quick Reference:**
```bash
# Start here for overview
PROJECT_SUMMARY.md           # Complete project overview
PROJECT_STATUS.md            # Current development status
game_design_doc.md          # Main design document

# For specific systems
docs/gameplay/[system].md   # Gameplay system design
docs/technical/[topic].md   # Technical architecture
docs/narrative/[content].md # Story and dialogue
```

---

## 🧪 Testing

### Manual Testing

**Follow**: `DIALOGUE_TEST_GUIDE.md`

**Quick Test Procedure:**
1. Open Unity project
2. Open TestScene
3. Press Play
4. Test player movement (WASD)
5. Move to NPC (purple capsule)
6. Press E to interact
7. Verify dialogue appears
8. Test dialogue choices

### Dialogue Testing

**Location**: `Assets/Dialogues/TestDialogue.yarn`

**Test Cases:**
- Dialogue display
- Choice selection
- Variable tracking
- Custom commands (wait, playSFX, changeScene)
- Dialogue end behavior

### Code Quality Checks

**Before Committing:**
- [ ] No compilation errors
- [ ] No warnings (if possible)
- [ ] Code follows naming conventions
- [ ] XML documentation for public methods
- [ ] TODO comments for unfinished work
- [ ] Test in Unity Editor

---

## 🔀 Git Workflow

### Branch Strategy

**Current Branch**: `claude/claude-md-mi3ytcl3cc1yebjd-014agcT8hiwRCmqQMMvf2a5r`

**Branch Naming:**
- All Claude AI branches start with `claude/`
- Must end with matching session ID
- Format: `claude/[description]-[session-id]`

### Commit Message Format

**Style**: Clear, descriptive, focused on "why"

**Examples:**
```bash
# Good
git commit -m "Add player interaction range visualization for debugging"
git commit -m "Implement dialogue state locking to prevent movement during conversations"

# Avoid
git commit -m "Update code"
git commit -m "Fix bug"
```

### Committing Changes

**Process:**
1. Review changes: `git status`
2. Stage files: `git add [files]`
3. Commit with message
4. Push to remote: `git push -u origin [branch-name]`

**Important:**
- NEVER commit without explicit user request
- NEVER run git operations with `--force` unless explicitly requested
- Check authorship before amending commits
- Use heredoc for multi-line commit messages

### Pushing to Remote

**Required:**
```bash
git push -u origin claude/[branch-name]
```

**Retry Logic**: If network failure, retry up to 4 times with exponential backoff (2s, 4s, 8s, 16s)

---

## 🚨 Troubleshooting

### Common Issues

#### "Yarn Spinner not found"
- **Solution**: Check `YARN_SPINNER_FREE_INSTALL.md` and `FIX_YARN_SPINNER_SETUP.md`
- Yarn Spinner v2.4.2 should be installed via Unity Package Manager

#### "Chinese characters not displaying"
- **Solution**: Ensure TextMeshPro is imported with Chinese font support
- Check font settings in TextMeshPro component

#### "DialogueRunner not found"
- **Solution**: Ensure GameDialogues.yarnproject is assigned in DialogueManager
- Check scene has DialogueManager GameObject

#### "Compilation errors"
- **Solution**: Check `UNITY_PROJECT_STATUS.md` for known issues
- Verify Unity version (2022.3.6 LTS recommended)
- Reimport all assets

#### "Player not moving"
- **Solution**:
  - Check PlayerController has Rigidbody2D component
  - Verify input system is not locked
  - Check GameState is "Playing" not "Dialogue"

### Getting Help

**Documentation Files:**
- `DIALOGUE_TEST_GUIDE.md` - Dialogue system issues
- `UNITY_SETUP_GUIDE.md` - Unity installation help
- `NEXT_STEPS.md` - Development guidance
- `HOW_TO_FIX_YARN_ERROR.md` - Yarn Spinner issues

**Check Project Status:**
```bash
# Current development status
PROJECT_STATUS.md

# Unity-specific status
UNITY_PROJECT_STATUS.md
```

---

## 📚 Essential Reading for AI Assistants

### Must Read First (Priority 0)
1. **This file** (`CLAUDE.md`) - You're reading it
2. `PROJECT_SUMMARY.md` - Complete project overview
3. `PROJECT_STATUS.md` - Current development status
4. `game_design_doc.md` - Core game design (first 100 lines minimum)

### Read Before Making Changes (Priority 1)
- `docs/technical/technical_architecture.md` - System architecture
- `docs/technical/technical_data_structure.md` - Data models
- Relevant system documentation in `docs/[category]/`

### Reference as Needed (Priority 2)
- `docs/narrative/dialogues/` - Complete dialogue library
- `docs/gameplay/` - Gameplay system specs
- `docs/ui/` - UI design specifications
- `docs/art/` and `docs/audio/` - Asset specifications

### For Specific Tasks
- **Adding dialogue**: `docs/narrative/dialogue_system.md`
- **Implementing systems**: `docs/gameplay/gameplay_[system].md`
- **Creating UI**: `docs/ui/ui_[component].md`
- **Understanding story**: `chapters/` folder + `docs/narrative/narrative_chapters.md`

---

## 🎯 Development Priorities

### Current Phase: Foundation Expansion (35% → 60%)

**High Priority (Do These First):**
1. Complete dialogue system end-to-end testing
2. Implement inventory/item system (specs in `docs/gameplay/`)
3. Implement diary system (specs in `docs/gameplay/gameplay_diary.md`)
4. Create first complete scene (Museum 2024)

**Medium Priority:**
5. Implement survival resource management
6. Create character relationship system
7. Import all 33 Yarn dialogue files
8. Develop save/load system

**Lower Priority (Later):**
9. Art asset integration (when available)
10. Audio implementation (when available)
11. Localization system
12. Performance optimization

### What NOT to Do

- ❌ Don't create placeholder art assets (wait for artist)
- ❌ Don't modify historical narrative content (already finalized)
- ❌ Don't change core game mechanics without design review
- ❌ Don't commit directly to main branch (use feature branches)
- ❌ Don't skip testing dialogue changes
- ❌ Don't use deprecated Unity APIs

---

## 💡 AI Assistant Best Practices

### When Starting a Task
1. **Read relevant documentation** from `docs/`
2. **Check current status** in `PROJECT_STATUS.md`
3. **Understand the context** (historical sensitivity, game design)
4. **Plan before coding** (especially for complex features)

### When Writing Code
1. **Follow conventions** (namespaces, naming, patterns)
2. **Add comments** (especially for complex logic)
3. **Use regions** to organize code
4. **Mark TODOs** for incomplete work
5. **Test immediately** in Unity Editor

### When Working with Dialogue
1. **Preserve Chinese text** exactly as written
2. **Use Yarn syntax** correctly
3. **Test in game** before committing
4. **Reference character profiles** for voice consistency

### When Uncertain
1. **Ask questions** rather than making assumptions
2. **Check documentation** first
3. **Look at existing code** for patterns
4. **Test thoroughly** before finalizing

---

## 📊 Quick Stats

```yaml
Project Metrics:
  Total Files: 150+ (code, docs, assets)
  Total C# Scripts: 5 core scripts
  Total Documentation: 80+ markdown files
  Total Dialogue: 102,000+ Chinese characters
  Total Lines of Code: ~1,000+ C#
  Documentation Size: ~3.6 MB

Content Complete:
  Dialogue: 100% ✅ (33 Yarn files)
  Story: 100% ✅ (16 chapter files)
  Design Docs: 100% ✅ (28 documents)

Development Status:
  Overall Progress: 35%
  Core Systems: 100% ✅
  Game Features: 35% ⏳
  Art Assets: 0% ⏳
  Audio Assets: 0% ⏳
```

---

## 🔗 External Resources

### Unity Documentation
- [Unity Manual 2022.3 LTS](https://docs.unity3d.com/2022.3/Documentation/Manual/)
- [C# Scripting Reference](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/)

### Yarn Spinner Documentation
- [Yarn Spinner Docs](https://docs.yarnspinner.dev/)
- [Yarn Syntax Guide](https://docs.yarnspinner.dev/using-yarnspinner-with-unity/writing-in-yarn)

### Historical Context
- Reference materials in `docs/narrative/historical_annotations.md`
- Character backgrounds in `docs/narrative/narrative_characters.md`

---

## 📝 Version History

| Date | Version | Changes |
|------|---------|---------|
| 2025-11-18 | 1.0.0 | Initial CLAUDE.md creation |

---

## 🙏 Final Notes

This project is more than a game—it's a memorial to history and a tribute to those who suffered. When working on this project:

- **Respect the history** and those who lived through it
- **Maintain quality** in all aspects of development
- **Follow conventions** to keep codebase maintainable
- **Document your work** for future developers
- **Test thoroughly** to ensure quality
- **Ask questions** when unsure

**Let us use the language of games to tell stories that should never be forgotten.**

*谨以此项目，献给所有在苦难中坚持做人的人们。*
*(This project is dedicated to all those who persevered in maintaining their humanity through suffering.)*

---

**Document Maintainer**: AI Assistants working on this project
**Last Updated**: 2025-11-18
**Status**: Living document - update as project evolves
