using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTrigger : MonoBehaviour
{
    bool isPlayerInRange; //keep track of whether the player is in range

    [SerializeField]
    private GameObject visualCue; //holds all of the visual cue content

    
    void Awake()
    {
        
        isPlayerInRange = false;
    }

    private void Start()
    {
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange)
        {
            //show visual cue if player is in range
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("VISUAL TRIGGER PICKUP");
                //remove visual cues
                isPlayerInRange = false;

                //initialize pickup!

                AnimalMechanic gamemanager = GameObject.Find("FirstPersonController").GetComponent<AnimalMechanic>();
                gamemanager.Pickup(gameObject, gameObject.transform.parent.gameObject);
                
            }
        }
        else
        {
            //don't show visual cue if player is not in range
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            //Debug.Log("IN RANGE");

            //show visual cue if player is in range
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            //Debug.Log("OUT OF RANGE");

            //don't show visual cue if player is not in range
            visualCue.SetActive(false);
        }
    }
}
