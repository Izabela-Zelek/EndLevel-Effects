using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private int randScore;

    private void Awake()
    {
        randScore = Random.Range(10, 30);
        //gameObject.GetComponent<TextMeshProUGUI>().text += randScore.ToString();
        gameObject.GetComponent<TextMeshProUGUI>().text += "120";
    }
}
