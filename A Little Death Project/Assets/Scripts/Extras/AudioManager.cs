using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> Attacks = new();

    public List<AudioSource> SFX = new();

    public AudioSource stunned;
    public AudioSource possessionA;
    public AudioSource possessionB;
    public AudioSource groundDrop;
    public AudioSource groundPound;
    public AudioSource spit;
    public AudioSource wallHit;


}
