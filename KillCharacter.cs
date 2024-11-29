using UnityEngine;

public class KillCharacter : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private int clickCount = 0;
    private static int totalPoints = 0; // Puntos acumulados de manera global
    private int pointsPerKill = 5; // Puntos que el jugador gana al matar a un personaje

    void Start()
    {
        animator = GetComponent<Animator>();  // Asignar el Animator
    }

    void OnMouseDown()  // Detectar clic del ratón
    {
        if (isDead) return;  // Evitar que el personaje muera varias veces

        clickCount++;

        if (clickCount == 2)  // Si se hace clic dos veces
        {
            Die();  // Llamar a la función Die
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;  // Marcar como muerto para evitar que muera varias veces
        Debug.Log("Activando Trigger 'Die'.");
        animator.SetTrigger("Die");  // Activar la animación de muerte

        // Destruir el personaje después de que termine la animación de muerte
        float animationLength = GetAnimationLength("Death");  // Obtener la duración de la animación
        Invoke(nameof(DestroyCharacter), animationLength);  // Destruir después de la animación

        AddPoints();  // Sumar puntos
    }

    // Obtener la duración de la animación de muerte
    float GetAnimationLength(string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("Animator o RuntimeAnimatorController no configurados.");
            return 2f;  // Duración predeterminada
        }

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"No se encontró la animación {animationName}.");
        return 2f;
    }

    // Función para destruir al personaje
    void DestroyCharacter()
    {
        Destroy(gameObject);  // Eliminar el personaje de la escena
    }

    // Función para sumar puntos
    void AddPoints()
    {
        totalPoints += pointsPerKill;  // Sumar puntos al total
        Debug.Log("Puntos acumulados: " + totalPoints);

        if (totalPoints >= 30)
        {
            EndGame();  // Terminar el juego cuando llegues a 30 puntos
        }
    }

    // Función para finalizar el juego
    void EndGame()
    {
        Debug.Log("¡Has ganado el juego!");
        // Aquí puedes agregar la lógica para terminar el juego, como mostrar una pantalla de victoria
    }
}
