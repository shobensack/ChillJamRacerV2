using Assets.Dialogue;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    [SerializeField]
    private DialogueNameContainer _playerNameContainer;
    [SerializeField]
    private DialogueNameContainer _characterNameContainer;

    [SerializeField]
    private DialogueBoxContainer _dialogueBoxContainer;

    [SerializeField]
    private DialogueOptionContainer[] _dialogueOptions;

    void Start()
    {
        HideOptions();
    }

    public bool IsDialogueBoxShown() { return this.isActiveAndEnabled; }

    public void ShowDialogueBox()
    {
        this.enabled = true;
    }
    public void HideDialogueBox()
    {
        this.enabled = false;
    }

    private void HideOptions()
    {
        foreach (var option in _dialogueOptions)
        {
            option.HideOption();
        }
    }

    public void ShowPrompt(string promptMsg)
    {
        _playerNameContainer.HideName();
        _characterNameContainer.HideName();

        _dialogueBoxContainer.SetText(promptMsg);
    }

    public void ShowPlayerDialogue(Message message)
    {
        HideOptions();

        _dialogueBoxContainer.SetText(message);

        if (message.Options != null)
        {
            // should just loop but whatever
            if (message.Options[0] != null) _dialogueOptions[0].ShowOption(message.Options[0].Text, message.Options[0].Goto);
            if (message.Options[1] != null) _dialogueOptions[1].ShowOption(message.Options[1].Text, message.Options[1].Goto);
            if (message.Options[2] != null) _dialogueOptions[2].ShowOption(message.Options[2].Text, message.Options[2].Goto);
        }

        // todo show image
        _characterNameContainer.HideName();
        _playerNameContainer.ShowName(message.Name, null);
    }

    public void ShowCharacterDialogue(Message message, Texture characterTexture = null)
    {
        HideOptions();

        _dialogueBoxContainer.SetText(message);

        // todo show image
        _playerNameContainer.HideName();
        _characterNameContainer.ShowName(message.Name, null);
    }
}