using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [Header("火机关设置")]
    [SerializeField] private Animator fireAnimator;  // 火机关的Animator组件
    [SerializeField] private bool isInitiallyActive = false;  // 初始状态是否激活

    [SerializeField] private bool isActive = false;  // 火机关当前状态
    
    private int cherryUseCount = 0;  // 樱桃使用次数计数

    void Start()
    {
        fireAnimator = GetComponent<Animator>();

        // 初始化火机关状态
        SetFireTrapState(isInitiallyActive);

        // 订阅陷阱状态变化事件
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;

        // 订阅樱桃使用事件（需在樱桃脚本中调用OnCherryUsed()方法）
    }

    void OnDestroy()
    {
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
    }

    // 陷阱状态变化回调
    private void OnTrapsStateChanged(bool newState)
    {
        
        
        if (!newState)
        {

            SetFireTrapState(true);
        }
        // 第二次使用樱桃：关闭机关并恢复受陷阱控制
        if (newState)
        {
            SetFireTrapState(false);
            cherryUseCount = 0; // 重置计数，以便下次继续交替
        }
    }

    // 樱桃使用时触发
    public void OnCherryUsed()
    {
        Debug.Log("OnCherryUsed method called.");
        cherryUseCount++;

        // 第一次使用樱桃：强制开启机关，无论当前状态如何
        
    }

    // 设置火机关状态
    private void SetFireTrapState(bool active)
    {
        isActive = active;

        // 更新动画状态
        if (fireAnimator != null)
        {
            fireAnimator.SetBool("isActive", isActive);
        }
    }
}