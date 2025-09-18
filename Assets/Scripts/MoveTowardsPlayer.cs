using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    // Змінна для координат об'єкту player
    private Transform _player;

    // Швидкість руху ворога
    public float speed = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find(
            "rocket"
        ).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = _player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position += (delta * moveSpeed);
    }
    
}