namespace Keyboard.ViewModel.ViewModelExtension
{
    using System;
    using System.Linq;

    internal static class EnumBuilder<T> where T : struct, IConvertible
    {
        public static void MakeEnum(bool[] flags, out T enumStorage)
        {
            var enumItems = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            enumStorage = new T();
            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i])
                {
                    enumStorage = enumItems[i];
                }
            }
        }
    }
}