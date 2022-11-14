using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class EndScreenEffects : MonoBehaviour
{
    GameObject _canvas;
    private bool _nextStage = true;
    //GAME OVER STAMP
    public bool _UseGameOver;
    public string _playerName;
    public GameObject _canvasPrefab;
    public string _gameOverText;
    public TextMeshProUGUI _EndScreenText;
    public Color32 _GameOverColour = new Color32();

    private GameObject _player;
    private bool _isStopped = false;
    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    private float offset = 80;
    private int count = 0;
    private bool inPosition = false;

    //SCORE COUNT UP
    public bool _useScoreCountUp;
    public TextMeshProUGUI _scoreGameObject;
    public string _scoreText;
    public Color32 _ScoreColour = new Color32();

    private int score;
    private int countUp = 1;
    private TextMeshProUGUI _endScreenScore;
    private bool numberIncreased = true;

    private void Start()
    {
       _canvas = Instantiate(_canvasPrefab) as GameObject;
        _canvas.name = _canvas.name.Replace("(Clone)", "");
        _canvas.SetActive(false);

        if (_UseGameOver)
        {
            _player = GameObject.Find(_playerName);
        }

        if(_useScoreCountUp)
        {
            string[] texts = _scoreGameObject.text.Split(' ');
            foreach (var text in texts)
            {
                int.TryParse(text.ToString(), out score);
            }

            _endScreenScore = _canvas.transform.Find("Image").transform.Find("Score").GetComponent<TextMeshProUGUI>();
            _canvas.transform.Find("Image").transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = _ScoreColour;
            _canvas.transform.Find("Image").transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = _scoreText;
            _endScreenScore.color = _ScoreColour;
        }
    }
    private void Update()
    {
        if (_player == null && !_isStopped)
        {
            Time.timeScale = 0;
            _canvas.SetActive(true);
            _isStopped = true;

        }

        if (_UseGameOver && _player == null && _isStopped && count < _gameOverText.Length)
        {
            _nextStage = false;
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
                texts[count].GetComponent<TextMeshProUGUI>().transform.localScale = new Vector3(texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.x - 0.25f, texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.y - 0.25f, texts[count].GetComponent<TextMeshProUGUI>().transform.localScale.z - 0.25f);
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
        else if (_UseGameOver && _player == null && _isStopped && count >= _gameOverText.Length)
        {
            _nextStage = true;
        }
   
        if(_useScoreCountUp && _player == null && _isStopped && countUp < score && _nextStage)
        {
            if (numberIncreased)
            {
                Debug.Log(countUp);

                _endScreenScore.text =  countUp.ToString();
                _endScreenScore.fontSize = 100;
                numberIncreased = false;
            }
            if (!numberIncreased)
            {
                _endScreenScore.fontSize -= 0.3f;
            }
            if(_endScreenScore.fontSize <= 68 && _endScreenScore.fontSize > 66)
            {
                _endScreenScore.GetComponent<AudioSource>().Play();
                numberIncreased = true;
                countUp++;
            }
        }
    }
}


