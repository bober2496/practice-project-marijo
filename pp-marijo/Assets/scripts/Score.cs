using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] private Scene[] allScenes;

    private int numberOfScenes;

    private void Awake()
    {
        numberOfScenes = SceneManager.sceneCount;
    }

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
