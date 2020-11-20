using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceToMain : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        
        if(index == 0)
        {
            if (Input.anyKey)
            {
                LoadNextLevel();
            }
        }
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void startTutorial()
    {
        StartCoroutine(LoadTutorial());
    }

    public void ExitToMain()
    {
        StartCoroutine(ExitMain());
    }

    IEnumerator LoadMenu(int levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadTutorial()
    {
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene("Greybox TUT Level");
    }

    IEnumerator ExitMain()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MainMenu");
    }
}
