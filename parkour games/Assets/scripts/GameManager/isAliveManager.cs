using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAliveManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static isAliveManager Instance;  // ����ʵ��

    // �����Ƿ����ȫ��״̬
    private bool _trapsAreAlive = true;
    public bool TrapsAreAlive
    {
        get => _trapsAreAlive;
        set
        {
            _trapsAreAlive = value;
            // ��������������¼�֪ͨ�������������Զ���Ӧ״̬�仯
            // ���磺OnTrapsStateChanged?.Invoke(value);
            OnTrapsStateChanged?.Invoke(value);
        }
    }

    public System.Action<bool> OnTrapsStateChanged;

    private void Awake()
    {
        // ����ģʽʵ��
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
