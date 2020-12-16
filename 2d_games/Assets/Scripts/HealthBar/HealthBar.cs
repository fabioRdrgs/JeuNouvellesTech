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
 
    // Start is called before the first frame update
    private void Awake()
    {
         bar = transform.Find("Bar");
    }
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
    private void Update()
    {
        if (Entity != null)
            entityPos = Entity.transform.position;
        else
            Destroy(instanceBarreDeVie);
        transform.position = new Vector3(entityPos.x, (entityPos.y+ HeightDifference));
    }
}
