using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{

    public WeaponType weaponType;
    public Vector3 attackPointOffset;
    // Start is called before the first frame update

    public enum WeaponType
    {
        sword, axe, wand, suriken
    }
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.attackPointOffset = attackPointOffset;
    }
}
