using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public int damage = 1;

    public float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOutTheBounds())
        {
            Destroy(
                gameObject
            );
        }

        Move();
    }

    void Move()
    {
        transform.Translate(
            Vector3.up * (speed * Time.deltaTime) ,
            Space.Self
        );
    }

    bool IsOutTheBounds()
    {
        Vector3 pos = transform.position;

        float left = Camera.main.ViewportToWorldPoint(
            Vector3.left
        ).x;
        float right = Camera.main.ViewportToWorldPoint(
            Vector3.right
        ).x;
        float bottom = Camera.main.ViewportToWorldPoint(
            Vector3.down
        ).y;
        float top = Camera.main.ViewportToWorldPoint(
            Vector3.up
        ).y;
        
        if (pos.x < left || pos.x > right || pos.y <= bottom || pos.y > top)
        {
            return true;
        }

        return false;
    }
}