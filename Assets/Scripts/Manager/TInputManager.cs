using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum TInputKeyType
{
    Down,
    Up
}

public class MouseControl
{
    public bool isJump;

    public Action<TInputKeyType> Jump;

    public bool isAround;

    public Action<TInputKeyType> Around;

    public Vector2 movement;

    public Vector2 rotate;

    public float scroll;
}

public class TouchControl
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

    public MouseControl mc = new MouseControl();

    public TouchControl tc = new TouchControl();

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
        mc.movement = inputActions.General.Movement.ReadValue<Vector2>();
        mc.rotate = inputActions.General.Rotate.ReadValue<Vector2>();
        mc.scroll = inputActions.General.Scroll.ReadValue<float>();
    }

    private void InitActions()
    {
        inputActions = new TInputActions();
        inputActions.General.Enable();
    }

    private void BindActions()
    {
        inputActions.General.Jump.performed += OnJump;
        inputActions.General.Jump.canceled += OnJump;
        inputActions.General.Around.performed += OnAround;
        inputActions.General.Around.canceled += OnAround;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        mc.isJump = ctx.performed;
        mc.Jump?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void OnAround(InputAction.CallbackContext ctx)
    {
        mc.isAround = ctx.performed;
        mc.Around?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }
}
