using System;
using System.Collections.Generic;
using System.IO;

namespace Matrix
{
    class Program
    {
        /// <summary>
        /// Основной метод, отвечающий за вызов остальных.
        /// </summary>
        static void Main()
        {
            do
            {
                Console.Clear();
                Function(out string option);
                StringsAndColumns(out int str, out int col);
                List<List<double>> matrix = Creation(str, col);
                matrix = Enter(matrix);
                List<List<double>> matrix2 = new List<List<double>>();
                if (int.TryParse(option, out int opt) && opt > 2 && opt < 6)
                {
                    Console.WriteLine($"А теперь вторая матрица:{Environment.NewLine}");
                    StringsAndColumns(out int str2, out int col2);
                    matrix2 = Creation(str2, col2);
                    matrix2 = Enter(matrix2);
                }
                if (matrix != null)
                {
                    switch (option)
                    {
                        case "1":
                            {
                                Trace(matrix);
                                break;
                            }
                        case "2":
                            {
                                Transposition(matrix);
                                break;
                            }
                        case "3":
                            {
                                Addition(matrix, matrix2);
                                break;
                            }
                        case "4":
                            {
                                Subtraction(matrix, matrix2);
                                break;
                            }
                        case "5":
                            {
                                Multiplication(matrix, matrix2);
                                break;
                            }
                        case "6":
                            {
                                Coefficient(matrix);
                                break;
                            }
                        case "7":
                            {
                                
                                Console.WriteLine($"Определитель матрицы приблизительно равен {Determinant(matrix)}");
                                Console.ReadLine();
                                break;
                            }
                        case "8":
                            {
                                Kramer(matrix);
                                break;
                            }
                    }
                }
                Console.WriteLine("Для повтора решения нажмите Enter, для завершения - какой-нибудь " +
                    "другой символ");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        /// <summary>
        /// Метод, выводящий на экран список функций программы и возвращающий номер выбранного варианта.
        /// </summary>
        /// <param name="option"></param>
        static void Function(out string option)
        {
            Console.WriteLine($"Выбор пункта осуществляется последством введения соответсвующей цифры.{Environment.NewLine}");
            Console.WriteLine("1. нахождение следа матрицы;");
            Console.WriteLine("2. транспонирование матрицы;");
            Console.WriteLine("3. сумма двух матриц;");
            Console.WriteLine("4. разность двух матриц;");
            Console.WriteLine("5. произведение двух матриц;");
            Console.WriteLine("6. умножение матрицы на число;");
            Console.WriteLine("7. нахождение определителя матрицы;");
            Console.WriteLine("8. решение СЛАУ методом Крамера;");
            Console.WriteLine("Или другой символ для ввода матрицы без выполнения операций над ней.");
            option = Console.ReadLine();
        }

        /// <summary>
        /// Метод, выводящий на экран список вариантов ввода и вызывающий метод для выбранного способа.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        static List<List<double>> Enter(List<List<double>> matrix)
        {
            Console.WriteLine($"Выберите способ заполнения матрицы:{Environment.NewLine}");
            Console.WriteLine("1. Случайные числа;");
            Console.WriteLine("2. Консоль;");
            Console.WriteLine("3. Из файла;");
            Console.WriteLine("Или дргуой символ для генерации нулевой матрицы.");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    {
                        RandomInput(ref matrix);
                        break;
                    }
                case "2":
                    {
                        ConsoleInput(ref matrix);
                        break;
                    }
                case "3":
                    {
                        FileInput(ref matrix);
                        break;
                    }
            }
            return matrix;
        }

        /// <summary>
        /// Метод, создающий лист заданого размера с нулевыми значениями.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        static List<List<double>> Creation(int str, int col)
        {
            List<List<double>> matrix = new List<List<double>>();
            for (int i = 0; i < str; i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < col; j++)
                {
                    matrix[i].Add(0);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Метод, получающий от пользователя количество строк и столбцов будущей матрицы.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="col"></param>
        static void StringsAndColumns(out int str, out int col)
        {
            Console.WriteLine("Введите количество строк (не более 10): ");
            str = NumberInput();
            Console.WriteLine("Введите количество столбцов (не более 10): ");
            col = NumberInput();
        }

        /// <summary>
        /// Метод, отвечающий за проверку корректности ввода количества строк/столбцов.
        /// </summary>
        /// <returns></returns>
        static int NumberInput()
        {
            int number;
            string inp = Console.ReadLine();
            while (!(int.TryParse(inp, out number) && number > 0 && number < 11))
            {
                Console.WriteLine("Ожидается целое положительное число, не большее десяти: ");
                inp = Console.ReadLine();
            }
            return number;
        }

        /// <summary>
        /// Метод, реализующий транспонирование матрицы.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        static List<List<double>> Transposition(List<List<double>> matrix)
        {
            double intermediate;
            List<List<double>> matrixT = Creation(matrix[0].Count, matrix.Count);
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = i; j < matrix[0].Count; j++)
                {
                    intermediate = matrix[i][j];
                    try
                    {

                        matrixT[i][j] = matrix[j][i];
                    }
                    catch (ArgumentOutOfRangeException) { }
                    try
                    {
                        matrixT[j][i] = intermediate;
                    }
                    catch (ArgumentOutOfRangeException) { }


                }
            }
            Console.WriteLine("Транспонированная матрица: ");
            CwTabTab(matrixT);
            return matrixT;
        }

        /// <summary>
        /// Метод, выводящий матрицу на экран.
        /// </summary>
        /// <param name="matrix"></param>
        static void CwTabTab(List<List<double>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    Console.Write($"{matrix[i][j]}  ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Метод, находящий след матрицы.
        /// </summary>
        /// <param name="matrix"></param>
        static void Trace(List<List<double>> matrix)
        {
            double tr = 0;
            if (matrix[0].Count != matrix.Count)
            {
                Console.WriteLine("Для поиска следа матрица должна быть квадратной!");
            }
            else
            {
                for (int i = 0; i < matrix.Count; i++)
                {
                    tr += matrix[i][i];
                }
                Console.WriteLine($"След матрицы равен {tr}");
            }
        }

        /// <summary>
        /// Метод, реализующий сложение матриц.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        static List<List<double>> Addition(List<List<double>> matrixA, List<List<double>> matrixB)
        {
            if (matrixA[0].Count != matrixB[0].Count || matrixA.Count != matrixB.Count)
            {
                Console.WriteLine("Для сложения матрицы должны быть одинакового размера!");
                return Creation(matrixA.Count, matrixA[0].Count);
            }
            else
            {
                List<List<double>> result = Creation(matrixA.Count, matrixA[0].Count);
                for (int i = 0; i < matrixA.Count; i++)
                {
                    for (int j = 0; j < matrixA[0].Count; j++)
                    {
                        result[i][j] = matrixA[i][j] + matrixB[i][j];
                    }
                }
                Console.WriteLine("Результат сложения: ");
                CwTabTab(result);
                return result;
            }
        }

        /// <summary>
        /// Метод, вычисляющий разность матриц.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        static void Subtraction(List<List<double>> matrixA, List<List<double>> matrixB)
        {
            if (matrixA[0].Count != matrixB[0].Count || matrixA.Count != matrixB.Count)
            {
                Console.WriteLine("Для нахождения разности матрицы должны быть одинакового размера!");
            }
            else
            {
                List<List<double>> result = Creation(matrixA.Count, matrixA[0].Count);
                for (int i = 0; i < matrixA.Count; i++)
                {
                    for (int j = 0; j < matrixA[0].Count; j++)
                    {
                        result[i][j] = matrixA[i][j] - matrixB[i][j];
                    }
                }
                Console.WriteLine("Результат вычитания: ");
                CwTabTab(result);
            }
        }

        /// <summary>
        /// Метод, реализующий умножение матрицы на коэффициент.
        /// </summary>
        /// <param name="matrix"></param>
        static void Coefficient(List<List<double>> matrix)
        {
            double coeff;
            Console.Write("Введите коэффциент: ");
            string input = Console.ReadLine();
            while (!double.TryParse(input, out coeff))
            {
                Console.WriteLine("Ожидается число: ");
                input = Console.ReadLine();
            }
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    matrix[i][j] *= coeff;
                }
            }
            Console.WriteLine("Результат умножения на число: ");
            CwTabTab(matrix);
        }

        /// <summary>
        /// Метод, реализующий произведение двух матриц.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        static void Multiplication(List<List<double>> matrixA, List<List<double>> matrixB)
        {
            if (matrixA[0].Count != matrixB.Count)
            {
                Console.WriteLine("Для нахождения произведения количество столбцов первой матрицы " +
                    "должно совпадать с количеством строк второй!");
                return;
            }
            else
            {
                List<List<double>> result = Creation(matrixA.Count, matrixB[0].Count);
                int minimumCount = Math.Min(matrixA[0].Count, matrixB.Count);
                for (int i = 0; i < matrixA.Count; i++)
                {
                    for (int j = 0; j < matrixB[0].Count; j++)
                    {
                        for (int k = 0; k < minimumCount; k++)
                        {
                            result[i][j] += matrixA[i][k] * matrixB[k][j];
                        }
                    }
                }
                Console.WriteLine("Результат произведения: ");
                CwTabTab(result);
            }
        }

        /// <summary>
        /// Метод, вычисляющий определитель матрицы.
        /// </summary>
        /// <param name="matrix0"></param>
        /// <returns></returns>
        static double Determinant(List<List<double>> matrix0)
        {
            List<List<double>> matrix = Creation(matrix0.Count, matrix0[0].Count);
            {
                for (int i = 0; i < matrix.Count; i++)
                {
                    for (int j = 0; j < matrix[0].Count; j++)
                    {
                        matrix[i][j] = matrix0[i][j];
                    }
                }
            }
            double det = 1;
            List<List<double>> intermediateMatrix = Creation(matrix.Count, matrix[0].Count);
            if (matrix[0].Count != matrix.Count)
            {
                Console.WriteLine("Для поиска определителя матрица должна быть квадратной!");
                return 0;
            }
            else
            {
                for (int i = 0; i < matrix.Count; ++i)
                {
                    int k = i;
                    for (int j = i + 1; j < matrix.Count; ++j)
                        if (Math.Abs(matrix[j][i]) > Math.Abs(matrix[k][i]))
                            k = j;
                    if (Math.Abs(matrix[k][i]) < double.Epsilon)
                    {
                        det = 0;
                        break;
                    }
                    intermediateMatrix[0] = matrix[i];
                    matrix[i] = matrix[k];
                    matrix[k] = intermediateMatrix[0];
                    if (i != k)
                        det = -det;
                    det *= matrix[i][i];
                    for (int j = i + 1; j < matrix.Count; ++j)
                        matrix[i][j] /= matrix[i][i];
                    for (int j = 0; j < matrix.Count; ++j)
                        if ((j != i) && (Math.Abs(matrix[j][i]) > double.Epsilon))
                            for (k = i + 1; k < matrix.Count; ++k)
                                matrix[j][k] -= matrix[i][k] * matrix[j][i];
                }
                if (Math.Abs(det % 1) > 0.99999999999999)
                {
                    det = (int)det / 1 + 1;
                }
                if (Math.Abs(det%1) < 0.00000000000002)
                {
                    det = (int)det / 1;
                }
                return det;
            }
        }

        /// <summary>
        /// Метод, заполняющий матрицу случайными числами.
        /// </summary>
        /// <param name="matrix"></param>
        static void RandomInput(ref List<List<double>> matrix)
        {
            Random random = new Random();
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    matrix[i][j] = random.NextDouble() * Math.Pow(10, random.Next(1, 10));
                }
            }
            Console.WriteLine("Сгенерированная матрица: ");
            CwTabTab(matrix);
        }

        /// <summary>
        /// Метод, заполняющий матрицу значениями из консоли.
        /// </summary>
        /// <param name="matrix"></param>
        static void ConsoleInput(ref List<List<double>> matrix)
        {
            string input;
            double value;
            Console.WriteLine("Введите значения элементов матрицы от -1000 до 1000 (через Enter): ");
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    input = Console.ReadLine();
                    while (!(double.TryParse(input, out value) && value >= -1000 && value <= 1000))
                    {
                        Console.WriteLine("Ожидается число от -1000 до 1000, попробуйте ввести его снова: ");
                        input = Console.ReadLine();
                    }
                    matrix[i][j] = value;
                }
            }
            Console.WriteLine("Ваша матрица: ");
            CwTabTab(matrix);
        }

        /// <summary>
        /// Метод, считывающий матрицу из файла, расположенного в каталоге с программой.
        /// </summary>
        /// <param name="matrix"></param>
        static void FileInput(ref List<List<double>> matrix)
        {
            string text;
            Console.WriteLine("Формат ввода: 1 матрица - 1 файл, матрица в файле расположена " +
                "как уважающая себя матрица (элементы от -1000 до 1000 в строке через пробел, столбцы - через Enter.");
            Console.WriteLine($"Предполагаемое расположение txt файла - {Directory.GetCurrentDirectory()}.");
            Console.WriteLine("Введите имя файла: ");
            string fileName = Console.ReadLine();
            try
            {
                string[] input;
                using (StreamReader file = new StreamReader(fileName))
                {
                    double value;

                    for (int i = 0; i < matrix.Count; i++)
                    {
                        text = file.ReadLine();
                        input = text.Split();
                        if (text.Split().Length != matrix[0].Count)
                        {
                            Console.WriteLine("Длина строки не совпадает с указанной");
                            Console.ReadLine();
                            matrix = null;
                            return;
                        }
                        for (int j = 0; j < input.Length; j++)
                        {
                            if (!(text != null && double.TryParse(input[j], out value) && value >= -1000 && value <= 1000))
                            {
                                Console.WriteLine("Во входном файле должны присутствовать только числа" +
                                    " от -1000 до 1000 в указанном формате ввода, а количество" +
                                    " элементов стрках и столбцах должно совпадать с указанным выше.");
                                Console.ReadLine();
                                matrix = null;
                                return;
                            }
                            matrix[i][j] = value;
                        }

                    }
                    Console.WriteLine("Ваша матрица:");
                    CwTabTab(matrix);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
                matrix = null;
                return;
            }
            catch (IOException)
            {
                Console.WriteLine("Файл не найден");
                matrix = null;
                return;
            }
            }

        /// <summary>
        /// Метод, решающий СЛАУ методом Крамера.
        /// </summary>
        /// <param name="matrix"></param>
        static void Kramer(List<List<double>> matrix)
        {
            Console.WriteLine("(формат ввода - квадратная матрица коэффициентов, затем столбец свободных членов)");
            double det = Determinant(matrix);
            double detRepl;
            if (matrix.Count < matrix[0].Count ||det==0)
            {
                Console.WriteLine("Введённая СЛАУ не имеет единственного решения.");
                return;
            }
            Console.WriteLine("Введите элементы столбца свободных членов (от -1000 до 1000):");
            List<List<double>> column = Creation(matrix.Count, 1);
            column = Enter(column);
            for (int i = 0; i<column.Count; i++)
            {
                detRepl = Determinant(Replasement(i, matrix, column));
                double result = detRepl / det;
                    if (Math.Abs(result % 1) > 0.99999999999999)
                {
                    result = (int)result / 1 + 1;
                }
                if (Math.Abs(result % 1) < 0.00000000000002)
                {
                    result = (int)result / 1;
                }
                Console.WriteLine($"{i+1}-ая неизестная: {result}");
            }
        }

        /// <summary>
        /// Метод, возвращающий матрицу со столбцом заменённым столбцом коэффициентов.
        /// Является вспомогательным к методу с Крамером.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="matrix"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        static List<List<double>> Replasement(int number, List<List<double>> matrix, List<List<double>> column)
        {
            List<List<double>> replacement = Creation(matrix.Count, matrix[0].Count);
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    replacement[i][j] = matrix[i][j];
                }
            }
            for (int i = 0; i < matrix.Count; i++)
            {
                    replacement[i][number] = column[i][0];
            }
            return replacement;
        }
    }
}
