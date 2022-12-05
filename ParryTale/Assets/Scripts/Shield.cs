using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerMove playerMove;
    public GameObject arrow;
    public GameObject shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Arrow")
        {
            if(playerMove.status == PlayerMove.PlayerStatus.parry)
            {
                Destroy(gameObject);
                StartCoroutine(reflect());
            }
            if(playerMove.status == PlayerMove.PlayerStatus.punish)
            {
                Destroy(gameObject);
                Destroy(this.gameObject);
            }
            if(playerMove.status == PlayerMove.PlayerStatus.normal)
            {
                Destroy(gameObject);
                playerMove.startKnockback();
            }
            
        }
        Debug.Log(gameObject.tag);
    }
    IEnumerator reflect()
    {
        yield return new WaitForSeconds(0);
        GameObject go = Instantiate(arrow);
        go.transform.rotation = shootPoint.transform.rotation;
        go.transform.position = shootPoint.transform.position;
        Debug.Log("REFLECTED");
    }
}
