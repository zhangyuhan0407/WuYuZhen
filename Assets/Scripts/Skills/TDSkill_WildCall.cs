using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSkill_WildCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Enemies/Wolf");
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(transform.parent);
        obj.transform.position = transform.position;

        obj.GetComponent<EnemyController>().destIndex = gameObject.GetComponent<EnemyController>().destIndex;
        obj.GetComponent<EnemyController>().Destinations = gameObject.GetComponent<EnemyController>().Destinations;
    }

}
