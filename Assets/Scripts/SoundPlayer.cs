using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] private List<AudioClip> tirs;
    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip recall;

    private AudioSource aS;
    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    public void PlayShot()
    {
        aS.clip = tirs[(int)Random.Range(0, tirs.Count)];
        aS.Play();
    }

    public void PlayStep()
    {
        aS.clip = step;
        aS.Play();
    }

    public void PlayRecall()
    {
        aS.clip = recall;
        aS.Play();
    }
}

public enum PlayerSounds
{
     
}
