using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    Iinput input;
    AgentMovement movement;

    // Si le joueur est actif, fournit les données entrées par l'utilisateur pour le mouvement
    private void OnEnable()
    {
        input = GetComponent<Iinput>();
        movement = GetComponent<AgentMovement>();
        input.OnMovementDirectionInput += movement.HandleMovementDirection;
        input.OnMovementInput += movement.HandleMovement;
    }

    // Si l'on désactive le joueur, désactive l'entrée utilisateur
    private void OnDisable()
    {
        input.OnMovementDirectionInput -= movement.HandleMovementDirection;
        input.OnMovementInput -= movement.HandleMovement;
    }
}
