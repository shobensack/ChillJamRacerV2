using Assets.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerNameContainer;
    [SerializeField]
    private GameObject _otherNameContainer;

    [SerializeField]
    private GameObject _dialogueBox;
    [SerializeField]
    private GameObject[] _options;

    private RawImage _playerImage;
    private RawImage _characterImage;

    private TextMeshPro _playerName;
    private TextMeshPro _characterName;

    private TextMeshPro _dialogueText;

    void Start()
    {
        HideOptions();
    }

    private void SetPlayerName(string str) { _playerName.text = str; }
    private void SetCharacterName(string str) { _characterName.text = str; }
    private void SetPlayerImage(Texture texture) { _playerImage.texture = texture; }
    private void SetCharacterImage(Texture texture) {_characterImage.texture = texture; }
    private void SetDialogueText(string str) { _dialogueText.text = str;}

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
        foreach (var option in _options)
        {
            option.SetActive(false);
        }
    }

    private void ShowOption(GameObject optionObj, string str)
    {
        var optionText = optionObj.GetComponentsInChildren<TextMeshPro>()?[0];

        if (optionText == null)
            throw new System.Exception("No option found!");

        optionText.text = str;
        optionObj.SetActive(true);
    }

    public void ShowPlayerDialogue(string str, Option[] options = null)
    {
        HideOptions();
        SetDialogueText(str);

        if (options != null)
        {
            // should just loop but whatever
            if (options[0] != null) ShowOption(_options[0], options[0].Text);
            if (options[1] != null) ShowOption(_options[1], options[1].Text);
            if (options[2] != null) ShowOption(_options[2], options[2].Text);
        }

        _otherNameContainer.SetActive(false);
        _playerNameContainer.SetActive(true);
    }

    public void ShowCharacterDialogue(string name, string str, Texture characterTexture = null)
    {
        HideOptions();

        if (characterTexture != null)
        {
            SetCharacterImage(characterTexture);
        }

        SetCharacterName(name);
        SetDialogueText(str);
        _playerNameContainer.SetActive(false);
        _otherNameContainer.SetActive(true);
    }
}
