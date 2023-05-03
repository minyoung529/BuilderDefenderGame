using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private float volume = 0.5f;

    public static SoundManager Instance { get; private set; }

    private Dictionary<Sound, AudioClip> soundClipDict;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);

        soundClipDict = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundClipDict[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundClipDict[sound], volume);
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public float GetVolume() => volume;
}


public enum Sound
{
    BuildingPlaced,
    BuildingDamaged,
    BuildingDestroyed,
    EnemyDie,
    EnemyHit,
    GameOver,
    HeartBullet,

    Count
}