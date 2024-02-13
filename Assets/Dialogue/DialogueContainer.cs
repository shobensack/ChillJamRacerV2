using Assets.Dialogue;
using System.Collections;
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
    [SerializeField]
    private Animation _dialogueBoxContainerShowAnimation;

    private bool _dialogueBoxShown = false;

    void Start()
    {
        HideOptions();
        HideDialogueBox();
    }

    private void Update()
    {
        
    }

    public bool IsDialogueBoxShown() 
    { 
        return _dialogueBoxShown;
    }

    public void TriggerShowDialogueBox()
    {
        this.enabled = true;
        //_dialogueBoxContainerShowAnimation.Play();
    }

    public void HideDialogueBox()
    {
        this.enabled = false;
        _dialogueBoxShown = false;
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

        // todo show image
        _characterNameContainer.HideName();
        _playerNameContainer.TriggerShowName(message.Name, null);

        _dialogueBoxContainer.SetText(message.Text);

        if (message.Options != null)
        {
            // should just loop but whatever
            if (message.Options[0] != null) _dialogueOptions[0].ShowOption(message.Options[0].Text, message.Options[0].Goto);
            if (message.Options[1] != null) _dialogueOptions[1].ShowOption(message.Options[1].Text, message.Options[1].Goto);
            if (message.Options[2] != null) _dialogueOptions[2].ShowOption(message.Options[2].Text, message.Options[2].Goto);
        }
    }

    public void ShowCharacterDialogue(Message message, Texture characterTexture = null)
    {
        HideOptions();

        // todo show image
        _playerNameContainer.HideName();
        _characterNameContainer.TriggerShowName(message.Name, null);
        
        _dialogueBoxContainer.SetText(message.Text);

    }

    private float GetRenderSpeedTime(string renderSpeedStr)
    {
        switch (renderSpeedStr)
        {
            case "default": return 0.05f;
            case "fast": return 0.5f;
            case "slow": return 0.005f;
            default: return 0.01f;
        }
    }

    // EVEN HANDLER DONT DELETE
    private void ContainerShownEventHandler()
    {
        _dialogueBoxShown = true;
    }
}
