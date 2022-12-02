using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // basic movement
        Vector2 movement = new Vector2(playerInput.actions["Hor_mov"].ReadValue<float>(), playerInput.actions["Ver_mov"].ReadValue<float>());
        movement = Time.deltaTime * speed * movement;
        gameObject.transform.Translate(movement);
    }
}
