using System.Collections;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public Player_Controlls _playerControlls;

    float horizontalMove = 0f;
    public float runSpeed;
    bool jump = false;

    public Joystick _joystick;

    public Animator _animator;

    public bool isAttack;
    public Transform attackPoint;
    public PlayerStats playerStats;

    public LayerMask enemyLayers;
    public float attackCircleRange;
    public GameObject FX;

    public Transform wallCheck;
    public LayerMask wallLAyer;
    private bool isWallSlide;
    public float wallSlideSpeed;

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position,0.2f, wallLAyer); 
    }

    bool isJumpedFromWall;
    private void WallSlide()
    {
        if(IsWalled() && !_playerControlls.m_Grounded && !isJumpedFromWall)
        {
            _animator.SetBool("isWallSlide", true);
            if (Input.GetButtonDown("Jump"))
            {
                _playerControlls.Jump(300);
                Vector2 backVector = new Vector2(transform.position.x - wallCheck.position.x, 0);
                
                _playerControlls.m_Rigidbody2D.AddForce(backVector.normalized);
                jump = true;
                _animator.SetBool("isJumping", true);
               
                isJumpedFromWall = true;
                StartCoroutine(IsJumpedFromWallCorotine());
                return;
            }

            isWallSlide = true;
            _playerControlls.m_Rigidbody2D.velocity= new Vector2(_playerControlls.m_Rigidbody2D.velocity.x, Mathf.Clamp(_playerControlls.m_Rigidbody2D.velocity.y, - wallSlideSpeed, float.MaxValue));
         


        }
        else
        {
            isWallSlide = false;
            _animator.SetBool("isWallSlide", false);
        }
    }

   IEnumerator IsJumpedFromWallCorotine()
    {
        _animator.SetBool("isWallSlide", false);
        yield return new WaitForSeconds(0.5f);
        isJumpedFromWall = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
        if (!isAttack )
        {
            if (!isAttacking)
            {
#if UNITY_EDITOR

                Move_UnityEditor();


#endif
#if UNITY_ANDROID
                Move_Android_Joystick();
#endif
            }
        }

        else
        {
            Attack();
        }

        WallSlide();
    }

    public void Attack()
    {
        
        StartCoroutine(AttackCorotine(playerStats.attackSpeed.GetValue()));
    }
    bool isAttacking;
    IEnumerator AttackCorotine(float attackSpeed)
    {
        isAttacking = true;
        isAttack = false;
        horizontalMove = 0f;
        Debug.Log("corotine");
        _animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackSpeed/4);
       
        bool playerAttacked = false;
        attackPoint.localPosition = EquipmentManager.instance.attackPointOffset;
       


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackCircleRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (!playerAttacked)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    playerAttacked = true;
                    CharacterStats enemyStats = enemy.gameObject.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(playerStats.damage.GetValue());
                   Debug.Log(playerStats.damage.GetValue());

                }
            }

        }
       
        yield return new WaitForSeconds(attackSpeed / 4);
        attackPoint.localPosition = new Vector3(0, 0, 0);

        Debug.Log("attackFinished");
        _animator.SetBool("isAttacking", false);
         isAttacking = false;
    }
    public void AttackingButton()
    {
        if (!isAttacking)
        {
            isAttack = true;
        }
       
    }
    void Move_UnityEditor()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            _animator.SetBool("isJumping", true);
          
        }
    }

    void Move_Android_Joystick()
    {
        if (_joystick.Horizontal >= 0.2f)
        {
            horizontalMove = 1f;
        }
        if (_joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -1f;
        }
        _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (_joystick.Vertical >= 0.7f)
        {

            if (!jump)
            {
                if (_playerControlls.m_Grounded)
                {
                    if (!isJumped)
                    {
                        jump = true;
                        isJumped = true;
                        StartCoroutine(JumpCorotine());
                        _animator.SetBool("isJumping", true);
                       
                    }
                  
                }

            }
            
        }

        if (_joystick.Vertical <= -0.30f)
        {
            crounch = true;
        }
        if (_joystick.Vertical > -0.30f)
        {
            crounch = false;
        }

        if(!_playerControlls.m_Grounded && !_animator.GetBool("isJumping"))
        {
          //  _animator.SetBool("isFalling", true);
        }
        if (_playerControlls.m_Grounded)
        {
           // _animator.SetBool("isFalling", false);
        }
    }
   public void OnLanding()
    {
        if (!isJumped)
        {
            _animator.SetBool("isJumping", false);
            isJumped = true;
            StartCoroutine(JumpCorotine());
        }
        
    }

    public void OnCrouching( bool isCrouching)
    {
       // _animator.SetBool("isCrouching", isCrouching);
    }

    
    bool isJumped;
    bool crounch;
    IEnumerator JumpCorotine()
    {
        yield return new WaitForSeconds(0.2f);
        isJumped = false;

    }

    private void FixedUpdate()
    {
        _playerControlls.Move(horizontalMove*runSpeed*Time.fixedDeltaTime, crounch,jump);
        jump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackCircleRange);
    }
}
