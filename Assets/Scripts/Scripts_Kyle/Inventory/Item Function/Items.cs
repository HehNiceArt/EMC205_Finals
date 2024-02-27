using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable]
public class Items : ScriptableObject
{
    #region test

    #endregion
    public int id;

    public string itemName;

    [TextArea(3, 3)] public string description;


    public GameObject prefab;
    public Texture icon;

    public int maxStack;
}
