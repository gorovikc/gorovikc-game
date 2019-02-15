using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _JumpForce = 5.0f;

    [SerializeField]
    private LayerMask _groundlayer;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false;

    private PlayerAnimation _PlayerAnim;
    private SpriteRenderer _PlayerSprite;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _PlayerAnim = GetComponent<PlayerAnimation>();
        _PlayerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {

        float move = Input.GetAxis("Horizontal");
        _grounded = IsGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
       

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()==true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _JumpForce);
            StartCoroutine(ResetJumpRoutine());
            _PlayerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _PlayerAnim.Move(move);
    }

    void Flip(bool faceRight)
    {
        if(faceRight == true)
        {
            _PlayerSprite.flipX = false;
        }
        else if( faceRight == false)
        {
            _PlayerSprite.flipX = true;
        }
    }

    bool IsGrounded()
    {
       RaycastHit2D hitinfo =  Physics2D.Raycast(transform.position,Vector2.down,1f,1<<8);
        Debug.DrawRay(transform.position,Vector2.down,Color.green);
        
         if (hitinfo.collider != null)
            {
            if (_resetJump == false)
               
                _PlayerAnim.Jump(false);
                return true;
             }
            return false;
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);

        _resetJump = false;
    }
}
