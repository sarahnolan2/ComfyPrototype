using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMechanic : MonoBehaviour
{
    //this script is on the player

    private int numberPickedUp;


    // Start is called before the first frame update
    void Start()
    {
        numberPickedUp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    public void Pickup(GameObject VisualTrigger, GameObject Animal)
    {
        //initialize pickup! 
        numberPickedUp++;

        //move the animal to be behind the player

        Animal.transform.parent = this.gameObject.transform;
        Vector3 localForwardInParentSpace = this.transform.localRotation * Vector3.forward; //finds the forward position of the player
        float forwardShift = 5.0f; //sets the shift distance
        Animal.transform.localPosition = this.transform.localPosition + localForwardInParentSpace * forwardShift; //changes the location 

        Animal.transform.parent = null; //resets the parent to avoid local position to the player always behind the player

        //start following player
        Animal.GetComponent<AnimalFollower>().setIfFollowing(true);
        
        //then remove visual trigger
        GameObject.Destroy(VisualTrigger);
        Debug.Log("picked up "+Animal.name);

        if(numberPickedUp == 5)
        {
            // show a get to the party thing or something
            Debug.Log("picked up all animals!!!");
        }
    }

}
