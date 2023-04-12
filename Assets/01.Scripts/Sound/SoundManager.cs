using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static SoundManager Instance { get; private set; }

    private Dictionary<Sound, AudioClip> soundClipDict;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        soundClipDict = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundClipDict[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundClipDict[sound]);
    }
}


public enum Sound
{
    BuildingPlaced,
    BuildingDamaged,
    BuildingDestroyed,
    EnemyDie,
    EnemyHit,
    GameOver,

    Count
}