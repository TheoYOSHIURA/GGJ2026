using UnityEngine;
using System;
using UnityEngine.VFX;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private float[] _bpm = { 50, 65, 85 };
    
    private int _value = 1;
    private int _bpmvalue = 0;
    [SerializeField] private AudioClip[] _musicClips;
    [SerializeField] private float _visualInputDelay = 0f;

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

    public float VisualInputDelay { get => _visualInputDelay; set => _visualInputDelay = value; }

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
        StartAudio(_musicClips[_value], _bpm[_bpmvalue]);
        _bpmvalue += 1;
        _value += 1;
        
    }

    void Update()
    {
        if (_audioSource.isPlaying)
        {
            int beatIndex = Mathf.FloorToInt((float)(SongTime / _secondsPerBeat));

            if (beatIndex != _currentBeatIndex)
            {
                _currentBeatIndex = beatIndex;
                _onBeat?.Invoke(beatIndex);
            }
        }
        else
        {
            Debug.Log("Audio finished playing.");
            StartAudio(_musicClips[_value], _bpm[_bpmvalue]);
            _bpmvalue += 1;
            _value += 1;
        }
    }

    private void StartAudio(AudioClip clip = null, float bpm = 65)
    {
        _bpm[_bpmvalue] = bpm;
        if (clip != null)
        {
            _audioSource.clip = clip;
        }
        _secondsPerBeat = 60.0 / _bpm[_bpmvalue];
        _startDelay = leadInBeats * _secondsPerBeat;
        _songStartDspTime = AudioSettings.dspTime + _startDelay; // slight delay to ensure scheduling works correctly
        _audioSource.PlayScheduled(_songStartDspTime);
    }
}
