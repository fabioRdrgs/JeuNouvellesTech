using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire avancer le joueur et dans la direction de la caméra lors de déplacement
// et jouer l'animation correspondate
public class AgentMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    // Ajout de variables pour simuler la gravité
    public float rotationSpeed, movementSpeed, gravity = 20;
    // Le vecteur dans lequel notre joueur va se déplacer
    Vector3 movementVector = Vector3.zero;
    // L'angle de direction dans lequel on va définir l'orientation du joueur
    private float desiredRotationAngle = 0;

    private void Start()
    {// Récupère les composants nécessaire au contrôle du joueur et à l'animation 
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Va recevoir les infos de mouvement et les traiter
    public void HandleMovement(Vector2 input)
    {
        // Vérifie si notre joueur est au sol afin d'empêcher du mouvement en l'air
        if (controller.isGrounded)
        {
            // Si on avance
            if (input.y > 0)
            {
                movementVector = transform.forward * movementSpeed;
                
            }
            // Arrête le joueur si pas de déplacement
            else
            {
                movementVector = Vector3.zero;


                // Définit l'état de l'animation à 0 qui est égal à l'état immobile
                animator.SetFloat("Move", 0);
            }
        }
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        // Calcule l'angle entre la direction TOUT droit et si l'on veut aller à droite ou à gauche
        // pour savoir vers où la caméra doit se tourner
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        // Trouver si l'on tourne à droite ou à gauche quand l'on avance
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    private void RotateAgent()
    {
        // Si la caméra va vers la gauche ou la droite lors de déplacement, la tourne de manière fluide
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }

    private float SetCorrectAnimation()
    {
        float currentAnimationSpeed = animator.GetFloat("Move");
        // Si on tourne la caméra à droite ou à gauche dans la limite donnée
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            // Si on est en dessous de 0.2 (état de marche), tourne la caméra
            if (currentAnimationSpeed < 0.2f)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
                // Assure le fait que lorsqu'on tourne, ça joue l'animation de marche et non de course
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, 0, 0.2f);
            }
            // Définit l'animation voulue correspondate à la valeur
            animator.SetFloat("Move", currentAnimationSpeed);
        }
        else
        {
            if (currentAnimationSpeed < 1)
            {
                currentAnimationSpeed += Time.deltaTime * 2;

            }
            else
            {
                //Comme notre limite pour l'animation est à 1 on la garde à 1 si elle est plus grande
                currentAnimationSpeed = 1;
            }
            animator.SetFloat("Move", currentAnimationSpeed);
        }
        return currentAnimationSpeed;
    }

    private void Update()
    {
        // Fait tourner le joueur lorsqu'on est sur le sol
        if (controller.isGrounded)
        {
            if (movementVector.magnitude > 0)
            {

                var animationSpeedMultiplier = SetCorrectAnimation();
                RotateAgent();

                // Si on est en train de tourner, réduit la vitesse à une vitesse de marche
                // sinon, remets la vitesse normale qui est une vitesse de course
                movementVector *= animationSpeedMultiplier;
            }
            
        }

        // Va faire avancer le joueur en avant tout en lui appliquant de la gravité pour vérifier
        // s'il est au sol ou non
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);
    }
}
