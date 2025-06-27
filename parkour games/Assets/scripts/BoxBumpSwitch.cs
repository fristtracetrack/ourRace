using UnityEngine;
using TMPro;

public class BoxBumpSwitch : MonoBehaviour
{
    public TextMeshPro textMesh; // 拖入TextMeshPro组件
    private string aliveText = "alive";
    private string deadText = "dead";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查碰撞点是否在方块下方
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y)
                {
                    SwitchText();
                    break;
                }
            }
        }
    }

    void SwitchText()
    {
        if (textMesh == null) return;
        if (textMesh.text == aliveText)
            textMesh.text = deadText;
        else
            textMesh.text = aliveText;
    }
}