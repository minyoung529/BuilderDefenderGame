using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private MusicManager musicManager;

    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;

    private void Awake()
    {
        GetButton("SoundIncreaseButton").onClick.AddListener(() => { soundManager.IncreaseVolume(); UpdateText(); });
        GetButton("SoundDecreaseButton").onClick.AddListener(() => { soundManager.DecreaseVolume(); UpdateText(); });
        GetButton("MusicIncreaseButton").onClick.AddListener(() => { musicManager.IncreaseVolume(); UpdateText(); });
        GetButton("MusicDecreaseButton").onClick.AddListener(() => { musicManager.DecreaseVolume(); UpdateText(); });
        GetButton("MainMenuButton").onClick.AddListener(() => { GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene); Time.timeScale = 1f; });

        soundVolumeText = transform.Find("SoundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateText();
        transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((x) => CameraHandler.Instance.SetEdgeScrolling(x));
    }

    private void UpdateText()
    {
        soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10f).ToString());
        musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10f).ToString());
    }

    public void TolggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if(gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private Button GetButton(string name)
    {
        return transform.Find(name).GetComponent<Button>();
    }
}
