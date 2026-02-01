using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private float _bpm = 65;
    
    private AudioSource _audioSource;
    private double _songStartDspTime;
    private double _secondsPerBeat;
    private int _currentBeatIndex = -1;
    private event Action<int> _onBeat;
    private int leadInBeats = 4;
    private double _startDelay;

    public double SongTime => AudioSettings.dspTime - _songStartDspTime;
    public double SecondsPerBeat => _secondsPerBeat;
    public int CurrentBeatIndex => _currentBeatIndex;
    public double SongStartDspTime => _songStartDspTime;
    public event Action<int> OnBeat
    {
        add { _onBeat -= value; _onBeat += value; }
        remove { _onBeat -= value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _bpm = 50;
        _secondsPerBeat = 60.0 / _bpm;
        _audioSource.clip = SoundsManager.Instance.SoundDict["Cadenza 100"];
        _startDelay = leadInBeats * _secondsPerBeat;
        _songStartDspTime = AudioSettings.dspTime + _startDelay; // slight delay to ensure scheduling works correctly
        _audioSource.PlayScheduled(_songStartDspTime);
        
        
    }

    void Update()
    {
        int beatIndex = Mathf.FloorToInt((float)(SongTime / _secondsPerBeat));

        if (beatIndex != _currentBeatIndex)
        {
            _currentBeatIndex = beatIndex;
            _onBeat?.Invoke(beatIndex);
        }
    }
}
