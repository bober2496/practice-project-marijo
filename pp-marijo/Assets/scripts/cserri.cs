using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cserri : MonoBehaviour
{
    private int Increment;

    private void LateUpdate()
    {
        Increment = Convert.ToInt32(GameObject.Find("Scoreboard").GetComponent<Text>().text);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool("isded", true);
            Increment++;
            GameObject.Find("Scoreboard").GetComponent<Text>().text = Increment.ToString();
            Destroy(gameObject, 0.5f);
        }
    }
}
