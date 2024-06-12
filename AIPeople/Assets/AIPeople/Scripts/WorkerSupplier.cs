using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSupplier : MonoBehaviour
{
    [SerializeField] private List<Worker> workers;
    [SerializeField] private List<Transform> homeLocations;

    [SerializeField] private Transform workLocation1;
    [SerializeField] private Transform workLocation2;

    [SerializeField] private float spawnDelay = 0.5f;

    [ContextMenu("GoWork1")]
    public void GoWork1()
    {
        GoToWork(5, workLocation1);
    }
    
    [ContextMenu("GoHome1")]
    public void GoHone1()
    {
        GoToHome(10, workLocation1);
    }
    
    public void GoToWork(int numOfWorkers, Transform workLocation)
    {
        StartCoroutine(SpawnWorkersWithDelay(numOfWorkers, workLocation));
    }

    private IEnumerator SpawnWorkersWithDelay(int numOfWorkers, Transform workLocation)
    {
        for (var i = 0; i < numOfWorkers; i++)
        {
            if (workers.Count == 0 || homeLocations.Count == 0)
            {
                Debug.LogWarning("No workers or home locations available to instantiate.");
                yield break;
            }

            var homeIndex = Random.Range(0, homeLocations.Count);
            var workerIndex = Random.Range(0, workers.Count);

            var workerPrefab = workers[workerIndex];
            var homeLocation = homeLocations[homeIndex];

            if (workerPrefab == null || homeLocation == null)
            {
                Debug.LogWarning("Worker prefab or home location is null.");
                continue;
            }

            var workerObj = Instantiate(workerPrefab, homeLocation.position, Quaternion.identity);
            workerObj.GoLocation(workLocation);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
    
    public void GoToHome(int numOfWorkers, Transform workLocation)
    {
        StartCoroutine(SpawnWorkersToHomeWithDelay(numOfWorkers, workLocation));
    }
    
    private IEnumerator SpawnWorkersToHomeWithDelay(int numOfWorkers, Transform workLocation)
    {
        for (var i = 0; i < numOfWorkers; i++)
        {
            if (workers.Count == 0 || homeLocations.Count == 0)
            {
                Debug.LogWarning("No workers or home locations available to instantiate.");
                yield break;
            }

            var homeIndex = Random.Range(0, homeLocations.Count);
            var workerIndex = Random.Range(0, workers.Count);

            var workerPrefab = workers[workerIndex];
            var homeLocation = homeLocations[homeIndex];

            if (workerPrefab == null || workLocation == null)
            {
                Debug.LogWarning("Worker prefab or work location is null.");
                continue;
            }

            var workerObj = Instantiate(workerPrefab, workLocation.position, Quaternion.identity);
            workerObj.GoLocation(homeLocation);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
