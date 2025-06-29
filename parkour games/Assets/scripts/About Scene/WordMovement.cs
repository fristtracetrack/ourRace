using System.Collections;
using UnityEngine;

public class WordMover : MonoBehaviour
{
    [Header("移动设置")]
    public float targetY = -100f; // 目标Y位置
    public float moveSpeed = 100f; // 移动速度
    public float delayBeforeMove = 0.5f; // 开始移动前的延迟时间

    [Header("组件引用")]
    public RectTransform wordRectTransform; // Word对象的RectTransform组件

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // 如果没有指定RectTransform，尝试从当前对象获取
        if (wordRectTransform == null)
        {
            wordRectTransform = GetComponent<RectTransform>();
        }

        // 如果还是没有找到，尝试从子对象中查找
        if (wordRectTransform == null)
        {
            wordRectTransform = GetComponentInChildren<RectTransform>();
        }

        if (wordRectTransform != null)
        {
            // 记录起始位置
            startPosition = wordRectTransform.anchoredPosition;

            // 设置目标位置（保持X和Z不变，只改变Y）
            targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

            // 开始移动协程
            StartCoroutine(MoveWord());
        }
        else
        {
            Debug.LogError("WordMover: 无法找到RectTransform组件！");
        }
    }

    IEnumerator MoveWord()
    {
        // 等待指定的延迟时间
        if (delayBeforeMove > 0)
        {
            yield return new WaitForSeconds(delayBeforeMove);
        }

        isMoving = true;
        Debug.Log($"开始移动Word对象从 {startPosition} 到 {targetPosition}");

        // 平滑移动
        float elapsedTime = 0f;
        float moveDuration = Vector3.Distance(startPosition, targetPosition) / moveSpeed;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            // 使用平滑插值
            wordRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, progress);

            yield return null;
        }

        // 确保最终位置精确
        wordRectTransform.anchoredPosition = targetPosition;
        isMoving = false;

        Debug.Log("Word对象移动完成！");
    }

    // 公共方法：手动开始移动
    public void StartMoving()
    {
        if (!isMoving && wordRectTransform != null)
        {
            StartCoroutine(MoveWord());
        }
    }

    // 公共方法：设置目标位置
    public void SetTargetPosition(float newTargetY)
    {
        targetY = newTargetY;
        targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);
    }

    // 公共方法：设置移动速度
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // 公共方法：重置到起始位置
    public void ResetToStart()
    {
        if (wordRectTransform != null)
        {
            wordRectTransform.anchoredPosition = startPosition;
            isMoving = false;
        }
    }
}