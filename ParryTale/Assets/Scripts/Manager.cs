using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Archer;
    public GameObject Swordsman;
    public bool winState = false;

    private int numEnemies;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void instantiatePlayer(Vector3 position)
    {
        GameObject player = Instantiate(Player);
        player.transform.position = position;
    }

    public void instantiateArcher(Vector3 position)
    {
        GameObject archer = Instantiate(Archer);
        archer.transform.position = position;
        Archer archerScript = archer.GetComponent<Archer>();
        archerScript.manager = this;
        numEnemies++;
    }

    public void instantiateSwordsman(Vector3 position)
    {
        GameObject swordsman = Instantiate(Swordsman);
        swordsman.transform.position = position;
        SwordsmanMove swordsmanScript = swordsman.GetComponent<SwordsmanMove>();
        swordsmanScript.manager = this;
        numEnemies++;
    }

    public void decrementEnemies()
    {
        numEnemies--;
        if (numEnemies == 0)
            winState = true;
    }
}
