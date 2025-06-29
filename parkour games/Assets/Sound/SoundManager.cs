using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public static SoundPlay Instance;
    [SerializeField] private AudioSource sfxSource; // “Ù–ß‘¥

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}