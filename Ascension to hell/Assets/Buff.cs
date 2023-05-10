using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{

    [SerializeField]
    public float power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ApplyPower(float stat) {
        return stat * (1 + power / 10);
    }
}
