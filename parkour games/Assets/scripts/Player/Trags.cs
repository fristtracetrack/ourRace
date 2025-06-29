using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trags : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator an;
    [SerializeField] private AudioClip deathSound; // 死亡音效

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
        // 播放死亡音效
        if (deathSound != null)
            SoundPlay.Instance.PlaySound(deathSound);

        an.SetTrigger("death");
        // 根据音效长度设置延迟（假设死亡音效长度为3秒）
        float delayTime = deathSound.length + 0.5f; // 额外增加0.5秒缓冲
        Invoke("RestartLevel", delayTime);

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
