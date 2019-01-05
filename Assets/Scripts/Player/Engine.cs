using UnityEngine;

[System.Serializable]
public class Engine
{
    [SerializeField]
    private Rigidbody2D body;
    private Transform transform;
    [SerializeField]
    private Rigidbody2D axisFront = null, axisBack = null;
    [SerializeField]
    private bool impulse;
    [SerializeField, Range(0f, 25000f)]
    private float wheelForce;

    [Space, SerializeField]
    private float maxSpeed;
    private float currentSpeed;

    [SerializeField, Range(0f, 2500f)]
    private float motorForce; // ускорение

    public Engine(Transform transform)
    {
        this.transform = transform;
    }

    public void Move(Wheel front, Wheel back, Controller controller)
    {
        Mobile(front, back, controller);
        Keyboard(front, back);
    }

    private void Mobile(Wheel wFront, Wheel wBack, Controller controller)
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                controller.StartPos = Input.GetTouch(0).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                motorForce = 0f;
                wheelForce = 0f;
                controller.StartPos = new Vector2(0f, 0f);
                controller.EndPos = new Vector2(0f, 0f);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                controller.EndPos = Input.GetTouch(0).position;

                if (controller.EndPos.x + controller.Offset < controller.StartPos.x)
                {
                    WheelUp(wFront, axisFront);
                }

                if (controller.EndPos.x - controller.Offset > controller.StartPos.x)
                {
                    WheelUp(wBack, axisBack);
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                OnMotor(wFront, wBack);
            }
        }
        else
        {
            // Обнуляем скорость и силу
            motorForce = 0f;
            wheelForce = 0f;
            controller.StartPos = new Vector2(0f, 0f);
            controller.EndPos = new Vector2(0f, 0f);
        }
    }

    private void Keyboard(Wheel wFront, Wheel wBack)
    {
        if (Input.GetKey(KeyCode.W)) WheelUp(wFront, axisFront);
        if (Input.GetKey(KeyCode.Q)) WheelUp(wBack, axisBack);
        if (Input.GetKey(KeyCode.Space)) OnMotor(wFront, wBack);
        else
        {
            // Обнуляем скорость и силу
            motorForce = 0f;
            wheelForce = 0f;
        }
    }

    private void OnMotor(Wheel wFront, Wheel wBack)
    {
        // Выполняем проверку какие колеса находятся на земле и взависимости от этого устанавливаем значение скорости
        // Обнуляем силу, которая поднимает колеса и устанавливаем скорость

        // Добавить значение body.drag

        float velocity = 0f;

        if ((wFront.IsGrounded && !wBack.IsGrounded) || (!wFront.IsGrounded && wBack.IsGrounded)) // если одно любое колесо не на земле
        {
            velocity = motorForce / 1.25f;
        }
        else if (wFront.IsGrounded && wBack.IsGrounded) // если оба колеса на земле
        {
            velocity = motorForce;
        }
        else velocity = 0f;

        currentSpeed = body.velocity.x;

        if (currentSpeed >= maxSpeed) body.velocity = new Vector2(maxSpeed, body.velocity.y); // если скорость больше максимальной
        else
        {
            body.AddForce(transform.right.normalized * velocity);
        }
    }

    private void WheelUp(Wheel wheel, Rigidbody2D w)
    {
        motorForce /= 1.25f;

        if (impulse)
        {
            if (wheel.IsGrounded) w.AddForce(transform.up.normalized * wheelForce, ForceMode2D.Impulse);
        }
        else
        {
            float acceleration = 0f;

            if (wheel.IsGrounded && w.velocity.y < 2f)
            {
                acceleration = wheelForce;
            }
            else if (!wheel.IsGrounded && w.velocity.y < 2.5f)
            {
                acceleration = wheelForce;
            }
            else if (!wheel.IsGrounded && w.velocity.y >= 2.2f)
            {
                acceleration = 0f;
                w.velocity = new Vector2(w.velocity.x, 2.2f);
            }

            w.AddForce(transform.up.normalized * acceleration);
        }
    }
}