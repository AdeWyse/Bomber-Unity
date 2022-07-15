using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int numberChar;
    public bool isControllable = true;
    private Text numberCharDisplay;
    private Slider numberCharSlider;
    private Toggle controllableToggle;

    // Start is called before the first frame update
    void Start()
    {
        numberCharSlider = GameObject.Find("SliderNumberOfChar").GetComponent<Slider>();
        numberCharDisplay = GameObject.Find("TextNumberOfChar").GetComponent<Text>();
        controllableToggle = GameObject.Find("ToggleCrontollable").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleValueControl()
    {
        isControllable = controllableToggle.isOn;
    }
    public void sliderValueControl()
    {
        numberChar = (int)numberCharSlider.value;
        numberCharDisplay.text = numberChar.ToString();
    }
}
