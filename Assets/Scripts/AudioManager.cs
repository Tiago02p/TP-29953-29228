using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFX;

    // Lista de clipes de áudio de fundo
    public List<AudioClip> backgroundClips;

    public AudioClip buttonSound;
    public AudioClip PowerUpSound;
    public AudioClip HealSound;
    public AudioClip HurtSound;
    public AudioClip breakBrickSound;
    public AudioClip deathSound;
    public AudioClip newLevelSound;
    public AudioClip gameOverSound;

    private void Start()
    {
        PlayRandomBackgroundMusic();
    }

    private void PlayRandomBackgroundMusic()
    {
        AudioClip randomBackgroundClip = backgroundClips[Random.Range(0, backgroundClips.Count)];
        musicSource.clip = randomBackgroundClip;
        musicSource.Play();
        StartCoroutine(PlayNextBackgroundMusic(randomBackgroundClip.length));
    }

    private IEnumerator PlayNextBackgroundMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayRandomBackgroundMusic();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

}
