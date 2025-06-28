using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pillMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 11f; //跳跃初速度
    [SerializeField] private float gravityScale = 3f; //控制重力缩放
    [SerializeField] private float walkSpeed = 14f; //行走速度

    [SerializeField] private LayerMask groundLayer;//地面所在图层

    private Rigidbody2D rd;
    private BoxCollider2D coll;
    private SpriteRenderer sr; //画布反转
    private Animator an; //动画控制
    private float horizontalInput; //水平方向输入

    int jumpCount = 0;//跳跃计数器
    int jumpMax = 1;//限制二段跳

    private enum MovementState { idle, run, jump, fall, doubleJump};

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = gravityScale;
        rd.freezeRotation = true; //冻结旋转

        coll = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (isGround())
        {
            jumpCount = 0;
        }

        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
            if(isGround() || jumpCount < jumpMax)
            {
                jump();
            }        
        }

        //左右移动
        horizontalInput = Input.GetAxisRaw("Horizontal"); //获取水平输入



        UpdateAnimationState(); //更新动画状态
    }
    private void FixedUpdate()
    {
        
        rd.velocity = new Vector2(horizontalInput * walkSpeed, rd.velocity.y);
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if(horizontalInput > 0f)
        {
            state = MovementState.run;
            sr.flipX = false;
        }
        else if(horizontalInput < 0f)
        {
            state = MovementState.run;
            sr.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rd.velocity.y > 0.1f && jumpCount == 0)
        {
            state = MovementState.jump;
        }
        else if(rd.velocity.y > 0.1f && jumpCount == 1)
        {
            state = MovementState.doubleJump;
        }
        else if (rd.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        an.SetInteger("state1", (int)state);

    }

    public bool isGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }


    public void jump()
    {
        rd.velocity = new Vector2(rd.velocity.x, jumpForce);
        jumpCount++;
    }
}

