using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
       // base.Use();
        EquipmentManager.instance.Equip(this);
        Debug.Log("yes");
        RemoveFromInventory();
    }
}
public enum EquipmentSlot
{
    Head, Chest, Legs, Weapon, Shield, Feet
}


    
