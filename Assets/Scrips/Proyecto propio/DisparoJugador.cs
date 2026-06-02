using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = (worldPos - shootPoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * shootForce, ForceMode.Impulse);
    }
}
