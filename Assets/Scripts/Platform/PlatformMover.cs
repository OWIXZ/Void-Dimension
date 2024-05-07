using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector2 movementVector = new Vector2(0f, 0f);  // Déplacement sur les axes X et Y
    public float speed = 2f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingToEnd = true;
    private WeightSensitivePlatform weightPlatform;  // Référence au script WeightSensitivePlatform

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x + movementVector.x, startPosition.y + movementVector.y, startPosition.z);
        weightPlatform = GetComponent<WeightSensitivePlatform>();  // Obtenez le script sur le même GameObject
    }

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float step = speed * Time.deltaTime;
        Vector3 targetPosition = movingToEnd ? endPosition : startPosition;
        targetPosition.y += weightPlatform.VerticalOffset;  // Appliquez l'offset vertical

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            movingToEnd = !movingToEnd;
        }
    }
}
