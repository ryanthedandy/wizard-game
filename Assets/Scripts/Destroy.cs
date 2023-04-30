using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyTimer = 1.5f;
    
    void Start()
    {
    
        Destroy(gameObject, destroyTimer);
    }

    
}
