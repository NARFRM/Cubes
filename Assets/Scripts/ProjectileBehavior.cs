using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 10f;  // Velocidad del proyectil
    public Transform origin;   // Lugar de origen (jugador)
    public Transform target;   // Objetivo (enemigo)
    private bool followingEnemy = false;  // Estado del proyectil
    private bool returning = false;  // Estado de regreso

    void Update()
    {
        // Disparo con clic derecho: sigue al enemigo
        if (Input.GetMouseButtonDown(0) && target != null)
        {
            followingEnemy = true;
            returning = false;
        }

        // Regreso con clic izquierdo: vuelve al origen
        if (Input.GetMouseButtonDown(1))
        {
            ReturnToOrigin();
        }

        Move();
    }

    public void ReturnToOrigin()
    {
        followingEnemy = false;
        returning = true;
    }

    void Move()
{
    if (followingEnemy && target != null)
    {
        MoveTowards(target.position);
    }
    else if (returning && origin != null)
    {
        MoveTowards(origin.position);

        if (Vector3.Distance(transform.position, origin.position) < 0.5f)
        {
            returning = false;
            followingEnemy = false;
            transform.position = origin.position; // Se queda en el jugador
        }
    }
}


    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
