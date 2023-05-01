using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tempSkill : MonoBehaviour
{
    public Transform player;
    [SerializeField] Joystick _joystick;
    [SerializeField] Ability ability;
    bool isUsing;
    bool isCD;
    public Image skill_icon;
    
    // Start is called before the first frame update
    void Start()
    {
        skill_icon.sprite = ability.icon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCD)
        {
            if (_joystick.Horizontal > 0.5f || _joystick.Horizontal < -0.5f || _joystick.Vertical > 0.5f || _joystick.Vertical < -0.5f)
            {
                Debug.Log("yes");
                isUsing = true;
                if (ability.isBuff)
                {
                    ability.UseAbility();
                    isCD = true;
                    isUsing = false;
                    StartCoroutine(SkillCD_corotine(ability.CouldDown));

                }

            }
            if (isUsing)
            {
                if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
                {
                    isUsing = false;
                    isCD = true;
                   StartCoroutine( SkillCD_corotine(ability.CouldDown));
                    Debug.Log("skill");
                }
                Debug.Log("omg");
            }
        }
        else
        {
            
            skill_icon.fillAmount += 1 / ability.CouldDown * Time.deltaTime;
        }
      
        
    }


    IEnumerator SkillCD_corotine(float seconds)
    {
        skill_icon.fillAmount = 0;
        GameObject _exf = Instantiate(ability.EFX, player.transform.position, Quaternion.identity);
        Destroy(_exf, 2f);
        yield return new WaitForSeconds(seconds);
        isCD = false;
        Debug.Log("COROTINE!");
       
    }


    
}
