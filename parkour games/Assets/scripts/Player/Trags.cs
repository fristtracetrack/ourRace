using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trags : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator an;
    [SerializeField] private AudioClip deathSound; // ������Ч

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    private void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trag") )
        {
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        // ����������Ч
        if (deathSound != null)
            SoundPlay.Instance.PlaySound(deathSound);

        an.SetTrigger("death");
        // ������Ч���������ӳ٣�����������Ч����Ϊ3�룩
        float delayTime = deathSound.length + 0.5f; // ��������0.5�뻺��
        Invoke("RestartLevel", delayTime);

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
