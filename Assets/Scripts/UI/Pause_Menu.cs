using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseMenuUi;

    private bool menuOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuOn == false)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject.FindWithTag("Enemy").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            PauseMenuUi.SetActive(true);
            menuOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuOn == true)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject.FindWithTag("Enemy").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            PauseMenuUi.SetActive(false);
            menuOn = false;
        }
    }
}
