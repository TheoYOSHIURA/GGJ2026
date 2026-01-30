using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
/*
public class NoteChecker : MonoBehaviour
{

    private KeyCode[] _keys = { KeyCode.d, KeyCode.f, KeyCode.j, KeyCode.k };
    [SerializeField] private EMasques _masque;

    private Collider[] _notes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetKey();
    }
    private void GetKey()
    {
        if (Input.GetKeyDown(_keys[0]))
        {

            if (_notes[0] != null && _notes[0].CompareTag("joie"))
            {
                _notes.RemoveAt(0);
                Destroy(_notes[0].gameObject);
            }
        }

        if (Input.GetKeyDown(_keys[1]))
        {
            if (_notes[0] != null && _notes[0].CompareTag("colere"))
            {
                _notes.RemoveAt(0);
                Destroy(_notes[0].gameObject);
            }
        }
        if (Input.GetKeyDown(_keys[2]))
        {
            if (_notes[0] != null && _notes[0].CompareTag("tristesse"))
            {
                _notes.RemoveAt(0);
                Destroy(_notes[0].gameObject);


            }
        }
        if (Input.GetKeyDown(_keys[3]))
        {
            if (_notes[0] != null && _notes[0].CompareTag("surprise"))
            {
                _notes.RemoveAt(0);
                Destroy(_notes[0].gameObject);
            }
        }
    }


    OnTriggerEnter(Collider other)
    {

        _notes.Add(other);

    }

}
*/