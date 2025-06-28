using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pillMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 11f; //��Ծ���ٶ�
    [SerializeField] private float gravityScale = 3f; //������������
    [SerializeField] private float walkSpeed = 14f; //�����ٶ�

    [SerializeField] private LayerMask groundLayer;//��������ͼ��

    private Rigidbody2D rd;
    private BoxCollider2D coll;
    private SpriteRenderer sr; //������ת
    private Animator an; //��������
    private float horizontalInput; //ˮƽ��������

    int jumpCount = 0;//��Ծ������
    int jumpMax = 1;//���ƶ�����

    private enum MovementState { idle, run, jump, fall, doubleJump};

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = gravityScale;
        rd.freezeRotation = true; //������ת

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

        //��Ծ
        if (Input.GetButtonDown("Jump"))
        {
            if(isGround() || jumpCount < jumpMax)
            {
                jump();
            }        
        }

        //�����ƶ�
        horizontalInput = Input.GetAxisRaw("Horizontal"); //��ȡˮƽ����



        UpdateAnimationState(); //���¶���״̬
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

