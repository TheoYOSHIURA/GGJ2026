using UnityEngine;
using System.Collections;

public class CheckerBounce : MonoBehaviour
{
    
    public AnimationCurve bounceCurve;
    public float bounceDuration = 0.5f;
    private Vector3 initialScale;   
    void Awake()
    {
        
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

    void Start()
    {
        initialScale = transform.localScale;
        //NoteChecker.Instance.OnNoteHit += TriggerBounce;
    }

    void Update()
    {
         
  
    }

    private void OnDestroy()
    {
        //NoteChecker.Instance.OnNoteHit -= TriggerBounce;
    }
}
