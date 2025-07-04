using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControlledObject : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 2f;        // 移动速度
    [SerializeField] private float moveDistance = 10f;    // 移动距离
    [SerializeField] private bool moveOnStart = false;    // 是否在开始时移动

    [Header("组件引用")]
    [SerializeField] private Rigidbody2D rb;              // 刚体组件

    private Vector2 startPosition;                        // 初始位置
    private Vector2 endPosition;                           // 终点位置
    private Vector2 targetPosition;                       // 目标位置
    private bool isMoving = false;                        // 是否正在移动
    private bool movingToEnd = true;                       // 当前是否朝终点移动

    void Start()
    {
        // 获取刚体组件
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // 记录初始位置
        startPosition = transform.position;
        endPosition = startPosition + Vector2.left * moveDistance;
        targetPosition = endPosition;

        // 设置初始移动状态
        isMoving = moveOnStart;

        // 订阅陷阱状态变化事件
        if (isAliveManager.Instance != null)
        {
            isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
        }
        
        Debug.Log($"CherryControlledObject 初始化完成 - 初始位置: {startPosition}, 目标位置: {targetPosition}");
    }

    void OnDestroy()
    {
        // 取消事件订阅，防止内存泄漏
        if (isAliveManager.Instance != null)
        {
            isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
        }
    }

    void FixedUpdate()
    {
        // 在FixedUpdate中处理物理移动
        if (isMoving)
        {
            MoveObject();
        }
    }

    /// <summary>
    /// 移动物体
    /// </summary>
    private void MoveObject()
    {
        if (rb != null)
        {
            // 计算移动方向
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            // 检查是否到达目标位置
            float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

            if (distanceToTarget > 0.1f)
            {
                // 移动物体
                rb.velocity = direction * moveSpeed;
                Debug.Log($"物体正在移动 - 速度: {rb.velocity}, 距离目标: {distanceToTarget}");
            }
            else
            {
                // 到达目标位置，停止移动
                rb.velocity = Vector2.zero;
                transform.position = targetPosition;
                isMoving = false;
                Debug.Log("物体移动完成");
            }
        }
        else
        {
            Debug.LogWarning("Rigidbody2D 组件未找到！");
        }
    }

    /// <summary>
    /// 陷阱状态变化回调
    /// </summary>
    /// <param name="newState">新的陷阱状态</param>
    private void OnTrapsStateChanged(bool newState)
    {
        // 每次触发都切换目标
        if (!isMoving)
        {
            if (movingToEnd)
            {
                targetPosition = endPosition;
            }
            else
            {
                targetPosition = startPosition;
            }
            isMoving = true;
            movingToEnd = !movingToEnd; // 切换方向
        }
    }

    /// <summary>
    /// 重置物体位置（可选功能）
    /// </summary>
    public void ResetPosition()
    {
        transform.position = startPosition;
        if (rb != null)
            rb.velocity = Vector2.zero;

        isMoving = false;
        Debug.Log("物体位置已重置");
    }

    /// <summary>
    /// 设置移动参数
    /// </summary>
    /// <param name="speed">移动速度</param>
    /// <param name="distance">移动距离</param>
    public void SetMoveParameters(float speed, float distance)
    {
        moveSpeed = speed;
        moveDistance = distance;
        endPosition = startPosition + Vector2.left * moveDistance;
        targetPosition = endPosition;
        Debug.Log($"移动参数已更新 - 速度: {speed}, 距离: {distance}");
    }

    // 在Inspector中显示当前状态
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            // 绘制起始位置
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPosition, 0.5f);

            // 绘制终点位置
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(endPosition, 0.5f);

            // 绘制移动路径
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPosition, endPosition);
        }
    }
}