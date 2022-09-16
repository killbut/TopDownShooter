﻿using System;
using UnityEngine;

public class BulletPool : MonoBehaviour, IBulletPoolObjectHandler
{
     [SerializeField] private Bullet _prefab;
     [SerializeField] private int _startSize = 10;
     [SerializeField] private bool _needAutoExpand = false;
     [SerializeField] private Transform _container;

     private PoolObject<Bullet> _bulletPoolObject;
     public PoolObject<Bullet> BulletPoolObject => _bulletPoolObject;

     protected void OnEnable()
     {
          EventBus.Subscribe(this);
     }

     protected void OnDisable()
     {
          EventBus.Subscribe(this);
     }

     protected void Start()
     {
          _bulletPoolObject = new PoolObject<Bullet>(_prefab, _needAutoExpand, _container, _startSize);
     }
     
     public void DeactivateBullet(GameObject bullet)
     {
          bullet.SetActive(false);
     }

     public void ShootBullet(Transform firePosition)
     {
          var bullet = _bulletPoolObject.GetFreeObject();
          var bulletTransform = bullet.transform;
          bulletTransform.position = firePosition.position;
          bulletTransform.rotation = firePosition.rotation;
          bullet.Rigidbody2D.AddForce(firePosition.up * bullet.Speed, ForceMode2D.Impulse);
     }
}