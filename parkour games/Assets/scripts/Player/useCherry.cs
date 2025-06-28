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
        if (isAlive.TrapsAreAlive)
        {
            isAlive.TrapsAreAlive = false;
        }
        else
        {
            isAlive.TrapsAreAlive = true;
        }
        // ��������ӣ�������壨��ѡ�������ʹ�ú��Ƴ�ӣ�ң�
        DestroyCherryChildren();

        // 3. ����ʹ�ö�������Ч����ѡ��
        // GetComponent<Animator>().SetTrigger("UseCherry");
    }

    // ����Ƿ�����Ϊ"cherry"��������
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
