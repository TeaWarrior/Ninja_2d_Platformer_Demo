using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : CharacterStats
{

    public delegate void OnDamageTook();
    public OnDamageTook onDamageTook;

    public Stat attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChange+= OnEquipmentChanged;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEquipmentChanged( Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
          
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (onDamageTook != null)
        {
            onDamageTook.Invoke();
        }
    }
    public override void Die()
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer("DeadPlayer");
        gameObject.layer = LayerIgnoreRaycast;
        Debug.Log("Current layer: " + gameObject.layer);
    }
}

