using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public GameObject parent;
    public Rigidbody2D rb;
    public CircleCollider2D Collider;

    // Untuk delegasi atribut 
    public UnityAction OnBirdDestroyed = delegate { };

    public UnityAction<Bird> OnBirdShot = delegate { };

    public BirdState State { get { return _state; } }

    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;

    
    void Start()
    {
        // Mengubah rigidbody menjadi kinematic agar gameobject tidak jatuh
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Mematikan fungsi collider
        Collider.enabled = false;
        _state = BirdState.Idle;
    }


    void FixedUpdate()
    {
        if(_state == BirdState.Idle && rb.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if((_state == BirdState.Thrown || _state == BirdState.HitSomething) 
            && rb.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;

            // Untuk delay 2 detik dan hancurkan gameobject
            StartCoroutine(DestroyAfter(2));

        }
    }

    private IEnumerator DestroyAfter (float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo (Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot ( Vector2 velocity, float distance, float speed)
    {
        Collider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = velocity * speed * distance;

        OnBirdShot(this);
    }

    // Method ini untuk action saat player melakukan tap setelah bird terbang
    // Kata kunci virtual kita tambahkan agar class turunan dari Bird dapat melakukan override terhadap fungsi ini
    public virtual void OnTap()
    {
        // Nothing happened
    }

    // Method OnDestroy meng-subscribe event delegate dari UnityAction
    private void OnDestroy()
    {
        if(_state == BirdState.Thrown || _state == BirdState.HitSomething)
        OnBirdDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _state = BirdState.HitSomething;    
    }
}
