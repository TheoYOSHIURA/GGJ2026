using Unity.VisualScripting;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _spriteIdle;
    [SerializeField] private Sprite _spriteJoie;
    [SerializeField] private Sprite _spriteTristesse;
    [SerializeField] private Sprite _spriteColere;
    [SerializeField] private Sprite _spriteSurprise;


    void Start()
    {
        NoteChecker.Instance.OnJoie += ChangeEmotionJoie;
        NoteChecker.Instance.OnTristesse += ChangeEmotionTristesse;
        NoteChecker.Instance.OnColere += ChangeEmotionColere;
        NoteChecker.Instance.OnSurprise += ChangeEmotionSurprise;
        NoteChecker.Instance.OnMiss += ResetEmotion;
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        NoteChecker.Instance.OnJoie -= ChangeEmotionJoie;
        NoteChecker.Instance.OnTristesse -= ChangeEmotionTristesse;
        NoteChecker.Instance.OnColere -= ChangeEmotionColere;
        NoteChecker.Instance.OnSurprise -= ChangeEmotionSurprise;
        NoteChecker.Instance.OnMiss -= ResetEmotion;
    }

    private void ChangeEmotionJoie()
    {
        _spriteRenderer.sprite = _spriteJoie;
    }

    private void ChangeEmotionTristesse()
    {
        _spriteRenderer.sprite = _spriteTristesse;
    }
    private void ChangeEmotionColere()
    {
        _spriteRenderer.sprite = _spriteColere;
    }
    private void ChangeEmotionSurprise()
    {
        _spriteRenderer.sprite = _spriteSurprise;
    }

    private void ResetEmotion()
    {
        _spriteRenderer.sprite = _spriteIdle;
    }
}
