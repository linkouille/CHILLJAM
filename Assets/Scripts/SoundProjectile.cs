using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProjectile : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private List<AudioClip> idles;

    private AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    public void PlaySound(int id)
    {
        aS.clip = sounds[id];
        aS.pitch = 1 + Random.Range(-0.2f, 0.2f);
        aS.Play();
    }

    public void PlaySound(ProjectileSound id)
    {
        PlaySound((int)id);
    }

    public void PlayIdle()
    {
        aS.clip = idles[(int)Random.Range(0,idles.Count)];
        aS.Play();
    }
}

public enum ProjectileSound
{
    Cri,Planter,Recup
}
