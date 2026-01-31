using UnityEngine;
using System.Collections;

public class CharaBounce : MonoBehaviour
{
    
    public AnimationCurve bounceCurve;
    public float bounceDuration = 0.5f;
    private Vector3 initialScale;   
    void Awake()
    {
        initialScale = transform.localScale;
    }

    private IEnumerator Bounce()
    {
        float animTime = 0f;

        while (animTime < bounceDuration)
        {
            float scaleValue = bounceCurve.Evaluate(animTime / bounceDuration);
            transform.localScale = initialScale * scaleValue;

            animTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
    
    }

    public void TriggerBounce()
    {
        StopAllCoroutines();

        StartCoroutine(Bounce());

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
         
  
    }
}
