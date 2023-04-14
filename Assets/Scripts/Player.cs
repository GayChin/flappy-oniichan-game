using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private Animator anim;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    private string ARM_ANIMATION = "MoveArm";
    
    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    public void Update()
    {
        anim.SetBool(ARM_ANIMATION, true);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            direction = Vector3.up * strength;
            audioSource.Play();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up;
                audioSource.Play();
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            FindObjectOfType<GameManager>().GameOver();
            audioSource3.Play();
            audioSource2.Stop();
            audioSource.Stop();
        }

        if (collision.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
            audioSource2.Play();
        }
    }

}
