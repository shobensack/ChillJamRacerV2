using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private Dictionary<int, Message> _script = new Dictionary<int, Message>();

        private bool _dialogueStarted = false;
        private Message _currentScriptDialogue = null;
        private int _currentDialogueIndex = 1;
        private bool _waitingOnInput = false;

        private readonly string _playerScriptName = "Player";
        private readonly string _noneScriptName = "None";

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
            if (string.IsNullOrWhiteSpace(_dialogueScriptPath))
                throw new System.Exception("Missing dialogue script path!");

            ParseScript();

            if (_script == null || _script.Count == 0)
                throw new System.Exception("Script is empty!");

            // This can/should be triggered else where
            this.StartDialogue();
        }

        public void StartDialogue()
        {
            _currentDialogueIndex = _script?.FirstOrDefault().Key ?? 1;
            _currentScriptDialogue = _script[_currentDialogueIndex];

            _dialogueContainer.TriggerShowDialogueBox();

            _dialogueStarted = true;
        }

        void Update()
        {
            // if dialogue hasn't started, do nothing
            if (!_dialogueStarted)
                return;

            // check if dialogue finished
            if (_currentDialogueIndex == -1)
            {
                _dialogueStarted = false;
                EndDialogue();
                return;
            }
            else
            {
                if (!_waitingOnInput)
                    ShowNextScriptItem();
                else
                    HandleInput();
            }

        }

        private void ParseScript()
        {
            var scriptFileContents = File.ReadAllText(_dialogueScriptPath);
            _script = JsonConvert.DeserializeObject<Dictionary<int, Message>>(scriptFileContents);

            //DebugScript();
        }

        private void ShowNextScriptItem()
        {
            if (_currentScriptDialogue.Name == _playerScriptName)
                _dialogueContainer.ShowPlayerDialogue(_currentScriptDialogue);
            else
                _dialogueContainer.ShowCharacterDialogue(_currentScriptDialogue);

            _waitingOnInput = true;
        }

        private void HandleInput()
        {
            if (!_dialogueStarted) return;

            // waiting on input, if we get input, show next item in script
            var scriptItemHasOptions = _currentScriptDialogue.Options?.Count > 0;

            if (!scriptItemHasOptions)
            {
                // just continue script to next item
                if (Input.anyKeyDown)
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

        private void EndDialogue()
        {
            _dialogueStarted = false;
            LoadNextScene();
        }

        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextSceneIndex);
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

