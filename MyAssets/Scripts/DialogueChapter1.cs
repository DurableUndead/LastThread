using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChapter1 : MonoBehaviour
{
    [Header("Memories Scene")]
    public string[] familyPictureString = 
    {
        "Alan: smile :Me alongside the best parents in the world.:italicbold"
    };
    public string[] certificateString =
    {
        "Alan: grinning_smile :I feel like I can do anything after I got this.:italicbold"
    };
    public string[] dialogueWithParents1 = 
    {
        "Alan: slightly_smile :I never imagined this day would come so soon.:normal",
        "Alan's Dad: grinning_smile :Don't worry about your parents, we will do just fine here.:normal",
        "Alan's Mom: slightly_smile :Not everyone could get this opportunity, so make sure you take advantage of this and enjoy.:normal",
        "Alan's Dad: grinning_smile :I'll send pocket money, so make sure to get a girlfriend, my boy.:normal",
        "Alan: grinning_sweat :No, not interested.:normal",
        "Alan's Mom: smile :Ara~:normal",
        "Alan's Mom: smile :Your dad is just joking around Al, but I also do not prohibit you for having a girlfriend.:normal",
        "Alan:	relieved	:My parents just being a parents:italicbold",
        "Alan:	grinning_sweat	:Hehe…:normal",
        "Alan:	smile	:Anyway, thank you, Mom, Dad. :normal",
        "Alan:	smile	:Anyway, thank you, Mom, Dad. :normal",
        "Alan's Mom:	slightly_smile	:Just so you know, we always love you whatever happens and wherever you are.:normal",
        "Alan's Dad:	grinning_smile	:Yes boy, do whatever you think is right and achieve your dreams.:normal",
        "Alan's Mom:	smile	:If anything goes south, our door is always open. Don't forget.:normal"
    };

    public string[] dialogueWithParents2 = 
    {
        "Alan:	smile	:Goodbye, Mom. Goodbye, Dad.:normal",
        "Alan's Mom:	smile	:Goodbye, my sweet boy. Take care.:normal",
        "Alan's Dad:	grinning_smile :Goodbye, son. We'll be waiting.:normal"
    };
    //string jika player belum berinteraksi dengan objek foto, sertifikat, dan orang tua
    public string[] dialogueBeforeExitMemories = 
    {
        "Alan: neutral : I think I must check everything before leaving.:italicbold"
    };

    public string[] dialogueAfterOpenDoor = 
    {
        "???: silhouette :Hello… :normal",
        "???: silhouette :Alan, are you still alive? :normal",
        "Alan: neutral :Whose voice is that? :normal",
    };

    [Header("Wake Up Scane")]
    public string[] dialogueWakeUp = 
    {
        "???:	grinning_smile	:Looks like you're not dead, thankfully…:normal",
        "Alan:	pensive	:Who?:normal",
        "Cindy:	grinning_smile	:Oh, I'm Cindy. Nice to meet you, if I'm not wrong, Alan?:normal",
        "Alan:	pensive	:That's me, how do you know?:normal",
        "Cindy:	smile	:I took a glance of your ID Card.:normal",
        "Cindy:	grinning_smile	:Not on purpose though. It just dropped out of your wallet, for real. Peace…:normal",
        "Cindy:	grinning_smile	:By the way, if you're a 02 liner, then we're both 22. But I'm older, hehe…:normal",
        "Cindy:	grinning :Even though I'm an art student,:normal",
        "Cindy:	smile	:I think we could get along with each other.:normal",
        "Alan:	neutral	:I can't process everything she said.:italicbold",
        "Alan:	pensive	:Oh, I don't…:normal",
        "Alan:	spitting	:(Spitting water):normal",
        "Cindy:	pouting	:Wow, hold on there. Get up and throw your wet shirt, you could catch a cold.:normal",
        "Cindy:	pouting	:This is not how a guy should talk to an older girl.:normal",
        "Alan:	pensive	:Why is she acting like she's so close with me?:italicbold",
    };

    [Header("Encounter Scene")]
    public string[] dialogueNoSwimming = 
    {
        "Alan: neutral :I don't swim to get here. I think I'm safe. :italicbold"
    };
    public string[] dialogueBench = 
    {
        "Alan: neutral :Even the thought of sitting feels like too much effort. :italicbold"
    };
    public string[] dialogueTrees =
    {
        "Alan: neutral :Funny how even a solid wood standing feels more alive than I do. :italicbold"
    };
    public string[] dialoguePickingUpWatch =
    {
        "Alan: neutral :Whose watch is this? It looks like it's been broken for years. :italicbold",
        "Alan: neutral :I think I'll just keep it for now. :italicbold"
    };
    public string[] dialogueBeforePickingUpWallet =
    {
        "Cindy: grinning :You might wanna get your wallet first before a ghost steal it. :normal"
    };

    public string[] dialogueWithCindyAfterPickingUpWallet =
    {
        "Alan: neutral :So, what a girl doing here alone in the middle of the night?:normal",
        "Cindy: pouting :Why are you so nosy, huh?:normal",
        "Alan: neutral :Said a girl who being overly familiar with a guy she just met.:normal",
        "Cindy: grinning_sweat :Uh… you know what night is it today?:normal",
        "Cindy: grinning :It's Thursday night.:normal",
        "Alan: neutral :So?:normal",
        "Cindy: smirk :Do you believe in ghost? Are you scared of ghost?:normal",
        "Alan: neutral :No, haven't seen any.:normal",
        "Cindy: grinning_smile :Cool. A folk lore around here said that you can sight a ghost on Thursday night.:normal",
        "Cindy: smile :So help me hunt this ghost. I'm just very curious.:normal",
        "Alan: neutral :I don't understand. Why me?:italicbold",
        "Alan: neutral :Why are you so interested in this?:normal",
        "Cindy: grinning_smile :Just for fun. The thrill of hunting ghosts with somebody excites me.:normal",
        "Cindy: grinning_sweat :And just so you know, I also easily get scared when I'm alone.:normal",
        "Alan: neutral :She is totally weird.:italicbold",
        "Alan: neutral :But she makes me feel less lonely.:italicbold",
        "Cindy: grinning :Let's go, I don't wanna waste any more time.:normal"
    };

    public string[] afterFoundWatchAndTalkWithCindy = 
    {
        "I'm walking…",
        "But it feels different than the last…",
        "No more despair…",
        "I can keep moving forward."
    };

    [Header("Happiness Scene")]
    public string[] dialogueAlanCindyWalkTogether =
    {
        "Cindy:	relieved	:You know? Before we met, I was chased by a creep. That was very scary.:normal",
        "Alan:	neutral	:Are you still scared?:normal",
        "Cindy:	relieved	:Maybe…:normal",
        "Alan:	neutral	:Why are you scared?:normal",
        "Cindy:	relieved	:I don't know… More like, I just have a feeling that he has a bad intention.:normal",
        "Cindy:	slightly_smile	:Alan, do you want to know a story of me?:normal",
        "Alan:	neutral	:Even if I said no, you would still tell me.:normal",
        "Cindy:	grinning_sweat	:Hehe…:normal",
        "Cindy:	neutral	:Me and my parents… we don't have a strong bond.:normal",
        "Cindy:	neutral	:We are not fighting, we are just not close.:normal",
        "Cindy:	neutral	:I always try to be a daughter they can be proud of.:normal",
        "Cindy:	neutral	:They give me everything I want and need materially, but… they were never paying their attention to me.:normal",
        "Cindy:	relieved	:I don't know why I'm telling you this… You must be thinking I'm weird right now.:normal",
        "Alan:	neutral	:Yes, you really are. This girl is very unpredictable, the first that I can't read.:italicbold",
        "Alan:	neutral	:I…:normal"
    };

    [Header("MiniGame Scene")]
    public string[] dialogueRound1MiniGameTree =
    {
        "Cindy:	silhouette	:Al, this hunt is getting boring.:normal",
        "Cindy:	silhouette	:How about we play something?:normal",
        "Alan:	neutral	:Huh? Where are you?:normal",
        "Cindy:	silhouette	:Behind one of the trees.:normal",
        "Cindy:	silhouette	:Try to find me... That's the game.:normal",
        "Cindy:	silhouette	:Now go, don't keep a girl waiting.:normal",
        "Alan:	neutral	:This girl… I don't know what she's up to.:italicbold",
        "Alan:	neutral	:I just want to follow her game.:italicbold",
        "Cindy:	silhouette	:Eh, I'll reward you if you can find me quickly enough.:normal"
    };

    public string[] dialogueWrongTree = 
    {
        "Alan: neutral :She's not here…: normal"
    };

    public string[] dialogueRound2MiniGameTree =
    {
        "Cindy:	laugh	:Ahahahaha:normal",
        "Cindy:	laugh	:Took you to long to find me.:normal",
        "Cindy:	grinning_smile	:But it's fun…:normal",
        "Cindy:	grinning	:Let's do it one more time.:normal",
        "Cindy:	smirk	:Close your eyes, Al. No cheating.:normal",
        "Alan:	neutral	:This is so childish and silly…:italicbold",
        "Alan:	slightly_smile	:But it's not the fun I seek, I just wanna be with someone.:italicbold"
    };

    public string[] dialogueFoundCindyRound2 =
    {
        "Alan: neutral :Cindy? I know you're here.:normal",
        "Alan: neutral :…:normal"
    };

    public string[] dialogueCindyOnTop = 
    {
        "Cindy:	shocked	:AH!!!!:normal",
        "Alan:	slightly_smile	:Is this the reward?:normal",
        "Cindy:	confounded	:No no no… you st- stupid. Perv-:normal",
        "Alan:	laugh	:Ahahahahaha:normal",
        "Alan:	slightly_smile	:Cindy… Thanks…:normal",
        "Cindy:	confounded	:Ff- for waht??:normal",
        "Alan:	slightly_smile	:I feel like I want to give it a try again after I know you…:normal",
        "Cindy:	confounded	:Try again for what?:normal",
        "Alan:	relieved	:To live…:normal",
        "Alan:	relieved	:I bounced back thanks to you…:normal",
        "Alan:	slightly_smile	:You told me your stories, now let me tell you mine.:normal",
        "Cindy:	grinning	:Oww… fair enough.:normal",
        "Alan:	slightly_smile	:But beforehand, could you please stand up?:normal",
        "Cindy:	flushed	:!!!:normal",
        "Alan:	grinning_sweat	:To be honest, I'd be lying if I don't like this moment.:italicbold"
    };

    public string[] thoughtsAfterMiniGameTree =
    {
        "I tell her my stories.",
        "Everything I need to share with someone.",
        "I realized I only need somebody to lean on for now.",
        "I feel so stupid for what I did before.",
        "And now, this night may come to an end."
    };

    [Header("Cindy Scene - Level 4")]
    public string[] dialogueAfterAlanDroppedWatch = 
    {
        "Cindy:	neutral	:That watch…:normal",
        "Alan:	neutral	:You knew about it? Seems broken for years.:normal",
        "Cindy:	neutral	:…:normal",
        "Alan:	worried	:Cindy?:normal",
        "Cindy:	neutral	:That's mine, a gift from my mom.:normal",
        "Alan:	neutral	:Uh.. take it then…:normal",
        "Cindy:	neutral	:Hey Al, do you want to listen my last story before we part?:normal",
        "Alan:	neutral	:I don't mind, I… kinda wanna hear it.:normal",
        "Cindy:	neutral	:I- I think I can tell you this now…:normal",
        "Cindy:	neutral	:That watch is the only gift from my mother,:normal",
        "Cindy:	neutral	:More like I asked her that gift.:normal",
        "Cindy:	neutral	:Still… I bragged about it to my friends…:normal",
        "Cindy:	neutral	:A gift from a mother.:normal",
        "Cindy:	pensive	:Alan, sorry...:normal",
        "Cindy:	pensive	:I lied about being older than you.:normal",
        "Cindy:	pensive	:We both are born in the same year, but I was never aged.:normal",
        "Alan:	neutral	:I don't like where this is going.:italicbold",
        "Cindy:	pensive	:2 years ago in this riverbank, I was murdered.:normal",
        "Cindy:	pensive	:The creep I told, that guy dropped my cold body under this river.:normal",
        "Alan:	worried	:Don't be scared, Al. Stay cool. She might need help.:italicbold",
        "Cindy:	pensive	:I'm sorry… for this truth.:normal",
        "Cindy:	pensive	:Something in my chest feels so itchy…:normal",
        "Cindy:	pensive	:I always wanted to tell somebody... about this. You…:normal",
        "Cindy:	cry	:Alan…:normal",
        "Cindy:	cry	:I'm scared… the fact that I actually don't want to be like this…:normal",
        "Cindy:	cry	:And this last 2 years feels so lonely until you came…:normal",
        "Cindy:	tears_smile	:I'm happy… with you…:normal",
        "Cindy:	tears_smile	:Feels like I can do anything with you.:normal",
        "Cindy:	cry	:I want it… but-:normal",
        "Alan:	neutral	:It's hurt to know the truth… but if she's not alive, It doesn't matter…:italicbold",
        "Alan:	relieved	:She helped me, after all… Now it's my turn to repay her the favor.:italicbold",
        "Alan:	slightly_smile	:Cindy, I-:normal",
    };
}
