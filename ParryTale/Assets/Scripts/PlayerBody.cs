using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public PlayerMaster playerScript;
    private AudioSource PlayerDeath;
    public AudioClip LavaBurn;
    public AudioClip Drowning;

    private float playerColor = 255;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeath = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerColor -= 127;
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Arrow")
        {
            playerScript.subtractHealth();
            Destroy(collidedObject);
            GetComponent<Renderer>().material.color = new Color(255, playerColor, playerColor);
        }
        if (collidedObject.tag == "Swordsman")
        {
            playerScript.subtractHealth();
            GetComponent<Renderer>().material.color = new Color(255, playerColor, playerColor);
        }
        if (collidedObject.tag == "Lava")
        {
            if (!PlayerDeath.isPlaying)
                PlayerDeath.PlayOneShot(LavaBurn, 1f);
            playerScript.deathWithNoSound();
        }
        if (collidedObject.tag == "Water")
        {
            if (!PlayerDeath.isPlaying)
                PlayerDeath.PlayOneShot(Drowning, 1f);
            playerScript.deathWithNoSound();
        }
    }
}
