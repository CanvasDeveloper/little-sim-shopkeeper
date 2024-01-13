using CanvasDEV.Runtime.Systems.DialogSystem.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.UI
{
    public class UIDialogManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogPanel;

        [SerializeField] private TextMeshProUGUI nameGUI;
        [SerializeField] private TextMeshProUGUI textGUI;
        [SerializeField] private UIDialogButton buttonPrefab;

        [SerializeField] private Transform parent;

        private List<UIDialogButton> instances = new();
        private List<DialogTrigger> triggers = new();

        private DialogTrigger _optionTrigger;

        private void Start()
        {
            dialogPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            foreach (var trigger in triggers)
            {
                trigger.OnUpdateGlobalDialogText -= DialogTrigger_OnUpdateGlobalDialogText;
                trigger.OnRequestOptions -= Trigger_OnRequestOptions;
                trigger.OnDialogFinished -= Trigger_OnDialogFinished;
                trigger.OnDialogStart -= Trigger_OnDialogStart;
            }

            triggers.Clear();

            if (instances.Count > 0)
            {
                DestroyButtons();
            }
        }

        public void RegisterTrigger(DialogTrigger trigger)
        {
            triggers.Add(trigger);

            trigger.OnUpdateGlobalDialogText += DialogTrigger_OnUpdateGlobalDialogText;
            trigger.OnRequestOptions += Trigger_OnRequestOptions;
            trigger.OnDialogFinished += Trigger_OnDialogFinished;
            trigger.OnDialogStart += Trigger_OnDialogStart;
        }

        public void UnRegisterTrigger(DialogTrigger trigger)
        {
            triggers.Remove(trigger);

            trigger.OnUpdateGlobalDialogText -= DialogTrigger_OnUpdateGlobalDialogText;
            trigger.OnRequestOptions -= Trigger_OnRequestOptions;
            trigger.OnDialogFinished -= Trigger_OnDialogFinished;
            trigger.OnDialogStart -= Trigger_OnDialogStart;
        }

        private void Trigger_OnDialogStart()
        {
            dialogPanel.SetActive(true);
        }

        private void Trigger_OnDialogFinished(bool obj)
        {
            dialogPanel.SetActive(false);
        }

        private void Trigger_OnRequestOptions(DialogTrigger trigger, Dialogs[] options)
        {
            _optionTrigger = trigger;

            for (int i = 0; i < options.Length; i++)
            {
                var instance = Instantiate(buttonPrefab, parent);

                instance.SetOption(options[i].optionTitle, i);

                instance.OnButtonClicked += Instance_OnButtonClicked;

                instances.Add(instance);
            }
        }

        private void Instance_OnButtonClicked(int index)
        {
            DestroyButtons();

            _optionTrigger.SetOption(index);
        }

        private void DestroyButtons()
        {
            foreach (var instance in instances)
            {
                instance.OnButtonClicked -= Instance_OnButtonClicked;
            }

            foreach (var instance in instances)
            {
                Destroy(instance.gameObject);
            }

            instances.Clear();
        }

        private void DialogTrigger_OnUpdateGlobalDialogText(string name, string message)
        {
            nameGUI.text = name;
            textGUI.text = message;
        }
    }
}