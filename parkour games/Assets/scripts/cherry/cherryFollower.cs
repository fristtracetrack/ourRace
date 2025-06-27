using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryFollower : MonoBehaviour
{
    public Transform player;     // ��ҵ�Transform
    public Vector2 offset;       // �����ҵ�ƫ�ƣ�Inspector�����ã�
    [HideInInspector]
    public bool facingRight;     // ��ҳ����Զ����£������ֶ����ã�
    private Rigidbody2D playerRb; // ��ҵ� Rigidbody2D ���

    void Start()
    {
        // ��ȡ��ҵ� Rigidbody2D ���
        playerRb = player.GetComponent<Rigidbody2D>();
        // ��ʼ�����򣨸�����ҳ�ʼ�����жϣ�
        facingRight = playerRb.velocity.x > 0;
        UpdatePosition();
    }

    void Update()
    {
        // ������ҵ�ˮƽ�ٶ��жϳ���
        float horizontalVelocity = playerRb.velocity.x;

        // ʵʱ�����ҳ���仯
        if (Mathf.Abs(horizontalVelocity) > 0.1f)
        {
            bool newFacingRight = horizontalVelocity > 0;
            if (newFacingRight != facingRight)
            {
                facingRight = newFacingRight;
                UpdatePosition();
            }
        }
    }

    void UpdatePosition()
    {
        // ���ݳ�����㷽�򣺳���ʱӣ��������Ҳࣨ-1 �� ʵ������࣬����⡰����߼���
        // ���볯��ʱӣ�����Ҳ࣬��Ϊ direction = facingRight ? 1f : -1f;
        float direction = facingRight ? -1f : 1f;
        transform.localPosition = new Vector2(offset.x * direction, offset.y);
    }
}
