using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //alap mozgashoz
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float _jumpForce, _speed;
    private float _horiz = 0;
    //layer-t kifejezo vatozo
    private LayerMask _ground;

    //ugrashoz szukseges dolgok
    private Transform _groundCheck;
    private int _doubleJump = 0;
    private bool _grounded;
    private bool _jumpButtonDown;

    //guggolashoz szukseges dolgok
    private Transform _headCheck;
    private CapsuleCollider2D _standingCollider;
    private Vector3 _originalScale;
    [SerializeField] private float _crouchSize;
    private bool _headBump;
    private bool _isCrouching;

    //animaciokhoz
    private Animator _animator;
    private float CPos, LPos;

    //A Start() elott futtatja le, az objektum letrehozasakor. 
    private void Awake()
    {
        //Inicializalas
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        _ground = LayerMask.GetMask("Ground");
        _groundCheck = GameObject.Find("Ground Check").GetComponent<Transform>();
        _headCheck = GameObject.Find("Head Check").GetComponent<Transform>();
        _standingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {        
        _originalScale = transform.localScale;
        LPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //boolean-t ad vissza (kor kozeppontjanak koordinatai, milyen sugaru legyen a kor, mivel lepjen kapcsolatba)
        //_grounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadious, _ground);
        _grounded = Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.13f, 0.037f), 0f, _ground);
        _headBump = Physics2D.OverlapCircle(_headCheck.position, 0.05f, _ground);
        
        //("Horizontal/Vertical") megmondja hogy a vector hogy all, lehet 1/-1 (jobb/bal,fel/le)
        //Raw egyből 1/-1 egészre ugrik, nincs felfutási idő
        _horiz = Input.GetAxisRaw("Horizontal");

        //Rigidbidy sebesseget allit (vector2 kell neki) -> new vector2(x,y)
        rb.velocity = new Vector2(_horiz * _speed * Time.fixedDeltaTime, rb.velocity.y);

        //ugras
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _doubleJump < 1 && !_isCrouching)
        {
            //RB sebessseget allit, az y tengelyen. Vector2.up (0,1) koordinatakat allit be ezt meg kell szorozni valami szammal.
            rb.velocity = Vector2.up * _jumpForce;
            _doubleJump++;
        }

        if (_grounded == true)
        {
            _doubleJump = 0;
            _animator.SetBool("fall", false);
        }

        //guggolas
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            _isCrouching = true;
            transform.localScale = new Vector3(transform.localScale.x, _crouchSize, transform.localScale.z);
            _standingCollider.direction = CapsuleDirection2D.Horizontal;
        }
        else if (!_headBump)
        {
            _isCrouching = false;
            transform.localScale = _originalScale;
            _standingCollider.direction = CapsuleDirection2D.Vertical;
        }
        

        //Animations

        //Jobbra/balra forgatas
        switch (_horiz)
        {
            case 1:
                sr.flipX = false;
                break;
            case -1:
                sr.flipX = true;
                break;
        }

        //zuhanas, ha nem akkor ugras, ha nem akkor futas, vagy pedig egyik sem
        CPos = transform.position.y;

        if (CPos < LPos && !_grounded)
        {
            _animator.SetBool("fall", true);
            _animator.SetBool("run", false);
            _animator.SetBool("jump", false);

        }
        else
        {
            _animator.SetBool("fall", false);
            if (CPos > LPos && _doubleJump > 0)
            {
                _animator.SetBool("jump", true);
                _animator.SetBool("run", false);
            }
            else
            {
                _animator.SetBool("jump", false);
                if (Convert.ToBoolean(_horiz) && _grounded)
                {
                    _animator.SetBool("run", true);
                }
                else
                {
                    _animator.SetBool("run", false);
                }
            }
        }

        LPos = CPos;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy fejerol ugrik
        if (collision.collider.tag == "enemHead") rb.velocity = Vector2.up * _jumpForce;
    }
}