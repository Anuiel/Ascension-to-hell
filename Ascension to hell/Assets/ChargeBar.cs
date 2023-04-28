using UnityEngine.UI;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    [SerializeField]
    Image barImage;

    [SerializeField]
    public PlayerController player;

    [SerializeField]
    bool isFixed;

    private Quaternion startRotation;
    private Vector3 startPositionDelta;
    private Vector3 startPosition;

    void Start() {
        startRotation = transform.rotation;
        startPosition = transform.position;
        startPositionDelta = player.transform.position - transform.position;
    }

    void Update() {
        transform.rotation = startRotation;
        if (isFixed) {
            transform.position = startPosition;
        } else {
            transform.position = player.transform.position - startPositionDelta;
        }
    }

    public void UpdateBar() {
        barImage.fillAmount = Mathf.Clamp(player.dashEnergy / player.dashAmount, 0, 1f);
    }
}
