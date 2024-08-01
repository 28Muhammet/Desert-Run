using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody rb;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch;

    public static bool fallTrue;

    [Header("Swipe Settings")]
    public float swipeRange = 50f;
    public float tapRange = 10f;

    [Header("Jump Settings")]
    [Tooltip("Z�plaman�n y�ksekli�i (�rne�in, 2 birim). Bu, karakterin ne kadar y�kse�e ��kaca��n� belirler.")]
    public float jumpHeight = .7f;
    [Tooltip("Z�plaman�n ileriye do�ru mesafesi (�rne�in, 5 birim). Bu, karakterin z�plarken ne kadar ileriye gidece�ini belirler.")]
    public float jumpDistance = 1f;
    [Tooltip("Z�plaman�n s�resi (�rne�in, 0.5 saniye). Bu, z�plaman�n ne kadar s�rece�ini belirler.")]
    public float jumpDuration = .5f;
    private float jumpStartTime;

    private Vector3 jumpStartPosition;
    private Vector3 jumpTargetPosition;
    private bool isJumping = false;
    private bool isGrounded = true;

    [Header("Move Settings")]
    [Tooltip("Yatay hareketin s�resi (�rne�in, 0.2 saniye). Bu, sa�a veya sola kayd�rman�n ne kadar s�rece�ini belirler.")]
    public float chracterSpeed;
    
    public float addForceSpeed;
    public float moveDuration = .2f;
    private float moveStartTime;
    private Vector3 moveStartPosition;
    private Vector3 moveTargetPosition;
    private bool isMoving;

    [Tooltip("Sol yatay hareketini s�n�rlar.")]
    public float leftBoundary = .55f;
    [Tooltip("Sa� yatay hareketini s�n�rlar.")]
    public float rightBoundary = 1.6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        stopTouch = false;
        fallTrue = false;
        isMoving = false;
    }

    private void Update()
    {
        if (fallTrue == false)
        {
            Run();
            Swipe();

            if(EndlessRoad.globalSpeed == true) 
            {
                chracterSpeed += addForceSpeed;
                EndlessRoad.globalSpeed = false;
            }

            if (isJumping)
            {
                SmoothJump();
            }

            if (isMoving)
            {
                SmoothMove();
            }

            Vector3 pos = transform.position;

            if (pos.x < leftBoundary)
            {
                pos.x = leftBoundary;
            }
            else if (pos.x > rightBoundary)
            {
                pos.x = rightBoundary;
            }

            transform.position = pos;
        }
    }

    private void Run()
    {
        if (SettingsMenu.activeRunner == true)
        {
            animator.SetBool("Running", true);
            rb.AddForce(transform.forward * chracterSpeed);
        }
    }

    private void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = currentTouchPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (distance.y > swipeRange && isGrounded)
                {
                    stopTouch = true;
                    OnSwipeUp();
                    StartCoroutine(DeactivateAfterSeconds(jumpDuration));
                }
                else if (distance.y < -swipeRange)
                {
                    stopTouch = true;
                    OnSwipeDown();
                    StartCoroutine(DeactivateAfterSeconds(1.533f));
                }
                else if (distance.x > swipeRange && !isMoving)
                {
                    stopTouch = true;
                    OnSwipeRight();
                    StartCoroutine(MoveCoroutine(Vector3.right * 0.53f));
                }
                else if (distance.x < -swipeRange && !isMoving)
                {
                    stopTouch = true;
                    OnSwipeLeft();
                    StartCoroutine(MoveCoroutine(Vector3.left * 0.53f));
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }

    private void OnSwipeUp()
    {
        if (isGrounded)
        {
            animator.SetBool("Jump", true);
            jumpStartPosition = transform.position;
            jumpTargetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + jumpDistance);
            isJumping = true;
            isGrounded = false;
            jumpStartTime = Time.time;
        }
    }

    private void SmoothJump()
    {
        float t = (Time.time - jumpStartTime) / jumpDuration;
        float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
        Vector3 currentPos = Vector3.Lerp(jumpStartPosition, jumpTargetPosition, t);
        currentPos.y += height;
        transform.position = currentPos;

        if (t >= 1)
        {
            isJumping = false;
            isGrounded = true;
        }
    }

    private void OnSwipeDown()
    {
        animator.SetBool("Slide", true);
    }

    private void OnSwipeRight()
    {
        if (!isMoving)
        {
            moveStartPosition = transform.position;
            moveTargetPosition = transform.position + new Vector3(0.53f, 0, 0);
            isMoving = true;
            moveStartTime = Time.time;
        }
    }

    private void OnSwipeLeft()
    {
        if (!isMoving)
        {
            moveStartPosition = transform.position;
            moveTargetPosition = transform.position + new Vector3(-0.53f, 0, 0);
            isMoving = true;
            moveStartTime = Time.time;
        }
    }

    private void SmoothMove()
    {
        float t = (Time.time - moveStartTime) / moveDuration;
        transform.position = Vector3.Lerp(moveStartPosition, moveTargetPosition, t);

        if (t >= 1)
        {
            isMoving = false;
        }
    }

    IEnumerator DeactivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Jump", false);
        animator.SetBool("Slide", false);
    }

    IEnumerator MoveCoroutine(Vector3 direction)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        isMoving = false;
    }
}
