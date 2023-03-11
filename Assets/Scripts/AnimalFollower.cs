using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFollower : MonoBehaviour
{
    bool isFollowing;

    public GameObject player;
    public Rigidbody thisRb;

    // Start is called before the first frame update
    void Start()
    {
        isFollowing = false;
        thisRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIfFollowing(bool follow)
    {
        isFollowing = follow;
        thisRb.freezeRotation = false;
    }

    private void FixedUpdate()
    {
        if(isFollowing)
        {

        
            ///for moving the animal towards the player

            Vector3 force = this.transform.position - player.transform.position; //define a force

            force.Normalize(); //means that we reduce the length to 1. 
                               //so we do this when we want the vector or all vectors to have the same magnitude 
                               // and only care about the direction

            //force *= 200.0f; // so when normalizing, the force will always be 200.

            //this.GetComponent<Rigidbody>().AddForce(0, 50.0f, 0);
            this.GetComponent<Rigidbody>().AddForce(-force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isFollowing)
        {

            ///for moving the animal in the air with the bounciness


            Vector3 force = this.transform.position - collision.transform.position; //define a force

            force.Normalize(); //means that we reduce the length to 1. 
                           //so we do this when we want the vector or all vectors to have the same magnitude 
                           // and only care about the direction

            //force *= 200.0f; // so when normalizing, the force will always be 200.

            this.GetComponent<Rigidbody>().AddForce(0,5.0f,0, ForceMode.Impulse);
            //this.GetComponent<Rigidbody>().AddForce(-force);
        }
    }


}
