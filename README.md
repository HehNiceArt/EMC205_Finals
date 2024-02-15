# EMC205_Finals
 Unity project for Finals

## Unity Repository Guide!
Use this guide as for standardized workflow in unity!

<details>
<Summary>Scene Order</Summary>
Use @ for parent GameObjects
 
```
├── ...
├── @Player                      # GameObjects related to Player!
|   ├── Player_Model
|   ├── Player_Hand
|   ├── Player_FootstepEffects
|   └── ...
├── @CameraHolder
|   └── Camera_Main
```

<b>DO NOT </b> touch other member's sandbox level without their permission! <br>
Create a sub folder when you have multiple scenes!

```
.
├── ...
├── Scene Folder                      # Only scenes folder!
|   └── MASTER
|   |   ├── Master_MainMenu
|   |   ├── Master_Level
|   |   └── ...
|   └── SANDBOX
│       ├── SNBox_Andrei              # Sanbox level for Andrei
│       ├── SNBox_Kyle                # Sanbox level for Kyle
│       ├── SNBox_Kriz                # Sanbox level for Kriz
│       └── SNBox_Maxi                # Sanbox level for Maxi
└── ...
```

</details>

<details>
 <summary>Folder Order</summary>
 
 Make sure that folder is clean!
 One folder per assets
 
 | Folder  | Contents |
 | --------| -------- |
 | Scripts | Only contains scripts! |
 | Materials | Only contains materials! |
</details>

<details>
 <Summary>Scripts Structure Tree</Summary>
 
 If there are multiple scripts for a mechanic then create a sub folder!
 Create your own folder under core folders! (To avoid merge conflicts)<br>
```
.
├── ...
├── Scripts Folder                  # Only scripts folder!
│   ├── Andrei_Scripts              # Scripts made by Andrei
|   |   ├── Camera Folder             # Folder that contains camera related scripts
|   |   └── Player Folder             # Folder that contains player related scripts 
│   ├── Kyle_Scripts                # Scripts made by Kyle
|   |   ├── Inventory Folder          # Folder that contains inventory related scripts
|   |   └── Items Folder              # Folder that contains items related scripts 
│   ├── Kriz_Scripts                # Scripts made by Kriz
|   |   └── ...                       # Folder that ...
│   └── Maxi_Scripts                # Scripts made by Maxi
|       └── ...                       # Folder that ...
│   
└── ...
```
</details>
