using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum TInputKeyType
{
    Down,
    Up
}

public class DesktopControl
{
    public bool isJump;

    public Action<TInputKeyType> Jump;

    public bool isAround;

    public Action<TInputKeyType> Around;

    public Vector2 movement;

    public Vector2 rotate;

    public float scroll;

    public void OnJump(InputAction.CallbackContext ctx)
    {
        isJump = ctx.performed;
        Jump?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    public void OnAround(InputAction.CallbackContext ctx)
    {
        isAround = ctx.performed;
        Around?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }
}

public class MobileControl
{
    public bool isDrag;

    public Action<PointerEventData> Drag;

    /// <summary>
    /// 做力处理
    /// </summary>
    public Vector2 forceMovement;

    /// <summary>
    /// 不做力处理
    /// </summary>
    public Vector2 movement;
}

public class TInputManager : TSingleton<TInputManager>
{
    private TInputActions inputActions;

    public DesktopControl dc = new DesktopControl();

    public MobileControl mc = new MobileControl();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitActions();
        BindActions();
    }

    private void Update()
    {
        ReadValues();
    }

    private void ReadValues()
    {
        dc.movement = inputActions.General.Movement.ReadValue<Vector2>();
        dc.rotate = inputActions.General.Rotate.ReadValue<Vector2>();
        dc.scroll = inputActions.General.Scroll.ReadValue<float>();
    }

    private void InitActions()
    {
        inputActions = new TInputActions();
        inputActions.General.Enable();
    }

    private void BindActions()
    {
        inputActions.General.Jump.performed += dc.OnJump;
        inputActions.General.Jump.canceled += dc.OnJump;
        inputActions.General.Around.performed += dc.OnAround;
        inputActions.General.Around.canceled += dc.OnAround;
    }
}
