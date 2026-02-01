using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    
    [SerializeField] private int count = 3;
    [SerializeField] private TextMeshProUGUI _textMeshProCountdown;

    void Start()
    {
        AudioManager.Instance.OnBeat += CountdownProcess;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        AudioManager.Instance.OnBeat -= CountdownProcess;
    }

    private void CountdownProcess(int beatIndex)
    {
        // -1 because the first beat index is too hard to hit
        _textMeshProCountdown.text = (Mathf.Abs(beatIndex - 1)).ToString();
        if (beatIndex - 1 >= 0)
        {
            Destroy(gameObject);
        }
    }
}
