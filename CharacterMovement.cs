using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    public float forwardLimit = 5f; // Límite hacia adelante
    public float backwardLimit = -5f; // Límite hacia atrás

    private bool movingForward = true; // Indica si el personaje se mueve hacia adelante
    private Animator animator; // Referencia al Animator

    void Start()
    {
        // Obtén el componente Animator del personaje
        animator = GetComponent<Animator>();

        // Activar animación de correr si existe
        if (animator != null)
        {
            animator.SetBool("isRunning", true); // Asegúrate de que "isRunning" exista en tu Animator
        }
    }

    void Update()
    {
        // Determinar la dirección de movimiento
        Vector3 movement = movingForward ? Vector3.forward : Vector3.back;
        transform.Translate(movement * speed * Time.deltaTime);

        // Verificar límites y cambiar dirección
        if (movingForward && transform.position.z >= forwardLimit)
        {
            movingForward = false; // Cambiar dirección hacia atrás
            Flip(); // Voltear el personaje
        }
        else if (!movingForward && transform.position.z <= backwardLimit)
        {
            movingForward = true; // Cambiar dirección hacia adelante
            Flip(); // Voltear el personaje
        }
    }

    // Método para voltear al personaje
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.z *= -1; // Invierte la escala en el eje Z para reflejar el cambio de dirección
        transform.localScale = localScale;
    }
}
