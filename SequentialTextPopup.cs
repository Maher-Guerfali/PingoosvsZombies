using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TypewriterTextPopup : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typewriterSpeed = 0.05f;
    public float displayDuration = 0.4f;

    private List<string[]> textScenarios = new List<string[]>
    {
         new string[] {
        "Psst... penguin buddy!",
        "Great job breaking the score!",
        "Bad news, Agent Smith cranked drone speeds to 'annoying' mode!"
    },
    
    // Zombie evolution
    new string[] {
        "Congratulations! ...kinda!",
        "New high score unlocked!",
        "Zombie Pingoos just discovered coffee - they're ZOOMING now!"
    },
    
    // Environmental threats
    new string[] {
        "Hey fish-breath!",
        "You’re crushing the leaderboards!",
        "The Matrix spawned turbo orcas - they do NOT want belly rubs!"
    },

    new string[] {
        "Ice to see you winning!",
        "New personal best achieved!",
        "Wood barriers grew spikes. Spikes with SPIKES. Good luck!"
    },
    
    // Comedic warnings
    new string[] {
        "Alert: You’re too good!",
        "Resistance HQ applauds your score!",
        "Agent Smith rage-bought laser sharks. Yes, LASER sharks."
    },

    new string[] {
        "*Penguin Yodel*",
        "Mountain peaks bow to your score!",
        "Zombies hired cheetah trainers - they’re ridiculous now!"
    },

    new string[] {
        "Stop making it look easy!",
        "High score record smashed!",
        "The Matrix added ricochet physics to icebergs. Because why not?"
    },

    new string[] {
        "News Flash!",
        "You’re officially a glacier legend!",
        "Frost orcas now come with jetpacks. We don’t get it either."
    },
        new string[] {
        "Breaking News!",
        "You've ice-olated the competition!",
        "Agent Smith deployed disco-ball drones - they're dangerous!"
    },
    new string[] {
        "Waddle of Fame!",
        "Your score melted our metrics!",
        "Zombies found roller skates - they're doing the conga at Mach 5!"
    },
    new string[] {
        "Frostbite Report!",
        "Leaderboards just got frostier!",
        "Matrix hacked snowmen - they throw EXPLODING carrots now!"
    },
    new string[] {
        "Fishy Business!",
        "New high score detected!",
        "Drone orcas brought friends - say hello to laser narwhals!"
    },
    new string[] {
        "Avalanche Alert!",
        "You're crushing glacial records!",
        "Ice bridges grew performance anxiety - they're collapsing faster!"
    },
    new string[] {
        "Slush Puppy!",
        "Score hotter than lava!",
        "Zombie Pingoos mastered teleportation - don't blink!"
    },
    new string[] {
        "Ice Ice Baby!",
        "Personal best shattered!",
        "Agent Smith installed banana peels on slopes. Classic villain move!"
    },
    new string[] {
        "Penguin Prodigy!",
        "Resistance HQ is cheering!",
        "Matrix added reverse gravity zones - hold onto your fish!"
    },
    new string[] {
        "Cold Hard Facts!",
        "You're dominating the frost charts!",
        "Snow trees learned karate - they're doing roundhouse kicks!"
    },
    new string[] {
        "Blizzard Wizard!",
        "Score bursting the thermometer!",
        "Zombies rented jetpacks - they're failing upwards... fast!"
    }
    };



    private Queue<string[]> messageQueue = new Queue<string[]>();
    private List<string[]> usedMessages = new List<string[]>();

    private void OnEnable()
    {
        InitializeQueue();
        ShowNextScenario();
    }

    private void InitializeQueue()
    {
        // Refresh queue when empty
        if (messageQueue.Count == 0)
        {
            // Add all messages to temporary list
            var tempList = new List<string[]>(textScenarios);

            // Remove already used messages
            tempList.RemoveAll(m => usedMessages.Contains(m));

            // If no new messages left, reset tracker
            if (tempList.Count == 0)
            {
                usedMessages.Clear();
                tempList = new List<string[]>(textScenarios);
            }

            // Shuffle remaining messages
            var shuffled = tempList.OrderBy(x => Random.value).ToList();

            // Enqueue one random message to potentially repeat
            if (usedMessages.Count > 0)
            {
                var repeatMessage = usedMessages[Random.Range(0, usedMessages.Count)];
                shuffled.Insert(Random.Range(0, 2), repeatMessage);
            }

            foreach (var message in shuffled)
            {
                messageQueue.Enqueue(message);
            }
        }
    }

    private void ShowNextScenario()
    {
        if (messageQueue.Count > 0)
        {
            var nextMessage = messageQueue.Dequeue();

            if (!usedMessages.Contains(nextMessage))
            {
                usedMessages.Add(nextMessage);
            }

            StartCoroutine(DisplayTextSequence(nextMessage));
        }
    }

    private IEnumerator DisplayTextSequence(string[] textParts)
    {
        // Existing display logic...
        for (int i = 0; i < textParts.Length; i++)
        {
            string part = textParts[i];
            textMeshPro.text = "";

            for (int j = 0; j <= part.Length; j++)
            {
                textMeshPro.text = part.Substring(0, j);
                yield return new WaitForSeconds(typewriterSpeed);
            }

            yield return new WaitForSeconds(displayDuration);
        }

        gameObject.SetActive(false);
    }
}
