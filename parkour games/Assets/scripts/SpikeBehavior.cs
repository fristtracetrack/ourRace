using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�

public class SpikeBehavior : MonoBehaviour
{
    public bool isAlive = true;
    private bool isFollowing = false;
    private Transform target;

    // ������box��TextMeshPro����
    public TextMeshPro boxTextMesh;

    void Update()
    {
        // ���box��������
        if (boxTextMesh != null && boxTextMesh.text == "alive")
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
        }

        if (isFollowing && isAlive)
        {
            transform.position = new Vector2(target.position.x, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFollowing && isAlive)
        {
            target = collision.transform;
            isFollowing = true;
        }
    }
}