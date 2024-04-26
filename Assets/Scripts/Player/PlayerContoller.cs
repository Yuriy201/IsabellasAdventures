using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
    public GameObject InventoryPanel;

    bool IsUI = false;

    InputSettings InputActions;
    CharacterPlayer character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterPlayer>();
        character.Controller = this;

        InputActions = new InputSettings();
        InputActions.UI.OpenInventory.performed += Key_OpenInventory;
        InputActions.Gameplay.Jump.performed += Jump_performed;
        InputActions.Enable();
    }

    private void Key_OpenInventory(InputAction.CallbackContext context)
    {
        OpenInventory();
    }

    public void OpenInventory()
    {
        IsUI = !IsUI;

        if (IsUI)
        {
            InputActions.Gameplay.Disable();
            InventoryPanel.SetActive(true);
        }
        else
        {
            InputActions.Gameplay.Enable();
            InventoryPanel.SetActive(false);
        }
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        character.Jump();
    }

    private void FixedUpdate()
    {
        character.InputMove(InputActions.Gameplay.Movement.ReadValue<float>());
    }

}
