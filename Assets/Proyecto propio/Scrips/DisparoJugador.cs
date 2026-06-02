using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public AudioSource SonidoDisparo;
    public CameraFollow cameraFollow;

    void Update()
    {
        if (GameManager.instance.gameOver) return;

        if (GameManager.instance.shotsLeft <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            GameManager.instance.UseShot();
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
        SonidoDisparo.Play();
        cameraFollow.SetTarget(projectile.transform);
    }
}
