using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    bool isOpened;
    public List<Item> chestItems;
    public GameObject itemPrephab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChest()
    {
        spriteRenderer.sprite = sprite;
        isOpened = true;
        foreach (Item item in chestItems)
        {
            GameObject itemGo = Instantiate(itemPrephab, transform.position, Quaternion.identity);
            ItemPick itemPick = itemGo.GetComponent<ItemPick>();
            itemPick.item = item;
            itemPick.isSummoned = true;
            itemPick.SetIcon();
            StartCoroutine(itemGoCorotine(itemPick));

        }
    }

    IEnumerator itemGoCorotine(ItemPick itemPick)
    {
        yield return new WaitForSeconds(1f);
        itemPick.isSummoned = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isOpened)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OpenChest();
            }
        }
       
    }
}
