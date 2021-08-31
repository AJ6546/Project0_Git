using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int size;
        public GameObject prefab;
        public string tag;
    }
    public static PoolManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Dictionary<string, Queue<GameObject>> pooldictionary;
    [SerializeField] List<Pool> pools = new List<Pool>();
    void Start()
    {
        pooldictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for(int i=0;i<pool.size;i++)
            {
                Vector3 pos = new Vector3(Random.Range(-50, 50), 2, Random.Range(-50, 50));
                GameObject prefab=Instantiate(pool.prefab, pos, transform.rotation);
                ActivatePrefab(ref prefab);
                objPool.Enqueue(prefab);
            }
            pooldictionary.Add(pool.tag,objPool);
        }
    }
    public void Spawn(string tag,Vector3 pos,Quaternion rot, string instantiator, Vector3 targetPos)
    {
        GameObject obj = pooldictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.GetComponent<Projectiles>().SetInstantiator(instantiator);
        obj.GetComponent<Projectiles>().SetAimLocation(targetPos);
        obj.GetComponent<Projectiles>().Move(true);
        pooldictionary[tag].Enqueue(obj);
    }
    void ActivatePrefab(ref GameObject prefab)
    {
        switch (prefab.tag)
        {
            case "Pickup":
                prefab.SetActive(true);
                break;
            default:
                prefab.SetActive(false);
                break;
        }
    }
}
