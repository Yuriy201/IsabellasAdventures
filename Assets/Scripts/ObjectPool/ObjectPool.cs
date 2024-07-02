using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<GameObject, Queue<GameObject>> _poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> _pooledOriginObject = new Dictionary<GameObject, GameObject>();

    [SerializeField] private int _poolSize;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab, Transform target)
    {
        if (_poolDictionary.ContainsKey(prefab) == false)
        {
            InitializeNewPool(prefab);
        }

        GameObject objectToGet = _poolDictionary[prefab].Dequeue();

        if (_poolDictionary[prefab].Count == 0)
        {
            CreateNewObject(prefab);
        }

        objectToGet.transform.parent = null;
        objectToGet.transform.position = target.position;
        objectToGet.SetActive(true);

        return objectToGet;
    }

    public void ReternObject(GameObject objectToRetern, float delay = 0.001f)
    {
        StartCoroutine(ReturnToPool(objectToRetern, delay));
    }

    private void InitializeNewPool(GameObject prefab)
    {
        _poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            CreateNewObject(prefab);
        }
    }

    private void CreateNewObject(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.SetActive(false);

        _poolDictionary[prefab].Enqueue(newObject);
        _pooledOriginObject[newObject] = prefab;
    }

    private IEnumerator ReturnToPool(GameObject objectToRetern, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject originalPrefab = _pooledOriginObject[objectToRetern];

        objectToRetern.SetActive(false);
        objectToRetern.transform.parent = transform;

        _poolDictionary[originalPrefab].Enqueue(objectToRetern);
    }
}
