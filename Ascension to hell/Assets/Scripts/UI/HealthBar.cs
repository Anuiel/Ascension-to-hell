using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image barImage;

    [SerializeField]
    public LivingCreature creature;

    [SerializeField]
    bool isFixed;
    
    private Quaternion startRotation;
    private Vector3 startPositionDelta;
    private Vector3 startPosition;

    void Start() {
        startRotation = transform.rotation;
        startPosition = transform.position;
        startPositionDelta = creature.transform.position - transform.position;
    }

    void Update() {
        transform.rotation = startRotation;
        if (isFixed) {
            transform.position = startPosition;
        } else {
            transform.position = creature.transform.position - startPositionDelta;
        }
    }

    public void UpdateBar() {
        barImage.fillAmount = Mathf.Clamp(creature.currentHP / creature.maxHP, 0, 1f);
    }
}
