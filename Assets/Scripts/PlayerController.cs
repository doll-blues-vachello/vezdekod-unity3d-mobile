using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovableObject
{
    [SerializeField] public static float Degree = 5f;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _leftWall;
    [SerializeField] private Transform _rightWall;
    [SerializeField] private Transform _topWall;
    [SerializeField] private Transform _bottomWall;
    [SerializeField] private float _range;
    private Vector2 _touchPosition;
    private float _wallOffset = 50;

    private void Update()
    {
        if ((transform == null) || (UIBehaviour.GameIsPaused))
            return;

        Move();
        KeepRotation();
        
        if (Input.touchCount == 0)
            return;

        for (int num = 0; num < Input.touchCount; num++)
        {
            Touch touch = Input.GetTouch(num);

            if (touch.position.x < _leftWall.position.x)
                Rotate(-Degree);
            else if (touch.position.x > _rightWall.position.x)
                Rotate(Degree);
        }
    }

    private void KeepRotation()
    {
        float yRot = transform.eulerAngles.y;

        transform.rotation = Quaternion.Euler(
            -90f,
            yRot,
            0f
        );
    }

    private void Rotate(float degree)
    {
        transform.Rotate(0f, 0f, degree); 
    }

    private void Move()
    {
        Vector3 pos = transform.position;
        Vector3 movement = -transform.forward * _speed;

        if (pos.x <= (_leftWall.position.x + _wallOffset / 2))
            transform.position = _rightWall.position + new Vector3(-_wallOffset, 0f, 0f);
        else if (pos.x >= (_rightWall.position.x - _wallOffset / 2))
            transform.position = _leftWall.position + new Vector3(_wallOffset, 0f, 0f);
        else if (pos.z <= (_bottomWall.position.z + _wallOffset / 2))
            transform.position = _topWall.position + new Vector3(0f, 0f, -_wallOffset);
        else if (pos.z >= (_topWall.position.z - _wallOffset / 2))
            transform.position = _bottomWall.position + new Vector3(0f, 0f, _wallOffset);
        else
            transform.Translate(movement);
    }
}
