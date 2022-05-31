using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    [SerializeField] private Notification inputCheck = null;
    [SerializeField] private Notification inputInventory = null;
    [SerializeField] private Notification inputCancel = null;

    [SerializeField] private PlayerMovement player = null;
    public GenericAbility[] abilities;
    private int current = 0;

    public void SwitchSkill(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        current = (current + 1 < abilities.Length) ? current + 1 : 0;
        // player.Ability = abilities[current];
    }
    // private PlayerInput _input;
    // private bool dialogMap = false;


    public void OnMove(InputAction.CallbackContext context) {
        player.TempMovement = context.ReadValue<Vector2>();
        // Debug.Log(player.TempMovement);
    }
    public void OnClickMove(InputAction.CallbackContext context) {
        Vector2 movement = context.ReadValue<Vector2>();
        //player.TempMovement = context.ReadValue<Vector2>();

        //Movement.Normalize();
        //player.rigidbody.MovePosition(player.position + movement * 0.1f);
    }

    public void OnAttack(InputAction.CallbackContext context) {
        player.Attacking();
    }
    public void OnAbility(InputAction.CallbackContext context) {
        // player.UseAbility();
    }
    public void OnCheck(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        // Debug.Log("Check!");
        if (!player.IsReceiveItem) {
            inputCheck.Raise();
            // ChangeActiveMap("Dialog");
        } else {
        }
    }
    public void OnContinue(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }
        inputCheck.Raise();
    }
    public void OnInventory(InputAction.CallbackContext context) {
        inputInventory.Raise();
        // ChangeActiveMap("Inventory");
    }
    public void OnPause(InputAction.CallbackContext context) {
        inputCancel.Raise();
    }

    // public void SwitchToPlayerMap() {
    //     DisableDialog();
    //     EnablePlayer();
    // }
    // public void SwitchToDialogMap() {
    //         DisablePlayer();
    //     // if (!dialogMap) {
    //     //     EnableDialog();
    //     //     dialogMap = true;
    //     // } else {
    //     //     SwitchToPlayerMap();
    //     //     dialogMap = false;
    //     // }
    // }
    // public void SwitchToInventoryMap() {
    //     DisablePlayer();
    //     EnableInventory();
    // }

    // public void Awake() {
    //     _input = new PlayerInput();
    //     // PlayerInput.SwitchCurrentActionMap("Player");
    //     // EnablePlayer();
    // }

    // void OnEnable() => _input.Enable();
    // void OnDisable() => _input.Disable();

    // void Update() {
    //     // player.TempMovement = _input.Player.Move.ReadValue<Vector2>();
    //     // player.Motion(direction);
    //     // if (direction.magnitude > 0) {
    //     //     player.Walking(direction);
    //     // } else {
    //     //     player.Idling();
    //     // }
    // }


    // void EnablePlayer() {
    //     Debug.Log(_input.Player);
    //     _input.Player.Check.performed += context => OnCheck(context);
    //     _input.Player.Attack.performed += context => OnAttack(context);
    //     _input.Player.Ability.performed += context => OnAbility(context);
    // }
    // void DisablePlayer() {
    //     _input.Player.Check.performed -= context => OnCheck(context);
    //     _input.Player.Attack.performed -= context => OnAttack(context);
    //     _input.Player.Ability.performed -= context => OnAbility(context);
    // }

    // void EnableDialog() {
    //     _input.Dialog.Continue.performed += context => OnContinue(context);
    // }
    // void DisableDialog() {
    //     _input.Dialog.Continue.performed -= context => OnContinue(context);
    // }

    // void EnableInventory() {
    //     _input.UI.Submit.performed += context => OnContinue(context);
    // }
    // void DisableInventory() {
    //     _input.UI.Submit.performed -= context => OnContinue(context);
    // }
}
