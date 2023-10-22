using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;   

    public static AudioManager Instance => instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;



    private void Awake()
    {
        if (instance != null) Debug.LogError("Only 1 instance of audio");
        instance = this;
    }

    public void playSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
