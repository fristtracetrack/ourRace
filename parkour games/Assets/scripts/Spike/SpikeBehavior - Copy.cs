using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

public class SpikeBehavior : MonoBehaviour
{
    public bool isAlive = true;
    private bool isFollowing = false;
    private Transform target;


    void Update()
    {

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