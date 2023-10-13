using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScript : MonoBehaviour
{    
    public TextMeshProUGUI generationText;
    public TextMeshProUGUI pauseText;
    public GameOfLife gameOfLife;
    bool pause;
    
    void Update()
    {
        generationText.text = "Gen: " + gameOfLife.generations.ToString();
    }
    public void Faster()
    {
        if (Application.targetFrameRate < 60)
        Application.targetFrameRate += 4;
    }
    public void SlowDown()
    {
        if (Application.targetFrameRate > 4)
        Application.targetFrameRate -= 4;
    }
    public void PauseTextChange()
    {
        if (!pause)
            pauseText.text = "Start";
        else
            pauseText.text = "Pause";
        pause = !pause;
    }
}
