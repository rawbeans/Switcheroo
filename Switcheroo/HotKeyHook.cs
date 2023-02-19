/*
 * Switcheroo - The incremental-search task switcher for Windows.
 * http://www.switcheroo.io/
 * Copyright 2009, 2010 James Sulak
 * Copyright 2014 Regin Larsen
 * 
 * Switcheroo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Switcheroo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Switcheroo.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;
using ManagedWinapi;
using ManagedWinapi.Hooks;

namespace Switcheroo
{
    public delegate void HotKeyHookEventHandler(object sender, HotKeyHookEventArgs args);

    public class HotKeyHookEventArgs : EventArgs
    {
        public bool CtrlDown { get; set; }
        public bool ShiftDown { get; set; }
        public bool Handled { get; set; }
    }

    public class HotKeyHook : IDisposable
    {
        public event HotKeyHookEventHandler Pressed;
        private System.Windows.Forms.Keys KeyCode;
        private const int AltKey = 32;
        private const int CtrlKey = 11;
        private readonly KeyboardKey _shiftKey = new KeyboardKey(Keys.LShiftKey);
        private readonly KeyboardKey _ctrlKey = new KeyboardKey(Keys.LControlKey);
        private readonly KeyboardKey _altKey = new KeyboardKey(Keys.LMenu);
        private readonly int WM_KEYDOWN = 0x0100;
        private readonly int WM_SYSKEYDOWN = 0x0104;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly LowLevelKeyboardHook _lowLevelKeyboardHook;

        public HotKeyHook(System.Windows.Forms.Keys keyCode)
        {
            _lowLevelKeyboardHook = new LowLevelKeyboardHook();
            _lowLevelKeyboardHook.MessageIntercepted += OnMessageIntercepted;
            _lowLevelKeyboardHook.StartHook();
            KeyCode = keyCode;
        }

        private void OnMessageIntercepted(LowLevelMessage lowLevelMessage, ref bool handled)
        {
            var keyboardMessage = lowLevelMessage as LowLevelKeyboardMessage;
            if (handled || keyboardMessage == null)
            {
                return;
            }

            if (!IsHotKeyDown(keyboardMessage))
            {
                return;
            }

            if (!IsKeyDown(_altKey))
            {
                return;
            }

            var shiftKeyDown = IsKeyDown(_shiftKey);
            var ctrlKeyDown = IsKeyDown(_ctrlKey);

            var eventArgs = OnPressed(shiftKeyDown, ctrlKeyDown);

            handled = eventArgs.Handled;
        }

        private static bool IsKeyDown(KeyboardKey keyboardKey)
        {
            return (keyboardKey.AsyncState & 32768) != 0;
        }

        private bool IsHotKeyDown(LowLevelKeyboardMessage keyboardMessage)
        {
            return keyboardMessage.VirtualKeyCode == (int)KeyCode &&
                   (keyboardMessage.Message == WM_KEYDOWN || keyboardMessage.Message == WM_SYSKEYDOWN);
        }

        private HotKeyHookEventArgs OnPressed(bool shiftDown, bool ctrlDown)
        {
            var hotKeyHookEventArgs = new HotKeyHookEventArgs { ShiftDown = shiftDown, CtrlDown = ctrlDown };
            var handler = Pressed;
            if (handler != null)
            {
                handler(this, hotKeyHookEventArgs);
            }
            return hotKeyHookEventArgs;
        }

        public void Dispose()
        {
            if (_lowLevelKeyboardHook != null)
            {
                _lowLevelKeyboardHook.Dispose();
            }
        }
    }
}