using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public GameObject Entity;
    [SerializeField]
    public float HeightDifference = 4.5f;
    [SerializeField]
    private GameObject instanceBarreDeVie;

    private Vector3 entityPos;
    private Transform bar;

    /// <summary>
    /// Exécute le code lors de l'instanciation du script
    /// </summary>
    private void Awake()
    {
        bar = transform.Find("Bar");
    }
    /// <summary>
    /// Méthode permettant de définir la taille de la barre de vie 
    /// </summary>
    /// <param name="sizeNormalized">Taille voulue</param>
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    /// <summary>
    /// Méthode permettant de définir la couleur du sprite de la barre de vie 
    /// </summary>
    /// <param name="color">Couleur voulue</param>
    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
    /// <summary>
    /// Appelle cette méthode à chaque image par secondes de l'application
    /// </summary>
    private void Update()
    {
        if (Entity != null)
            entityPos = Entity.transform.position;
        else
            Destroy(instanceBarreDeVie);
        transform.position = new Vector3(entityPos.x, (entityPos.y + HeightDifference));
    }
}
