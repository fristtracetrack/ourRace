using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMPVertexLegAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textMeshPro;
    public float stretchLength = 20f; // 腿部最大拉伸长度
    public float walkSpeed = 2f;      // 行走速度
    private bool isWalking = false;

    void Reset()
    {
        // 自动赋值TextMeshPro组件
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (isWalking)
        {
            AnimateLegs();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isWalking = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isWalking = false;
        // 恢复原始mesh
        textMeshPro.ForceMeshUpdate();
    }

    void AnimateLegs()
    {
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            char c = textMeshPro.text[i];
            if (c == 'y' || c == 'g')
            {
                int vertexIndex = charInfo.vertexIndex;
                int materialIndex = charInfo.materialReferenceIndex;
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                // 计算腿部动画偏移
                float offset = Mathf.Abs(Mathf.Sin(Time.time * walkSpeed + i)) * stretchLength;

                // 下半部分两个顶点（左下、右下）向下拉伸
                vertices[vertexIndex + 2].y -= offset;
                vertices[vertexIndex + 3].y -= offset;
            }
        }

        // 应用修改
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
} 