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
        // 检测W键按下
        if (Input.GetKeyDown(KeyCode.W) && HasCherryChild())
        {
            UseCherry();
        }
    }

    
    private void UseCherry()
    {
        if (isAlive.TrapsAreAlive)
        {
            isAlive.TrapsAreAlive = false;
        }
        else
        {
            isAlive.TrapsAreAlive = true;
        }
        // 销毁所有樱桃子物体（可选：如果想使用后移除樱桃）
        DestroyCherryChildren();

        // 3. 播放使用动画或特效（可选）
        // GetComponent<Animator>().SetTrigger("UseCherry");
    }

    // 检查是否有名为"cherry"的子物体
    private bool HasCherryChild()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("cherry"))
            {
                return true; // 找到樱桃子物体
            }
        }
        return false; // 未找到
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
