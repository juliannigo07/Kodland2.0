using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public AudioSource SonidoDisparo;
    // public CameraFollow cameraFollow;

    public float maxDistance = 5f;   // limite del mouse

    public GameObject puntoPrefab;
    public int numeroPuntos = 15;
    public float tiempoEntrePuntos = 0.1f;

    private List<GameObject> puntos = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numeroPuntos; i++)
        {
            GameObject p = Instantiate(puntoPrefab);
            puntos.Add(p);
        }
    }

    void Update()
    {
        if (GameManager.instance.gameOver) return;

        if (GameManager.instance.shotsLeft <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            GameManager.instance.UseShot();
        }

        VisualAim();
        DibujarTrayectoria();
    }

    void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3 rawDirection = worldPos - shootPoint.position;

        // limitar distancia
        Vector3 direction = Vector3.ClampMagnitude(rawDirection, maxDistance);

        direction.z = 0f;

        // separar horizontal y vertical
        float horizontal = direction.x;
        float vertical = direction.y;

        // crear fuerza (ajustable)
        float fuerza = Mathf.Abs(horizontal) * 2f;
        float altura = Mathf.Abs(vertical) * 5f;

        Vector3 finalForce = new Vector3(horizontal, altura, 0);

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(finalForce * fuerza, ForceMode.Impulse);

        SonidoDisparo.Play();
    }

    void VisualAim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3 direction = worldPos - shootPoint.position;
        direction = Vector3.ClampMagnitude(direction, maxDistance);

        direction.z = 0f; 
    }

    void DibujarTrayectoria()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3 direction = worldPos - shootPoint.position;
        direction = Vector3.ClampMagnitude(direction, maxDistance);
        direction.z = 0f;

        float horizontal = direction.x;
        float vertical = direction.y;

        float fuerza = Mathf.Abs(horizontal) * 2f;
        float altura = Mathf.Abs(vertical) * 5f;

        Vector3 finalForce = new Vector3(horizontal, altura, 0);

        Vector3 velocidadInicial = finalForce * fuerza / 1f; // masa = 1

        for (int i = 0; i < puntos.Count; i++)
        {
            float t = i * tiempoEntrePuntos;

            Vector3 posicion = shootPoint.position +
                               velocidadInicial * t +
                               0.5f * Physics.gravity * t * t;

            posicion.z = 0f;

            puntos[i].transform.position = posicion;
        }
    }
}
