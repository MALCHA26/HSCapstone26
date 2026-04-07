using UnityEngine;

public class BookMocapInteraction : MonoBehaviour
{
    [Header("Grab Settings")]

    [SerializeField] private Transform grabPoint;        
    [SerializeField] private string grabbableTag = "Grabbable";
    [SerializeField] private float grabRadius = 0.15f;
    [SerializeField] private float snapSpeed = 15f;

    private Transform grabbedObject = null;
    private Rigidbody grabbedRb = null;

    void Update()
    {
        if (grabbedObject == null)
        {
            FindAndGrabObject();
        }
        else
        {
            UpdateGrabbedObjectPosition();
        }
    }

    private void FindAndGrabObject()
    {
        Collider[] colliders = Physics.OverlapSphere(grabPoint.position, grabRadius);

        Transform closestObj = null;
        float minDistance = float.MaxValue;

        foreach (Collider col in colliders)
        {
            if (col.CompareTag(grabbableTag))
            {
                float dist = Vector3.Distance(grabPoint.position, col.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestObj = col.transform;
                }
            }
        }

        if (closestObj != null)
        {
            Grab(closestObj);
        }
    }

    private void Grab(Transform obj)
    {
        grabbedObject = obj;
        grabbedRb = obj.GetComponent<Rigidbody>();

        if (grabbedRb != null)
        {
            grabbedRb.isKinematic = true;
            grabbedRb.useGravity = false;
        }
    }

    private void UpdateGrabbedObjectPosition()
    {
        grabbedObject.position = grabPoint.position;
        grabbedObject.rotation = grabPoint.rotation;
    }

    void OnDrawGizmosSelected()
    {

        if (grabPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(grabPoint.position, grabRadius);
        }
    }
}
