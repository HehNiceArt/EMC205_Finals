# EMC205_Finals
 Unity project for Finals

## Unity Repository Guide!
Use this guide as for the style in unity

<details>
<Summary>Scene Order</Summary>
Use @ for main GameObjects
 
<dl>
 <dt>@Player</dt>
 <dd>PlayerModel<br>Hand</dd>
 <dt>@CameraHolder</dt>
 <dd>Camera</dd>
</dl>
</details>
<details>
 <summary>Folder Structure</summary>
 Make sure that folder is clean!
 One folder per assets
 
 | Folder  | Contents |
 | --------| -------- |
 | Scripts | Only contains scripts! |
 | Materials | Only contains materials! |

 If there are multiple scripts for a mechanic then create a sub folder!
 <dl>
  <dt>Scripts Folder</dt>
  <dd>
   <b>Player</b>
   <br>
   <pre>
    PlayerMovement.cs
    PlayerCamera.cs</dd>
  <dd>
   <b>Inventory</b>
   <pre>
    ItemList.cs
    ItemContainers.cs</dd>
 </dl>
</details>
