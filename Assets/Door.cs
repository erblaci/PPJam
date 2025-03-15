using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorTransform;         // The door object to move (can be the same as this transform)
    public Vector3 openPositionOffset = new Vector3(0, 3, 0); // Offset from closed position when open
    public float openSpeed = 3f;              // Speed of door movement

    [Header("Pressure Plates")]
    public Button_Plate[] plates;          // Assign all pressure plates that control this door

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        // If no separate doorTransform is assigned, use self
        if (doorTransform == null)
            doorTransform = transform;

        closedPosition = doorTransform.position;
        openPosition = closedPosition + openPositionOffset;
    }

    void Update()
    {
        // Check if all assigned pressure plates are activated
        if (AllPlatesActivated())
        {
            if (!isOpen)
                OpenDoor();
        }
        else
        {
            if (isOpen)
                CloseDoor();
        }
    }

    bool AllPlatesActivated()
    {
        foreach (Button_Plate plate in plates)
        {
            if (!plate.isPressed)
                return false;
        }
        return true;
    }

    void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(openPosition));
        isOpen = true;
    }

    void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(closedPosition));
        isOpen = false;
    }

    System.Collections.IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = doorTransform.position;

        while (elapsedTime < 1f)
        {
            doorTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }
        doorTransform.position = targetPosition;
    }
}
