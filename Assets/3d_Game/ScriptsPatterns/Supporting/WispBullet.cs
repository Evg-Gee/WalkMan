using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class WispBullet : MonoBehaviour
{
    [SerializeField] private float _speed, _lifeTime;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rigitBody;
    private bool _direction;

    private void Awake()
    {
        if (_rigitBody == null)
        {
            _rigitBody = GetComponent<Rigidbody2D>();
        }
    }
    public void Init(Vector3 posirion, bool direction)
    {
        _direction = direction;
        gameObject.SetActive(true);
        transform.position = posirion;
        transform.rotation = direction ? Quaternion.identity : Quaternion.Euler(0, 0, 180f);
        
        ApplyForce();
        StartCoroutine(DeacticateCorutine());
    }
    private IEnumerator DeacticateCorutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }
    private void ApplyForce()
    {
        _rigitBody.velocity = transform.right * _speed;
    }
    public int GetWispDamage()
    {
        return _damage;
    }
    public bool GetDirectionWispDamage()
    {
        return _direction;
    }
}