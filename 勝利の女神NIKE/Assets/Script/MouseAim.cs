using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public Camera cam;
    public LayerMask groundLayer;
    public Transform aimTarget;

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            
            if (aimTarget != null)
                aimTarget.position = hit.point;

          
            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            transform.forward = dir;
        }
    }
}