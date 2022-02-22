using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource bgIntro;
    public AudioSource bgLoop;
    public AudioSource buttonSound;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMusic()
    {
        if (bgIntro.isPlaying || bgLoop.isPlaying)
            return;
        bgIntro.Play();
        bgLoop.PlayDelayed(bgIntro.clip.length);
    }

    public void StopMusic()
    {
        bgLoop.Stop();
    }
}
