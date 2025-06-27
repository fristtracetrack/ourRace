using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    //是否跟随
    public bool isAlive = true;
    private bool isFollowing = false;

    //跟随的角色
    private Transform target;

    //检查是否跳过尖刺
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isFollowing)
        {
            //获取玩家
            target = collision.transform;
            //激活跟随
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
