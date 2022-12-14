using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    [SerializeField]
    protected int maxShots;
    protected int currentShots;
    protected Camera cm;
    protected Vector2 shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        Starting();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shoot(Vector2 point)
    {
        shotPoint = GetMousePosition(point);
        currentShots -= 1;
    }

    protected virtual void Starting()
    {
        currentShots = maxShots;
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    protected virtual Vector2 GetMousePosition(Vector2 point)
    {
        Vector2 MousePos = cm.ScreenToWorldPoint(point);
        return MousePos;
    }
}
