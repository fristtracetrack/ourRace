using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("按钮设置")]
    public UnityEngine.UI.Button startButton;    // 开始按钮
    public UnityEngine.UI.Button quitButton;     // 退出按钮
    
    [Header("场景设置")]
    public string gameSceneName = "1st";         // 游戏场景名称
    
    void Start()
    {
        // 设置按钮监听器
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        
        // 确保鼠标可见（如果在游戏中隐藏了鼠标）
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        Debug.Log("开始游戏！");
        
        // 加载游戏场景
        SceneManager.LoadScene(gameSceneName);
        
        // 或者使用场景索引（如果Build Settings中1st场景是索引1）
        // SceneManager.LoadScene(1);
    }
    
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("退出游戏！");
        
        // 在编辑器中停止播放
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 在构建版本中退出应用
            Application.Quit();
        #endif
    }
    
    /// <summary>
    /// 设置游戏场景名称
    /// </summary>
    public void SetGameSceneName(string sceneName)
    {
        gameSceneName = sceneName;
    }
} 