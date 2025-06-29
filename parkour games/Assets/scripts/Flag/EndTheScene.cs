using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTheScene : MonoBehaviour
{
    [Header("组件引用")]
    public Animator flagAnimator; // 旗子的Animator组件
    public Collider2D flagCollider; // 旗子的碰撞器组件

    [Header("动画设置")]
    public string finishAnimationName = "finshi"; // finish动画的名称

    [Header("场景设置")]
    public int nextSceneIndex = -1; // 下一个场景的索引，-1表示自动下一个场景
    [Range(0f, 3f)]
    public float sceneTransitionDelay = 0.2f; // 场景切换延迟时间（减少默认延迟）
    public bool instantTransition = false; // 是否立即切换场景（无延迟）

    [Header("触发设置")]
    [Range(0.1f, 1f)]
    public float contactCooldown = 0.2f; // 接触冷却时间，防止重复触发（减少默认冷却）

    private bool isFinishAnimationPlaying = false; // 是否正在播放finish动画
    private bool isTransitioning = false; // 是否正在切换场景
    private float lastContactTime = 0f; // 上次接触时间

    // 事件：当旗子状态改变时触发
    public System.Action<bool> OnFlagStateChanged;

    void Start()
    {
        // 如果没有指定Animator，尝试从当前对象获取
        if (flagAnimator == null)
        {
            flagAnimator = GetComponent<Animator>();
        }

        // 如果没有指定Collider，尝试从当前对象获取
        if (flagCollider == null)
        {
            flagCollider = GetComponent<Collider2D>();
        }

        // 确保碰撞器是触发器
        if (flagCollider != null)
        {
            flagCollider.isTrigger = true;
        }

        // 开始监听旗子状态
        StartCoroutine(MonitorFlagState());
    }

    // 监听旗子状态变化
    IEnumerator MonitorFlagState()
    {
        while (true)
        {
            if (flagAnimator != null)
            {
                // 检查是否正在播放finish动画
                bool wasPlayingFinish = isFinishAnimationPlaying;
                isFinishAnimationPlaying = flagAnimator.GetCurrentAnimatorStateInfo(0).IsName(finishAnimationName);

                // 如果状态发生变化，触发事件
                if (wasPlayingFinish != isFinishAnimationPlaying)
                {
                    OnFlagStateChanged?.Invoke(isFinishAnimationPlaying);
                    Debug.Log($"旗子状态变化 - Finish动画: {isFinishAnimationPlaying}");
                }
            }

            yield return new WaitForSeconds(0.05f); // 减少检查间隔，提高响应速度
        }
    }

    // 当玩家进入触发器时
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家
        if (!other.CompareTag("Player"))
            return;

        // 检查冷却时间
        if (Time.time - lastContactTime < contactCooldown)
            return;

        lastContactTime = Time.time;

        // 检查旗子是否可以触发场景切换
        if (CanTriggerSceneTransition())
        {
            Debug.Log("玩家接触旗子，立即触发场景切换");
            StartSceneTransition();
        }
        else
        {
            Debug.Log("旗子未处于finish状态，无法触发场景切换");
        }
    }

    // 检查是否可以触发场景切换
    private bool CanTriggerSceneTransition()
    {
        // 如果正在切换场景，不能重复触发
        if (isTransitioning)
            return false;

        // 旗子必须正在播放finish动画
        if (!isFinishAnimationPlaying)
            return false;

        return true;
    }

    // 开始场景切换
    private void StartSceneTransition()
    {
        if (isTransitioning) return;

        isTransitioning = true;
        Debug.Log("开始切换到下一个场景");

        // 根据设置选择切换方式
        if (instantTransition)
        {
            // 立即切换场景
            TransitionToNextSceneImmediate();
        }
        else
        {
            // 延迟切换场景
            StartCoroutine(TransitionToNextScene());
        }
    }

    // 立即切换到下一个场景
    private void TransitionToNextSceneImmediate()
    {
        // 获取下一个场景的索引
        int targetSceneIndex = nextSceneIndex;

        // 如果指定的是-1，则切换到下一个场景
        if (targetSceneIndex == -1)
        {
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }

        // 检查场景索引是否有效
        if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"立即切换到场景 {targetSceneIndex}");
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.LogWarning($"场景索引 {targetSceneIndex} 无效，无法切换场景");
            isTransitioning = false; // 重置状态，允许重试
        }
    }

    // 切换到下一个场景（延迟版本）
    IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(sceneTransitionDelay);

        // 获取下一个场景的索引
        int targetSceneIndex = nextSceneIndex;

        // 如果指定的是-1，则切换到下一个场景
        if (targetSceneIndex == -1)
        {
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }

        // 检查场景索引是否有效
        if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"切换到场景 {targetSceneIndex}");
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.LogWarning($"场景索引 {targetSceneIndex} 无效，无法切换场景");
            isTransitioning = false; // 重置状态，允许重试
        }
    }

    // 公共方法：手动触发场景切换
    public void ForceTransitionToNextScene()
    {
        StartSceneTransition();
    }

    // 公共方法：立即切换场景（忽略延迟）
    public void ForceTransitionImmediate()
    {
        if (isTransitioning) return;
        
        isTransitioning = true;
        TransitionToNextSceneImmediate();
    }

    // 公共方法：检查旗子是否可以触发
    public bool IsFlagReadyForTransition()
    {
        return CanTriggerSceneTransition();
    }

    // 公共方法：获取finish动画播放状态
    public bool IsFinishAnimationPlaying()
    {
        return isFinishAnimationPlaying;
    }

    // 公共方法：重置切换状态
    public void ResetTransitionState()
    {
        isTransitioning = false;
        lastContactTime = 0f;
    }

    // 公共方法：设置切换延迟
    public void SetTransitionDelay(float delay)
    {
        sceneTransitionDelay = Mathf.Clamp(delay, 0f, 3f);
    }

    // 公共方法：设置是否立即切换
    public void SetInstantTransition(bool instant)
    {
        instantTransition = instant;
    }
}