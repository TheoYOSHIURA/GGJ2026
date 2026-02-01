using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _missClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NoteChecker.Instance.OnMiss += PlaySoundEffectMiss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        NoteChecker.Instance.OnMiss -= PlaySoundEffectMiss;
    }

    private void PlaySoundEffectMiss()
    {
        AudioSource.PlayClipAtPoint(_missClip, Camera.main.transform.position);
    }
}
