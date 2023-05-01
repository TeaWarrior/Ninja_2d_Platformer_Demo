
using UnityEngine;

public class SpeedBuff :  MonoBehaviour, IGiveBuff  
{
    [SerializeField] float forcePower;
    [SerializeField] GameObject VFX_Prefab;
    public void UseBuff(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * forcePower);
        player.GetComponent<Player_Controlls>().SetAirControl(2f);
        GameObject VFX = Instantiate(VFX_Prefab, transform.position, Quaternion.identity);
        Destroy(VFX, 2f);
        Destroy(gameObject);
    }

   

}
