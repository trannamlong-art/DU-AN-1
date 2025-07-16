using UnityEngine;

public class BlockSound : MonoBehaviour
{
    public AudioClip moveClip;
    public AudioClip rotateClip;
    public AudioClip dropClip;
    public AudioClip lineClearClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoveSound()
    {
        PlaySound(moveClip);
    }

    public void PlayRotateSound()
    {
        PlaySound(rotateClip);
    }

    public void PlayDropSound()
    {
        PlaySound(dropClip);
    }

    public void PlayLineClearSound()
    {
        PlaySound(lineClearClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
