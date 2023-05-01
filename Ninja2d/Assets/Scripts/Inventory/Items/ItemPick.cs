using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour
{

    public Item item;
    public GameObject fxPick;
    public SpriteRenderer itemIcon;
    public bool isSummoned;



    private void Awake()
    {
        SetIcon();
    }

    public void SetIcon()
    {
        itemIcon.sprite = item.icon;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSummoned)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PickUP();
            }
        }
       
    }

    void PickUP( )
    {
        Debug.Log("Picking " + item.itemName);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            GameObject fx = Instantiate(fxPick, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
            Destroy(gameObject);
        }
       
    }

    public void IsSummoned()
    {
        StartCoroutine(itemGoCorotine());
    }
   public IEnumerator itemGoCorotine()
    {
       
        yield return new WaitForSeconds(1f);
        isSummoned = false;
       
    }
}
