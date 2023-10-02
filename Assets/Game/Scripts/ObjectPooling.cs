using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Header("Define a quantidade de objetos que serão criados")]
    [SerializeField] private int amountToPool;
    
    [Header("Define qual o objeto que irá compor o pool")]
    [SerializeField] private GameObject prefab;
    
    private List<GameObject> pooledObjects = new();
    
    // Start is called before the first frame update
    private void Start()
    {
        for (int index = 0; index < amountToPool; index++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    public GameObject GetPooledObject()
    {
        for (int index = 0; index < pooledObjects.Count; index++)
        {
            if (!pooledObjects[index].activeInHierarchy)
            {
                return pooledObjects[index];
            }
        }
        return null;
    }
}
