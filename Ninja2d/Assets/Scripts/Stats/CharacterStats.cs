using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public float currentHealth { get; private set; }
    public float maxHealth;
    public Stat damage;
    public Stat armor;
    public Stat additionalHealth;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Died");
        
        Destroy(gameObject);
       

    }

   
}
