using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    [SerializeField] private GameObject _audioOn;
    [SerializeField] private GameObject _audioOff;
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _endGameSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _noMoneySound;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 1);
        if (AudioListener.volume == 1)
        {
            EnableAudio();
        }
        else
        {
            DisableAudio();
        }
    }

    public void DisableAudio()
    {
        _audioOn.SetActive(false);
        _audioOff.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetFloat("AudioVolume", 0);
    }

    public void EnableAudio()
    {
        _audioOff.SetActive(false);
        _audioOn.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetFloat("AudioVolume", 1);
    }

    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void PlayBuySound()
    {
        _audioSource.PlayOneShot(_buySound);
    }

    public void PlayEndGameSound()
    {
        _audioSource.PlayOneShot(_endGameSound);
    }

    public void PlayExplosionSound()
    {
        _audioSource.PlayOneShot(_explosionSound);
    }

    public void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shootSound);
    }

    public void PlayNoMoneySound()
    {
        _audioSource.PlayOneShot(_noMoneySound);
    }
}
