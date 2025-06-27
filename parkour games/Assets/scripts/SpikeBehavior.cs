using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    //�Ƿ����
    public bool isAlive = true;
    private bool isFollowing = false;

    //����Ľ�ɫ
    private Transform target;

    //����Ƿ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isFollowing)
        {
            //��ȡ���
            target = collision.transform;
            //�������
            isFollowing = true;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing && isAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
        
    }


    

}
