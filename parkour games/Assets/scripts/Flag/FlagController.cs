using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Animator flag_anim; // 旗子的Animator组件
    private bool hasRaised = false; // 避免重复触发
    private isAliveManager stateManager; // 全局状态管理器

    private void Start()
    {
        flag_anim = GetComponent<Animator>();
        stateManager = isAliveManager.Instance;

        // 初始化旗子状态
        UpdateFlagState(stateManager.TrapsAreAlive);

        // 订阅全局状态变化事件
        stateManager.OnTrapsStateChanged += UpdateFlagState;
    }
    private void OnDestroy()
    {
        // 取消订阅以避免内存泄漏
        stateManager.OnTrapsStateChanged -= UpdateFlagState;
    }

    // 根据全局状态更新旗子状态
    private void UpdateFlagState(bool trapsAlive)
    {
        if (!trapsAlive)
        {
            // 陷阱激活时：旗子降下，禁止通关
            flag_anim.SetBool("down",true); // 假设Animator中有"reset"触发器用于降下旗子
            flag_anim.SetBool("up", false);
            hasRaised = false;
        }
        else
        {
            // 陷阱禁用时：旗子保持当前状态（可能已升起或未升起）
            // 不自动触发升起，需玩家触碰
            // 触发旗子升起动画
            flag_anim.SetBool("up", true);
            flag_anim.SetBool("down", false);
            hasRaised = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果旗子已升起或陷阱处于激活状态，则不触发
        if (!hasRaised || !stateManager.TrapsAreAlive) return;

        // 检查进入触发器的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 触发通关逻辑
            TriggerLevelComplete();
        }
    }
    // 通关逻辑（可根据需求扩展）
    private void TriggerLevelComplete()
    {
        Debug.Log("关卡完成！");
        // 这里可以添加加载下一关、显示结算界面等逻辑
        // 例如：LevelManager.Instance.LoadNextLevel();
    }

}
