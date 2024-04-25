using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1Scene : MonoBehaviour
{
    [Header("Scripts")]
    public OpeningGameplay openingGameplay;

    [Header("Chapter 1 - Level 1")]
    public GameObject blackScreenGO;
    public Text blackScreenText;
    public GameObject dialogueWithBossGO;
    public Text dialogueWithBossText;
    [SerializeField] int intAlanText = 0;
    [SerializeField] float fadeInText = 3f;
    [SerializeField] float fadeOutText = 3f;
    [SerializeField] float delayText = 3f;
    float defaultDelayText;

    [Header("Alan Script")]
    public string[] alanBlackScreen = new string[] {
        "Life was very smooth,",
        "until last year.",
        "It has been a painful year.",
    };

    public string[] alanDialogueWithBoss = new string[] {
        "Alan`s boss: \"Even after a year, all of your work are still trash!\"",
        "Alan`s boss: \"You were never grown, shame on you.\"",
        "Alan`s boss: \"Alan, no thanks to you. You`re fired!\"",
        "Alan_blank:  \"…\""
    };
    public string[] alanThoughts = new string[]
    {
        "How did it come to this?",
        "Wasn't I good enough?",
        "Wasn't my dedication worth anything?",
        "I’ve done whatever they asked.",
        "I thought things would just gonna run perfectly.",
        //Music Stop
        "Maybe they were right to let me go.",
        "Maybe I am just a failure.",
        "I may have never been a grown man."
    };

    private List<System.Action> scriptAlanFunction = new List<System.Action>();
    private List<System.Action> transitionAlanFunction = new List<System.Action>();
    private int alanFunc = 1;
    private int alanTransition = 1;
    bool thisCanRun = true;

    // Start is called before the first frame update
    void Start()
    {
        blackScreenGO.SetActive(true);
        dialogueWithBossGO.SetActive(false);

        defaultDelayText = delayText;

        openingGameplay.enabled = false;

        scriptAlanFunction.Add(DoNothing);
        scriptAlanFunction.Add(AlanOPBlackScreen);
        scriptAlanFunction.Add(AlanDialogueWithBoss);
        scriptAlanFunction.Add(AlanThoughts);

        blackScreenText.text = alanBlackScreen[intAlanText];
        blackScreenText.color = new Color(255, 255, 255, 0);

        transitionAlanFunction.Add(DoNothing);
        transitionAlanFunction.Add(TransitionAlanOPBlackScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (!thisCanRun)
            return;

        scriptAlanFunction[alanFunc]();
        transitionAlanFunction[alanTransition]();

    }

    void TransitionAlanOPBlackScreen()
    {
        delayText -= Time.deltaTime;
        if (delayText < defaultDelayText / 2)
            blackScreenText.color = new Color(255, 255, 255, blackScreenText.color.a - Time.deltaTime / fadeInText);
        else
            blackScreenText.color = new Color(255, 255, 255, blackScreenText.color.a + Time.deltaTime / fadeOutText);
    }

    void DoNothing()
    {
        return;
    }

    void AlanOPBlackScreen()
    {   
        if (delayText > 0)
            return;

        delayText = defaultDelayText;
        intAlanText++;
        if (intAlanText >= alanBlackScreen.Length)
        {
            blackScreenText.text = "";
            // openingGameplay.enabled = true;
            
            alanTransition = 0;
            alanFunc++;
            intAlanText = 0;

            dialogueWithBossGO.SetActive(true);
            dialogueWithBossText.text = alanDialogueWithBoss[intAlanText];
            // this.enabled = false; //disabled this scripts
            return;
        }
        blackScreenText.text = alanBlackScreen[intAlanText];
    }
    
    //if get clicked
    public void AlanDialogueWithBoss()
    {   
        //menekan tombol panah kanan atau kiri untuk melanjutkan dialog
        if (Input.GetKeyDown(KeyCode.RightArrow) )
        {
            if (intAlanText < alanDialogueWithBoss.Length - 1)
            {
                intAlanText++;
                dialogueWithBossText.text = alanDialogueWithBoss[intAlanText];
            }
            else
            {
                dialogueWithBossGO.SetActive(false);
                alanFunc++;
                intAlanText = 0;
                this.enabled = false;
                openingGameplay.enabled = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (intAlanText > 0)
            {
                intAlanText--;
                dialogueWithBossText.text = alanDialogueWithBoss[intAlanText];
            }
        }

        //skip dialog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueWithBossGO.SetActive(false);
            alanFunc++;
            intAlanText = 0;
            this.enabled = false;
            openingGameplay.enabled = true;
            return;
        }
    }

    void AlanThoughts()
    {
        if (intAlanText < alanThoughts.Length)
        {
            intAlanText++;
        }
    }
}
