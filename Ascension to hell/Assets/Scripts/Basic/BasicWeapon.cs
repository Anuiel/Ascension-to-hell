using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField]
    int maxShots;
    int currentShots;
    private Camera cm;

    // Start is called before the first frame update
    void Start()
    {
        currentShots = maxShots;
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shoot(Vector2 point)
    {
        Vector2 shotPoint = cm.ScreenToWorldPoint(point);
        Debug.Log(shotPoint);
        currentShots -= 1;
    }
}
