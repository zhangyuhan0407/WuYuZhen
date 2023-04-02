using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDSkill : MonoBehaviour
{
    [HideInInspector]
    public TDOperator op;

    // Start is called before the first frame update
    void Start()
    {
        op = gameObject.GetComponent<TDOperator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void CastSkill()
    {
        Debug.LogWarning(gameObject.name + " CastSkill");
    }


}
