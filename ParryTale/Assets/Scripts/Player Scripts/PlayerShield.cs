using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    ParticleSystem sparkFx;
    public PlayerMaster playerScript;
    private bool shieldState = false;
    private bool inCooldown = false;
    private GameObject swordsman;
    private GameObject arrow;
    private Arrow arrowScript;
    private SwordsmanMove swordScript;
    private Color ogColor;
    private Rigidbody2D rb;
    private AudioSource ArrowHit;
    public AudioClip ArrowHitMuff;
    public AudioClip swordHitShield;
    public AudioClip parrySound;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ogColor = GetComponent<Renderer>().material.color;
        sparkFx = this.GetComponent<ParticleSystem>();
        sparkFx.Stop();
        ArrowHit = GetComponent<AudioSource>();
        playerScript = GetComponentInParent<PlayerMaster>();
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
        yield return new WaitForSeconds(.25f);

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
            sparkFx.Play();
            if (shieldState)
            {

                if (!ArrowHit.isPlaying)
                    ArrowHit.PlayOneShot(ArrowHitMuff, .7f);
                arrowScript.Reflect(rb.rotation);

            }
            else
            {
                if (!ArrowHit.isPlaying)
                    ArrowHit.PlayOneShot(ArrowHit.clip, .8f);
                playerScript.startShortKnockback();
                Destroy(collidedObject);
            }
        }
        if (collidedObject.tag == "Swordsman")
        {
            swordsman = collidedObject;
            swordScript = swordsman.GetComponent<SwordsmanMove>();
            sparkFx.Play();
            if (shieldState)
            {
                if (!ArrowHit.isPlaying)
                    ArrowHit.PlayOneShot(parrySound, .6f);
                swordScript.startParriedKnockback();
            }
            else
            {
                if (!ArrowHit.isPlaying)
                    ArrowHit.PlayOneShot(swordHitShield, .8f);
                swordScript.status = SwordsmanMove.TrackType.knockback;
                swordScript.startKnockback();
                playerScript.startLongKnockback();
            }
        }
    }
}
