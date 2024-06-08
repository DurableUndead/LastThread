using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChapter2 : MonoBehaviour
{
    [Header("Limbo - Level 1")]
    public string[] dialogueLimbo1 =
    {
        "Cindy:	neutral	:Hi, Alan…:normal",
        "Cindy:	neutral	:This is the realm between life and death, or you can call it limbo.:normal",
        "Cindy:	neutral	:You've spent too much time with that weak body around a ghost…:normal",
        "Cindy:	neutral	:And now look where it's gotten you.:normal",
        "Cindy:	neutral	:So unfortunate we come unto this…:normal",
        "Cindy:	neutral	:Another truth,:normal",
        "Cindy:	neutral	:Only those who feel despair and attempted a suicide could see a ghost.:normal",
        "Cindy:	neutral	:Just say the word, and we'll leave this world.:normal",
        "Alan:	neutral	:I can't. I don't want to.:normal",
        "Cindy:	neutral	:Why?:normal"

    };

    public string[] dialogueLimbo2Her = 
    {
        "Alan:	neutral	:You are not her.:normal",
        "Alan:	neutral	:You are me.:normal",
        "Alan:	neutral	:You are my despair.:normal",
        "Alan (Despair): pensive	:Yes. Now say it.:normal",
        "Alan (Despair): pensive	:You want it before. Here's the chance.:normal"
    };

    public string[] dialogueLimbo3No = 
    {
        "Alan:	neutral	:No.:normal",
        "Alan:	relieved	:I want to live.:normal",
        "Alan (Despair): pensive	:Why?:normal",
        "Alan (Despair):	pensive	:This world has no place for you, no future for you.:normal",
        "Alan (Despair):	pensive	:You were never be a grown man.:normal",
        "Alan (Despair):	pensive	:So why bother stay?:normal"
    };

    public string[] dialogueLimbo4Tomorrow = 
    {
        "Alan:	slightly_smile	:I want to live for tomorrow.:normal",
        "Alan:	relieved	:I got a second chance to atone.:normal",
        "Alan:	neutral	:This world may be cruel, but that's the challenge.:normal",
        "Alan:	relieved	:My place? I will figure it somehow.:normal",
        "Alan:	relieved	:I will live. That's my resolve.:normal",
        "Alan:	neutral	:Now leave. Cindy is… and my parents are waiting for me.:normal"
    };

    [Header("After Limbo - Level 2")]
    public string[] dialogueWakeUpAfterLimbo =
    {
        "Cindy:	grinning_smile	:Looks like you're not dead, thankfully…:normal",
        "Alan:	neutral	:Who?:normal",
        "Cindy:	grinning_smile	:Oh, I'm Cindy. Nice to meet you, if I'm not wrong, Alan?:normal",
        "Alan:	neutral	:That's me, how do you know?:normal",
        "Cindy:	laugh	:Of course I know you, silly. We spent a night together.:normal",
        "Alan:	laugh	:Ahahahahaha:normal",
        "Cindy:	laugh	:Ahahahahaha:normal",
        "Alan:	slightly_smile :Cindy…:normal",
        "Alan:	slightly_smile :I want to return the favor.:normal",
        "Alan:	slightly_smile :I'll help you get a good rest.:normal",
        "Cindy:	flushed	:…:normal",
        "Cindy:	smile	:Yes…:normal",
        "Cindy:	tears_smile	:Thank you.:normal",
        "Cindy:	tears_smile	:Alan…:normal",
        "Cindy:	tears_smile	:And I…:normal",
        "Cindy:	tears_smile	:I need you to live.:normal",
        "Cindy:	tears_smile	:Sorry… for being selfish.:normal",
        "Alan:	smile	:Yes… Thanks… Once again…:normal"
    };

    public string[] dialogueBlackScreenAfterLimbo =
    {
        "Cindy:	neutral	:…:normal",
        "Cindy:	grinning_sweat	:But if I'm still alive, you would ask for my contact tonight, right?:normal",
        "Alan:	neutral	:Uh… for real, where's that come from?:normal",
        "Cindy:	slightly_smile	:I just imagine a universe where we can meet with none of this happened.:normal",
        "Cindy:	grinning_smile	:It's funny… a ghost being delusional.:normal",
        "Alan:	relieved	:I would…:normal",
        "Cindy:	flushed	:…:normal",
        "Cindy:	smile	:…:normal"
    };

    public string[] thoughtsAfterWakeUp =
    {
        "That night was very cold…",
        "But her existence warms me…",
        "I will treasure our fateful meeting…",
        "An another stories cut short,",
        "I helped her find her body,",
        "I met her parents,",
        "I do everything I could to help her…",
        "Until our interaction comes to an end…"
    };

    public string[] dialogueCindyHuggingAlan = 
    {
        "Alan:	slightly_smile	:Are you gonna disappear?:normal",
        "Cindy:	slightly_smile	:Maybe.:normal",
        "Cindy:	grinning_smile	:But I'm very happy.:normal",
        "Cindy:	relieved :Spending my last fond memories with you…:normal",
        "Cindy:	slightly_smile	:Al…:normal",
        "Cindy:	slightly_smile	:don't jump again.:normal",
        "Alan:	slightly_smile	:I won't.:normal",
        "Cindy:	smile	:If I were still alive….:italicbold",
        "Cindy:	smile	:I'd capture your eternal presence in my canvas.:italicbold",
        "Cindy:	smile	:Thank you… for this short moment.:normal"
    };

    public string[] cindyThoughtsYellowText = 
    {
        "You must live…",
        "I really wish I were still alive…",
        "To draw him on my canvas…"
    };

    public string[] dialogueAlanCallMom =
    {
        "Alan:	slightly_smile	:Hi, Mom.:normal",
        "Alan's Mom:	silhouette	:Yes, my dear.:normal",
        "Alan:	slightly_smile	:Can I stay at home for a while?:normal",
        "Alan:	slightly_smile	:I have a story to tell Dad about.:normal",
        "Alan's Mom:	silhouette	:Ara~ you're not telling me?:normal",
        "Alan:	grinning_sweat	:You too, Mom. Hehe…:normal",
        "Alan's Mom:	silhouette	:Come son, we're waiting.:normal",
        "Alan:	grinning_smile	:Alright, see you soon.:normal",
        "Alan's Mom:	silhouette	:Al, wait.:normal",
        "Alan:	slightly_smile	:Yes? I'm still here.:normal",
        "Alan's Mom:	silhouette	:I love you.:normal",
        "Alan:	grinning_sweat	:Hehe… I know that. Love you too,_Mom.:normal"
    };

    [Header("Home - Level 3")]
    public string[] dialogueAlanHome =
    {
        "Alan:	grinning_smile	:Hi… Mom, Dad.:normal",
        "Alan's Dad:	grinning_smile	:Son, where's your girlfriend? My wife tells me you have a love story to tell me.:normal",
        "Alan's Mom:	smile	:…:normal",
        "Alan:	grinning_sweat	:Ehh…:normal",
        "Alan's Mom:	slightly_smile	:Anyway, welcome home, my dear.:normal",
        "Alan's Dad:	grinning_smile	:Yeah son, welcome home, boy.:normal",
        "Alan's Mom:	slightly_smile	:I'm gonna prepare your favorite snack.:normal",
        "Alan's Dad:	grinning_smile	:And your handsome dad is gonna rest a bit. Your room is ready by the way.:normal",
        "Alan's Mom:	smile	:And a handsome husband.:normal",
        "Alan:   relieved	:They know I'm standing in front of them… right?:italicbold",
        "Alan:	grinning_smile	:Alright. go ahead. I'm coming.:normal",
        "Alan's Mom:	slightly_smile	:Okay… Don't spend too much time outside here.:normal",
        "Alan's Mom:	slightly_smile	:They said ghost spotted around here.:normal",
        "Alan's Mom:	slightly_smile	:You don't wanna get terrified by seeing them, don't you?:normal",
        "Alan:   relieved	:If I told you about Cindy, you're the one who gonna get terrified.:italicbold"
    };

    public string[] alanLastThoughts =
    {
        "Cindy…",
        "I want to live…",
        "Thanks to you…",
        "You saved my last thread…",
        "Memories of you will always be in my arrays…"
    };
}
