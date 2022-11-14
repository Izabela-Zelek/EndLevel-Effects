using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreenEffects : MonoBehaviour
{
    public bool _UseGameOver;
    public string _playerName;
    public GameObject _canvasPrefab;
    public string _gameOverText;
    public TextMeshProUGUI _EndScreenText;
    public Color32 _GameOverColour;

    private GameObject _player;
    private bool _isStopped = false;
    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    private float offset = 80;
    private int count = 0;
    private bool inPosition = false;
    private void Start()
    {
        if (_UseGameOver)
        {
            _player = GameObject.Find(_playerName);
        }
    }
    private void Update()
    {
        if (_UseGameOver)
        {
            if (_player == null && !_isStopped)
            {
                Time.timeScale = 0;
                Instantiate(_canvasPrefab);
                _isStopped = true;

            }
            if (_player == null && _isStopped)
            {
                if (count < _gameOverText.Length)
                {
                    if (!inPosition)
                    {
                        texts.Add(Instantiate(_EndScreenText, new Vector3(990 + offset, 540, 0), Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>()));
                        texts[count].GetComponent<TextMeshProUGUI>().text = System.Convert.ToString(_gameOverText[count]);
                        texts[count].GetComponent<TextMeshProUGUI>().color = _GameOverColour;
                        offset += 80;
                        inPosition = true;
                    }
                    if (texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.x > 1)
                    {
                        texts[count].GetComponent<TextMeshProUGUI>().transform.localScale = new Vector3(texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.x - 0.35f, texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.y - 0.35f, texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.z - 0.35f);
                    }
                    else if (texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.x <= 1)
                    {
                        if (texts[count].GetComponent<TextMeshProUGUI>().text != " ")
                        {
                            texts[count].GetComponent<AudioSource>().Play();
                        }
                        count++;
                        inPosition = false;
                    }
                }
            }
        }

    }
}


