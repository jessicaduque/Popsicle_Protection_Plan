using System.Collections;
using UnityEngine;

public class Dummy_Penguin_Controller : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float _xDirection;
    private float _speed;
    private float _finalXPos;
    private bool _canMove = false;
    private PoolManager _poolManager => PoolManager.I;
    private void Start()
    {
        _speed = Player_Penguin_Controller.I.GetSpeed();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayMovementCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _canMove = false;
    }

    private void FixedUpdate()
    {
        if(_canMove) Movement();
    }

    #region Movement and Animation

    private void Movement()
    {
        this.transform.position += new Vector3(_xDirection * (_speed * Time.deltaTime), 0, 0);
        if (_finalXPos > 0)
        {
            if (transform.position.x >= _finalXPos)
            {
                _poolManager.ReturnPool(gameObject);
            }
        }
        else
        {
            if (transform.position.x <= _finalXPos)
            {
                _poolManager.ReturnPool(gameObject);
            }
        }
        MovementAnimationControl();
    }

    private void MovementAnimationControl()
    {
        animator.SetFloat("Walk_X", _xDirection);
        animator.SetFloat("Walk_Y", 0);
    }

    private IEnumerator DelayMovementCoroutine()
    {
        float randomDelay = Random.Range(0f, 3f);
        yield return new WaitForSeconds(randomDelay);
        _canMove = true;
    }
    
    #endregion
    
    #region Set

    public void SetDirection(int direction)
    {
        _xDirection = direction;
    }
    
    public void SetFinalXPos(float x)
    {
        _finalXPos = x;
    }
    
    #endregion
}
