using System.Threading;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private NoteController[] _noteControllers;
    [SerializeField] private float _bpm;
    [SerializeField] private int _notesPerBeat = 1;
    [SerializeField] private float _timeElapsed = 0;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _noteContainer;
    [SerializeField] private Transform _noteChecker;

    public float Bpm { get => _bpm; set => _bpm = value; }

    void Start()
    {

    }

    
    void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= 60 / _notesPerBeat / _bpm) // Pas sûr que ce soit ça lol
        {
            _timeElapsed -= 60 / _notesPerBeat / _bpm;
            SpawnNote();
        }
    }

    private void SpawnNote()
    {
        int randomId = Random.Range(0, _noteControllers.Length);
        NoteController _note = Instantiate(_noteControllers[randomId], _spawnPosition.position, _spawnPosition.rotation, _noteContainer);
        _note.NoteSpawner = this;
        _note.NoteChecker = _noteChecker;
    }
}
