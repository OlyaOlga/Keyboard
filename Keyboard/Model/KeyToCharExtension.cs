namespace Keyboard.Model
{
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Input;

    internal static class KeyToCharExtension
    {
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll")]
        private static extern int ToUnicode(
            uint virtKey,
            uint scanCode,
            byte[] keyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
                StringBuilder pwszBuff,
            int cchBuff,
            uint flags);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] keyState);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint code, MapType mapType);

        /// <summary>
        /// Converts virtual key to real char
        /// </summary>
        /// <param name="key">Input key</param>
        /// <returns>Real char</returns>
        public static char GetCharFromKey(this Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                {
                    ch = stringBuilder[0];
                    break;
                }

                default:
                {
                    ch = stringBuilder[0];
                    break;
                }
            }

            return ch;
        }
    }
}