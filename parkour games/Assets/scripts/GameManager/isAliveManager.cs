using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAliveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static isAliveManager Instance;  // 单例实例

    // 陷阱是否存活的全局状态
    private bool _trapsAreAlive = true;
    public bool TrapsAreAlive
    {
        get => _trapsAreAlive;
        set
        {
            _trapsAreAlive = value;
            // 可以在这里添加事件通知，让所有陷阱自动响应状态变化
            // 例如：OnTrapsStateChanged?.Invoke(value);
            OnTrapsStateChanged?.Invoke(value);
        }
    }

    public System.Action<bool> OnTrapsStateChanged;

    private void Awake()
    {
        // 单例模式实现
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
