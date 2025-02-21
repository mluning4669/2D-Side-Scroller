using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    public void PlayHitEfect()
    {
        audioSources[0].Play();
    }

    public void PlayDeathEffect()
    {
        audioSources[1].Play();
    }
}
