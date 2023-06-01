using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private int amountObjects;
    [SerializeField] private GameObject objects;
    private List<GameObject> _poolingObjects;

    private void Start() => InitializedObjects();

    private void Update() => Fire();

    private void InitializedObjects()
    {
        _poolingObjects = new List<GameObject>();
        GameObject tempObject;

        for (int i = 0; i < amountObjects; i++)
        {
            tempObject = Instantiate(objects);
            tempObject.transform.SetParent(this.transform);
            tempObject.SetActive(false);
            _poolingObjects.Add(tempObject);
        }
    }
    public GameObject GetObjects()
    {
        for(int i = 0; i < _poolingObjects.Count; i++)
        {
            if(!_poolingObjects[i].activeInHierarchy)
            { 
                return _poolingObjects[i];
            }
        }

        return null;
    }

    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject cube = GetObjects();

            if(cube != null)
            {
                cube.SetActive(true);
                StartCoroutine(DeactiveObject(cube));
            }
        }
    }

    private IEnumerator DeactiveObject(GameObject cube)
    {
        yield return new WaitForSeconds(2f);
        cube.SetActive(false);
    }    
}
