using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class WispSpawner : MonoBehaviour
    {
        public static WispSpawner Instance { get; private set; }

        [SerializeField] 
        private GameObject wispPrefab;
        [SerializeField] 
        private int initialPoolSize = 20;

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

        public GameObject GetWisp()
        {
            // Reuse wisp from pool or create a new one if pool is empty
            if (wispPool.Count > 0)
            {
                GameObject wisp = wispPool.Dequeue();
                wisp.SetActive(true);
                return wisp;
            }
            else
            {
                GameObject newWisp = Instantiate(wispPrefab);
                return newWisp;
            }
        }

        public void ReturnWisp(GameObject wisp)
        {
            wisp.SetActive(false);
            wispPool.Enqueue(wisp);
        }
    }