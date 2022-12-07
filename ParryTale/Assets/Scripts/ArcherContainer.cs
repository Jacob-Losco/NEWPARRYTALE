using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherContainer : MonoBehaviour
{
    public AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDeathSound()
    {
        if (!deathSound.isPlaying)
        {
            deathSound.PlayOneShot(deathSound.clip, .75f);
        }
        
    }
}
