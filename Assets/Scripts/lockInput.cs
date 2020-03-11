using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockInput : MonoBehaviour
{
    bool isAi;

    private void Start()
    {
        isAi = gameObject.transform.GetChild(7).gameObject.activeSelf;
        gameObject.GetComponent<CharMovement>().enabled = false;
        if(isAi)
        {
            gameObject.GetComponent<EnemyAI>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        Invoke("antiStart",2);
    }

    public void antiStart()
    {
        Debug.Log("anti");
        gameObject.GetComponent<CharMovement>().enabled = true;
        if(isAi)
        {
            gameObject.GetComponent<EnemyAI>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
