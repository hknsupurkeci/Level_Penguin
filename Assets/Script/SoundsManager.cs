using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;  // Singleton instance

    public List<AudioClip> jumpSounds;  // Zýplama sesleri
    public AudioClip checkpointSound;  // Checkpoint sesi
    public AudioClip deadSound;  // Ölüm sesi

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern uygulamasý
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Scene deðiþikliklerinde yok olmamasý için
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Ses dosyasýný ve ses seviyesini parametre olarak alýr
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
