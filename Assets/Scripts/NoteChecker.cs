using System;
using NUnit.Framework;
using UnityEngine;

public enum EHitQuality
{
    Miss,
    Good,
    Perfect
}

public class NoteChecker : MonoBehaviour
{
    [Header("Key Bindings")]
    [SerializeField] private KeyCode[] _keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };

    [SerializeField] private NoteSpawner _noteSpawner;

    private static NoteChecker _instance;
    public static NoteChecker Instance => _instance;

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
        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                _onNoteHit?.Invoke();
                switch (_keys[i])
                {
                    case KeyCode.D:
                        HitNote("joie");
                        break;
                    case KeyCode.F:
                        HitNote("tristesse");
                        break;
                    case KeyCode.J:
                        HitNote("colere");
                        break;
                    case KeyCode.K:
                        HitNote("surprise");
                        break;
                }
            }
        }
    }

    private EHitQuality HitNote(string tag)
    {
        // Find the closest note to hit
        if (_noteSpawner.Notes == null) return EHitQuality.Miss;
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
            return EHitQuality.Miss;
        }

        /* Check if the note has the correct tag
        if (closestNote.CompareTag(tag) == false)
        {
            Destroy(closestNote.gameObject);
            return EHitQuality.Miss;
        } //*/

        // Calculate the offset between the note's hit time and the current song time
        double TimingOffset = Mathf.Abs((float)(closestNote.HitTime - AudioManager.Instance.SongTime));
        Debug.Log($"Hit Time :  {closestNote.HitTime:F3} | Song Time: {AudioManager.Instance.SongTime:F3} | Hit offset: {TimingOffset:F3}");

        if (TimingOffset <= hitWindowPerfect)
        {
            Debug.Log("Perfect!");
            Destroy(closestNote.gameObject);
            return EHitQuality.Perfect;
        }
        else if (TimingOffset <= hitWindowGood)
        {
            Debug.Log("Good");
            Destroy(closestNote.gameObject);
            return EHitQuality.Good;
        }
        else
        {
            Destroy(closestNote.gameObject);
            return EHitQuality.Miss;
        }
    }
}
