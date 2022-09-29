﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody2D;
    private Queue<Ray2D> _reflectPoints;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public float Speed => _speed;
        
    protected void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer==LayerMask.NameToLayer("Walls"))
            ReflectBullet();
    }
    
    protected void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    public void Shot(Transform firePosition)
    {
        transform.position = firePosition.position;
        transform.rotation = firePosition.rotation;
        _rigidbody2D.velocity = firePosition.up *_speed;
        _reflectPoints=ReflectPoints.Reflect(firePosition.position, firePosition.up);
    }

    private void ReflectBullet()
    {
        if (_reflectPoints.TryDequeue(out var ray))
        {
            _rigidbody2D.velocity = ray.direction * _speed;
            _rigidbody2D.position = ray.origin;
            var angle = Mathf.Atan2(ray.direction.y ,ray.direction.x) * Mathf.Rad2Deg-90f;
            _rigidbody2D.rotation = angle;
        }
    }
}