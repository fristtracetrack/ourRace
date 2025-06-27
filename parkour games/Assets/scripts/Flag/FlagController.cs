using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Animator flag_anim; // ���ӵ�Animator���
    private bool hasRaised = false; // �����ظ�����


    private void Start()
    {
        flag_anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // ��������Ѿ��������ٴ���
        if (hasRaised) return;
        // �����봥�����Ķ����Ƿ������
        if (other.CompareTag("Player"))
        {
            // �����������𶯻�
            flag_anim.SetTrigger("Raise");
            hasRaised = true; // ���Ϊ������
        }
    }
}
