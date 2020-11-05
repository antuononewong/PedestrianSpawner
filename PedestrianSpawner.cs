using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    // Dependancies
    public GameObject pedestrianPrefab;

    private RuntimeAnimatorController[] _pedestrianAnimators;

    // Play space
    private float _heightBounds;
    private float _widthBounds;

    // Game logic
    private int _initialPedestrians = 400;
    private float _maxSpawnTimer = .25f;
    private float _spawnTimer;

    void Awake()
    {
        _pedestrianAnimators = Resources.LoadAll<RuntimeAnimatorController>("AnimationControllers");
        Camera camera = Camera.main;
        _heightBounds = camera.orthographicSize - 10f;
        _widthBounds = camera.orthographicSize * 1.15f;
        InitialSpawns();
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnPedestrian();
            _spawnTimer = _maxSpawnTimer;
        }
    }

    private void SpawnPedestrian()
    {
        int controllerIndex = Random.Range(0, _pedestrianAnimators.Length);
        GameObject pedestrian = Instantiate<GameObject>(pedestrianPrefab);
        pedestrian.transform.position = new Vector3(Random.Range(-_widthBounds, _widthBounds), Random.Range(-_heightBounds, _heightBounds), 0f);
        pedestrian.GetComponent<PedestrianController>().SetInitialNextPosition(pedestrian.transform.position);
        pedestrian.GetComponent<PedestrianController>().SetAnimationController(_pedestrianAnimators[controllerIndex]);
    }

    private void InitialSpawns()
    {
        for (int i = 0; i < _initialPedestrians; i++)
        {
            SpawnPedestrian();
        }
    }
}
