using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathFinding : MonoBehaviour
{
    [SerializeField]
    AIPath path;

    private void Awake() {
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").GetComponent<Transform>();
    }
}
