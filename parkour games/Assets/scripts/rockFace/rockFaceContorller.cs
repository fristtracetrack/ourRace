using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFaceContorller : MonoBehaviour
{
    [Header("��������")]
    public Animator monsterAnimator;  // �����Animator���
    [SerializeField] private float minBlinkInterval = 2f; // գ����С���
    [SerializeField] private float maxBlinkInterval = 5f; // գ�������
    [Header("��������")]
    [SerializeField] private float detectionRadius = 5f; // ���뾶
    [SerializeField] private float followSpeed = 3f;    // �����ٶ�
    [SerializeField] private float stoppingDistance = 1f; // ֹͣ�������
    [Header("�������")]
    [SerializeField] private Transform player; // ��Ҳο�
    [SerializeField] private Rigidbody2D rb;   // ����ĸ���


    private Vector2 initialPosition; // ��ʼλ��
    private bool isFollowing = false; // �Ƿ����ڸ���
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        monsterAnimator = GetComponent<Animator>();
        StartCoroutine(BlinkRoutine());
        initialPosition = transform.position;
        isAliveManager.Instance.OnTrapsStateChanged += OnTrapsStateChanged;
    }
    void OnDestroy()
    {
        // ȡ�������Ա����ڴ�й©
        isAliveManager.Instance.OnTrapsStateChanged -= OnTrapsStateChanged;
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

    void Update()
    {
        // ȷ����Ҵ���
        if (player == null) return;

        // ��������ҵľ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �������Ƿ���뷶Χ
        if (distanceToPlayer <= detectionRadius && !isFollowing && isAliveManager.Instance.TrapsAreAlive)
        {
            StartFollowing();
        }

        // ������뿪��ⷶΧʱֹͣ����
        if (isFollowing && distanceToPlayer > detectionRadius * 1.2f || !isAliveManager.Instance.TrapsAreAlive)
        {
            StopFollowing();
        }

    }
    void FixedUpdate()
    {
        // �����߼���FixedUpdate�д���������£�
        if (isFollowing && player != null && isAliveManager.Instance.TrapsAreAlive)
        {
            FollowPlayer();
        }
    }
    private void StartFollowing()
    {
        isFollowing = true;
    }
    private void StopFollowing()
    {
        isFollowing = false;

        // ֹͣ�ƶ�
        rb.velocity = Vector2.zero;
    }
    private void FollowPlayer()
    {
        // ���㳯����ҵķ���
        Vector2 direction = (player.position - transform.position).normalized;

        // ����������ֹͣ���룬���ƶ�
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > stoppingDistance)
        {
            rb.velocity = direction * followSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        
        
    }
    private void OnTrapsStateChanged(bool newState)
    {
        if (newState) // ���������¼���ʱ
        {
            // ���ø���״̬��Ŀ��
            isFollowing = false; 
        }
    }


}
