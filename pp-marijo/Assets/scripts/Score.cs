using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = Convert.ToInt32(GetComponent<Text>().text);
        if (score == 4)
        {
            SceneManager.LoadScene("Scene2");
        }
    }
}
