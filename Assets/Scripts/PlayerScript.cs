using System;
using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float playerSpeed = 2.0f;

    public Transform laser;
    private Transform _currentLaser = null;
    private float _currentSpeed = 0.0f;

    public List<KeyCode> upButton;
    public List<KeyCode> downButton;
    public List<KeyCode> leftButton;
    public List<KeyCode> rightButton;

    public List<KeyCode> attackBtn;

    private Vector3 _lastMovement = new Vector3();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Attack();
    }


    void Attack()
    {
        if (IsAttacking() && _currentLaser == null)
        {

            Vector3 worldPos = Input.mousePosition;
            worldPos = Camera.main.ScreenToWorldPoint(
                worldPos
            );
            var dx = this.transform.position.x - worldPos.x;
            var dy = transform.position.y - worldPos.y;

            float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            var rot = Quaternion.Euler(
                new Vector3(0, 0, angle + 90)
            );
            
            _currentLaser = Instantiate(
                laser,
                transform.position,
                rot
            );
        }
    }

    bool IsAttacking()
    {
        foreach (var key in attackBtn)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }

        return false;
    }

    void Rotation()
    {
        Vector3 worldPos = Input.mousePosition;

        worldPos = Camera.main.ScreenToWorldPoint(
            worldPos
        );

        // Зберігаємо координати покажчика миші
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        // Обчислюємо кут між об'єктами «Корабель» і «Покажчик»
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        // Трансформуємо кут в вектор
        Quaternion rot = Quaternion.Euler(
            new Vector3(0, 0, angle + 90)
        );

        // Змінюємо поворот героя
        transform.rotation = rot;
    }

    void Movement()
    {
        // Рух героя до мишки
        Vector3 movement = new Vector3();

        // Перевірка натиснутих клавіш
        movement += MoveIfPressed(
            upButton,
            Vector3.up
        );
        movement += MoveIfPressed(
            downButton,
            Vector3.down
        );
        movement += MoveIfPressed(
            leftButton,
            Vector3.left
        );
        movement += MoveIfPressed(
            rightButton,
            Vector3.right
        );
        // Якщо натиснуто кілька кнопок, обробляємо це
        movement.Normalize();

        // Перевірка натискання кнопки
        if (movement.magnitude > 0)
        {
            // Після натискання рухаємося в цьому напрямку
            _currentSpeed = playerSpeed;
            transform.Translate(
                movement * Time.deltaTime * playerSpeed,
                Space.World
            );
            _lastMovement = movement;
        }
        else
        {
            // Якщо нічого не натиснуто

            transform.Translate(
                _lastMovement * Time.deltaTime * _currentSpeed,
                Space.World
            );
            _currentSpeed *= 0.9f;
        }
    }

    Vector3 MoveIfPressed(
        List<KeyCode> keyList,
        Vector3 movement
    )
    {
        foreach (KeyCode element in keyList)
        {
            if (Input.GetKey(element))
            {
                return movement;
            }
        }

        return Vector3.zero;
    }
}