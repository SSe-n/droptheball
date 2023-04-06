using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("[ BGM ] ")]
    public AudioSource bgmPlayer;

    [Header("[ SFX ] ")]
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    public enum Sfx { Button, Buy, Exit, GameOver, RankUp, Touch_small, Touch_rubber, Touch_mid, Touch_large }
    int sfxCursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SfxPlay(Sfx type)
    {
        Debug.Log("�뷡");

        switch (type)
        {
            case Sfx.Button:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.Buy:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.Exit:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.GameOver:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.RankUp:
                sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(4, 7)];
                break;
            case Sfx.Touch_large:
                sfxPlayer[sfxCursor].clip = sfxClip[7];
                break;
            case Sfx.Touch_small:
                sfxPlayer[sfxCursor].clip = sfxClip[8];
                break;

        }
        Debug.Log("�뷡1");

        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}
