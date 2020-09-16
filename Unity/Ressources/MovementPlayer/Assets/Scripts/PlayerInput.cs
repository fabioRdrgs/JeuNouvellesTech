//Code trouvé dans cette vidéo :
// https://youtu.be/Sg0Z09HpU_s
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, Iinput
{
    //Donne des valeurs à n'importe quelle méthode qu'on assigne à ça lors du mouvement
    public Action<Vector2> OnMovementInput { get; set; }
    //Fournit la direction à la cible
    public Action<Vector3> OnMovementDirectionInput { get; set; }

    private void Start()
    {
        //Verrouille le curseur au centre de l'écrn
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Appel de méthodes lors de changements d'états d'entrées 
    private void Update()
    {
        GetMovementInput();
        GetMovementDirection();
    }

    // Permet récupérer la direction vers où se déplacer (Direction de la caméra)
    private void GetMovementDirection()
    {
        var cameraForwardDirection = Camera.main.transform.forward;
        Debug.DrawRay(Camera.main.transform.position, cameraForwardDirection * 10, Color.red);
        var directionToMoveIn = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));
        Debug.DrawRay(Camera.main.transform.position, directionToMoveIn * 10, Color.blue);

        // Fournit la direction de la caméra pour l'entrée de déplacement
        OnMovementDirectionInput?.Invoke(directionToMoveIn);
    }

    // Récupère les différentes entrées en axe X et Y
    private void GetMovementInput()
    {
        // Récupère les axes horizontaux et verticaux pour le mouvement
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        // Si quelque chose est en train d'écouter/listening/récupérer des infos à cet endroit (le ?), fournit la valeur de l'entrée
        OnMovementInput?.Invoke(input);
    }
}
