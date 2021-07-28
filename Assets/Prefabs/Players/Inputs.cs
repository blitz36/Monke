// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/Players/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Base"",
            ""id"": ""e7041230-ce64-476e-ad16-a95b9768f020"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9a4aac59-5816-4434-aa0e-e6073e1370c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dashing"",
                    ""type"": ""PassThrough"",
                    ""id"": ""02e28a0b-a066-4f80-9ec4-ea0c3c05b332"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""453b01c9-e30d-4b0e-b09f-909934a3c167"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1c99af77-4592-4eb5-898d-f6846598d076"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=0.1,pressPoint=0.1)""
                },
                {
                    ""name"": ""Equip"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cbcbc7bf-1990-49fb-bf49-615923161e6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c290796f-1af6-4b66-8cf2-82de7b6fe57e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89bddfdd-b796-4ddd-8b56-f5fe431ece1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cc203ff1-e975-443f-8e86-4594a77a5761"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b5a0744d-4d97-4524-8488-5689ea0790aa"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""78e7a72d-aab6-4c27-b440-b49393b77f8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=0.35)""
                },
                {
                    ""name"": ""RotateCamLeft"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00f54bd0-f2c5-4643-b466-42790daa27c8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCamRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b8b4e0db-06ce-4234-aaa3-11facc7a1c07"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ce9fe01c-5b52-4b2b-95db-7d78fd7288e3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a03800be-ccdf-43c2-ab61-19011f952daf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bba36c8c-1ace-4625-873a-06b4c7e22c84"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f3014e69-c65c-4fa7-b7ce-f22e99278e48"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""652be4ca-8a49-40cf-a3a3-7029234bc559"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""34a377eb-6e1b-4b20-8131-3344ad443b8e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dashing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3870de4b-a5b8-4946-a520-283b9ab5fc89"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a90690c9-2815-4eec-9d94-bc227fbaef3f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df7fb816-ac67-4ce2-a066-f4bebddc5f21"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2b873b0-b380-48a5-9613-e0b961e4b929"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6029037f-8d3c-487b-b200-6199b6ffce98"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3670e904-a723-4166-8e03-396bc3219f25"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1118389-524e-49e7-96cb-8d0f3060a5c8"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ff09d2a-dca3-411d-99d7-0cca6f2cd252"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf5bbf7e-9efc-4ee2-9226-b23d1c905761"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a95c7e24-1d06-4991-8e1c-4ab350bbb625"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Base
        m_Base = asset.FindActionMap("Base", throwIfNotFound: true);
        m_Base_Move = m_Base.FindAction("Move", throwIfNotFound: true);
        m_Base_Dashing = m_Base.FindAction("Dashing", throwIfNotFound: true);
        m_Base_Attack = m_Base.FindAction("Attack", throwIfNotFound: true);
        m_Base_Block = m_Base.FindAction("Block", throwIfNotFound: true);
        m_Base_Equip = m_Base.FindAction("Equip", throwIfNotFound: true);
        m_Base_MousePosition = m_Base.FindAction("MousePosition", throwIfNotFound: true);
        m_Base_Interact = m_Base.FindAction("Interact", throwIfNotFound: true);
        m_Base_Escape = m_Base.FindAction("Escape", throwIfNotFound: true);
        m_Base_Zoom = m_Base.FindAction("Zoom", throwIfNotFound: true);
        m_Base_HeavyAttack = m_Base.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Base_RotateCamLeft = m_Base.FindAction("RotateCamLeft", throwIfNotFound: true);
        m_Base_RotateCamRight = m_Base.FindAction("RotateCamRight", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Base
    private readonly InputActionMap m_Base;
    private IBaseActions m_BaseActionsCallbackInterface;
    private readonly InputAction m_Base_Move;
    private readonly InputAction m_Base_Dashing;
    private readonly InputAction m_Base_Attack;
    private readonly InputAction m_Base_Block;
    private readonly InputAction m_Base_Equip;
    private readonly InputAction m_Base_MousePosition;
    private readonly InputAction m_Base_Interact;
    private readonly InputAction m_Base_Escape;
    private readonly InputAction m_Base_Zoom;
    private readonly InputAction m_Base_HeavyAttack;
    private readonly InputAction m_Base_RotateCamLeft;
    private readonly InputAction m_Base_RotateCamRight;
    public struct BaseActions
    {
        private @Inputs m_Wrapper;
        public BaseActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Base_Move;
        public InputAction @Dashing => m_Wrapper.m_Base_Dashing;
        public InputAction @Attack => m_Wrapper.m_Base_Attack;
        public InputAction @Block => m_Wrapper.m_Base_Block;
        public InputAction @Equip => m_Wrapper.m_Base_Equip;
        public InputAction @MousePosition => m_Wrapper.m_Base_MousePosition;
        public InputAction @Interact => m_Wrapper.m_Base_Interact;
        public InputAction @Escape => m_Wrapper.m_Base_Escape;
        public InputAction @Zoom => m_Wrapper.m_Base_Zoom;
        public InputAction @HeavyAttack => m_Wrapper.m_Base_HeavyAttack;
        public InputAction @RotateCamLeft => m_Wrapper.m_Base_RotateCamLeft;
        public InputAction @RotateCamRight => m_Wrapper.m_Base_RotateCamRight;
        public InputActionMap Get() { return m_Wrapper.m_Base; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseActions set) { return set.Get(); }
        public void SetCallbacks(IBaseActions instance)
        {
            if (m_Wrapper.m_BaseActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                @Dashing.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnDashing;
                @Dashing.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnDashing;
                @Dashing.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnDashing;
                @Attack.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnAttack;
                @Block.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnBlock;
                @Equip.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnEquip;
                @Equip.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnEquip;
                @Equip.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnEquip;
                @MousePosition.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnMousePosition;
                @Interact.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnInteract;
                @Escape.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnEscape;
                @Zoom.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnZoom;
                @HeavyAttack.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnHeavyAttack;
                @RotateCamLeft.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamLeft;
                @RotateCamLeft.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamLeft;
                @RotateCamLeft.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamLeft;
                @RotateCamRight.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamRight;
                @RotateCamRight.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamRight;
                @RotateCamRight.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnRotateCamRight;
            }
            m_Wrapper.m_BaseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dashing.started += instance.OnDashing;
                @Dashing.performed += instance.OnDashing;
                @Dashing.canceled += instance.OnDashing;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Equip.started += instance.OnEquip;
                @Equip.performed += instance.OnEquip;
                @Equip.canceled += instance.OnEquip;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @RotateCamLeft.started += instance.OnRotateCamLeft;
                @RotateCamLeft.performed += instance.OnRotateCamLeft;
                @RotateCamLeft.canceled += instance.OnRotateCamLeft;
                @RotateCamRight.started += instance.OnRotateCamRight;
                @RotateCamRight.performed += instance.OnRotateCamRight;
                @RotateCamRight.canceled += instance.OnRotateCamRight;
            }
        }
    }
    public BaseActions @Base => new BaseActions(this);
    public interface IBaseActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDashing(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnEquip(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnRotateCamLeft(InputAction.CallbackContext context);
        void OnRotateCamRight(InputAction.CallbackContext context);
    }
}
