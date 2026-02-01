using System;
using NUnit.Framework;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum EHitQuality
{
    Miss,
    Good,
    Perfect
}

public class NoteChecker : MonoBehaviour
{
    [Header("Key Bindings")]
    [SerializeField] private KeyCode[] _keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4 };

    [SerializeField] private NoteSpawner _noteSpawner;

    [Header("Hit Settings")]
    [SerializeField] private float hitWindowPerfect = 0.05f; // 50 ms
    [SerializeField] private float hitWindowGood = 0.1f;     // 100 ms

    private int _misses = 0;

    private static NoteChecker _instance;
    public static NoteChecker Instance => _instance;

    public float HitWindowGood { get => HitWindowGood1; set => HitWindowGood1 = value; }
    public float HitWindowGood1 { get => hitWindowGood; set => hitWindowGood = value; }

    #region Events
    private event Action _onPerfect;
    public event Action OnPerfect
    {
        add { _onPerfect -= value; _onPerfect += value; }
        remove { _onPerfect -= value; }
    }

    private event Action _onGood;
    public event Action OnGood
    {
        add { _onGood -= value; _onGood += value; }
        remove { _onGood -= value; }
    }

    private event Action _onMiss;
    public event Action OnMiss
    {
        add { _onMiss -= value; _onMiss += value; }
        remove { _onMiss -= value; }
    }

    private event Action _onJoie;
    public event Action OnJoie
    {
        add { _onJoie -= value; _onJoie += value; }
        remove { _onJoie -= value; }
    }

    private event Action _onTristesse;
    public event Action OnTristesse
    {
        add { _onTristesse -= value; _onTristesse += value; }
        remove { _onTristesse -= value; }
    }

    private event Action _onColere;
    public event Action OnColere
    {
        add { _onColere -= value; _onColere += value; }
        remove { _onColere -= value; }
    }

    private event Action _onSurprise;
    public event Action OnSurprise
    {
        add { _onSurprise -= value; _onSurprise += value; }
        remove { _onSurprise -= value; }
    }
    #endregion Events

    

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
            DestroyNote(null, EHitQuality.Miss, tag);
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
            DestroyNote(null, EHitQuality.Miss, tag);
            return;
        }

        //* Check if the note has the correct tag
        if (closestNote.CompareTag(tag) == false)
        {
            DestroyNote(closestNote, EHitQuality.Miss, tag);
            return;
        } //*/

        // Calculate the offset between the note's hit time and the current song time
        double TimingOffset = Mathf.Abs((float)(closestNote.HitTime - AudioManager.Instance.SongTime));
        Debug.Log($"Hit Time :  {closestNote.HitTime:F3} | Song Time: {AudioManager.Instance.SongTime:F3} | Hit offset: {TimingOffset:F3}");

        if (TimingOffset <= hitWindowPerfect - AudioManager.Instance.VisualInputDelay)
        {
            DestroyNote(closestNote, EHitQuality.Perfect, tag);
            return;
        }
        else if (TimingOffset <= HitWindowGood - AudioManager.Instance.VisualInputDelay)
        {
            DestroyNote(closestNote, EHitQuality.Good, tag);
            return;
        }
        else
        {
            DestroyNote(closestNote, EHitQuality.Miss, tag);
            return;
        }
    }

    private void DestroyNote(NoteController note = null, EHitQuality quality = EHitQuality.Miss, string tag = "")
    {

        switch (quality)
        {
            case EHitQuality.Perfect:
                _misses = 0;
                ScoreManager.Instance.Score += 300;
                _onPerfect?.Invoke();
                break;
            case EHitQuality.Good:
                if (_misses > 0) _misses--;
                ScoreManager.Instance.Score += 100;
                _onGood?.Invoke();
                break;
            case EHitQuality.Miss:
                ScoreManager.Instance.Score -= 50;
                _misses++;
                if (_misses >= 5)
                {
                    SceneManager.LoadScene("GameOver");
                }
                _onMiss?.Invoke();
                break;
        }

        if (quality != EHitQuality.Miss)
        {
            switch (tag)
            {
                case "joie":
                    _onJoie?.Invoke();
                    break;
                case "tristesse":
                    _onTristesse?.Invoke();
                    break;
                case "colere":
                    _onColere?.Invoke();
                    break;
                case "surprise":
                    _onSurprise?.Invoke();
                    break;
            }
        }
        if (note == null) return;

        NoteSpawner.Instance.Notes.Remove(note);
        Destroy(note.gameObject);
    }
}
