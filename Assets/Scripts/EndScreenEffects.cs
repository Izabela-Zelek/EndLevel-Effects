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
    private int countUp = 0;
    private TextMeshProUGUI _endScreenScore;
    private bool numberIncreased = true;

    private void Start()
    {
        _canvas = Instantiate(_canvasPrefab) as GameObject;
        _canvas.name = _canvas.name.Replace("(Clone)", "");
        _canvas.SetActive(false);

        _player = GameObject.Find(_playerName);
        

        if(_useScoreCountUp)
        {
            string[] texts = _scoreGameObject.text.Split(' ');
            foreach (var text in texts)
            {
                int.TryParse(text.ToString(), out score);
            }
            
            _endScreenScore = _canvas.transform.Find("Image").transform.Find("Score").GetComponent<TextMeshProUGUI>();
            _canvas.transform.Find("Image").transform.localScale = new Vector3(Screen.width / 1920, Screen.width / 1920, Screen.width / 1920);
            float calc = Screen.width / 3.428571428571429f;
            Debug.Log(calc);
            Debug.Log(Screen.width  - Screen.width - calc);
            _canvas.transform.Find("Image").transform.position = new Vector3(Screen.width - (Screen.width / 2) - calc, Screen.height / 2 - 10, 0);
            _canvas.transform.Find("Image").transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = _ScoreColour;
            _canvas.transform.Find("Image").transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = _scoreText;
            _endScreenScore.color = _ScoreColour;
        }
    }
    private void Update()
    {
        if (_useScoreCountUp)
        {
            string[] texts = _scoreGameObject.text.Split(' ');
            foreach (var text in texts)
            {
                int.TryParse(text.ToString(), out score);
            }
        }

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
                Vector3 position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(0, 0, 0));
                position.x += offset;
                texts.Add(Instantiate(_EndScreenText,position , Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>()));
                texts[count].GetComponent<TextMeshProUGUI>().text = System.Convert.ToString(_gameOverText[count]);
                texts[count].GetComponent<TextMeshProUGUI>().color = _GameOverColour;
                texts[count].GetComponent<TextMeshProUGUI>().fontSize = (Screen.height / 10);
                int offsetSize = Screen.height;
                while(offsetSize > 1000)
                {
                    offsetSize -= 1000;
                }
                offset += offsetSize;
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
   
        if(_useScoreCountUp && _player == null && _isStopped && countUp <= score && _nextStage)
        {
            if (numberIncreased)
            {
                _endScreenScore.text =  countUp.ToString();
                _endScreenScore.fontSize = 100;
                numberIncreased = false;
            }
            if (!numberIncreased)
            {
                if (score / 54.5f >= 0.22f)
                {
                    _endScreenScore.fontSize -= (score / 54.5f);
                }
                else
                {
                    _endScreenScore.fontSize -= 0.22f;
                }
            }
            if(_endScreenScore.fontSize <= 68 && _endScreenScore.fontSize > 66)
            {
                numberIncreased = true;
                countUp++;
            }
            else
            {
                _endScreenScore.GetComponent<AudioSource>().Play();
            }
        }
    }
}


