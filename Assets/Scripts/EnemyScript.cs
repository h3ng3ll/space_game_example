using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 2;

    private void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.name.Contains("laser"))
        {
            LaserScript laser = theCollision.gameObject.GetComponent<LaserScript>();

            health -= laser.damage;
            Destroy(
                theCollision.gameObject
            );
        }

        if (health <= 0)
        {
            // May be already destroyed game object
            GameController controller = GameObject.Find(
                "GameController"
            )?.GetComponent<GameController>();
            
            if (controller)
            {
                controller.KilledEnemy();
            }

            Destroy(
                gameObject
            );
        }
    }
}