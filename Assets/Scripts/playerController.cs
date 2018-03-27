using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public int playerID;
    public float fuel;
    public Text fuelText;

    public float Damage;
    public Text DamageText;

    public float RotationSpeed;
    public float ThrustForce;
    public Animator mainThrusterAnim;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        Damage = 100;
        fuel = 15;

        rb2d.AddForce(transform.up * 300);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        if (playerID == 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb2d.angularVelocity = RotationSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb2d.angularVelocity = -RotationSpeed;
            }
            else
            {
                rb2d.angularVelocity = 0;
            }


            //Thrust button
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(transform.up * ThrustForce);
                if (mainThrusterAnim != null && mainThrusterAnim.runtimeAnimatorController != null)
                {
                    mainThrusterAnim.SetBool("ApplyingThrust", true);
                }

                fuel -= 0.01f;
                fuelText.text = "Fuel " + Mathf.Round(fuel);

            }
            else
            {
                if (mainThrusterAnim != null && mainThrusterAnim.runtimeAnimatorController != null)
                {
                    mainThrusterAnim.SetBool("ApplyingThrust", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }


    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 10;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 5.0f);

    }
    private void OnCollisionEnter2D(Collision2D hitInfo)
    {

        //If collision with landing pad
        if (hitInfo.gameObject.tag == "Landing_Pad")
        {
            
            if (hitInfo.relativeVelocity.magnitude > 5)
            {
                Damage -= 5;
                HandleLanderDestroy();
            }

        }
        else

        if (hitInfo.relativeVelocity.magnitude > 3)
        {
            Damage -= hitInfo.relativeVelocity.magnitude * 2;
            HandleLanderDestroy();
        }else
        {
            Damage -= 5;
        }

        DamageText.text = "Damage " + Mathf.Round(Damage) +" %";
        if (Damage <30)
            DamageText.color = Color.red;
        else
            DamageText.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Fuel")
        {
            //fuel += 10f;
            Destroy(collider.gameObject);
        }
    }

    private void HandleLanderDestroy()
    {
        //if (explosionPrefab != null)
        //{
        //   var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //    Destroy(explosion, 1f);
        //}
        if (Damage < 0)
        {
            Destroy(gameObject);
        }
        //restartbutton.gameObject.GetComponent<Image>().enabled = true;
        //restartbutton.gameObject.transform.FindChild("Text").GetComponent<Text>().enabled = true;
    }
}
