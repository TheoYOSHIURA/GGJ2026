using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class SpectatorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _spriteNeutral;
    [SerializeField] private Sprite _spriteHappy;
    [SerializeField] private Sprite _spriteSad;
    private bool _hasToLeaveScene = false;
    private Transform _frontStagePoint;
    private Vector3 _closestPoint;
    private Rigidbody _rb;
    private Vector3 _leavePoint;
    private float _timerEnter;
    private float _timerLeave;
    private float _timerReset = 600f;

    public bool HasToLeaveScene { get => _hasToLeaveScene; set => _hasToLeaveScene = value; }
    public Transform FrontStagePoint { get => _frontStagePoint; set => _frontStagePoint = value; }

    void Start()
    {
        NoteChecker.Instance.OnPerfect += ChangeSpriteHappy;
        NoteChecker.Instance.OnGood += ResetSprite;
        NoteChecker.Instance.OnMiss += ChangeSpriteSad;


        Vector3 randomOffset = Random.insideUnitSphere * 1.5f;
        randomOffset.y = 0;
        _closestPoint = _frontStagePoint.position + randomOffset;
        _rb = GetComponent<Rigidbody>();
        _leavePoint = transform.position;
        _timerEnter = _timerReset;
        _timerLeave = _timerReset;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (HasToLeaveScene)
        {
            LeaveScene();
        }
        else
        {
            GoToFrontStage();
        }
    }

    void OnDestroy()
    {
        NoteChecker.Instance.OnPerfect -= ChangeSpriteHappy;
        NoteChecker.Instance.OnGood -= ResetSprite;
        NoteChecker.Instance.OnMiss -= ChangeSpriteSad;
    }

    private void LeaveScene()
    {
        _rb.MovePosition(Vector3.MoveTowards(transform.position, _leavePoint, Time.fixedDeltaTime * 2f));
        _timerLeave--;
        if (Vector3.Distance(transform.position, _leavePoint) < 1f || _timerLeave <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GoToFrontStage()
    {
        _timerEnter--;
        if (Vector3.Distance(transform.position, _closestPoint) > 1f && _timerEnter >= 0)
        {
            _rb.MovePosition(Vector3.MoveTowards(transform.position, _closestPoint, Time.fixedDeltaTime * 2f));
        }
    }

    private void ChangeSpriteHappy()
    {
        _spriteRenderer.sprite = _spriteHappy;
    }

    private void ChangeSpriteSad()
    {
        _spriteRenderer.sprite = _spriteSad;
    }

    private void ResetSprite()
    {
        _spriteRenderer.sprite = _spriteNeutral;
    }
}
