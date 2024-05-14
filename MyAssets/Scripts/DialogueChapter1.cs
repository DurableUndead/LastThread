using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChapter1 : MonoBehaviour
{
    [Header("Memories Scene")]
    public string[] familyPictureString = 
    {
        "Alan: Smile : Me alongside the best parents in the world.:italic"
    };
    public string[] certificateString =
    {
        "Alan: Smile : I feel like I can do anything after I got this.:italic"
    };
    public string[] dialogueWithParents1 = 
    {
        "Alan: Smile : I never imagined this day would come so soon :normal",
        "Alan's Dad: Flat : Don't worry about your parents, we will do just fine here :normal",
        "Alan's Mom: Smile : Not everyone could get this opportunity, so make sure you take advantage of this and enjoy :normal",
        "Alan's Dad: Flat : I'll send pocket money, so make sure to get a girlfriend, my boy :normal",
        "Alan: Sad : No, not interested :normal",
        "Alan's Mom: Smile : Ara~ :normal",
        "Alan's Mom: Sad : Your dad is just joking around Al, but I also do not prohibit you for having a girlfriend :normal",
        "Alan: Flat : My parents just being a parents :normal",
        "Alan: Smile : Hehe… :normal",
        "Alan: Sad : Anyway, thank you, Mom, Dad :normal",
        "Alan: Smile : Someday, I will do or bring something to make you guys proud of me :normal",
        "Alan's Mom: Sad : Just so you know, we always love you whatever happens and wherever you are :normal",
        "Alan's Dad: Flat : Yes boy, do whatever you think is right and achieve your dreams :normal",
        "Alan's MoM: Flat : If anything goes south, our door is always open. Don't forget!. :normal",
    };

    public string[] dialogueWithParents2 = 
    {
        "Alan: Smile :Goodbye, Mom. Goodbye, Dad.:normal",
        "Alan's Mom: Smile :Goodbye, my sweet boy. Take care.:normal",
        "Alan's Dad: Smile :Goodbye, son. We'll be waiting.:normal",
    };
    //string jika player belum berinteraksi dengan objek foto, sertifikat, dan orang tua
    public string[] dialogueBeforeExitMemories = 
    {
        "Alan: Flat : I think I must check everything before leaving.:italic"
    };

    public string[] dialogueAfterOpenDoor = 
    {
        "???: Smile :Hello… :normal",
        "???: Sad :Alan, are you still alive? :normal",
        "Alan: Flat :Whose voice is that? :normal",
    };

    [Header("Wake Up Scane")]
    public string[] dialogueWakeUp = 
    {
        "???: Flat :Looks like you're not dead, thankfully… :normal",
        "Alan: Sad :Who? :normal",
        "Cindy: Smile :Oh, I'm Cindy. Nice to meet you, if I'm not wrong, Alan? :normal",
        "Alan: Flat :That's me, how do you know? :normal",
        "Cindy: Sad :I took a glance of your ID Card. :normal",
        "Cindy: Smile :Not on purpose though. It just dropped out of your wallet, for real. Peace… :normal",
        "Cindy: Sad :By the way, if you're a 02 liner, then we're both 22. But I'm older, hehe… :normal",
        "Cindy: Smile :Even though I'm an art student,",
        "Cindy: Flat :I think we could get along with each other. :normal",
        "Alan: Flat :I can't process everything she said. :normal",
        "Alan: Sad :Oh, I don't… :normal",
        "Alan: Smile :(Spouting water) :normal",
        "Cindy: Smile :Wow, hold on there. Get up and throw your wet shirt, you could catch a cold. :normal",
        "Cindy: Flat :This is not how a guy should talk to an older girl. :normal",
        "Alan: Flat :Why is she acting like she's so close with me? :normal",
    };

    [Header("Encounter Scene")]
    public string[] dialogueNoSwimming = 
    {
        "Alan: Sad :I don't swim to get here. I think I'm safe. :bold_italic"
    };
    public string[] dialogueBeach = 
    {
        "Alan: Smile :Even the thought of sitting feels like too much effort. :bold_italic"
    };
    public string[] dialogueTrees =
    {
        "Alan: Flat :Funny how even a solid wood standing feels more alive than I do. :bold_italic"
    };
    public string[] dialoguePickingUpWatch =
    {
        "Alan: Flat :Whose watch is this? It looks like it's been broken for years. :bold_italic",
        "Alan: Flat :I think I'll just keep it for now. :bold_italic"
    };
    public string[] dialogueBeforePickingUpWallet =
    {
        "Cindy: Smile :You might wanna get your wallet first before a ghost steal it. :normal"
    };

    public string[] dialogueWithCindyAfterPickingUpWallet =
    {
        "Alan: Smile :So, what a girl doing here alone in the middle of the night? :normal",
        "Cindy: Flat :Why are you so nosy, huh? :normal",
        "Alan: Sad :Said a girl who being overly familiar with a guy she just met. :normal",
        "Cindy: Smile :Uh… you know what night is it today? :normal",
        "Cindy: Flat :It's Thursday night. :normal",
        "Alan: Sad :So? :normal",
        "Cindy: Flat :Do you believe in ghost? Are you scared of ghost? :normal",
        "Alan: Smile :No, haven't seen any. :normal",
        "Cindy: Sad :Cool. A folk lore around here said that you can sight a ghost on Thursday night. :normal",
        "Cindy: Smile :So help me hunt this ghost. I'm just very curious. :normal",
        "Alan: Flat :I don't understand. Why me? :normal",
        "Alan: Sad :Why are you so interested in this? :normal",
        "Cindy: Flat :Just for fun. The thrill of hunting ghosts with somebody excites me. :normal",
        "Cindy: Smile :And just so you know, I also easily get scared when I'm alone. :normal",
        "Alan: Sad :She is totally weird. :normal",
        "Alan: Smile :But she make me feels less lonely. :normal",
        "Cindy: Flat :Let's go, I don't wanna waste any more time. I'm going home before midnight. :normal",
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
        "Cindy: Flat :You know? Before we met, I was chased by a creep. That was very scary.: normal",
        "Alan: Smile :Are you still scared?: normal",
        "Cindy: Flat :Maybe…: normal",
        "Alan: Sad :Why are you scared?: normal",
        "Cindy: Smile :I don't know… More like, I just have a feeling that he has a bad intention.: normal",
        "Cindy: Sad :Alan, do you want to know a story of me?: normal",
        "Alan: Flat :Even if I said no, you would still tell me.: normal",
        "Cindy: Smile :Hehe…  : normal",
        "Cindy: Flat :Me and my parents… we don't have a strong bond.: normal",
        "Cindy: Sad :We are not fighting, we are just not close.: normal",
        "Cindy: Smile :I always try to be a daughter they can be proud of.: normal",
        "Cindy: Flat :They give me everything I want and need materially, but… they were never paying their attention to me.: normal",
        "Cindy: Smile :I don't know why I'm telling you this… You must be thinking I'm weird right now.: normal",
        "Alan: Sad :Yes, you really are. This girl is very unpredictable, the first that I can't read.: normal",
        "Alan: Flat :I…: normal"
    };

    [Header("MiniGame Scene")]
    public string[] dialogueRound1MiniGameTree =
    {
        "Cindy: Flat :Al, this hunt is getting boring.: normal",
        "Cindy: Smile :How about we play something?: normal",
        "Alan: Flat :Huh? Where are you?: normal",
        "Cindy: Smile :Behind one of the trees.: normal",
        "Cindy: Flat :Try to find me... That's the game.: normal",
        "Cindy: Smile :Now go, don't keep a girl waiting.: normal",
        "Alan: Flat :This girl… I don't know what she's up to.: bold_italic",
        "Alan: Smile :I just want to follow her game.: bold_italic",
        "Cindy: Flat :Eh, I'll reward you if you can find me quickly enough.: normal"
    };

    public string[] dialogueWrongTree = 
    {
        "Alan: Flat :She's not here…: normal"
    };

    public string[] dialogueRound2MiniGameTree =
    {
        "Cindy: Smile :Ahahahaha:normal",
        "Cindy: Flat :Took you to long to find me.:normal",
        "Cindy: Flat :But it's fun…:normal",
        "Cindy: Smile :Let's do it one more time.:normal",
        "Cindy: Smile :Close your eyes, Al. No cheating.:normal",
        "Alan: Flat :This is so childish and silly…:normal",
        "Alan: Smile :But it's not the fun I seek, I just wanna be with someone.:normal"
    };

    public string[] dialogueFoundCindyRound2 =
    {
        "Alan: Flat :Cindy? I know you're here.:normal",
        "Alan: Flat :…:normal"
    };

    public string[] dialogueCindyOnTop = 
    {
        "Cindy: Smile :AH!!!!:normal",
        "Alan: Flat :Is this the reward?:normal",
        "Cindy: Sad :No no no… you st- stupid. Perv-:normal",
        "Alan: Smile :Ahahahahaha:normal",
        "Alan: Flat :Cindy… Thanks…:normal",
        "Cindy: Sad :Ff- for waht??:normal",
        "Alan: Smile :I feel like I want to give it a try again after I know you…:normal",
        "Cindy: Sad :Try again for what?:normal",
        "Alan: Flat :To live:normal",
        "Alan: Smile :I bounced back thanks to you…:normal",
        "Alan: Flat :You told me your stories, now let me tell you mine.normal",
        "Cindy: Sad :Oww… fair enough.:normal",
        "Alan: Smile :But beforehand, could you please stand up?:normal",
        "Cindy: Smile :!!!:normal",
        "Alan: Sad :To be honest, I'd be lying if I don't like this moment.:normal",
    };

    public string[] thoughtsAfterMiniGameTree =
    {
        "I tell her my stories.",
        "Everything I need to share with someone.",
        "I realized I only need somebody to lean on for now.",
        "I feel so stupid for what I did before.",
        "And now, this night may come to an end.",
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
