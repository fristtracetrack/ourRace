using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�

public class SpikeBehavior : MonoBehaviour
{
    private bool isFollowing = false;
    private Transform target;

    void Start()
    {
        // ��������״̬�仯�¼�
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
    }

    void OnDestroy()
    {
        // ȡ�������Ա����ڴ�й©
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
    }

    void Update()
    {

        if (isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            target = collision.transform;
            isFollowing = true;
        }
    }

    // ����״̬�仯ʱ�Ļص�
    private void OnTrapsStateChanged(bool newState)
    {
        if (newState) // ���������¼���ʱ
        {
            // ���ø���״̬��Ŀ��
            isFollowing = false;
            target = null;
        }
    }
}
