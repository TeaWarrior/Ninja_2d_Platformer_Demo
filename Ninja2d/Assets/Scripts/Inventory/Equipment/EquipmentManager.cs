using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singeltone
    public static EquipmentManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one EqupmentManager");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnEquipmentChange( Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChange;

    public  Equipment[] currentEquipment;

    Inventory inventory;

    public GameObject[] currentEquipmentGameobjects;
    public GameObject[] additionalPants;

    public Vector3 attackPointOffset;
    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots =    System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        
       
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = null;
      
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChange != null)
        {
            onEquipmentChange.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;
        currentEquipmentGameobjects[slotIndex].GetComponent<SpriteRenderer>().sprite = newItem.icon;
    }
   public void Unequip( int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, oldItem);
            }
            currentEquipmentGameobjects[slotIndex].GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
