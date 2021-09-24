using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundSlider_Offline : MonoBehaviour{

    public TextMeshProUGUI valueText;
    public Slider slider;

    MatchManager manager;

    void Start(){
        manager = GameObject.Find("MatchManager").GetComponent<MatchManager>();
    }

    public void SetText(){

        valueText.text = slider.value.ToString();
    }

    public void SetRounds(){

        manager.SetTotalRounds((int) slider.value);
    }
}
