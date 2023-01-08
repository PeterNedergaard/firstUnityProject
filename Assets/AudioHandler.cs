using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    
    void Start()
    {
    }


    void Update()
    {
    }

    public void PlayClipAt(AudioClip clip, Vector3 pos, float volume, float pitch)
    {
        GameObject tempObj = new GameObject("TempAudio");
        tempObj.transform.position = pos;
        
        AudioSource audioSrc = tempObj.AddComponent<AudioSource>();
        audioSrc.clip = clip;
        audioSrc.volume = volume;
        audioSrc.pitch = pitch;
        audioSrc.spatialBlend = 1;

        audioSrc.Play();
        Destroy(tempObj, clip.length); // Destroys after the clip has finished playing
    }
    
}
