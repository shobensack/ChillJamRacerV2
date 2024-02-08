using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNameContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private RawImage _nameImage;

    public void ShowName(string name, Texture profileImage = null)
    {
        _nameText.text = name;

        if (profileImage != null)
        {
            _nameImage.texture = profileImage;
        }

        this.gameObject.SetActive(true);
    }

    public void HideName()
    {
        this.gameObject.SetActive(false);
        _nameText.text = string.Empty;
    }
}
