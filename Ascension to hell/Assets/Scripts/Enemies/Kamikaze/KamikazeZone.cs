using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeZone : MonoBehaviour
{
    public bool explode;
    private SpriteRenderer sr;
    private Sprite explodeCircle;
    private Color explodeCircleColor;

    float timeSinceActive;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();   
        explodeCircle = sr.sprite;
        explodeCircleColor = sr.color;
        sr.sprite = null;
    }

    private void Start()
    {
        explode = false;
    }

    void Update() {
        if (explode) {
            timeSinceActive += Time.deltaTime;
            Color current_color = sr.color;
            current_color.a = Mathf.Clamp(timeSinceActive / 2, 0, 1f);
            sr.color = current_color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = explodeCircle;
            sr.color = explodeCircleColor;
            explode = true;
        }
    }
}
