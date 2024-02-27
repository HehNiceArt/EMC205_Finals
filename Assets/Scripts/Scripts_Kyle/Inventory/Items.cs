//@Kyle Rafael
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable]
public class Items : ScriptableObject
{
    #region test

    #endregion
    public int Id;

    public string ItemName;

    [TextArea(3, 3)] public string Description;


    public GameObject Prefab;
    public Texture Icon;

    public int MaxStack;
}
