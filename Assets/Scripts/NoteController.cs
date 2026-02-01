using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NoteController : MonoBehaviour
{
    private Vector3 _spawnPos;
    private Vector3 _hitPos;
    private double _hitTime;
    private int _beatIndex;
    private double _travelTime;

    public int BeatIndex { get => _beatIndex; set => _beatIndex = value; }
    public double HitTime { get => _hitTime; set => _hitTime = value; }

    void Start()
    {
        _hitTime = _beatIndex * AudioManager.Instance.SecondsPerBeat;
        _spawnPos = transform.position;
        _hitPos = NoteSpawner.Instance.HitPosition.position;
        _travelTime = NoteSpawner.Instance.BeatsAhead * AudioManager.Instance.SecondsPerBeat;
    }

    void Update()
    {
        double spawnTime = _hitTime - _travelTime - AudioManager.Instance.VisualInputDelay;
        double t = (AudioManager.Instance.SongTime - spawnTime) / _travelTime;
        transform.position = Vector3.Lerp(_spawnPos, _hitPos, Mathf.Clamp01((float)t));

        if (AudioManager.Instance.SongTime >= _hitTime + NoteChecker.Instance.HitWindowGood)
        {
            // Missed note
            ScoreManager.Instance.Score -= 50;
            NoteSpawner.Instance.Notes.Remove(this);
            Destroy(gameObject);
        }
    }
}
