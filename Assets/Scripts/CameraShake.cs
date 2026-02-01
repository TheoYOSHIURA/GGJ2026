using System;
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

public static CameraShake Instance { get; private set; }
    private float shakeDuration = 0.3f;
    private float shakeMagnitude = 0.2f;
    private Vector3 initialPosition; 

    void Awake()
    {
        initialPosition = transform.localPosition;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //shake the camera
    public void ShakeCamera()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Shake()
    {
        float shakeTime = 0.2f;

        while (shakeTime < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);

            shakeTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}