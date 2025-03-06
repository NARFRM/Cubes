using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMov : MonoBehaviour
{
    public float jumpForce = 10.0f;  // Fuerza de salto
    public float torqueForce = 10f;  // Fuerza de torque (giro de la esfera)
    public Transform cameraTransform;  // Cámara para orientar el movimiento
    private Rigidbody rb;  // Componente Rigidbody de la esfera
    private bool isGrounded;  // Verifica si la esfera está en el suelo

    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform enemy;  // Referencia al enemigo

    private GameObject activeProjectile;  // Referencia al proyectil activo


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f; // Limita la velocidad angular
    }

    void FixedUpdate()
    {
        // Movimiento de la esfera
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 torqueDirection = (right * moveZ - forward * moveX).normalized;
        rb.AddTorque(torqueDirection * torqueForce, ForceMode.Force);

        // Salto si está en el suelo y se presiona espacio
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        

        /*

        // Disparar proyectil con clic izq. (0)
        if (Input.GetMouseButtonDown(0) && activeProjectile == null)
        {
            activeProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            ProjectileBehavior projScript = activeProjectile.GetComponent<ProjectileBehavior>();
            projScript.origin = transform; // Define el origen como el jugador
            projScript.target = enemy;     // Define el enemigo como el objetivo
        }

        // Recuperar el proyectil con clic Der. (1)
        if (Input.GetMouseButtonDown(1) && activeProjectile != null)
        {
            ProjectileBehavior projScript = activeProjectile.GetComponent<ProjectileBehavior>();
            projScript.ReturnToOrigin(); // Ordena al proyectil regresar
        }
        */
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
