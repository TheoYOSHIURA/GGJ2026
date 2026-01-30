using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private float _spawnDistance = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private NoteSpawner _noteSpawner;
    [SerializeField] private Transform _noteChecker;
    //[SerializeField] private NoteChecker _noteChecker;

    public NoteSpawner NoteSpawner { get => _noteSpawner; set => _noteSpawner = value; }
    public Transform NoteChecker { get => _noteChecker; set => _noteChecker = value; }

    void Start()
    {
        _spawnDistance = NoteChecker.position.x - transform.position.x;
        _speed = _spawnDistance * NoteSpawner.Bpm / 3600;
    }

    void Update()
    {
            
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + _speed, transform.position.y, transform.position.z);
    }
}
