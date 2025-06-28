using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFaceContorller : MonoBehaviour
{
    [Header("动画控制")]
    public Animator monsterAnimator;  // 怪物的Animator组件
    [SerializeField] private float minBlinkInterval = 2f; // 眨眼最小间隔
    [SerializeField] private float maxBlinkInterval = 5f; // 眨眼最大间隔
    [Header("跟随设置")]
    [SerializeField] private float detectionRadius = 5f; // 检测半径
    [SerializeField] private float followSpeed = 3f;    // 跟随速度
    [SerializeField] private float stoppingDistance = 1f; // 停止跟随距离
    [Header("组件引用")]
    [SerializeField] private Transform player; // 玩家参考
    [SerializeField] private Rigidbody2D rb;   // 怪物的刚体


    private Vector2 initialPosition; // 初始位置
    private bool isFollowing = false; // 是否正在跟随
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        monsterAnimator = GetComponent<Animator>();
        StartCoroutine(BlinkRoutine());
        initialPosition = transform.position;
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
    }
    void OnDestroy()
    {
        // 取消订阅以避免内存泄漏
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
    }

    IEnumerator BlinkRoutine()
    {
        while (true)  // 无限循环
        {
            // 等待随机时间间隔
            yield return new WaitForSeconds(Random.Range(minBlinkInterval, maxBlinkInterval));

            // 触发眨眼动画
            if (monsterAnimator != null)
            {
                monsterAnimator.SetTrigger("blink");
            }
        }
    }

    void Update()
    {
        // 确保玩家存在
        if (player == null) return;

        // 计算与玩家的距离
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 检测玩家是否进入范围
        if (distanceToPlayer <= detectionRadius && !isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            StartFollowing();
        }

        // 当玩家离开检测范围时停止跟随
        if (isFollowing && distanceToPlayer > detectionRadius * 1.2f || !isAliveManager.Instance.TrapsAreAlive)
        {
            StopFollowing();
        }

    }
    void FixedUpdate()
    {
        // 跟随逻辑在FixedUpdate中处理（物理更新）
        if (isFollowing && player != null && isAliveManager.Instance.TrapsAreAlive)
        {
            FollowPlayer();
        }
    }
    private void StartFollowing()
    {
        isFollowing = true;
    }
    private void StopFollowing()
    {
        isFollowing = false;

        // 停止移动
        rb.velocity = Vector2.zero;
    }
    private void FollowPlayer()
    {
        // 计算朝向玩家的方向
        Vector2 direction = (player.position - transform.position).normalized;

        // 如果距离大于停止距离，则移动
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > stoppingDistance)
        {
            rb.velocity = direction * followSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        
        
    }
    private void OnTrapsStateChanged(bool newState)
    {
        if (newState) // 当陷阱重新激活时
        {
            // 重置跟踪状态和目标
            isFollowing = false; 
        }
    }


}
