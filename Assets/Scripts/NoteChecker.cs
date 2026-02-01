using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EHitQuality
{
    Miss,
    Good,
    Perfect
}

public class NoteChecker : MonoBehaviour
{
    [Header("Key Bindings")]
    [SerializeField] private KeyCode[] _keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K,  KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4};

    [SerializeField] private NoteSpawner _noteSpawner;

    private static NoteChecker _instance;
    public static NoteChecker Instance => _instance;

    public float HitWindowGood { get => hitWindowGood; set => hitWindowGood = value; }

    private event Action _onNoteHit;
    public event Action OnNoteHit
    {
        add { _onNoteHit -= value; _onNoteHit += value; }
        remove { _onNoteHit -= value; }
    }

    [Header("Hit Settings")]
    [SerializeField] private float hitWindowPerfect = 0.05f; // 50 ms
    [SerializeField] private float hitWindowGood = 0.1f;     // 100 ms

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(_keys[0]) || Input.GetKeyDown(_keys[4]))
        {
            HitNote("joie");
        }
        if (Input.GetKeyDown(_keys[1]) || Input.GetKeyDown(_keys[5]))
        {
            HitNote("tristesse");
        }
        if (Input.GetKeyDown(_keys[2]) || Input.GetKeyDown(_keys[6]))
        {
            HitNote("colere");
        }
        if (Input.GetKeyDown(_keys[3]) || Input.GetKeyDown(_keys[7]))
        {
            HitNote("surprise");
        }
    }

    private void HitNote(string tag)
    {
        // Find the closest note to hit
        if (_noteSpawner.Notes == null)
        {
            DestroyNote(null, EHitQuality.Miss);
            return;
        }

        NoteController closestNote = null;
        double smallestOffset = double.MaxValue;

        foreach (var note in _noteSpawner.Notes)
        {
            double offset = AudioManager.Instance.SongTime - note.HitTime;
            double absOffset = Math.Abs(offset);

            if (absOffset < smallestOffset)
            {
                smallestOffset = absOffset;
                closestNote = note;
            }
        }
        if (closestNote == null)
        {
            DestroyNote(null, EHitQuality.Miss);
            return;
        }

        //* Check if the note has the correct tag
        if (closestNote.CompareTag(tag) == false)
        {
            DestroyNote(closestNote, EHitQuality.Miss);
            return;
        } //*/

        // Calculate the offset between the note's hit time and the current song time
        double TimingOffset = Mathf.Abs((float)(closestNote.HitTime - AudioManager.Instance.SongTime));
        Debug.Log($"Hit Time :  {closestNote.HitTime:F3} | Song Time: {AudioManager.Instance.SongTime:F3} | Hit offset: {TimingOffset:F3}");

        if (TimingOffset <= hitWindowPerfect - AudioManager.Instance.VisualInputDelay)
        {
            DestroyNote(closestNote, EHitQuality.Perfect);
            return;
        }
        else if (TimingOffset <= HitWindowGood - AudioManager.Instance.VisualInputDelay)
        {
            DestroyNote(closestNote, EHitQuality.Good);
            return;
        }
        else
        {
            DestroyNote(closestNote, EHitQuality.Miss);
            return;
        }
    }

    private void DestroyNote(NoteController note = null, EHitQuality quality = EHitQuality.Miss)
    {
        
        switch (quality)
        {
            case EHitQuality.Perfect:
                ScoreManager.Instance.Score += 300;
                Debug.Log("Perfect Hit!");
                break;
            case EHitQuality.Good:
                ScoreManager.Instance.Score += 100;
                Debug.Log("Good Hit!");
                break;
            case EHitQuality.Miss:
                ScoreManager.Instance.Score -= 50;
                Debug.Log("Missed Note!");
                break;
        }

        if (note == null) return;
        
        NoteSpawner.Instance.Notes.Remove(note);
        Destroy(note.gameObject);
    }
}
