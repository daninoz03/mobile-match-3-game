# Quick Start Guide - 5 Minutes to Playable Game!

## Step 1: Install Unity (15-30 minutes)

1. Download Unity Hub: https://unity.com/download
2. Install Unity Editor 2021.3 LTS or newer
3. Include iOS Build Support (Mac only) and Android Build Support

## Step 2: Open Project (2 minutes)

1. Open Unity Hub
2. Click "Add" → "Add project from disk"
3. Select this folder: `/Users/dan.obrien/code/mobile-game`
4. Wait for Unity to import

## Step 3: Create Your First Scene (5 minutes)

### A. Create Scene
1. File → New Scene → 2D
2. File → Save As → `Assets/Scenes/MainGame`

### B. Create Board
1. Hierarchy → Right-click → Create Empty
2. Rename to "Board"
3. Inspector → Add Component → "Board" script
4. Set Width: 8, Height: 8

### C. Create Tile Prefab
1. Hierarchy → Right-click → 2D Object → Sprites → Square
2. Rename to "Tile"
3. Add Component → "Tile" script
4. Add Component → Box Collider 2D (for mouse clicks)
5. Drag "Tile" from Hierarchy to Assets/Prefabs folder
6. Delete "Tile" from Hierarchy

### D. Connect Tile to Board
1. Select "Board" in Hierarchy
2. In Inspector, find the "Tile Prefab" field
3. Drag the Tile prefab from Assets/Prefabs/ into this field

### E. Create 6 Colored Tiles (Simple Method)
1. In Assets/Sprites, create 6 Materials or use sprites
2. For quick test: Select Board → Tile Sprites → Set Size to 0 (will use default white sprites)

### F. Add Camera Setup
1. Select Main Camera
2. Set Position: (3.5, 3.5, -10)
3. Set Size: 6 (in Orthographic view)

### G. Add Game Manager
1. Hierarchy → Create Empty → "GameManager"
2. Add Component → "GameManager" script

## Step 4: Press Play! ▶️

Click the Play button at the top of Unity editor. You should see:
- An 8x8 grid of white tiles
- Click a tile, then click an adjacent tile to swap
- If 3+ match horizontally or vertically, they disappear!

## Next Steps

See the full README.md for:
- Adding colored sprites
- Creating UI for score/moves
- Building for mobile devices
- Customization options

## Troubleshooting

**No tiles appear?**
- Check Console for errors (Window → General → Console)
- Verify Tile Prefab is assigned to Board
- Make sure camera can see position (0,0) to (8,8)

**Can't click tiles?**
- Add Box Collider 2D to Tile prefab
- Make sure tiles have the Tile.cs script attached

**Tiles don't swap?**
- Check Console for errors
- Verify Board script has MatchDetector and TileSwapper components (auto-added)

## That's It!

You now have a working match-3 game. Start customizing and have fun! 🎮

