using DG.Tweening;
using Shot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PianoSkillObject : MonoBehaviour
{
    private bool isPlayerTurn = false;

    private List<Keyboard> keyboards = new();

    private readonly int SOUND_COUNT = 3;
    private readonly int SCALE = 8;
    private List<int> sounds = new();
    private int tryCount = 0;
    private int successCount = 0;

    private HeartShot heartShot;

    // Debug Dict
    private Dictionary<int, string> soundDict = new();

    [SerializeField]
    private GameObject piano;

    public Action OnEnd { get; set; }

    private void Awake()
    {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector3.zero;

        keyboards = GetComponentsInChildren<Keyboard>().ToList();
        heartShot = GetComponent<HeartShot>();

        keyboards.ForEach(x => x.OnDown += Check);

        soundDict.Add(0, "C (도)");
        soundDict.Add(1, "C# (도#)");
        soundDict.Add(2, "D (레)");
        soundDict.Add(3, "D# (레#)");
        soundDict.Add(4, "E (미)");
        soundDict.Add(5, "F (파)");
        soundDict.Add(6, "F# (파#)");
        soundDict.Add(7, "G (솔)");
    }

    private void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(PlaySample());

        for (int i = 0; i < SOUND_COUNT; i++)
        {
            int randomVal;
            do
            {
                randomVal = UnityEngine.Random.Range(0, SCALE);
            } while (sounds.Contains(randomVal));

            sounds.Add(randomVal);
        }
    }

    private IEnumerator PlaySample()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < SOUND_COUNT; i++)
        {
            keyboards[sounds[i]].PlaySound();

            yield return new WaitForSecondsRealtime(0.1f);
        }

        isPlayerTurn = true;
    }

    private void Check(int index)
    {
        if (sounds[tryCount] == index)
        {
            keyboards[index].Success();
            successCount++;
        }
        else
        {
            keyboards[index].Fail();
        }

        if (++tryCount == SOUND_COUNT)
        {
            // 0이면 1
            Sequence delay = DOTween.Sequence();
            delay.SetUpdate(true);
            delay.AppendInterval(1f);
            delay.AppendCallback(() => Shoot(successCount + 1));
        }
    }

    private void Shoot(int count)
    {
        piano.SetActive(false);
        Time.timeScale = 1f;

        if (count == 4)
        {
            heartShot.MultipleShot(count, OnEnd);
            Debug.Log("성공 ^__^");
        }
        else
        {
            heartShot.MultipleShot(count, OnEnd);
            Debug.Log("실패 ㅠ__ㅠ");
        }

        SoundManager.Instance.PlaySound(Sound.HeartBullet);
    }
}
