using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControll : MonoBehaviour
{
    DistanceJoint2D _distanceJoint;
    [SerializeField] GameObject _anchorPoint;
    [SerializeField] SpriteRenderer _anchorSprite;
    [SerializeField] string _tagCollision;
    [SerializeField] Transform checkGround;
    [SerializeField] Transform frontPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject DeathVFX_Prefab;
    private Rigidbody2D _playerRigidbody2d;
    bool isLost;
    bool isGameStart;
    bool isAnchorPuted;
    public event EventHandler OnPlayerDie;
    [SerializeField] Transform playerGraphix;
    [SerializeField] GameObject _lineRenderer_Game_Object;
   
    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint = GetComponent<DistanceJoint2D>();

        _playerRigidbody2d = GetComponent<Rigidbody2D>();
        RemoveAnchor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isGameStart)
            {
                GameStart();
            }
            else
            {
                if (!isAnchorPuted)
                {
                    PutTheAnchor();
                }
                else
                {
                    RemoveAnchor();
                }
               
            }
        }

        if (isGameStart)
        {
           // MovePlayer();

        }
        if (IsGrounded() && isGameStart)
        {
            MovePlayer();
            RemoveAnchor();
        }

     

    }

    float k = 0.001f;
    private void FixedUpdate()
    {
        if (isAnchorPuted)
        {
            Vector2 lookDir = _anchorPoint.transform.position - transform.position;
            float angel = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            _playerRigidbody2d.rotation = angel;
            if (_playerRigidbody2d.velocity.x > 0f && _playerRigidbody2d.velocity.y < 0f)
            {
                playerGraphix.localScale = Vector3.one;
            }
            if(_playerRigidbody2d.velocity.x<0f && _playerRigidbody2d.velocity.y < 0f)
            {
                playerGraphix.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }


    public void PutTheAnchor()
    {
        //mousePosition 
        _distanceJoint.enabled = true;
        _anchorSprite.enabled = true;
        isAnchorPuted = true;
        Vector3 mousePosition = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(mousePosition);
        //---------
        _anchorPoint.transform.position = new Vector3 (mousePosition.x, mousePosition.y, transform.position.z);
        _playerRigidbody2d.AddForce((frontPoint.position - transform.position) * moveSpeed *3f);
        _lineRenderer_Game_Object.SetActive(true);
    }

    public void RemoveAnchor()
    {
        _distanceJoint.enabled = false;
        _anchorSprite.enabled = false;
        isAnchorPuted = false;
        _playerRigidbody2d.rotation=0f;
        _lineRenderer_Game_Object.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tagCollision))
        {

            if (!isLost)
            {
                LostGameCondition();
            }

            isLost = true;


        }
    }

    public void MovePlayer()
    {
        
        _playerRigidbody2d.AddForce ((frontPoint.position - transform.position) * moveSpeed*Time.deltaTime);
    }





    private bool IsGrounded()
    {
       RaycastHit2D raycastHit2D =   Physics2D.Raycast(transform.position, checkGround.position - transform.position, 1f,GroundLayer);
        Debug.DrawRay(transform.position, checkGround.position - transform.position);
        
        return raycastHit2D.collider != null;
    }
    
    public void GameStart()
    {
        isGameStart = true;
        _playerRigidbody2d.gravityScale = 1f;
        
    }

    public void LostGameCondition()
    {
        if (OnPlayerDie != null)
        {
            OnPlayerDie(this, EventArgs.Empty);
        }
        GameObject VFX = Instantiate(DeathVFX_Prefab, transform.position, Quaternion.identity);
        Destroy(VFX, 2f);
        gameObject.SetActive(false);

    }

    
}
