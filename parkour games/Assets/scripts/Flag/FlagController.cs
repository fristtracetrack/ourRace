using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Animator flag_anim; // ���ӵ�Animator���
    private bool hasRaised = false; // �����ظ�����
    private isAliveManager isAlive;

    private void Start()
    {
        flag_anim = GetComponent<Animator>();
        isAlive = isAliveManager.Instance;

        // ��ʼ������״̬
        UpdateFlagState(isAlive.TrapsAreAlive);

        // ����ȫ��״̬�仯�¼�
        isAlive.OnTrapsStateChanged += UpdateFlagState;
    }
    private void OnDestroy()
    {
        // ȡ�������Ա����ڴ�й©
        isAlive.OnTrapsStateChanged -= UpdateFlagState;
    }

    private void UpdateFlagState(bool trapsAlive)
    {

        if (!trapsAlive)
        {
            // ���弤��ʱ�����ӽ��£���ֹͨ��
            flag_anim.SetBool("down", true);
            flag_anim.SetBool("up", false);
            hasRaised = false;
        }
        else
        {
            // �����������𶯻�
            flag_anim.SetBool("up",true);
            flag_anim.SetBool("down", false);
            hasRaised = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // �����������������崦�ڼ���״̬���򲻴���
        if (hasRaised || isAlive.TrapsAreAlive) return;

        // �����봥�����Ķ����Ƿ������
        if (other.CompareTag("Player"))
        {
            
            // ����ͨ���߼�
            TriggerLevelComplete();
        }
    }

    // ͨ���߼����ɸ���������չ��
    private void TriggerLevelComplete()
    {
        Debug.Log("�ؿ���ɣ�");
        // ���������Ӽ�����һ�ء���ʾ���������߼�
        // ���磺LevelManager.Instance.LoadNextLevel();
    }
}
