using UnityEngine;
using System.Collections;

public class GunRaycast : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;

    public LineRenderer tracer;
    public Transform muzzle; // 銃口


    public float fireRate = 0.1f;
    float nextFireTime = 0f;

    void Start()
    {
        tracer.positionCount = 2;

        tracer.startWidth = 0.05f;
        tracer.endWidth = 0.05f;

        tracer.material = new Material(Shader.Find("Unlit/Color"));
        tracer.material.color = Color.yellow;

        tracer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Shoot();
        }

        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 endPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            endPoint = hit.point;

            // ヒットデバッグ
            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.red, 1f);

            //  敵にダメージ
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
        }
        else
        {
            endPoint = ray.origin + ray.direction * range;
        }

        StartCoroutine(ShowTracer(muzzle.position, endPoint));
    }


    IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        tracer.SetPosition(0, start);
        tracer.SetPosition(1, end);

        tracer.enabled = true;

        yield return new WaitForSeconds(0.2f); 

        tracer.enabled = false;
    }
}
