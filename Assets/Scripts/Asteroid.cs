using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MovableObject
{
    public static float Speed;
    public static uint Damage;
    public bool IsActive;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _leftWall;
    [SerializeField] private Transform _rightWall;
    [SerializeField] private Transform _topWall;
    [SerializeField] private Transform _bottomWall;
    private Vector3 _direction;
    private Quaternion _basicRotation;
    private float _wallOffset = 50;

    private void Start()
    {
        _basicRotation = transform.rotation;

        IsActive = false;
    }

    private void Update()
    {
        if ((transform == null) || !IsActive)
            return;
        
        if (_direction == Vector3.zero)
            _direction = _player.position - transform.position;

        Vector3 pos = transform.position;

        if ((pos.x <= (_leftWall.position.x + _wallOffset / 2)) ||
            (pos.x >= (_rightWall.position.x - _wallOffset / 2)) ||
            (pos.z <= (_bottomWall.position.z + _wallOffset / 2)) ||
            (pos.z >= (_topWall.position.z - _wallOffset / 2)))
        {
            Destroy(transform.gameObject);
        }
        
        // if (pos.x <= (_leftWall.position.x + _wallOffset / 2))
        //     transform.position = _rightWall.position + new Vector3(-_wallOffset, 0f, 0f);
        // else if (pos.x >= (_rightWall.position.x - _wallOffset / 2))
        //     transform.position = _leftWall.position + new Vector3(_wallOffset, 0f, 0f);
        // else if (pos.z <= (_bottomWall.position.z + _wallOffset / 2))
        //     transform.position = _topWall.position + new Vector3(0f, 0f, -_wallOffset);
        // else if (pos.z >= (_topWall.position.z - _wallOffset / 2))
        //     transform.position = _bottomWall.position + new Vector3(0f, 0f, _wallOffset);
            
        transform.Translate(_direction * Time.deltaTime * Speed);
        transform.rotation = _basicRotation;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var player = coll.gameObject.GetComponent<Player>();

            if (Player.Shields > 0)
            {
                Player.Shields--;
            }
            else
            {
                player.Hp.Reduce(Damage);
                player.HealthBar.value = player.Hp.Get();
            }
        }
    }
}
