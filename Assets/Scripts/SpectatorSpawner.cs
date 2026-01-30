using UnityEngine;

public class SpectatorSpawner : MonoBehaviour
{
    [SerializeField] private SpectatorController _spectator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SpawnSpectator()
    {
        Instantiate(_spectator);
    }
}
