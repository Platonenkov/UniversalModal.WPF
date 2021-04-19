using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalModal.WPF.Helpers
{
    internal static class IEnumerableExtensions
    {
        /// <summary>Выполнение метода для каждого элемента последовательности</summary>
        /// <typeparam name="T">Тип элементов последовательности</typeparam>
        /// <param name="collection">Последовательность элементов</param>
        /// <param name="action">Метод, выполняемый для каждого элемента последовательности</param>
        public static void ForEach<T>([NotNull] this IEnumerable<T> collection, [NotNull] Action<T> action)
        {
            foreach (var item in collection) action(item);
        }
        /// <summary>Преобразование перечисления в массив с преобразованием элементов</summary>
        /// <typeparam name="T">Тип элементов исходного перечисления</typeparam>
        /// <typeparam name="TValue">Тип элементов результирующего массива</typeparam>
        /// <param name="enumerable">Исходное перечисление</param>
        /// <param name="converter">Метод преобразования элементов</param>
        /// <returns>
        /// Если ссылка на исходное перечисление не пуста, то
        ///     Результирующий массив, состоящий из элементов исходного перечисления, преобразованных указанным методом
        /// иначе
        ///     пустая ссылка на массив
        /// </returns>
        [NotNull]
        public static TValue[] ToArray<T, TValue>(
          [NotNull] this IEnumerable<T> enumerable,
          [NotNull] Func<T, TValue> converter)
        {
            switch (enumerable)
            {
                case T[] objArray4:
                    int length = objArray4.Length;
                    if (length == 0)
                        return System.Array.Empty<TValue>();
                    TValue[] objArray1 = new TValue[length];
                    for (int index = 0; index < length; ++index)
                        objArray1[index] = converter(objArray4[index]);
                    return objArray1;
                case List<T> objList:
                    int count1 = objList.Count;
                    if (count1 == 0)
                        return System.Array.Empty<TValue>();
                    TValue[] objArray2 = new TValue[count1];
                    for (int index = 0; index < count1; ++index)
                        objArray2[index] = converter(objList[index]);
                    return objArray2;
                case IList<T> objList:
                    int count2 = objList.Count;
                    if (count2 == 0)
                        return System.Array.Empty<TValue>();
                    TValue[] objArray3 = new TValue[count2];
                    for (int index = 0; index < count2; ++index)
                        objArray3[index] = converter(objList[index]);
                    return objArray3;
                default:
                    return enumerable.Select<T, TValue>(converter).ToArray<TValue>();
            }
        }

    }
}
