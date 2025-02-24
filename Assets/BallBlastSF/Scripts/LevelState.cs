using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private Cart _cart;
    [SerializeField] private StoneSpawner _stoneSpawner;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private float _timer;
    private bool _checkPassed;

    private void Awake()
    {
        _cart.OnStoneCollision.AddListener(OnCartCollisionWithStone);
        _stoneSpawner.Completed.AddListener(OnSpawnCompleted);
    }

    private void OnDestroy()
    {
        _cart.OnStoneCollision.RemoveListener(OnCartCollisionWithStone);
        _stoneSpawner.Completed.RemoveListener(OnSpawnCompleted);
    }

    public void OnCartCollisionWithStone()
    {
        Defeat.Invoke();
    }

    public void OnSpawnCompleted()
    {
        _checkPassed = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
        {
            if (_checkPassed == true && FindObjectsOfType<Stone>().Length == 0)
                Passed.Invoke();

            _timer = 0f;
        }
    }


}
