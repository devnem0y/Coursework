using UnityEngine;

public class Engine
{
    private Rigidbody2D body;
    private Transform transform;
    private Rigidbody2D axisFront = null, axisBack = null;
    private bool impulse;
    private float wheelForce;
    private float maxSpeed;
    private float currentSpeed;
    private float motorForce;
    private float velocity = 0f;
    private float acceleration = 0f;

    public Engine(Transform transform, Rigidbody2D body, Rigidbody2D axisFront, Rigidbody2D axisBack, bool impulse, float wheelForce, float maxSpeed, float motorForce)
    {
        this.transform = transform;
        this.body = body;
        this.axisFront = axisFront;
        this.axisBack = axisBack;
        this.impulse = impulse;
        this.wheelForce = wheelForce;
        this.maxSpeed = maxSpeed;
        this.motorForce = motorForce;
    }

    public void Move(Wheel front, Wheel back, Controller controller)
    {
        Mobile(front, back, controller);
        Keyboard(front, back);

        currentSpeed = body.velocity.x;
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
                velocity = 0f;
                acceleration = 0f;
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
            velocity = 0f;
            acceleration = 0f;
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
            velocity = 0f;
            acceleration = 0f;
        }
    }

    private void OnMotor(Wheel wFront, Wheel wBack)
    {
        // Выполняем проверку какие колеса находятся на земле и взависимости от этого устанавливаем значение скорости
        // Обнуляем силу, которая поднимает колеса и устанавливаем скорость

        // Добавить значение body.drag

        if ((wFront.IsGrounded && !wBack.IsGrounded) || (!wFront.IsGrounded && wBack.IsGrounded)) // если одно любое колесо не на земле
        {
            velocity = motorForce / 1.25f;
        }
        else if (wFront.IsGrounded && wBack.IsGrounded) // если оба колеса на земле
        {
            velocity = motorForce;
        }
        else velocity = 0f;

        if (currentSpeed >= maxSpeed) body.velocity = new Vector2(maxSpeed, body.velocity.y); // если скорость больше максимальной
        else
        {
            body.AddForce(transform.right.normalized * velocity);
        }
    }

    private void WheelUp(Wheel wheel, Rigidbody2D w)
    {
        velocity /= 0.75f;

        if (impulse)
        {
            if (wheel.IsGrounded) w.AddForce(transform.up.normalized * wheelForce, ForceMode2D.Impulse);
        }
        else
        {
            if (wheel.IsGrounded && w.velocity.y < 4f) // 2
            {
                acceleration = wheelForce;
            }
            else if (!wheel.IsGrounded && w.velocity.y < 4.5f) // 2.5
            {
                acceleration = wheelForce;
            }
            else if (!wheel.IsGrounded && w.velocity.y >= 4.2f) // 2.2
            {
                acceleration = 0f;
                w.velocity = new Vector2(w.velocity.x, 4.2f); // 2.2
            }

            w.AddForce(transform.up.normalized * acceleration);
        }
    }
}