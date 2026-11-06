using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Gameplay SFX")]
    public AudioClip ballRollClip;
    public AudioClip pinHitClip;
    public AudioClip strikeClip;
    public AudioClip spareClip;

    [Header("Power-Up SFX")]
    public AudioClip powerUpPickupClip;
    public AudioClip powerUpSpawnClip;
    public AudioClip obstacleHitClip;
    public AudioClip mudSplashClip;
    public AudioClip smallBallClip;
    public AudioClip iceSlideClip;

    [Header("UI / Game State SFX")]
    public AudioClip buttonClickClip;
    public AudioClip gameOverClip;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip endMusic;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, float volume = 0.35f)
    {
        if (musicSource == null || clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlayBallRoll() => PlaySFX(ballRollClip, 0.45f);
    public void PlayPinHit() => PlaySFX(pinHitClip);
    public void PlayStrike() => PlaySFX(strikeClip);
    public void PlaySpare() => PlaySFX(spareClip);

    public void PlayPowerUpPickup() => PlaySFX(powerUpPickupClip);
    public void PlayPowerUpSpawn() => PlaySFX(powerUpSpawnClip);
    public void PlayObstacleHit() => PlaySFX(obstacleHitClip);
    public void PlayMudSplash() => PlaySFX(mudSplashClip);
    public void PlaySmallBall() => PlaySFX(smallBallClip);
    public void PlayIceSlide() => PlaySFX(iceSlideClip, 0.6f);

    public void PlayButtonClick() => PlaySFX(buttonClickClip, 0.8f);
    public void PlayGameOver() => PlaySFX(gameOverClip);

    public void PlayMenuMusic() => PlayMusic(menuMusic, 0.35f);
    public void PlayGameMusic() => PlayMusic(gameMusic, 0.25f);
    public void PlayEndMusic() => PlayMusic(endMusic, 0.35f);
}