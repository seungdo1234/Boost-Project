using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bgm { Main }
public enum Sfx { Thrust }
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels; // 많은 효과음을 내기 위한 채널 시스템
    public AudioSource[] sfxPlayers;
    private int channelIndex; // 채널 갯수 만큼 순회하도록 맨 마지막에 플레이 했던 SFX의 인덱스번호를 저장하는 변수


    private void Awake()
    {
        instance = this;
        Init();
    }
    private void Start()
    {
        //PlayBgm(0);
    }
    private void Init()
    {
        // 배경음 플레이어 초기화
        // 오브젝트 생성
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        //  bgmPlayer.playOnAwake = t; // 플레이 하자마자 시작 false
        bgmPlayer.loop = true; // 반복 true
        bgmPlayer.volume = bgmVolume; // 볼륨

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
   //         sfxPlayers[i].bypassListenerEffects = true; // 하이패스에 안 걸리게 함
        }

        sfxPlayers[0].clip = sfxClips[0];
        sfxPlayers[0].loop = true;
    }

    public void PlayBgm(int bgmNumber) // BGM 플레이 함수
    {
        bgmPlayer.clip = bgmClip[bgmNumber];
        bgmPlayer.Play(); // Bgm 플레이

    }
    public void EndBgm()
    {
        bgmPlayer.Stop();
    }

    public void PlayThrustSfx()
    {
        if (sfxPlayers[0].isPlaying)
        {
            return;
        }
        sfxPlayers[0].Play();
    }

    public void PlayerSfx(Sfx sfx)
    {
        for (int i = 1; i < sfxPlayers.Length; i++) // 0번은 추력 소리 채널이기 때문에 1 ~ channels의 채널만 순회
        {
            // 예를들어 5번 인덱스를 마지막으로 사용했으면 6 7 8 9 10 1 2 3 4 5 이런식으로 순회하게 하기위한 계산임
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) // 해당 채널이 Play 중이라면
            {
                continue;
            }
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break; // 효과음이 빈 채널에서 재생 됐기 때문에 반드시 break로 반복문을 빠져나가야함
        }
    }
}
