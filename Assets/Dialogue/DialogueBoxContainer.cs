using Assets.Dialogue;
using TMPro;
using UnityEngine;

public class DialogueBoxContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _dialogueText;

    public void SetText(string text)
    {
        _dialogueText.text = text;
    }

    public void SetText(Message message)
    {
        _dialogueText.text = message.Text;
    }
}
