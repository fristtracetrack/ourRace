using UnityEngine;
using TMPro; // �����TextMeshPro

public class PlatformShowText : MonoBehaviour
{
    [Header("Ҫ��ʾ������UI")]
    public GameObject textObject; // �ϵ�Inspector�ָ�����Text��TMP����

    private void Start()
    {
        if (textObject != null)
            textObject.SetActive(false); // ��ʼ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textObject != null)
        {
            textObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textObject != null)
        {
            textObject.SetActive(false);
        }
    }
}