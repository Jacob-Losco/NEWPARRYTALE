using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFiveManager : MonoBehaviour
{
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = this.GetComponent<Manager>();

        manager.instantiatePlayer(new Vector3(0, 0, 0));
        manager.instantiateSwordsman(new Vector3(6, 2.5f, 0));
        manager.instantiateSwordsman(new Vector3(-6, 2.5f, 0));
        manager.instantiateArcher(new Vector3(7, 0, 0));
        manager.instantiateArcher(new Vector3(2, 3, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.winState)
        {
            StartCoroutine(moveNewScene());
        }
    }

    IEnumerator moveNewScene()
    {
        yield return new WaitForSeconds(.5f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }
}
