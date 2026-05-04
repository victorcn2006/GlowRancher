using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

namespace GlowRancher.UI
{
    /// <summary>
    /// This component hooks into <see cref="RebindActionUI.updateBindingUIEvent"/> to replace 
    /// keyboard/mouse binding text with icons.
    /// </summary>
    public class KeyboardIconsExample : MonoBehaviour
    {
        public KeyboardIcons keyboard;
        public MouseIcons mouse;

        protected void OnEnable()
        {
            // Hook into all updateBindingUIEvents on all RebindActionUI components in our hierarchy.
            var rebindUIComponents = transform.GetComponentsInChildren<RebindActionUI>();
            foreach (var component in rebindUIComponents)
            {
                component.updateBindingUIEvent.AddListener(OnUpdateBindingDisplay);
                component.UpdateBindingDisplay();
            }
        }

        protected void OnUpdateBindingDisplay(RebindActionUI component, string bindingDisplayString, string deviceLayoutName, string controlPath)
        {
            if (string.IsNullOrEmpty(deviceLayoutName) || string.IsNullOrEmpty(controlPath))
                return;

            var icon = default(Sprite);
            
            if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Keyboard"))
            {
                icon = keyboard.GetSprite(controlPath);
            }
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Mouse"))
            {
                icon = mouse.GetSprite(controlPath);
            }

            var textComponent = component.bindingText;

            // Grab Image component.
            // Note: The structure is assumed to be the same as the GamepadIconsExample (an "ActionBindingIcon" child of the text's parent)
            var imageGO = textComponent.transform.parent.Find("ActionBindingIcon");
            if (imageGO == null) return;

            var imageComponent = imageGO.GetComponent<Image>();

            if (icon != null)
            {
                textComponent.gameObject.SetActive(false);
                imageComponent.sprite = icon;
                imageComponent.gameObject.SetActive(true);
            }
            else
            {
                textComponent.gameObject.SetActive(true);
                imageComponent.gameObject.SetActive(false);
            }
        }

        [Serializable]
        public struct KeyboardIcons
        {
            [Header("Letters")]
            public Sprite q, w, e, r, t, y, u, i, o, p;
            public Sprite a, s, d, f, g, h, j, k, l;
            public Sprite z, x, c, v, b, n, m;

            [Header("Numbers")]
            public Sprite digit1, digit2, digit3, digit4, digit5, digit6, digit7, digit8, digit9, digit0;

            [Header("Modifiers & Special")]
            public Sprite space;
            public Sprite leftShift, rightShift, leftCtrl, rightCtrl, leftAlt, rightAlt;
            public Sprite tab, escape, enter, backspace, capsLock;

            [Header("Arrows")]
            public Sprite upArrow, downArrow, leftArrow, rightArrow;

            public Sprite GetSprite(string controlPath)
            {
                switch (controlPath)
                {
                    case "q": return q; case "w": return w; case "e": return e; case "r": return r; case "t": return t;
                    case "y": return y; case "u": return u; case "i": return i; case "o": return o; case "p": return p;
                    case "a": return a; case "s": return s; case "d": return d; case "f": return f; case "g": return g;
                    case "h": return h; case "j": return j; case "k": return k; case "l": return l;
                    case "z": return z; case "x": return x; case "c": return c; case "v": return v; case "b": return b;
                    case "n": return n; case "m": return m;

                    case "1": case "digit1": return digit1;
                    case "2": case "digit2": return digit2;
                    case "3": case "digit3": return digit3;
                    case "4": case "digit4": return digit4;
                    case "5": case "digit5": return digit5;
                    case "6": case "digit6": return digit6;
                    case "7": case "digit7": return digit7;
                    case "8": case "digit8": return digit8;
                    case "9": case "digit9": return digit9;
                    case "0": case "digit0": return digit0;

                    case "space": return space;
                    case "leftShift": return leftShift; case "rightShift": return rightShift;
                    case "leftCtrl": return leftCtrl; case "rightCtrl": return rightCtrl;
                    case "leftAlt": return leftAlt; case "rightAlt": return rightAlt;
                    case "tab": return tab; case "escape": return escape;
                    case "enter": return enter; case "backspace": return backspace;
                    case "capsLock": return capsLock;

                    case "upArrow": return upArrow; case "downArrow": return downArrow;
                    case "leftArrow": return leftArrow; case "rightArrow": return rightArrow;
                }
                return null;
            }
        }

        [Serializable]
        public struct MouseIcons
        {
            public Sprite leftButton;
            public Sprite rightButton;
            public Sprite middleButton;
            public Sprite scrollWheel;

            public Sprite GetSprite(string controlPath)
            {
                switch (controlPath)
                {
                    case "leftButton": return leftButton;
                    case "rightButton": return rightButton;
                    case "middleButton": return middleButton;
                    case "scroll": return scrollWheel;
                }
                return null;
            }
        }
    }
}
