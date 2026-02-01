using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NoteSpawner : MonoBehaviour
{
    private static NoteSpawner _instance;
    public static NoteSpawner Instance => _instance;

    [Header("Note Prefabs")]
    [SerializeField] private NoteController[] _noteControllers;

    [Header("Positions")]
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _hitPosition;
    [SerializeField] private Transform _noteContainer;

    [Header("Timing")]
    private List<NoteController> _notes = new List<NoteController>();
    private int _beatsAhead = 4;

    public List<NoteController> Notes { get => _notes; set => _notes = value; }
    public Transform HitPosition { get => _hitPosition; set => _hitPosition = value; }
    public int BeatsAhead { get => _beatsAhead; set => _beatsAhead = value; }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        AudioManager.Instance.OnBeat += SpawnNote;
    }

    void OnDestroy()
    {
         AudioManager.Instance.OnBeat -= SpawnNote;
    }

    void Update()
    {
        
    }

    private void SpawnNote(int beatIndex)
    {
        if (_noteControllers.Length == 0) return;
        //if (beatIndex < _beatsAhead) return;

        int randomIndex = Random.Range(0, _noteControllers.Length);
        NoteController note = Instantiate(
            _noteControllers[randomIndex],
            _spawnPosition.position,
            _spawnPosition.rotation,
            _noteContainer
        );

        // +1 because the first beat index is too hard to hit
        note.BeatIndex = beatIndex + BeatsAhead + 1;
        _notes.Add(note);
    }
}
