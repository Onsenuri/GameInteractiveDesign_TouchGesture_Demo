using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

}
