using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    private int _score = 0;
    public static ScoreManager Instance { get => _instance; set => _instance = value; }
    public int Score
    {
        get => _score;
        set
        {
            
            if (value - Score > 0)
            {
                _onScoreIncrease.Invoke();
                Debug.Log("Score Increase invoked");
            }
            else if (value - Score < 0) 
            {
                _onScoreDecrease.Invoke();
                Debug.Log("Score Decrease invoked");
            }
            _score = value;
        }
    }

    #region Events
    private event Action _onScoreIncrease;
    public event Action OnScoreIncrease
    {
        add
        {
            _onScoreIncrease -= value;
            _onScoreIncrease += value;
        }
        remove
        {
            _onScoreIncrease -= value;
        }
    }

    private event Action _onScoreDecrease;
    public event Action OnScoreDecrease
    {
        add
        {
            _onScoreDecrease -= value;
            _onScoreDecrease += value;
        }
        remove
        {
            _onScoreDecrease -= value;
        }
    }
    #endregion Events

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
