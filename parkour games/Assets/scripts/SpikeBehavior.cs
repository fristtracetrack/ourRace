using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

public class SpikeBehavior : MonoBehaviour
{
    public bool isAlive = true;
    private bool isFollowing = false;
    private Transform target;

    // 新增：box的TextMeshPro引用
    public TextMeshPro boxTextMesh;

    void Update()
    {
        // 检查box文字内容
        if (boxTextMesh != null && boxTextMesh.text == "alive")
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
        }

        if (isFollowing && isAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFollowing && isAlive)
        {
            target = collision.transform;
            isFollowing = true;
        }
    }
}