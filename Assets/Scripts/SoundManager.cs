using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _missClip;
    [SerializeField] private AudioClip _clickClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NoteChecker.Instance.OnMiss += PlaySoundEffectMiss;
        NoteChecker.Instance.OnPerfect += PlaySoundEffectClick;
        NoteChecker.Instance.OnGood += PlaySoundEffectClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySoundEffectClick()
    {
        AudioSource.PlayClipAtPoint(_clickClip, Camera.main.transform.position);
    }
    void OnDestroy()
    {
        NoteChecker.Instance.OnMiss -= PlaySoundEffectMiss;
        NoteChecker.Instance.OnPerfect -= PlaySoundEffectClick;
        NoteChecker.Instance.OnGood -= PlaySoundEffectClick;
    }

    private void PlaySoundEffectMiss()
    {
        AudioSource.PlayClipAtPoint(_missClip, Camera.main.transform.position);
        CameraShake.Instance.ShakeCamera();

    }
}
