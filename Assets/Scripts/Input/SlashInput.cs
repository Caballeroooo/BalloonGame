//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/SlashInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @SlashInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @SlashInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SlashInput"",
    ""maps"": [
        {
            ""name"": ""Slash"",
            ""id"": ""fa99a7b5-2dcf-41f6-98a7-d181a243c605"",
            ""actions"": [
                {
                    ""name"": ""SlashPress"",
                    ""type"": ""Button"",
                    ""id"": ""30089af9-558c-46dd-a60e-8d4920d63402"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SlashRelease"",
                    ""type"": ""Button"",
                    ""id"": ""10b8f8e9-c599-4081-995f-049181efcfcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SlashPosition"",
                    ""type"": ""Value"",
                    ""id"": ""af399c9f-f8ca-404e-9dab-18502a018f1c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a6ac115c-6bd3-42df-aaed-01a813c7e185"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""SlashPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1749e5e-2f59-48a4-939e-3379701bf744"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""SlashPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16abd2e0-b245-4475-aa89-a929f690e72d"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""SlashPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5055062e-b6d5-4557-b9a8-be2068331a7c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""SlashPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6fbf7d2-4be3-4971-8559-63182be7a738"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""SlashRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""932d8455-6458-4e55-b39c-981318e72eca"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""SlashRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Slash
        m_Slash = asset.FindActionMap("Slash", throwIfNotFound: true);
        m_Slash_SlashPress = m_Slash.FindAction("SlashPress", throwIfNotFound: true);
        m_Slash_SlashRelease = m_Slash.FindAction("SlashRelease", throwIfNotFound: true);
        m_Slash_SlashPosition = m_Slash.FindAction("SlashPosition", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Slash
    private readonly InputActionMap m_Slash;
    private List<ISlashActions> m_SlashActionsCallbackInterfaces = new List<ISlashActions>();
    private readonly InputAction m_Slash_SlashPress;
    private readonly InputAction m_Slash_SlashRelease;
    private readonly InputAction m_Slash_SlashPosition;
    public struct SlashActions
    {
        private @SlashInput m_Wrapper;
        public SlashActions(@SlashInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SlashPress => m_Wrapper.m_Slash_SlashPress;
        public InputAction @SlashRelease => m_Wrapper.m_Slash_SlashRelease;
        public InputAction @SlashPosition => m_Wrapper.m_Slash_SlashPosition;
        public InputActionMap Get() { return m_Wrapper.m_Slash; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SlashActions set) { return set.Get(); }
        public void AddCallbacks(ISlashActions instance)
        {
            if (instance == null || m_Wrapper.m_SlashActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SlashActionsCallbackInterfaces.Add(instance);
            @SlashPress.started += instance.OnSlashPress;
            @SlashPress.performed += instance.OnSlashPress;
            @SlashPress.canceled += instance.OnSlashPress;
            @SlashRelease.started += instance.OnSlashRelease;
            @SlashRelease.performed += instance.OnSlashRelease;
            @SlashRelease.canceled += instance.OnSlashRelease;
            @SlashPosition.started += instance.OnSlashPosition;
            @SlashPosition.performed += instance.OnSlashPosition;
            @SlashPosition.canceled += instance.OnSlashPosition;
        }

        private void UnregisterCallbacks(ISlashActions instance)
        {
            @SlashPress.started -= instance.OnSlashPress;
            @SlashPress.performed -= instance.OnSlashPress;
            @SlashPress.canceled -= instance.OnSlashPress;
            @SlashRelease.started -= instance.OnSlashRelease;
            @SlashRelease.performed -= instance.OnSlashRelease;
            @SlashRelease.canceled -= instance.OnSlashRelease;
            @SlashPosition.started -= instance.OnSlashPosition;
            @SlashPosition.performed -= instance.OnSlashPosition;
            @SlashPosition.canceled -= instance.OnSlashPosition;
        }

        public void RemoveCallbacks(ISlashActions instance)
        {
            if (m_Wrapper.m_SlashActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISlashActions instance)
        {
            foreach (var item in m_Wrapper.m_SlashActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SlashActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SlashActions @Slash => new SlashActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_TouchscreenSchemeIndex = -1;
    public InputControlScheme TouchscreenScheme
    {
        get
        {
            if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
            return asset.controlSchemes[m_TouchscreenSchemeIndex];
        }
    }
    public interface ISlashActions
    {
        void OnSlashPress(InputAction.CallbackContext context);
        void OnSlashRelease(InputAction.CallbackContext context);
        void OnSlashPosition(InputAction.CallbackContext context);
    }
}
