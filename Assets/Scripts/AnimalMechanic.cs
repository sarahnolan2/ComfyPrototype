using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AnimalMechanic : MonoBehaviour
{
    //this script is on the player

    private int numberPickedUp;

    [SerializeField]
    private GameObject cafeTextObj;
    [SerializeField]
    private GameObject visualcue;

    TextMeshPro cafeText;

    [SerializeField]
    AudioSource aso;
    public AudioClip[] clips = new AudioClip[2];

    private int totalNumberAnimals;

    // Start is called before the first frame update
    void Start()
    {
        totalNumberAnimals = 5;
        numberPickedUp = 0;
        cafeText = cafeTextObj.GetComponentInChildren<TextMeshPro>();
        visualcue.SetActive(false);
        aso.loop = false;
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

        Vector3 movementShift = this.transform.localPosition + localForwardInParentSpace * forwardShift;  //changes the location
        //Animal.transform.localPosition = movementShift; 

        Animal.GetComponent<Rigidbody>().AddForce(-movementShift, ForceMode.Impulse);
        
        aso.clip = clips[0];
        aso.Play();

        Animal.transform.parent = null; //resets the parent to avoid local position to the player always behind the player

        //start following player
        Animal.GetComponent<AnimalFollower>().setIfFollowing(true);
        
        //then remove visual trigger
        GameObject.Destroy(VisualTrigger);
        Debug.Log("picked up "+Animal.name);

        if(numberPickedUp >= totalNumberAnimals)
        {
            // show a get to the party thing or something
            //Debug.Log("picked up all animals!!!");
            aso.clip = clips[2];
            aso.Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CafeDoor"))
        {
            if (numberPickedUp >= totalNumberAnimals)
            {
                //show message
                visualcue.SetActive(true);
                //all animals found and can enter cafe
                //Debug.Log("all animals found, can enter cafe");

                cafeText.text = "Everyone is here! Entering the cafe...";

                
                Debug.Log("testestest");
                aso.clip = clips[1];

                StartCoroutine(LoadNextScene());
            }
            else
            {
                //show message
                visualcue.SetActive(true);
                //animals missing
                //Debug.Log("at cafe but animals missing");

                cafeText.text = "Some friends are still missing.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "CafeDoor")
        {
            //hide message
            //Debug.Log("hiding message");
            cafeText.text = "";
            visualcue.SetActive(false);
        }
    }

    IEnumerator LoadNextScene()
    {

        yield return new WaitForSeconds(3);
        aso.Play();
        SceneManager.LoadScene(1);
    }
}
