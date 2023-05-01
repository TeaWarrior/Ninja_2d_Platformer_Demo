using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public GameObject itemPrephab;
   

    public override void Die()
    {
        
       
            Loot();

        
        Destroy(gameObject);


    }

    public List<Item> lootItems;
    public void Loot()
    {
        foreach (var item in lootItems)
        {
            GameObject itemGo = Instantiate(itemPrephab, transform.position, Quaternion.identity);
            ItemPick itemPick = itemGo.GetComponent<ItemPick>();
            itemPick.item = item;
            itemPick.isSummoned = true;
            itemPick.SetIcon();
            itemPick.IsSummoned();
        }
    }

    
}
