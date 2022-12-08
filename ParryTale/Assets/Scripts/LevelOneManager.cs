using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = this.GetComponent<Manager>();

        manager.instantiatePlayer(new Vector3(0, -2, 0));
        manager.instantiateArcher(new Vector3(0, 2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.winState)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        }
    }
}
