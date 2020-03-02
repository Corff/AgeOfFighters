using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float timer;
    public string setName;

    //Variables below is used to make scene transitioning feel better
    public Animator anim;
    public float transitionTime;

    void Update()
    {
        
    }

    public void LoadNextLevel(string name)
    {
        StartCoroutine(LoadLevel(name));     
    }

    public IEnumerator LoadLevel(string name)
    {
        anim.SetTrigger("startTransition");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }

    public void ExitRequest()
    {
        Debug.Log("I WANT TO QUIT!");
        Application.Quit();
    }
}
