using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipgunBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _playerView;
    [SerializeField] private GameObject _explodeEffect;
    [SerializeField] private ParticleSystem _poof;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    
    private ParticleSystem _boom;


    private void Start()
    {
        _boom = _explodeEffect.GetComponent<ParticleSystem>();
    }

    public void Shoot()
    {
        RaycastHit hit;
        Transform camera = _playerView.transform;

        _poof.Play();

        if (Physics.Raycast(camera.position, camera.forward, out hit, _range))
        {
            if ((hit.transform != null) && hit.transform.CompareTag("Asteroid"))
            {
                _explodeEffect.transform.position = hit.transform.position;

                Destroy(hit.transform.gameObject);
                _boom.Play();
            }
        }
    }
}
