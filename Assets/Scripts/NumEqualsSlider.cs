using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumEqualsSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private Text text;
    
    void Start() {
        text = GetComponent<Text>();
        UpdateNum();
    }

    public void UpdateNum()
    {
        if(slider != null && text != null)
            text.text = Mathf.RoundToInt(slider.value).ToString();
    }
}
