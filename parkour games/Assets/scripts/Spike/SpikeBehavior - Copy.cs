using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

public class SpikeBehavior : MonoBehaviour
{
    private bool isFollowing = false;
    private Transform target;

    void Start()
    {
        // 订阅陷阱状态变化事件
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
    }

    void OnDestroy()
    {
        // 取消订阅以避免内存泄漏
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
    }

    void Update()
    {

        if (isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            target = collision.transform;
            isFollowing = true;
        }
    }

    // 陷阱状态变化时的回调
    private void OnTrapsStateChanged(bool newState)
    {
        if (newState) // 当陷阱重新激活时
        {
            // 重置跟踪状态和目标
            isFollowing = false;
            target = null;
        }
    }
}
