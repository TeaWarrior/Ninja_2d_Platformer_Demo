using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="New Ability", menuName = "Ability/New")]
public class Ability : ScriptableObject
{

   public bool isBuff;
   public float  power;
   public  float CouldDown;
   public GameObject EFX;
   public Sprite icon;
    // Start is called before the first frame update
    public virtual void UseAbility()
    {
        Debug.Log("Use");
        
    }



}
