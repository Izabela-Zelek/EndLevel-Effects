using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class TestSuite
{
    private EndScreenEffects endScreenEffects;
    private TextMeshProUGUI text;


    [UnityTest]
    public IEnumerator GameStops()
    {
        SceneManager.LoadScene("TestScene");

        yield return new WaitForSecondsRealtime(2.5f);
        Assert.AreEqual(0, Time.timeScale);
    }

    [UnityTest]
    public IEnumerator ColorCheck()
    {
        SceneManager.LoadScene("TestScene");
        yield return new WaitForSecondsRealtime(5.0f);
        endScreenEffects = GameObject.Find("GameManager").GetComponent<EndScreenEffects>();
  
        text = GameObject.Find("Canvas").transform.Find("Image").transform.Find("Score").GetComponent<TextMeshProUGUI>();
        Color decColor = new Color(endScreenEffects._ScoreColour.r / 255.0F, endScreenEffects._ScoreColour.g / 255.0F, endScreenEffects._ScoreColour.b / 255.0F, endScreenEffects._ScoreColour.a / 255.0F);
        Assert.AreEqual(decColor, text.color);
    }


}