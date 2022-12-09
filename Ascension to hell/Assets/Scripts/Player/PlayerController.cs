using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    private PlayerInput playerInput;
    private PlayerBeing playerBeing;
    private List<GameObject> pickUpWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        pickUpWeapon = new();
        playerInput = GetComponent<PlayerInput>();
        playerBeing = GetComponent<PlayerBeing>();
    }

    // Update is called once per frame
    void Update()
    {
        // basic movement
        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        Vector2 movement = Time.deltaTime * speed * movementVector;
        gameObject.transform.Translate(movement);

        // shooting
        Vector2 shootingPoint = playerInput.actions["ShootingTarget"].ReadValue<Vector2>();
        if (playerInput.actions["Shooting"].triggered)
        {
            playerBeing.ShootGun(shootingPoint);
        }

        // pick up weapon
        if (pickUpWeapon.Count != 0)
        {
            if (playerInput.actions["CollectGun"].triggered)
            {
                playerBeing.WeaponPick(pickUpWeapon[0].GetComponent<BasicWeapon>());
            }
        }
    }

    Vector2 SpeedScaler(float speed, float hor, float ver)
    {
        Vector2 res = new(speed * hor, speed * ver);
        if (hor * ver != 0)
            res /= Mathf.Sqrt(2);
        return res;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Remove(collision.gameObject);
        }
    }
}
