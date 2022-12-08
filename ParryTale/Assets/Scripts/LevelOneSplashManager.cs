using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSplashManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveScene()
    {
        yield return new WaitForSeconds(5f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelOne");
    }
}
