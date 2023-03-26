using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManagerScript : MonoBehaviour
{

    public float imagePlayTime;
    public Sprite[] ImageSequence;
    private int currentImageIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {   
        gameObject.AddComponent<SpriteRenderer>();
        StartCoroutine(PlayCutscene(imagePlayTime));
    }

    IEnumerator PlayCutscene(float imagePlayTime) 
    {
        foreach (var image in ImageSequence)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ImageSequence[currentImageIndex];
            yield return new WaitForSeconds(imagePlayTime);
            currentImageIndex++;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
