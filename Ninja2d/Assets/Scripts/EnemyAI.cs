using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    AdditionalManager additionalManager;
    CharacterStats enemyStats;
    public Transform _player;
    public float agroRange, moveSpeed;
    public Animator animator;

    Rigidbody2D rb2d;

    public Transform castPoint,  castPointTop,  castPointBack;
    public Transform patrolPointLeft, patrolPointRight;

    public LayerMask playerLayer;
    public float attackRange;
    public float attackSpeed;

    public Transform attackPoint;


    bool isAtackCd;
    public float attackCircleRange;
    public LayerMask enemyLayers;
    void Start()
    {
        _player = AdditionalManager.instance.playerTransform;
        enemyStats = transform.GetComponent<CharacterStats>();
    }
    private void Awake()
    {
        additionalManager = AdditionalManager.instance;
        
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float distanceToplayer = Vector2.Distance(transform.position, _player.transform.position);

       
            if (CanSeePLayer(agroRange))
            {
                
                         if (distanceToplayer < attackRange)
                         {
                AttackPlayer();
                         }
                   else
                    {

                if (!isAttacking)
                {
                    ChasePlayer();
                }
               
                       }
            }
            else
            {

            if (!isPatroling)
            {
                StopChasingPlayer();
            }
            else
            {
                Patrol();
            }
               
            }
           
        
    }

    bool isFacingForward = true;
    bool isPatroling;
    bool isGointToleft=false;
    public void ChasePlayer()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttack", false);
        animator.Play("Move");
        isPatroling = false;
        
       
        if (transform.position.x < _player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingForward = true;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingForward = false;
        }
    }

    public void StopChasingPlayer()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isAttack", false);

        if (!isPatrolCorotine)
        {
            StartCoroutine(PatrolCorotine());
        }
        
    }
    bool isPatrolCorotine=false;
    public float idleTime;
    IEnumerator PatrolCorotine()
    {

        isPatrolCorotine = true;
        Debug.Log("corotine");
        yield return new WaitForSeconds(idleTime);
        if (!CanSeePLayer(agroRange))
        {
            isPatroling = true;
        }
        isPatrolCorotine = false;

    }
    public void Patrol()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttack", false);
        animator.Play("Move");
        Transform pointToPatrol;
      
        if (!isGointToleft)
        {
            pointToPatrol = patrolPointRight;
        }
        else
        {
            pointToPatrol = patrolPointLeft;
        }
        if (transform.position.x < pointToPatrol.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingForward = true;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingForward = false;
        }
        float distance = Vector2.Distance(transform.position, pointToPatrol.position);
        Debug.Log(distance);
        if (distance < 2.5f)
        {
            
            if (!isGointToleft)
            {
                isGointToleft = true;
            }
            else
            {
                isGointToleft = false;
            }
            isPatroling = false;
        }


    }
    bool isAttacking;
    public void AttackPlayer()
    {
        if (!isAttacking)
        {
            animator.SetBool("isAttack", true);
            isAttacking = true;
        }
        if (!isAtackCd)
        {
            StartCoroutine(attackCourutibe(attackSpeed));
        }
    }
   
    IEnumerator attackCourutibe( float attackspeed)
    {
       
        isAtackCd = true;
        yield return new WaitForSeconds(attackspeed/2);
        bool playerAttacked = false;
       
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackCircleRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (!playerAttacked)
            {
                if (enemy.CompareTag("Player"))
                {
                    playerAttacked = true;
                    PlayerStats playerStats = enemy.gameObject.GetComponent<PlayerStats>();
                    playerStats.TakeDamage(enemyStats.damage.GetValue());
                    

                }
            }

        }
        yield return new WaitForSeconds(attackspeed / 2);
        
        isAtackCd = false; isAttacking = false;
      
    }

    public bool CanSeePLayer( float distance)
    {

        bool val = false;
        bool val1=false, val2=false, val3=false;
        float castDist = distance ;
        int Flip=1;
        if (isFacingForward)
        {
            Flip = 1;
        }
        else
        {
            Flip = -1;
        }

        Vector2 endPos = castPoint.position + Vector3.right * distance*Flip;
        Vector2 endPosTop = castPointTop.position + Vector3.right * distance * Flip;
        Vector2 endPosBack = castPointBack.position - Vector3.right * distance * Flip;
       

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, playerLayer);
        RaycastHit2D hit2 = Physics2D.Linecast(castPointTop.position, endPosTop, playerLayer);
        RaycastHit2D hitBack = Physics2D.Linecast(castPointBack.position, endPosBack, playerLayer);
       
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val1 = true;
            }
            else
            {

                val1 = false;
            }
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
           
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.green);
        
        }
        if (hit2.collider != null)
        {
            if (hit2.collider.CompareTag("Player"))
            {
                val2 = true;
            }
            else
            {
                
                val2 = false;
            }
            
            Debug.DrawLine(castPointTop.position, endPosTop, Color.blue);
        
        }
        else
        {
            
            Debug.DrawLine(castPointTop.position, endPosTop, Color.green);
          
        }
        if (hitBack.collider != null)
        {
            if (hitBack.collider.CompareTag("Player"))
            {
                val3 = true;
            }
            else
            {
                val3 = false;
            }
          
            Debug.DrawLine(castPointBack.position, endPosBack, Color.blue);
        }
        else
        {
            
            Debug.DrawLine(castPointBack.position, endPosBack, Color.green);
        }


        if(val1 || val2 || val3)
        {
            val = true;
        }
        else
        {
            val = false;
        }
        return val;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackCircleRange);
    }
}
