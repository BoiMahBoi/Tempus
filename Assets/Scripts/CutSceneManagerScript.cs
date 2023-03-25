using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManagerScript : MonoBehaviour
{

    public float loadTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAfterTime(loadTime));
    }

    IEnumerator LoadLevelAfterTime( float loadTime) 
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
