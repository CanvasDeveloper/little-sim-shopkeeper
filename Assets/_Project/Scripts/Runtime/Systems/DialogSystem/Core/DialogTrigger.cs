using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using NaughtyAttributes;
using CanvasDEV.Runtime.Systems.DialogSystem.UI;
using CanvasDEV.Runtime.Systems.DialogSystem.Utilities;
using CanvasDEV.Runtime.Core.Interfaces;
using UnityEngine.Events;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    public struct GlobalDialogUpdate
    {
        public Sprite speakerEmotion;
        public string speakerName;
        public string text;
    }

    public class DialogTrigger : MonoBehaviour
    {
        public event Action<GlobalDialogUpdate> OnUpdateGlobalDialogText;
        public event Action OnDialogStart;
        public event Action<bool> OnDialogFinished;
        public event Action<DialogTrigger, Dialogs[]> OnRequestOptions;

        [SerializeField] private bool useRandomName;

        [ShowIf("useRandomName")]
        [SerializeField] private RandomNameData randomNameData;
        [ShowIf("useRandomName")]
        [SerializeField] private string randomName;

        [HorizontalLine(2, EColor.Green)]
        [SerializeField] private InputActionReference passButtonReference;

        [SerializeField] private UnityEvent onFinishDialog;

        public DialogData dialogData;

        private Dialog[] _targetDialogs;
        private int _currentIndex;

        private int _count;
        private bool _isTypping;
        private bool _isWaitingForOptions;

        private IBlocker _currentBlocker;

        private UIDialogManager _uiDialogManager;

        public bool Started { get; private set; }

        public void UpdateDialogFile(DialogData dialogSystemData)
        {
            dialogData = dialogSystemData;

            _targetDialogs = dialogData.firstTimeDialogs;
        }

        private void Start()
        {
            _currentIndex = 0;

            _uiDialogManager = FindObjectOfType<UIDialogManager>();

            _uiDialogManager.RegisterTrigger(this);

            if (useRandomName)
                randomName = randomNameData.GenerateName();
        }

        private void OnDestroy()
        {
            _uiDialogManager.UnRegisterTrigger(this);
        }

        private void PassText(InputAction.CallbackContext callbackContext)
        {
            PassDialog();
        }

        [Button("Start")]
        public void StartDialog()
        {
            Started = true;

            passButtonReference.action.Enable();
            passButtonReference.action.started += PassText;

            StopAllCoroutines();

            if (_count <= 0)
            {
                _targetDialogs = dialogData.firstTimeDialogs;
            }
            else if (dialogData.secondTimeDialogs.Length > 0)
            {
                _targetDialogs = dialogData.secondTimeDialogs;
            }

            _currentBlocker?.Block();

            StartCoroutine(Typewritter());
            OnDialogStart?.Invoke();
        }


        private IEnumerator Typewritter()
        {
            _isTypping = true;

            var cachedDialog = _targetDialogs[_currentIndex];

            var chars = cachedDialog.message.ToCharArray();

            string phase = "";

            for (int i = 0; i < chars.Length; i++)
            {
                phase += chars[i];

                UpdateText(new GlobalDialogUpdate()
                {
                    speakerEmotion = dialogData.emotionData ? dialogData.emotionData.GetSpriteByEmotion(cachedDialog.emotion) : null,
                    speakerName = cachedDialog.speecherName,
                    text = phase
                });

                yield return new WaitForSeconds(dialogData.timeBetweenChars);
            }

            _isTypping = false;
        }

        [Button("Pass")]
        private void PassDialog()
        {
            if (_isTypping)
            {
                StopAllCoroutines();

                var cachedDialog = _targetDialogs[_currentIndex];
                UpdateText(new GlobalDialogUpdate()
                {
                    speakerEmotion = dialogData.emotionData ? dialogData.emotionData.GetSpriteByEmotion(cachedDialog.emotion) : null,
                    speakerName = cachedDialog.speecherName,
                    text = cachedDialog.message
                });

                _isTypping = false;
                return;
            }

            if (_isWaitingForOptions)
            {
                return;
            }

            if (_targetDialogs[_currentIndex].waitForOptions)
            {
                _isWaitingForOptions = true;

                UpdateText(new GlobalDialogUpdate()
                {
                    speakerEmotion = null,
                    speakerName = "",
                    text = ""
                });

                OnRequestOptions?.Invoke(this, dialogData.options);

                return;
            }

            _targetDialogs[_currentIndex].onFinishMessage?.Invoke();

            _currentIndex++;

            if (_currentIndex >= _targetDialogs.Length)
            {
                FinishDialog();
                return;
            }

            StartCoroutine(Typewritter());
        }

        public void FinishDialog(bool byNatural = true)
        {
            StopAllCoroutines();

            if (dialogData.secondTimeDialogs.Length > 0 && byNatural)
            {
                _count++;
                if (_count >= 100)
                    _count = 100;
            }

            _currentIndex = 0;

            UpdateText(new GlobalDialogUpdate()
            {
                speakerEmotion = null,
                speakerName = "",
                text = ""
            });

            bool allOptionsSelected = true;

            foreach (var dialogs in dialogData.options)
            {
                if (dialogs.requiredOption && !dialogs.hasSelected)
                {
                    allOptionsSelected = false;
                }
            }

            _currentBlocker?.UnBlock();
            _currentBlocker = null;

            onFinishDialog?.Invoke();
            OnDialogFinished?.Invoke(allOptionsSelected);

            passButtonReference.action.started -= PassText;
            passButtonReference.action.Disable();

            Started = false;
        }

        private void UpdateText(GlobalDialogUpdate dialogUpdate)
        {
            if (useRandomName)
            {
                name = randomName;
            }

            OnUpdateGlobalDialogText?.Invoke(dialogUpdate);
        }

        public void SetOption(int index)
        {
            _targetDialogs = dialogData.options[index].dialogs;
            _isWaitingForOptions = false;

            _currentIndex = 0;

            dialogData.options[index].hasSelected = true;

            StartCoroutine(Typewritter());
        }

        public void ForcedSetOption(int index)
        {
            SetOption(index);
        }

        public void RegisterBlocker(IBlocker blocker)
        {
            _currentBlocker = blocker;
        }
    }
}