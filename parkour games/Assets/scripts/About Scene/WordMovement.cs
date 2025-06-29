using System.Collections;
using UnityEngine;

public class WordMover : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float targetY = -100f; // Ŀ��Yλ��
    public float moveSpeed = 100f; // �ƶ��ٶ�
    public float delayBeforeMove = 0.5f; // ��ʼ�ƶ�ǰ���ӳ�ʱ��

    [Header("�������")]
    public RectTransform wordRectTransform; // Word�����RectTransform���

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // ���û��ָ��RectTransform�����Դӵ�ǰ�����ȡ
        if (wordRectTransform == null)
        {
            wordRectTransform = GetComponent<RectTransform>();
        }

        // �������û���ҵ������Դ��Ӷ����в���
        if (wordRectTransform == null)
        {
            wordRectTransform = GetComponentInChildren<RectTransform>();
        }

        if (wordRectTransform != null)
        {
            // ��¼��ʼλ��
            startPosition = wordRectTransform.anchoredPosition;

            // ����Ŀ��λ�ã�����X��Z���䣬ֻ�ı�Y��
            targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

            // ��ʼ�ƶ�Э��
            StartCoroutine(MoveWord());
        }
        else
        {
            Debug.LogError("WordMover: �޷��ҵ�RectTransform�����");
        }
    }

    IEnumerator MoveWord()
    {
        // �ȴ�ָ�����ӳ�ʱ��
        if (delayBeforeMove > 0)
        {
            yield return new WaitForSeconds(delayBeforeMove);
        }

        isMoving = true;
        Debug.Log($"��ʼ�ƶ�Word����� {startPosition} �� {targetPosition}");

        // ƽ���ƶ�
        float elapsedTime = 0f;
        float moveDuration = Vector3.Distance(startPosition, targetPosition) / moveSpeed;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            // ʹ��ƽ����ֵ
            wordRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, progress);

            yield return null;
        }

        // ȷ������λ�þ�ȷ
        wordRectTransform.anchoredPosition = targetPosition;
        isMoving = false;

        Debug.Log("Word�����ƶ���ɣ�");
    }

    // �����������ֶ���ʼ�ƶ�
    public void StartMoving()
    {
        if (!isMoving && wordRectTransform != null)
        {
            StartCoroutine(MoveWord());
        }
    }

    // ��������������Ŀ��λ��
    public void SetTargetPosition(float newTargetY)
    {
        targetY = newTargetY;
        targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);
    }

    // ���������������ƶ��ٶ�
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // �������������õ���ʼλ��
    public void ResetToStart()
    {
        if (wordRectTransform != null)
        {
            wordRectTransform.anchoredPosition = startPosition;
            isMoving = false;
        }
    }
}