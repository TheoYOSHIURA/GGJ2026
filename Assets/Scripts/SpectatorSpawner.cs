using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpectatorSpawner : MonoBehaviour
{
    [SerializeField] private SpectatorController _spectator;
    [SerializeField] private Transform[] _spawnpoints;
    [SerializeField] private Transform _spectatorContainer;
    [SerializeField] private Transform _frontStagePoint;

    void Start()
    {
        ScoreManager.Instance.OnScoreIncrease += SpawnSpectator;
        ScoreManager.Instance.OnScoreDecrease += RemoveSpectator;
    }

    void Update()
    {
        
    }

    private void SpawnSpectator()
    {
        int randomId = Random.Range(0, _spawnpoints.Length);
        SpectatorController specatator = Instantiate(_spectator, _spawnpoints[randomId].position, _spawnpoints[randomId].rotation, _spectatorContainer);
        specatator.FrontStagePoint = _frontStagePoint;
    }

    private void RemoveSpectator()
    {
        if (_spectatorContainer.childCount > 0)
        {
            int randomId = Random.Range(0, _spectatorContainer.childCount);
            Transform spectatorToRemove = _spectatorContainer.GetChild(randomId);
            SpectatorController spectatorController = spectatorToRemove.GetComponent<SpectatorController>();
            spectatorController.HasToLeaveScene = true;
        }
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreIncrease -= SpawnSpectator;
        ScoreManager.Instance.OnScoreDecrease -= RemoveSpectator;
    }
}
