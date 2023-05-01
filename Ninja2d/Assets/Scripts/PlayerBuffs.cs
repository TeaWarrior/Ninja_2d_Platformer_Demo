using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    [SerializeField] string _buffTag;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_buffTag))
        {
            collision.GetComponent<IGiveBuff>().UseBuff(gameObject);
        }
    }
}
