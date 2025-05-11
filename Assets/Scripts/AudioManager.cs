using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    //public AudioSource gameAudio
    public AudioSource angryAudio;
    public AudioSource pickItemAudio;
    public AudioSource dropItemAudio;
    public AudioSource itemPlacedAudio;
    public AudioSource stepAudio;
    public AudioSource safezoneAudio;
    public AudioSource closeDoor;
    public AudioSource openDoor;
    public AudioSource happyAudio;

    public void HappySound()
    {
        StartCoroutine(CutAudio());
    }

    public IEnumerator CutAudio()
    {
        happyAudio.Play();
        yield return new WaitForSeconds(0.3f);
        happyAudio.Stop();
    }
}
