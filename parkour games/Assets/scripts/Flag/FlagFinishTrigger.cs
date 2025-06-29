using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagFinishTrigger : MonoBehaviour
{
    [Header("组件引用")]
    public Animator flagAnimator; // 旗子的Animator组件
    public Collider2D flagCollider; // 旗子的碰撞器组件
    
    [Header("动画设置")]
    public string finishAnimationName = "finshi"; // finish动画的名称
    
    [Header("场景设置")]
    public int nextSceneIndex = -1; // 下一个场景的索引，-1表示自动下一个场景
    public float sceneTransitionDelay = 1f; // 场景切换延迟时间
    
    [Header("触发设置")]
    public float contactCooldown = 0.5f; // 接触冷却时间，防止重复触发
    
    private bool isTransitioning = false; // 是否正在切换场景
    private float lastContactTime = 0f; // 上次接触时间
    
    void Start()
    {
        if (flagAnimator == null)
            flagAnimator = GetComponent<Animator>();
        if (flagCollider == null)
            flagCollider = GetComponent<Collider2D>();
        if (flagCollider != null)
            flagCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (Time.time - lastContactTime < contactCooldown)
            return;
        lastContactTime = Time.time;

        // 只判断是否在finish动画
        if (IsFinishAnimationPlaying())
        {
            Debug.Log("玩家接触旗子，触发场景切换");
            StartSceneTransition();
        }
        else
        {
            Debug.Log("旗子未处于finish动画，无法触发场景切换");
        }
    }
    
    private bool IsFinishAnimationPlaying()
    {
        if (flagAnimator == null) return false;
        return flagAnimator.GetCurrentAnimatorStateInfo(0).IsName(finishAnimationName);
    }
    
    private void StartSceneTransition()
    {
        if (isTransitioning) return;
        isTransitioning = true;
        StartCoroutine(TransitionToNextScene());
    }
    
    IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(sceneTransitionDelay);
        int targetSceneIndex = nextSceneIndex;
        if (targetSceneIndex == -1)
            targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.LogWarning($"场景索引 {targetSceneIndex} 无效，无法切换场景");
            isTransitioning = false;
        }
    }
} 