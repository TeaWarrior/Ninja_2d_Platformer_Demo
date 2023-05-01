using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentSLot : MonoBehaviour
{

    public Image icon;
    public GameObject imageGO;
    public Equipment equipment;
    public Button equipmentButton;
    public EquipmentSlot equipment_index;


   
    public void ClearSlot()
    {
        if (equipment != null)
        {
            
               
               
         
            int someInt =(int) equipment_index;
            EquipmentManager.instance.Unequip(someInt);
            equipment = null;
            imageGO.SetActive(false);
            // equipmentButton.interactable = false;
            Debug.Log("So");
            if (equipment_index == EquipmentSlot.Weapon)
            {
                EquipmentManager.instance.attackPointOffset = new Vector3(0, 0, 0);
            }
        }
     
     
    }

    public void RefreshSlot()
    {

    }
}
