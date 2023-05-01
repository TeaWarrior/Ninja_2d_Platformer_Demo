using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EquipmentPanel : MonoBehaviour
{
    public GameObject equipment_Panel;
    public UI_EquipmentSLot[] equipmentSlot;
    Inventory inventory;
    EquipmentManager equipment;
    private void Start()
    {
        inventory = Inventory.instance;
        equipment = EquipmentManager.instance;
        inventory.onItemChangedCallBack += refreshSlot;
    }
    public void CloseEquipment()
    {
        equipment_Panel.SetActive(false);
    }
    public void refreshSlot()
    {
        for (int i = 0; i < equipment.currentEquipment.Length; i++)
        {
            if (equipment.currentEquipment[i] != null)
            {
                equipmentSlot[i].icon.sprite = equipment.currentEquipment[i].icon;
                equipmentSlot[i].equipment = equipment.currentEquipment[i];
                equipmentSlot[i].imageGO.SetActive(true);
            }
        }
    }
}
