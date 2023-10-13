using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider Slider;

    [SerializeField] private TextMeshProUGUI SliderText;
    // Start is called before the first frame update
    void Start()
    {
        Slider.onValueChanged.AddListener((v) =>
        {
            SliderText.text = v.ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
