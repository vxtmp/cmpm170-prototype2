using System.Collections.Generic;
using UnityEngine;

public class WispSpawner : MonoBehaviour
{
    public static WispSpawner Instance { get; private set; }

    [SerializeField]
    private GameObject wispPrefab;
    [SerializeField]
    private int initialPoolSize = 20;
    private int currentActive = 0;

    private float timeSinceLastSpawn = 0.0f;
    private float spawnRate = 0.8f;


    // define a region that should contain the wisp objects
    //[SerializeField] private GameObject wispRegion;

    private Queue<GameObject> wispPool = new Queue<GameObject>();

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Prepopulate pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject wisp = Instantiate(wispPrefab);
            wisp.transform.parent = this.transform;
            wisp.SetActive(false);
            wispPool.Enqueue(wisp);
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnRate)
        {
            if (currentActive < initialPoolSize)
            {
                GetWisp();
            }
            timeSinceLastSpawn = 0.0f;
        }
    }

    public GameObject GetWisp()
    {
        GameObject newWisp;
        // Reuse wisp from pool or create a new one if pool is empty
        if (wispPool.Count > 0)
        {
            newWisp = wispPool.Dequeue();
        }
        else
        {
            newWisp = Instantiate(wispPrefab);
        }
        newWisp.transform.position = getRandomPositionInRoom();
        newWisp.SetActive(true);
        newWisp.GetComponent<Wisp>().makeInvisible();
        currentActive++;
        return newWisp;
    }

    public Vector3 getRandomPositionInRoom()
    {
        float x = Random.Range(-4.0f, 4.0f);
        float y = Random.Range(2.0f, 4.0f); // avoiding clipping with chair / table
        float z = Random.Range(-4.0f, 4.0f);
        return new Vector3(x, y, z);
    }

    public void ReturnWisp(GameObject wisp)
    {
        wisp.SetActive(false);
        wispPool.Enqueue(wisp);
        currentActive--;
    }
}