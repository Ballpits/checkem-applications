using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace ProjectSC.ViewModels
{
    public sealed class HotKey : IDisposable
    {
        private readonly IntPtr _handle;

        private readonly int _id;

        private bool _isKeyRegistered;

        public HotKey(ModifierKeys modifierKeys, Key key, Window window)
            : this(modifierKeys, key, new WindowInteropHelper(window))
        {
        }

        public HotKey(ModifierKeys modifierKeys, Key key, WindowInteropHelper window)
            : this(modifierKeys, key, window.Handle)
        {
        }

        public HotKey(ModifierKeys modifierKeys, Key key, IntPtr windowHandle)
        {
            Key = key;
            KeyModifier = modifierKeys;
            _id = GetHashCode();
            _handle = windowHandle;
            RegisterHotKey();
            ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
        }

        ~HotKey()
        {
            Dispose();
        }

        public event Action<HotKey> HotKeyPressed;

        public Key Key { get; private set; }

        public ModifierKeys KeyModifier { get; private set; }

        private int InteropKey
        {
            get
            {
                return KeyInterop.VirtualKeyFromKey(Key);
            }
        }

        public void Dispose()
        {
            ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
            UnregisterHotKey();
        }

        private void OnHotKeyPressed()
        {
            if (HotKeyPressed != null)
            {
                HotKeyPressed(this);
            }
        }

        private void RegisterHotKey()
        {
            if (Key == Key.None)
            {
                return;
            }

            if (_isKeyRegistered)
            {
                UnregisterHotKey();
            }

            _isKeyRegistered = HotKeyWinApi.RegisterHotKey(_handle, _id, KeyModifier, InteropKey);

            if (!_isKeyRegistered)
            {
                throw new ApplicationException("Hotkey already in use");
            }
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            if (handled)
            {
                return;
            }

            if (msg.message != HotKeyWinApi.WmHotKey || (int)(msg.wParam) != _id)
            {
                return;
            }

            OnHotKeyPressed();
            handled = true;
        }

        private void UnregisterHotKey()
        {
            _isKeyRegistered = !HotKeyWinApi.UnregisterHotKey(_handle, _id);
        }
    }

    internal class HotKeyWinApi
    {
        public const int WmHotKey = 0x0312;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
