using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    [SerializeField]
    List<GameObject> guns;
    [SerializeField]
    GameObject wall;
    [SerializeField]
    int width;
    [SerializeField]
    int height;

    List<GameObject> SpawnedGuns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -width/2; i < width / 2 + 1; i++)
        {
            Instantiate(wall, transform.position + new Vector3(i, height / 2, 0), wall.transform.rotation);
            Instantiate(wall, transform.position + new Vector3(i, -height / 2, 0), wall.transform.rotation);
        }

        for (int i = -height / 2 + 1; i < height / 2; i++)
        {
            Instantiate(wall, transform.position + new Vector3(width / 2, i, 0), wall.transform.rotation);
            Instantiate(wall, transform.position + new Vector3(-width / 2, i, 0), wall.transform.rotation);
        }
        SpawnGuns();
    }

    public void SpawnGuns()
    {
        for (int i = 0; i < SpawnedGuns.Count; i++)
        {
            Destroy(SpawnedGuns[i]);
        }
        SpawnedGuns = new List<GameObject>();
        for (int idx = 0; idx < guns.Count; idx++)
        {
            GameObject gun = Instantiate(guns[idx], transform.position + new Vector3(idx * 3 - guns.Count, -2, 0), guns[idx].transform.rotation);
            gun.GetComponent<BasicWeapon>().player = GameObject.Find("Player").GetComponent<PlayerController>();
            SpawnedGuns.Add(gun);
        }
    }

}
