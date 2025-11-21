# Match-3 Mobile Game

A cross-platform match-3 puzzle game built with Unity and C#, deployable to both iOS and Android.

## 🎮 About This Game

This is a classic match-3 game where players swap adjacent tiles to create matches of 3 or more of the same color. When tiles match, they disappear and new tiles fall from the top, potentially creating cascade matches!

## 🚀 Getting Started

### Prerequisites

1. **Download Unity Hub**: https://unity.com/download
2. **Install Unity Editor**: 
   - Version 2021.3 LTS or newer recommended
   - Include iOS Build Support (for Mac only)
   - Include Android Build Support
   - Install Android SDK & NDK when prompted

### Opening the Project

1. Open Unity Hub
2. Click "Add" or "Open"
3. Navigate to this project folder: `/Users/dan.obrien/code/mobile-game`
4. Select the folder and click "Open"
5. Unity will import the project (this may take a few minutes the first time)

## 📁 Project Structure

```
mobile-game/
├── Assets/
│   ├── Scripts/         # All C# game scripts
│   │   ├── Tile.cs           # Individual tile/gem behavior
│   │   ├── Board.cs          # Game board management
│   │   ├── GameManager.cs    # Overall game control & scoring
│   │   ├── MatchDetector.cs  # Match detection logic
│   │   └── TileSwapper.cs    # Tile swapping logic
│   ├── Prefabs/         # Reusable game objects (tiles, UI)
│   ├── Scenes/          # Unity scenes (game levels)
│   ├── Sprites/         # 2D images for tiles and UI
│   └── Materials/       # Visual materials
└── ProjectSettings/     # Unity project configuration
```

## 🎨 Setting Up Your First Scene

### Step 1: Create the Main Scene

1. In Unity, go to **File → New Scene**
2. Save it as `MainGame` in the `Assets/Scenes/` folder

### Step 2: Create the Game Board

1. Right-click in the Hierarchy panel
2. Select **Create Empty**
3. Rename it to "Board"
4. In the Inspector, click **Add Component**
5. Search for and add the `Board` script

### Step 3: Create a Tile Prefab

1. Right-click in the Hierarchy
2. Select **2D Object → Sprite → Square**
3. Rename it to "Tile"
4. In the Inspector, click **Add Component**
5. Add the `Tile` script
6. Drag this Tile from Hierarchy to the `Assets/Prefabs/` folder
7. Delete the Tile from the Hierarchy (the prefab is saved)

### Step 4: Connect the Prefab to the Board

1. Select the "Board" object in Hierarchy
2. In the Inspector, find the `Board` component
3. Drag the Tile prefab from `Prefabs/` to the "Tile Prefab" field

### Step 5: Create Tile Sprites (Colors)

**Option A: Use Unity's Built-in Sprites (Quick Start)**
1. The Tile prefab already uses Unity's default white square
2. You can change colors in code or create colored materials

**Option B: Create Your Own Sprites (Recommended)**
1. Create 6 colored square images (Red, Blue, Green, Yellow, Purple, Orange)
2. Each should be 256x256 pixels or larger
3. Save them as PNG files
4. Drag them into `Assets/Sprites/`
5. Select the Board object
6. In the Inspector, expand "Tile Sprites" to size 6
7. Drag each colored sprite to an element (0-5)

### Step 6: Create the Game Manager

1. Right-click in Hierarchy
2. **Create Empty**, rename to "GameManager"
3. Add the `GameManager` component
4. Configure the settings in Inspector:
   - Move Limit: 30
   - Target Score: 1000
   - Match 3 Score: 100

### Step 7: Add UI (Optional but Recommended)

1. Right-click in Hierarchy → **UI → Canvas**
2. Right-click Canvas → **UI → Text** (create 3 of these):
   - ScoreText
   - MovesText
   - GameOverText
3. Position them at the top of the screen
4. Drag these text objects to the GameManager's UI fields in Inspector

### Step 8: Setup the Camera

1. Select the Main Camera
2. Set Position: `X=3.5, Y=3.5, Z=-10` (adjust based on your board size)
3. Set Size: `6` for Orthographic camera (adjust to fit board)

## 🎮 How to Play

1. Click **Play** button in Unity Editor
2. Click on a tile to select it
3. Click an adjacent tile to swap them
4. Match 3 or more tiles of the same color
5. Score points and try to reach the target before running out of moves!

## 📱 Building for Mobile

### For Android:

1. Go to **File → Build Settings**
2. Select **Android**
3. Click **Switch Platform**
4. Click **Player Settings**
5. Configure:
   - Company Name
   - Product Name
   - Bundle Identifier (e.g., com.yourname.match3game)
6. Click **Build** or **Build and Run**

### For iOS:

1. Go to **File → Build Settings**
2. Select **iOS**
3. Click **Switch Platform**
4. Configure Player Settings similar to Android
5. Click **Build**
6. Open the generated Xcode project on a Mac
7. Build and deploy from Xcode

## 🔧 Customization

### Adjusting Board Size

In the Board component:
- `Width`: Number of columns (default: 8)
- `Height`: Number of rows (default: 8)
- `Tile Spacing`: Distance between tiles (default: 1.0)

### Changing Difficulty

In the GameManager:
- `Move Limit`: Number of moves allowed
- `Target Score`: Score needed to win
- Scoring values for different match sizes

### Animation Speed

In the Board component:
- `Swap Duration`: How fast tiles swap (default: 0.3s)
- `Fall Duration`: How fast tiles fall (default: 0.5s)

## 🎨 Adding Better Graphics

1. **Tile Sprites**: Replace with colorful gem images
2. **Background**: Add a background image to the scene
3. **Particles**: Add particle effects when matches occur
4. **Sound**: Add AudioSource components for sound effects
5. **UI Polish**: Use Unity's UI styling for better menus

## 📚 Next Steps

### Features to Add:
- ✅ Basic match-3 mechanics (DONE)
- ✅ Scoring system (DONE)
- ✅ Move counter (DONE)
- ⬜ Power-ups (4+ matches create special tiles)
- ⬜ Level progression
- ⬜ Sound effects and music
- ⬜ Particle effects for matches
- ⬜ Animations and juice
- ⬜ Save/load game state
- ⬜ Leaderboards
- ⬜ In-app purchases (optional)

### Learning Resources:
- Unity Documentation: https://docs.unity3d.com/
- Unity Learn: https://learn.unity.com/
- Brackeys YouTube Channel: https://www.youtube.com/user/Brackeys
- Unity Asset Store: https://assetstore.unity.com/ (free assets available)

## 🐛 Troubleshooting

### Tiles Not Appearing?
- Check that the Tile Prefab is assigned in the Board component
- Make sure sprites are assigned in the Tile Sprites array
- Verify camera position can see the board

### Tiles Not Swapping?
- Ensure tiles have a Collider2D component for mouse clicks
- Check that the Board script is properly attached
- Look for errors in the Unity Console (Window → General → Console)

### Build Errors?
- Make sure all required build modules are installed in Unity Hub
- Check Player Settings for required configuration
- For iOS, you need a Mac with Xcode installed

## 🤝 Contributing

This is a starter template! Feel free to:
- Add new features
- Improve the code
- Create better graphics
- Share your improvements

## 📄 License

This project is provided as-is for learning and commercial use. No attribution required.

## 🎉 Have Fun!

You now have a fully functional match-3 game foundation. Start customizing, add your own creative touches, and deploy to mobile devices!

---

**Need Help?** 
- Unity Forums: https://forum.unity.com/
- Unity Discord: https://discord.com/invite/unity
- Stack Overflow: https://stackoverflow.com/questions/tagged/unity3d

