using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public PlayerMaster playerScript;
    private bool shieldState = false;
    private bool inCooldown = false;
    private GameObject swordsman;
    private SwordsmanMove swordScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inCooldown)
        {
            StartCoroutine(parry());
        }
    }

    IEnumerator parry()
    {
        yield return new WaitForSeconds(0f);

        shieldState = true;
        inCooldown = true;
        GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        StartCoroutine(parryEffectCooldown());
        StartCoroutine(parryActionCooldown());
    }

    IEnumerator parryEffectCooldown()
    {
        yield return new WaitForSeconds(.5f);

        GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        shieldState = false;
    }

    IEnumerator parryActionCooldown()
    {
        yield return new WaitForSeconds(1f);

        inCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Arrow")
        {
            if (shieldState)
            {
                //reflect arrow
            }
            else
            {
                Destroy(collidedObject);
            }
        }
        if (collidedObject.tag == "Swordsman")
        {
            swordsman = collidedObject;
            swordScript = swordsman.GetComponent<SwordsmanMove>();
            
            if (shieldState)
            {
                swordScript.status = SwordsmanMove.TrackType.knockback;
                swordScript.startParriedKnockback();
            }
            else
            {
                swordScript.status = SwordsmanMove.TrackType.knockback;
                swordScript.startKnockback();
                playerScript.knockback();
            }
        }
    }
}
