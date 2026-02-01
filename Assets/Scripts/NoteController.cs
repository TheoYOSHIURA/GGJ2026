using UnityEngine;

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
        /* Note is not moving fast enough
        double t = (AudioManager.Instance.SongTime - _hitTime) / AudioManager.Instance.SecondsPerBeat;
        transform.position = Vector3.Lerp(
            _spawnPos,
            _hitPos,
            (float)t
        );//*/

        double spawnTime = _hitTime - _travelTime;
        double t = (AudioManager.Instance.SongTime - spawnTime) / _travelTime;
        transform.position = Vector3.Lerp(_spawnPos, _hitPos, Mathf.Clamp01((float)t));

        if (AudioManager.Instance.SongTime >= _hitTime + NoteChecker.Instance.HitWindowGood)
        {
            NoteSpawner.Instance.Notes.Remove(this);
            Destroy(gameObject);
        }
    }
}
