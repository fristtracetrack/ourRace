using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Animator flag_anim; // 旗子的Animator组件
    private bool hasRaised = false; // 避免重复触发


    private void Start()
    {
        flag_anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // 如果旗子已经升起，则不再触发
        if (hasRaised) return;
        // 检查进入触发器的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 触发旗子升起动画
            flag_anim.SetTrigger("Raise");
            hasRaised = true; // 标记为已升起
        }
    }
}
