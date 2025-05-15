using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class SFXController : PooledObject
{
    private AudioSource _audioSource;
    private float _currentCount;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();

        //_audioSource.playOnAwake = true;
    }

    private void Update()
    {
        // 한 프레임이 갱신될때까지의 시간 = DeltaTime
        _currentCount -= Time.deltaTime;    // 프레임마다 시간 계산.

        if(_currentCount <= 0)
        {
            //_audioSource.Stop();
            //_audioSource.clip = null;
            ReturnPool();
        }
    }

    public void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();

        _currentCount = clip.length;         // lenght 오디오 클립이 몇초인지.?
    }

}
