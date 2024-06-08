using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInterect : MonoBehaviour
{
    [Header("Scripts")]
    public Chapter1Scene scriptChapter1Scene;
    public TransitionFunction scriptTransitionFunction;
    public GuideScript scriptGuideScript;
    public AudioManager scriptAudioManager;
    private PlayerMovement scriptPlayerMovement;

    [Header("Interact")]
    public bool canInteract = false;
    public bool canDetectObject = true;
    public Collider2D currentCollider;
    Dictionary<string, Action> triggerObjectList = new Dictionary<string, Action>();
    List<string> targetObjectTags = new List<string>{"Door", "Photo", "Trophy", "Parent", "NoSwimming", "Trees", "Bench", "Watch","Cindy"};
    public List<GameObject> tempTrees = new List<GameObject>();

    void Start()
    {
        scriptPlayerMovement = GetComponent<PlayerMovement>();
        triggerObjectList.Add("Photo", scriptChapter1Scene.TriggerObjectPhoto);
        triggerObjectList.Add("Trophy", scriptChapter1Scene.TriggerObjectTrophy);
        triggerObjectList.Add("Parent", scriptChapter1Scene.TriggerObjectParent);
        triggerObjectList.Add("Door", scriptChapter1Scene.TriggerObjectDoor);
        triggerObjectList.Add("NoSwimming", scriptChapter1Scene.TriggerObjectNoSwimmingSign);
        triggerObjectList.Add("Bench", scriptChapter1Scene.TriggerObjectBench);
        triggerObjectList.Add("Watch", scriptChapter1Scene.TriggerObjectWatch);
        triggerObjectList.Add("Cindy", scriptChapter1Scene.TriggerObjectCindy);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (scriptTransitionFunction.isDialogue)
                return;

            scriptAudioManager.PlayTriggerOrNextSoundUI();
            scriptPlayerMovement.StopWalkingOrRunning();
            if (currentCollider.gameObject.tag == "Trees")
                scriptChapter1Scene.TriggerObjectTrees(currentCollider.gameObject);
            else
                triggerObjectList[currentCollider.gameObject.tag]();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDetectObject)
            return;

        if (targetObjectTags.Contains(other.gameObject.tag))
        {
            // scriptGuideScript.FadeInGuideInteraction();
            // mengakses child pada other dan melakukan enable gameobject child
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            canInteract = true;
            currentCollider = other;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (targetObjectTags.Contains(other.gameObject.tag))
        {
            // scriptGuideScript.FadeOutGuideInteraction();
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            canInteract = false;
            // currentCollider = null;
        }
    }

    public void ManualTriggerExit2DTree(GameObject other)
    {
        if (other.tag == "Trees")
        {
            // scriptGuideScript.FadeOutGuideInteraction();
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            canInteract = false;
            currentCollider = null;
        }
    }
}
