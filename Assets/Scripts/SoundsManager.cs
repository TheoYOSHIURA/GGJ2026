using UnityEngine;
using System.Collections.Generic;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }
    public Dictionary<string, AudioClip> SoundDict { get => _soundDict; set => _soundDict = value; }

    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioSource _audioSource;
    private Dictionary<string, AudioClip> _soundDict;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

      
        SoundDict = new Dictionary<string, AudioClip>();
        foreach (var clip in _sounds)
        {
            if (clip != null && !SoundDict.ContainsKey(clip.name))
                SoundDict.Add(clip.name, clip);
        }
    }

    public void PlaySound(string soundName)
    {
        if (SoundDict.TryGetValue(soundName, out var clip))
        {
            _audioSource.PlayOneShot(clip);
        }
        
    }
}
