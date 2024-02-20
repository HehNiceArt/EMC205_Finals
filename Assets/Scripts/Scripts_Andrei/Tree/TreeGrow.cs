using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[System.Serializable]
public class TreeGrow : MonoBehaviour
{
    [Header("Tree")]
    public GameObject TreeScale;
    public float TimeToGrow;

    public HandItems HandItems;
    [Header("Scriptable Object")]
    public TreeGrowthItems[] TreeGrowthItems;
    //Singleton
    private static TreeGrow _instance;
    public static TreeGrow _Instance { get { return _instance; } }


    private float _num;
    private KeyCode[] _keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };

    private void Awake()
    {
        if( _instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        SwapItems();
    }
    float _growthItems;
    private void SwapItems()
    {
        for(int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                _growthItems = TreeGrowthItems[i].ItemValue;
                HandItems.ItemCheck();
                Debug.Log(HandItems.TreeItems[i].gameObject.name + "\n\r" + TreeGrowthItems[i].ItemName + " " + TreeGrowthItems[i].ItemID + " " + TreeGrowthItems[i].ItemValue);
            }
        }
    }
    //Still buggy but eh
    //good enough for now
    public void GrowTreeScale()
    {
        StartCoroutine(IncreaseScale());
    }
    IEnumerator IncreaseScale()
    {
            Vector3 _growth = new Vector3(_growthItems, _growthItems, _growthItems);
            TreeScale.transform.localScale += Vector3.MoveTowards(TreeScale.transform.localScale, _growth / 2, Time.deltaTime * TimeToGrow);
            yield return null;
    }
}
