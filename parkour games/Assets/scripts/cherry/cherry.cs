using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cherry : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cherry"))
        {
            GameObject cherry = collision.gameObject;


            // ����ӣ�ҵĸ�����Ϊ��ң�ʹ���������ƶ�
            cherry.transform.SetParent(transform);

            // ���ӣ�Ҹ���ű����������̬λ��
            CherryFollower follower = cherry.AddComponent<CherryFollower>();
            follower.player = this.transform;
            follower.offset = new Vector2(0.2f, -0.1f); // �������ֵ������ӣ�ҵ����λ��

            // ��Сӣ�ҵĴ�С����������Ϊԭ����һ�룬�ɰ������
            cherry.transform.localScale = new Vector2(0.8f, 0.8f);

            // ����ӣ�ҵ���ײ�壬�����ظ������Լ�������ƶ���ɸ���
            Collider2D cherryCollider = cherry.GetComponent<Collider2D>();
            if (cherryCollider != null)
            {
                cherryCollider.enabled = false;
            }
            
        }
    }





}
