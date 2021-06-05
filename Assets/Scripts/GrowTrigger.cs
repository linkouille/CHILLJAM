using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTrigger : MonoBehaviour
{
    public int potatoesRequired;
    public int potatoesPlaced;

    public MovableObject movableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(potatoesPlaced == potatoesRequired)
        {
            movableObject.activateMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Potatoe"))
        {
            potatoesPlaced++;

            //Consume potato
        }
    }
}
