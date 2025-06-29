using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDarknessController : MonoBehaviour
{
    [Header("屏幕变暗设置")]
    [SerializeField] private Image darknessOverlay;        // 用于变暗的UI遮罩
    [SerializeField] private float fadeDuration = 0.5f;    // 变暗/变亮的持续时间
    [SerializeField] private float maxDarkness = 0.7f;     // 最大暗度 (0-1)
    
    [Header("自动设置")]
    [SerializeField] private bool autoCreateOverlay = true; // 是否自动创建遮罩
    
    private bool isDark = false;                           // 当前是否处于暗状态
    private Coroutine fadeCoroutine;                       // 渐变协程引用

    void Start()
    {
        // 自动创建遮罩UI
        if (autoCreateOverlay && darknessOverlay == null)
        {
            CreateDarknessOverlay();
        }
        
        // 确保遮罩初始状态是透明的
        if (darknessOverlay != null)
        {
            Color color = darknessOverlay.color;
            color.a = 0f;
            darknessOverlay.color = color;
        }
        
        // 订阅陷阱状态变化事件
        if (isAliveManager.Instance != null)
        {
            isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
        }
        
        Debug.Log("ScreenDarknessController 初始化完成");
    }

    void OnDestroy()
    {
        // 取消事件订阅
        if (isAliveManager.Instance != null)
        {
            isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
        }
    }

    /// <summary>
    /// 陷阱状态变化回调 - 当使用樱桃时触发
    /// </summary>
    /// <param name="newState">新的陷阱状态</param>
    private void OnTrapsStateChanged(bool newState)
    {
        // 每次使用樱桃时切换屏幕明暗
        ToggleDarkness();
    }

    /// <summary>
    /// 切换屏幕明暗状态
    /// </summary>
    public void ToggleDarkness()
    {
        if (darknessOverlay == null)
        {
            Debug.LogWarning("DarknessOverlay 未设置！");
            return;
        }

        // 停止之前的渐变协程
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // 开始新的渐变
        if (isDark)
        {
            fadeCoroutine = StartCoroutine(FadeToLight());
        }
        else
        {
            fadeCoroutine = StartCoroutine(FadeToDark());
        }
        
        isDark = !isDark;
        Debug.Log($"屏幕状态切换: {(isDark ? "变暗" : "变亮")}");
    }

    /// <summary>
    /// 渐变到暗色
    /// </summary>
    private IEnumerator FadeToDark()
    {
        Color startColor = darknessOverlay.color;
        Color targetColor = startColor;
        targetColor.a = maxDarkness;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            darknessOverlay.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        darknessOverlay.color = targetColor;
        Debug.Log("屏幕变暗完成");
    }

    /// <summary>
    /// 渐变到亮色
    /// </summary>
    private IEnumerator FadeToLight()
    {
        Color startColor = darknessOverlay.color;
        Color targetColor = startColor;
        targetColor.a = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            darknessOverlay.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        darknessOverlay.color = targetColor;
        Debug.Log("屏幕变亮完成");
    }

    /// <summary>
    /// 自动创建遮罩UI
    /// </summary>
    private void CreateDarknessOverlay()
    {
        // 查找Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            // 如果没有Canvas，创建一个
            GameObject canvasGO = new GameObject("DarknessCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999; // 确保在最上层
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // 创建遮罩Image
        GameObject overlayGO = new GameObject("DarknessOverlay");
        overlayGO.transform.SetParent(canvas.transform, false);
        
        darknessOverlay = overlayGO.AddComponent<Image>();
        darknessOverlay.color = new Color(0f, 0f, 0f, 0f); // 初始透明
        
        // 设置遮罩覆盖整个屏幕
        RectTransform rectTransform = darknessOverlay.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        Debug.Log("自动创建了屏幕遮罩UI");
    }

    /// <summary>
    /// 手动设置遮罩引用
    /// </summary>
    /// <param name="overlay">遮罩Image组件</param>
    public void SetDarknessOverlay(Image overlay)
    {
        darknessOverlay = overlay;
    }

    /// <summary>
    /// 设置变暗参数
    /// </summary>
    /// <param name="duration">渐变持续时间</param>
    /// <param name="darkness">最大暗度</param>
    public void SetDarknessParameters(float duration, float darkness)
    {
        fadeDuration = duration;
        maxDarkness = Mathf.Clamp01(darkness); // 确保在0-1范围内
        Debug.Log($"变暗参数已更新 - 持续时间: {duration}, 最大暗度: {darkness}");
    }
} 