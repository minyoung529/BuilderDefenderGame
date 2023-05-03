using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private float volume = 0.5f;
    private AudioSource audioSource;

    private Dictionary<BuildingTypeSO, AudioSource> audioDict = new();
    private Dictionary<BuildingTypeSO, int> checks = new();

    public static MusicManager Instance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.volume = volume;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            BuildingTypeSO type = child.GetComponent<BuildingTypeHolder>().buildingType;

            if (type)
            {
                audioDict.Add(type, child.GetComponent<AudioSource>());
                checks.Add(type, 0);

                if(type.nameString == "Drum")
                {
                    checks[type] = 100000;
                }
            }
        }

        Instance = this;
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public float GetVolume() => volume;

    public void Active(BuildingTypeSO buildingType)
    {
        if (audioDict.ContainsKey(buildingType))
        {
            audioDict[buildingType].volume = 1;
            ++checks[buildingType];
        }
    }

    public void Inactive(BuildingTypeSO buildingType)
    {
        if (audioDict.ContainsKey(buildingType))
        {
            audioDict[buildingType].volume = 0;
        }
    }

    public void Destroyed(BuildingTypeSO buildingType)
    {
        if (audioDict.ContainsKey(buildingType))
        {
            if (--checks[buildingType] == 0)
            {
                audioDict[buildingType].volume = 0;
            }
        }
    }

    public void VolumeDown()
    {
        foreach (AudioSource audioSource in audioDict.Values)
        {
            audioSource.volume = 0f;
        }
    }

    public void VolumeUp()
    {
        foreach (KeyValuePair<BuildingTypeSO, AudioSource> pair in audioDict)
        {
            if (checks[pair.Key] > 0)
            {
                pair.Value.volume = 1f;
            }
        }
    }
}
