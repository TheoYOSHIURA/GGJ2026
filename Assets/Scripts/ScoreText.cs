using System;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    void Start()
    {
        _scoreText.text = "Score:" + ScoreManager.Instance.Score.ToString();
        ScoreManager.Instance.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
