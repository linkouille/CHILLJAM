using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowTrigger : MonoBehaviour
{
    public int potatoesRequired;
    public int potatoesPlaced;

    public MovableObject movableObject;
    public List<Projectile> potatoesPlanted;
    public Text indicator;

    // Update is called once per frame
    void Update()
    {
        if(potatoesPlaced == potatoesRequired)
        {
            movableObject.activateMovement();
        }

        indicator.text = potatoesPlaced  + "/" + potatoesRequired;
    }

    public void recallPotatoes()
    {
        if(potatoesPlaced > 0)
        {
            for (int i = 0; i < potatoesPlanted.Count; i++)
            {
                potatoesPlanted[i].ChangeMode(ProjectileMode.Follow);
            }
            potatoesPlaced = 0;
            movableObject.reverseMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Projectile"))
        {
            potatoesPlaced++;
            potatoesPlanted.Add(collision.GetComponent<Projectile>());
            collision.GetComponent<Projectile>().SetModeToPlanted();
        }
    }
}
