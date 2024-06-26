using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public CharacterDialogue[] characterDialogues;
    public Image characterImage; // Reference to the UI image object for character sprite
    public SceneManagement sceneManager;

    private int currentCharacterIndex = 0;
    private int currentSentenceIndex = 0;
    private bool isDialoguePlaying = false;

    void Update()
    {
        if (QuitMenuController.isMenuOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDialoguePlaying)
            {
                StartDialogueForCurrentCharacter();
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    void DisplayNextSentence()
    {
        currentSentenceIndex++;
        if (currentSentenceIndex < characterDialogues[currentCharacterIndex].sentences.Length)
        {
            dialogueText.text = characterDialogues[currentCharacterIndex].sentences[currentSentenceIndex];
        }
        else
        {
            // Mark the current character's dialogue as finished
            isDialoguePlaying = false;
            // Hide the text box
            animator.SetBool("IsOpen", false);
            // Move to the next character's dialogue
            currentCharacterIndex++;
            currentSentenceIndex = 0; // Reset sentence index for the next character
        }
    }

    void StartDialogueForCurrentCharacter()
    {
        if (currentCharacterIndex < characterDialogues.Length)
        {
            // Show the text box
            animator.SetBool("IsOpen", true);
            // Start dialogue for the current character
            isDialoguePlaying = true;
            nameText.text = characterDialogues[currentCharacterIndex].name;
            dialogueText.text = characterDialogues[currentCharacterIndex].sentences[currentSentenceIndex];

            // Set the character's sprite
            if (currentCharacterIndex < characterDialogues.Length)
            {
                characterImage.sprite = characterDialogues[currentCharacterIndex].characterSprite;
            }
            else
            {
                // Debug.LogWarning("No character sprite assigned for the current character.");
            }
        }
        else
        {
            sceneManager.LoadNextScene();
        }
    }
}



[System.Serializable]
public class CharacterDialogue
{
    public string name;
    public Sprite characterSprite;
    [TextArea(3,10)]
    public string[] sentences;
}
