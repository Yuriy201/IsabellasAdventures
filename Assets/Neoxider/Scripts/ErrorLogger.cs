using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NeoxiderUi
{
    public class ErrorLogger : MonoBehaviour
    {
        [Header("Main Settings")]
        public TextMeshProUGUI textMesh;
        public LogType[] logTypesToDisplay = { LogType.Error, LogType.Exception };
        public bool addText = true;
        public bool checkExistingErrors = true;

        public string errorText;

        private List<string> errorList = new List<string>();

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
            textMesh.raycastTarget = false;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (logTypesToDisplay.Length == 0 || Array.Exists(logTypesToDisplay, t => t == type))
            {
                string errorText = type + "\n -- " + logString + "\n -- " + stackTrace + "\n\n";

                if (checkExistingErrors && errorList.Contains(errorText))
                {
                    return;
                }

                errorList.Add(errorText);

                if (addText)
                {
                    AppendText(errorText);
                }
                else
                {
                    UpdateText(errorText);
                }
            }
        }

        public void UpdateText(string newText)
        {
            if (textMesh != null)
            {
                textMesh.text = newText;
            }
        }

        public void AppendText(string additionalText)
        {
            if (textMesh != null)
            {
                textMesh.text += additionalText;
            }
        }
    }
}
