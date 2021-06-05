using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [Header("Is a triggered obstacle")]
    public int id;
    [Header("Unique activation")]
    public bool activated;
    [Header("Movement properties")]
    public Vector2 movementOffset;
    public float timeToMove;
    public GameObject[] plateformes;

    private void Awake()
    {
        for (int i = 0; i < plateformes.Length; i++)
        {
            plateformes[i].GetComponent<SpriteRenderer>().enabled = false;
            plateformes[i].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void activateMovement()
    {
        if(!activated)
        {
            activated = true;
            for (int i = 0; i < plateformes.Length; i++)
            {
                plateformes[i].GetComponent<SpriteRenderer>().enabled = true;
                plateformes[i].GetComponent<Collider2D>().enabled = true;
            }

            LeanTween.moveX(gameObject, transform.position.x + movementOffset.x, timeToMove).setEaseLinear();
            LeanTween.moveY(gameObject, transform.position.y + movementOffset.y, timeToMove).setEaseLinear();
        }
    }

    public void reverseMovement()
    {
        if (activated)
        {
            activated = false;
            for (int i = 0; i < plateformes.Length; i++)
            {
                plateformes[i].GetComponent<SpriteRenderer>().enabled = false;
                plateformes[i].GetComponent<Collider2D>().enabled = false;
            }

            LeanTween.moveX(gameObject, transform.position.x - movementOffset.x, timeToMove).setEaseLinear();
            LeanTween.moveY(gameObject, transform.position.y - movementOffset.y, timeToMove).setEaseLinear();
        }
    }
}
