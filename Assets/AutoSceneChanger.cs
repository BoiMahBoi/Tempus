using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChanger : MonoBehaviour
{

    public string nextScene;

    private void OnEnable()
    {
        SceneManager.LoadScene(nextScene);
    }
}
