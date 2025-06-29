using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useCherry : MonoBehaviour
{
    // Start is called before the first frame update
    
    private isAliveManager isAlive;

    void Start()
    {
        isAlive = isAliveManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // ���W������
        if (Input.GetKeyDown(KeyCode.W) && HasCherryChild())
        {
            UseCherry();
        }
    }

    
    private void UseCherry()
    {
        // 切换陷阱状态 - 这会自动触发OnTrapsStateChanged事件
        isAlive.TrapsAreAlive = !isAlive.TrapsAreAlive;
        
        // 销毁樱桃子物体（使用樱桃后移除樱桃）
        DestroyCherryChildren();

        // 3. 可以添加使用樱桃的动画效果（可选）
        // GetComponent<Animator>().SetTrigger("UseCherry");
        
        Debug.Log($"使用樱桃 - 陷阱状态: {(isAlive.TrapsAreAlive ? "激活" : "停用")}");
    }

    // ǷΪ"cherry"��������
    private bool HasCherryChild()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("cherry"))
            {
                return true; // �ҵ�ӣ��������
            }
        }
        return false; // δ�ҵ�
    }

    private void DestroyCherryChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("cherry"))
            {
                Destroy(child.gameObject);
            }
        }
    }

}
