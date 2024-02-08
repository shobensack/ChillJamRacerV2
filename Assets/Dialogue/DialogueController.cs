using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Dialogue
{
    /// <summary>
    /// Class that loads dialogue script.
    /// 
    /// There will be multiple dialogue scripts, one for each "conversation" after race.
    /// </summary>
    public class DialogueController : MonoBehaviour
    {
        [SerializeField]
        private string _dialogueScriptPath;
        [SerializeField]
        private DialogueContainer _dialogueContainer;

        private Dictionary<int, Message> _script;

        private Message _currentScriptDialogue = null;
        private int _currentDialogueIndex = 1;
        private bool _waitingOnInput = false;

        // Start is called before the first frame update
        void Start()
        {
            if (string.IsNullOrWhiteSpace(_dialogueScriptPath))
                throw new System.Exception("Missing dialogue script path!");

            ParseScript();

            if (_script == null || _script.Count == 0)
                throw new System.Exception("Script is empty!");
        }

        void Update()
        {
            // MORE TEST CODE
            // TODO: SOMETHING HAS TO HAPPEN HERE
            if (_currentDialogueIndex <= 0)
            {
                _dialogueContainer.ShowPrompt("CONVO OVERRRRRRRRR");
                return;
            }

            // no dialogue, start new one
            if (_currentScriptDialogue == null)
            {
                _currentScriptDialogue = _script[_currentDialogueIndex];
            }

            if (!_waitingOnInput)
            {

                // not waiting on input, show next item in script
                var speaker = _currentScriptDialogue.Name;

                if (speaker == "Player")
                {
                    _dialogueContainer.ShowPlayerDialogue(_currentScriptDialogue);
                }
                else
                {
                    _dialogueContainer.ShowCharacterDialogue(_currentScriptDialogue);
                }

                _waitingOnInput = true;
            }
            else
            {
                // waiting on input, if we get input, show next item in script
                var scriptItemHasOptions = _currentScriptDialogue.Options?.Count > 0;

                if (!scriptItemHasOptions)
                {
                    // just continue script to next item
                    if (Input.GetKeyDown("a"))
                    {
                        _currentDialogueIndex = _currentScriptDialogue.Goto;
                        _currentScriptDialogue = _script[_currentDialogueIndex];
                        _waitingOnInput = false;
                    }
                }
                else
                {
                    // if it do, wait for choose input and then move to that script
                    // TODO: this is obviously test code!!!!
                    if (Input.GetKeyDown("1") && _currentScriptDialogue.Options.Count >= 1)
                    {
                        _currentDialogueIndex = _currentScriptDialogue.Options[0].Goto;
                        _currentScriptDialogue = _script[_currentDialogueIndex];
                        _waitingOnInput = false;
                    }

                    if (Input.GetKeyDown("2") && _currentScriptDialogue.Options.Count >= 2)
                    {
                        _currentDialogueIndex = _currentScriptDialogue.Options[1].Goto;
                        _currentScriptDialogue = _script[_currentDialogueIndex];
                        _waitingOnInput = false;
                    }

                    if (Input.GetKeyDown("3") && _currentScriptDialogue.Options.Count >= 3)
                    {
                        _currentDialogueIndex = _currentScriptDialogue.Options[2].Goto;
                        _currentScriptDialogue = _script[_currentDialogueIndex];
                        _waitingOnInput = false;
                    }
                }

            }




        }

        private void ParseScript()
        {
            var scriptFileContents = File.ReadAllText(_dialogueScriptPath);
            _script = JsonConvert.DeserializeObject<Dictionary<int, Message>>(scriptFileContents);

            //DebugScript();
        }

        private void DebugScript()
        {
            if (_script == null || _script.Count == 0)
                return;

            Debug.Log("!! Remember to click into log entry, to see full text output.");

            foreach (var test in _script)
            {
                var str = "";
                str += "\n|-- " + test.Key + ". " + test.Value.Name + " --|\n";
                str += "\"" + test.Value?.Text + "\"\n\n";

                if (test.Value?.Options != null)
                {
                    str += "\n|| Options: ";

                    var optionIndex = 1;
                    foreach (var option in test.Value.Options)
                    {
                        str += optionIndex + ". " + option.Text + " (goes to " + option.Goto + " )\n";
                        optionIndex++;
                    }
                }

                str += "\n";

                Debug.Log(str);
            }
        }
    }
}

