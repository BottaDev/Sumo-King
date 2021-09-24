using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountOfConsumables : MonoBehaviour{

    public TextMeshProUGUI text;
    public PlayerState state;

    int count;

    void Update(){

        count = state.consumablesCount;

        text.text = count.ToString();
    }
}
