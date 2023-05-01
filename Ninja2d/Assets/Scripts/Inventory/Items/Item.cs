
using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public int price;
    public virtual void Use()
    {
        Debug.Log("Using Item " + itemName);
    }
  
   
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
