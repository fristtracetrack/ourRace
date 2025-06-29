using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private Animator fireAnimator;  // ����ص�Animator���
    [SerializeField] private bool isInitiallyActive = false;  // ��ʼ״̬�Ƿ񼤻�

    [SerializeField] private bool isActive = false;  // ����ص�ǰ״̬
    
    private int cherryUseCount = 0;  // ӣ��ʹ�ô�������

    void Start()
    {
        fireAnimator = GetComponent<Animator>();

        // ��ʼ�������״̬
        SetFireTrapState(isInitiallyActive);

        // ��������״̬�仯�¼�
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;

        // ����ӣ��ʹ���¼�������ӣ�ҽű��е���OnCherryUsed()������
    }

    void OnDestroy()
    {
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
    }

    // ����״̬�仯�ص�
    private void OnTrapsStateChanged(bool newState)
    {
        
        
        if (!newState)
        {

            SetFireTrapState(true);
        }
        // �ڶ���ʹ��ӣ�ң��رջ��ز��ָ����������
        if (newState)
        {
            SetFireTrapState(false);
            cherryUseCount = 0; // ���ü������Ա��´μ�������
        }
    }

    // ӣ��ʹ��ʱ����
    public void OnCherryUsed()
    {
        Debug.Log("OnCherryUsed method called.");
        cherryUseCount++;

        // ��һ��ʹ��ӣ�ң�ǿ�ƿ������أ����۵�ǰ״̬���
        
    }

    // ���û����״̬
    private void SetFireTrapState(bool active)
    {
        isActive = active;

        // ���¶���״̬
        if (fireAnimator != null)
        {
            fireAnimator.SetBool("isActive", isActive);
        }
    }
}