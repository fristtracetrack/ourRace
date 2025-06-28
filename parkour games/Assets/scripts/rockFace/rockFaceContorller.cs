using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFaceContorller : MonoBehaviour
{
    [Header("��������")]
    public Animator monsterAnimator;  // �����Animator���
    [SerializeField] private float minBlinkInterval = 2f; // գ����С���
    [SerializeField] private float maxBlinkInterval = 5f; // գ�������

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)  // ����ѭ��
        {
            // �ȴ����ʱ����
            yield return new WaitForSeconds(Random.Range(minBlinkInterval, maxBlinkInterval));

            // ����գ�۶���
            if (monsterAnimator != null)
            {
                monsterAnimator.SetTrigger("blink");
            }
        }
    }
}
