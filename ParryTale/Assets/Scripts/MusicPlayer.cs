using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource Music;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Music = GetComponent<AudioSource>();
        Music.PlayOneShot(Music.clip, .2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
