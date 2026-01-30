using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class NoteChecker : MonoBehaviour
{

    private KeyCode[] _keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };
    private List<Collider2D> _notes = new List<Collider2D>();

    void Start()
    {

    }

    void Update()
    {
        GetKey();
    }
    private void GetKey()
    {
        if (Input.GetKeyDown(_keys[0]))
        {
            BurnNote("joie");
        }

        if (Input.GetKeyDown(_keys[1]))
        {
            BurnNote("tristesse");
        }

        if (Input.GetKeyDown(_keys[2]))
        {
            BurnNote("colere");
        }

        if (Input.GetKeyDown(_keys[3]))
        {
            BurnNote("surprise");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _notes.Add(collision);
        //Debug.Log("Added a note");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _notes.Remove(collision);
        //Debug.Log("Remove a note");
    }

    private void BurnNote(string tag)
    {
        if (_notes.Count > 0)
        {
            if (_notes[0].CompareTag(tag))
            {
                float distance = Mathf.Abs(_notes[0].transform.position.x - transform.position.x);
                ScoreManager.Instance.Score += Mathf.RoundToInt(100 - distance);
            }
            else
            {
                ScoreManager.Instance.Score -= 10;
            }
            GameObject note = _notes[0].gameObject;
            _notes.RemoveAt(0);
            Destroy(note);
        }

        Debug.Log(ScoreManager.Instance.Score);
    }
}
