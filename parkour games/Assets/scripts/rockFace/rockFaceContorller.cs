using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFaceContorller : MonoBehaviour
{
    [Header("动画控制")]
    public Animator monsterAnimator;  // 怪物的Animator组件
    [SerializeField] private float minBlinkInterval = 2f; // 眨眼最小间隔
    [SerializeField] private float maxBlinkInterval = 5f; // 眨眼最大间隔

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        StartCoroutine(BlinkRoutine());
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
}
