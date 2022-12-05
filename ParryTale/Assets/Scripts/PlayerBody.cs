using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public PlayerMaster playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Arrow")
        {
            playerScript.subtractHealth();
        }
        if (collidedObject.tag == "Swordsman")
        {
            playerScript.subtractHealth();
        }
        if (collidedObject.tag == "Lava")
        {
            playerScript.death();
        }
        if (collidedObject.tag == "Water")
        {
            playerScript.death();
        }
    }
}
