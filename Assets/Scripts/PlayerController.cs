using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerJump = 10f;
    public float gravityModifier = 0;
    public bool isOnGround = true;
    public bool gameOver = false;
    public ParticleSystem explostionSystem;
    public ParticleSystem dirtSystem;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;
    private Rigidbody playerRb;
    private Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && gameOver == false)
        {
            playerRb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtSystem.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtSystem.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explostionSystem.Play();
            dirtSystem.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }


}
