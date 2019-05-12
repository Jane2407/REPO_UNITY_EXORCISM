using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sound;

    private void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
