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


            // 设置樱桃的父物体为玩家，使其跟随玩家移动
            cherry.transform.SetParent(transform);

            // 添加樱桃跟随脚本组件，处理动态位置
            CherryFollower follower = cherry.AddComponent<CherryFollower>();
            follower.player = this.transform;
            follower.offset = new Vector2(0.2f, -0.1f); // 调整这个值来设置樱桃的相对位置

            // 缩小樱桃的大小，这里设置为原来的一半，可按需调整
            cherry.transform.localScale = new Vector2(0.8f, 0.8f);

            // 禁用樱桃的碰撞体，避免重复触发以及对玩家移动造成干扰
            Collider2D cherryCollider = cherry.GetComponent<Collider2D>();
            if (cherryCollider != null)
            {
                cherryCollider.enabled = false;
            }
            
        }
    }





}
