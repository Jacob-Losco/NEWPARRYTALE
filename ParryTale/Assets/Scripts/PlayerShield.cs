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
    private GameObject arrow;
    private Arrow arrowScript;
    private SwordsmanMove swordScript;
    private Color ogColor;

    // Start is called before the first frame update
    void Start()
    {
        ogColor = GetComponent<Renderer>().material.color;
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
        GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        StartCoroutine(parryEffectCooldown());
        StartCoroutine(parryActionCooldown());
    }

    IEnumerator parryEffectCooldown()
    {
        yield return new WaitForSeconds(.5f);

        GetComponent<Renderer>().material.color = ogColor;
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
            arrow = collidedObject;
            arrowScript = arrow.GetComponent<Arrow>();
            if (shieldState)
            {
                // Arrow Hit Shield Muff Sound Effect
                arrowScript.reflect();
            }
            else
            {
               //Arrow Hit shield Sound Effect
                Destroy(collidedObject);
            }
        }
        if (collidedObject.tag == "Swordsman")
        {
            swordsman = collidedObject;
            swordScript = swordsman.GetComponent<SwordsmanMove>();
            
            if (shieldState)
            {
                // Parry Sound Effect
                swordScript.status = SwordsmanMove.TrackType.knockback;
                swordScript.startParriedKnockback();
            }
            else
            {
                //Sword Clash Sound FX
                swordScript.status = SwordsmanMove.TrackType.knockback;
                swordScript.startKnockback();
                playerScript.knockback();
            }
        }
    }
}
