using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;
    Inventory_Slot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
        UpdateUI();
        inventory.onItemChangedCallBack += UpdateUI;
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i< inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
