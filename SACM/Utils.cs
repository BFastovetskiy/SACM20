using System;
using System.Collections.Generic;
using System.Text;

namespace SACM
{
    public class Utils
    {
        /// <summary>
        /// Приобразование объекта в Base64
        /// </summary>
        /// <param name="source">Исходный объект</param>
        /// <returns>Строка в формате Base64</returns>
        public static string ToBase64(object source)
        {
            source = source ?? string.Empty;
            byte[] buffer = toBytes(source.ToString());
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Приобразование строки в Base64
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Строка в формате Base64</returns>
        public static string ToBase64(string source)
        {
            byte[] buffer = toBytes(source);
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Перекодировка Base64 в строку
        /// </summary>
        /// <param name="source">Строка в формате Base64</param>
        /// <returns>UTF-8 строка</returns>
        public static string FromBase64(string source)
        {
            return toString(Convert.FromBase64String(source));
        }

        /// <summary>
        /// Конвертация строки в массив байт
        /// </summary>
        /// <param name="source">исходная строка</param>
        /// <returns>Результирующий массив байт</returns>
        private static byte[] toBytes(string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        /// <summary>
        /// Конвертация массив байт в строку
        /// </summary>
        /// <param name="buffer">массив байт</param>
        /// <returns>Результирующая строка</returns>
        private static string toString(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
