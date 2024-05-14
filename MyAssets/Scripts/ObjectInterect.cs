using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInterect : MonoBehaviour
{
    public Chapter1Scene scriptChapter1Scene;
    public TransitionFunction scriptTransitionFunction;
    public GuideScript scriptGuideScript;
    public bool canInteract = false;
    public Collider2D currentCollider;
    List<string> targetObjectTags = new List<string>{"Door", "Photo", "Certificate", "Parent", "NoSwimming", "Trees", "Beach", "Watch","Cindy"};
    public List<GameObject> tempTrees = new List<GameObject>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (scriptTransitionFunction.isDialogue)
                return;
            if (currentCollider.gameObject.tag == "Photo")
                scriptChapter1Scene.TriggerObjectPhoto();
            else if (currentCollider.gameObject.tag == "Certificate")
                scriptChapter1Scene.TriggerObjectCertificate();
            else if (currentCollider.gameObject.tag == "Parent")
                scriptChapter1Scene.TriggerObjectParent();
            else if (currentCollider.gameObject.tag == "Door")
                scriptChapter1Scene.TriggerObjectDoor();
            else if (currentCollider.gameObject.tag == "NoSwimming")
                scriptChapter1Scene.TriggerObjectNoSwimmingSign();
            else if (currentCollider.gameObject.tag == "Trees")
                scriptChapter1Scene.TriggerObjectTrees(currentCollider.gameObject);
            else if (currentCollider.gameObject.tag == "Beach")
                scriptChapter1Scene.TriggerObjectBeach();
            else if (currentCollider.gameObject.tag == "Watch")
                scriptChapter1Scene.TriggerObjectWatch();
            else if (currentCollider.gameObject.tag == "Cindy")
                scriptChapter1Scene.TriggerObjectCindy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag == "Door" || other.gameObject.tag == "Photo" || other.gameObject.tag == "Certificate" || other.gameObject.tag == "Parent")
        if (targetObjectTags.Contains(other.gameObject.tag))
        {
            scriptGuideScript.FadeInGuideInteraction();
            canInteract = true;
            currentCollider = other;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (targetObjectTags.Contains(other.gameObject.tag))
        {
            scriptGuideScript.FadeOutGuideInteraction();
            canInteract = false;
            currentCollider = null;
        }
    }

    public void ManualTriggerExit2DTree(GameObject other)
    {
        if (other.tag == "Trees")
        {
            scriptGuideScript.FadeOutGuideInteraction();
            canInteract = false;
            currentCollider = null;
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     //key input E
    //     if (Input.GetKeyDown(KeyCode.E) && canInteract)
    //     {
    //         if (scriptChapter1Scene.isDialogue)
    //             return;
    //         if (other.gameObject.tag == "Photo")
    //             scriptChapter1Scene.TriggerObjectPhoto();
    //         else if (other.gameObject.tag == "Certificate")
    //             scriptChapter1Scene.TriggerObjectCertificate();
    //         else if (other.gameObject.tag == "Parent")
    //             scriptChapter1Scene.TriggerObjectParent();
    //         else if (other.gameObject.tag == "Door")
    //             scriptChapter1Scene.TriggerObjectDoor();
    //     }
    // }
}
