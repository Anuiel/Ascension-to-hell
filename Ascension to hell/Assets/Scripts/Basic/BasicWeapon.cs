using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField]
    int maxShots;
    int currentShots;
    // Start is called before the first frame update
    void Start()
    {
        currentShots = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shoot(Transform point){}
}
