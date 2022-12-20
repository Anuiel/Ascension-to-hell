using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField]
    GameObject tagret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(tagret.transform.position.x, tagret.transform.position.y, transform.position.z);
    }
}
