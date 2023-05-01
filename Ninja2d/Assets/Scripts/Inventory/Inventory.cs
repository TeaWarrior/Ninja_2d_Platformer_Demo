using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singeltone
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More then one Inventory");
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnItemChange();
    public OnItemChange onItemChangedCallBack;
    public List<Item> items = new List<Item>();

    public int space = 20;
    public bool Add (Item item)
    {
        
        if(items.Count >= space)
        {
            Debug.Log("not Enought room");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
