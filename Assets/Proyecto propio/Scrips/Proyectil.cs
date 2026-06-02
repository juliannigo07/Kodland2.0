using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public GameObject hitEffect;
    float lifeTime = 5f;

    void Start()
    {
        Invoke("DestroyProjectileTime", lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Manzana") || collision.gameObject.CompareTag("Persona"))
        {
            CancelInvoke();
        }

        if (collision.gameObject.CompareTag("Manzana"))
        {
            GameManager.instance.HitApple();
            GameManager.instance.SonidoManzana.Play();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);     
            Destroy(gameObject);
            CameraShake.instance.Shake(0.2f, 0.2f);           
        }
        else if (collision.gameObject.CompareTag("Persona"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            GameManager.instance.HitPerson();         
            CameraShake.instance.Shake(0.4f, 0.4f);
            GameManager.instance.SonidoPersona.Play();
            Destroy(gameObject);
        }
    }

    void DestroyProjectileTime()
    {
        GameManager.instance.MissShot();
        Destroy(gameObject);
        Camera.main.GetComponent<CameraFollow>().ResetCamera();
    }
}
