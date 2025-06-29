using UnityEngine;
using TMPro; // 如果用TextMeshPro

public class PlatformShowText : MonoBehaviour
{
    [Header("要显示的文字UI")]
    public GameObject textObject; // 拖到Inspector里，指向你的Text或TMP对象

    private void Start()
    {
        if (textObject != null)
            textObject.SetActive(false); // 初始隐藏
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