using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{

    public class Error
    {
        private static readonly Dictionary<int, string> Errors = new Dictionary<int, string>
        {
            {1, "Неожиданный конец строки."},
            {2, "Неизвестный тип переменной."},
            {3, "Тип переменной отсутствует."},
            {4, "Невозможно присвоить значение."},
            {5, "Неправильное имя переменной."},
            {6, "Неизвестный символ."},
            {7,"Синтаксическая ошибка. Ожидался символ ';'." },
            {8,"Синтаксическая ошибка. Ожидался символ '='." },
            {9, "Отсутствует название переменной." },
            {10, "Переменной не существует." }

        };
        public int Code { get; private set; }//код ошибки
        public int Line { get; private set; }//строка
        public int Column { get; private set; }//столбец, элемент массива строк

        public Error(int code, int line, int column)
        {
            Code = code;
            Line = line;
            Column = column;
        }


        public Error()
        {

        }

        public string FormattedError()
        {
            if ((Code == 1))
            {
                return string.Format("ERROR-{0}: {1} (Строка {2})", Code, Errors[Code], Line);
            }
            else
            {
                return string.Format("ERROR-{0}: {1} (Строка {2}, Столбец {3})", Code, Errors[Code], Line, Column);
            }
        }
    }
    public class Token
    {
        public int Code { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public Token(int code, int line, int column)
        {
            Code = code;
            Line = line;
            Column = column;
        }

    }
    public class Scanner
    {
        public List<List<Token>> ScannedText { get; private set; }//список списков токенов
                                                                  // public NumResolution numResolution = new NumResolution();
        public Scanner(string[] source)
        {
            Errors = new List<Error>();
            ScannedText = Scan(source);
        }

        public List<Error> Errors { get; set; }//список ошибок

        //1 bool
        //2 int
        //3 char
        //4 double
        //5 float
        //6 long
        //7 unsigned
        //8 целое число int
        //9 число с плавающей точкой float
        //10 символ для присваивания
        //11 имя переменной
        //12 ;
        //13 =
        //14 true
        //15 false
        //16 -
        //17 целое число unsigned
        //18 число с плавающей точкой double
        //19 число превосходящее все типы

        private List<List<Token>> Scan(string[] source)//функция сканирования
        {
            var scannedText = new List<List<Token>>();//переменная, хранящая просканированный текст
            foreach (var line in source)
            {
                var scanned = new List<Token>();//переменная, хранящая просканированную строку
                for (var i = 0; i < line.Length; i++)
                {
                    //1 bool
                    if (i + 3 < line.Length)
                        if (line[i] == 'b' && line[i + 1] == 'o' && line[i + 2] == 'o' && line[i + 3] == 'l')//если bool
                        {
                            scanned.Add(new Token(1, Array.IndexOf(source, line) + 1, i + 1));//добавление bool в список токенов
                            i += 3;
                            continue;
                        }
                    //2 int
                    if (i + 2 < line.Length)
                        if (line[i] == 'i' && line[i + 1] == 'n' && line[i + 2] == 't')//если int
                        {
                            scanned.Add(new Token(2, Array.IndexOf(source, line) + 1, i + 1));//добавление int в список токенов
                            i += 2;
                            continue;
                        }
                    //3 char
                    if (i + 3 < line.Length)
                        if (line[i] == 'c' && line[i + 1] == 'h' && line[i + 2] == 'a' && line[i + 3] == 'r')//если char
                        {
                            scanned.Add(new Token(3, Array.IndexOf(source, line) + 1, i + 1));//добавление char в список токенов
                            i += 3;
                            continue;
                        }
                    //4 double
                    if (i + 5 < line.Length)
                        if (line[i] == 'd' && line[i + 1] == 'o' && line[i + 2] == 'u' && line[i + 3] == 'b' && line[i + 4] == 'l'
                            && line[i + 5] == 'e')//если double
                        {
                            scanned.Add(new Token(4, Array.IndexOf(source, line) + 1, i + 1));//добавление double в список токенов
                            i += 5;
                            continue;
                        }
                    //5 float

                    if (i + 4 < line.Length)
                        if (line[i] == 'f' && line[i + 1] == 'l' && line[i + 2] == 'o' && line[i + 3] == 'a' && line[i + 4] == 't')//если int
                        {
                            scanned.Add(new Token(5, Array.IndexOf(source, line) + 1, i + 1));//добавление int в список токенов
                            i += 4;
                            continue;
                        }
                    //6 long
                    if (i + 3 < line.Length)
                        if (line[i] == 'l' && line[i + 1] == 'o' && line[i + 2] == 'n' && line[i + 3] == 'g')//если int
                        {
                            scanned.Add(new Token(6, Array.IndexOf(source, line) + 1, i + 1));//добавление int в список токенов
                            i += 3;
                            continue;
                        }
                    //7 unsigned
                    if (i + 7 < line.Length)
                        if (line[i] == 'u' && line[i + 1] == 'n' && line[i + 2] == 's' && line[i + 3] == 'i' && line[i + 4] == 'g'
                            && line[i + 5] == 'n' && line[i + 6] == 'e' && line[i + 7] == 'd')//если double
                        {
                            scanned.Add(new Token(7, Array.IndexOf(source, line) + 1, i + 1));//добавление double в список токенов
                            i += 7;
                            continue;
                        }
                    //8 целое число
                    //9 число с плавающей точкой
                    if (char.IsDigit(line[i]))//если цифра
                    {
                        bool checknul = true;
                        List<char> a = new List<char>();
                        string b = "";
                        bool fraction = false;
                        while (i < line.Length)//цикл на прохождение конца числа
                        {
                            if (char.IsDigit(line[i]))
                            {
                                if (line[i] == '0' && checknul)
                                {
                                    i++;
                                    continue;
                                }
                                else
                                {
                                    if (!fraction)
                                        a.Add(line[i]);
                                    i++;
                                    checknul = false;
                                    continue;
                                }
                            }
                            if (line[i] == '.' || line[i] == ',')
                            {
                                i++;
                                fraction = true;
                                continue;
                            }
                            if (checknul)
                                a.Add('0');
                            break;
                        }
                        i--;
                        for (int j = 0; j < a.Count; j++)
                            b += a[j].ToString();
                        int n = NumResolution(b, fraction);
                        scanned.Add(new Token(n, Array.IndexOf(source, line) + 1, i + 1));//добавить код знаков в список токенов 
                        continue;
                    }
                    //10 символ для присваивания

                    if (i + 2 < line.Length)
                        if (line[i] == '\'' && (line[i + 1] >= 'a' & line[i + 1] <= 'z' || line[i + 1] >= 'A' & line[i + 1] <= 'Z') && line[i + 2] == '\'')//если int
                        {
                            scanned.Add(new Token(10, Array.IndexOf(source, line) + 1, i + 1));//добавление int в список токенов
                            i += 2;
                            continue;
                        }
                    //11 имя переменной

                    if (line[i] >= 'a' & line[i] <= 'z' || line[i] >= 'A' & line[i] <= 'Z')//если переменная
                    {
                        scanned.Add(new Token(11, Array.IndexOf(source, line) + 1, i + 1));//добавление кода переменной в список токенов
                        i++;
                        while (i < line.Length)//цикл до конца переменной
                        {
                            if (line[i] >= 'a' & line[i] <= 'z' || line[i] >= 'A' & line[i] <= 'Z' || line[i] >= '0' & line[i] <= '9')
                            {
                                i++;
                                continue;
                            }
                            break;
                        }
                        i--;
                        continue;
                    }
                    //14 true
                    if (i + 3 < line.Length)
                        if (line[i] == 't' && line[i + 1] == 'r' && line[i + 2] == 'u' && line[i + 3] == 'e')//если true
                        {
                            scanned.Add(new Token(14, Array.IndexOf(source, line) + 1, i + 1));//добавление char в список токенов
                            i += 3;
                            continue;
                        }
                    //15 false

                    if (i + 4 < line.Length)
                        if (line[i] == 'f' && line[i + 1] == 'a' && line[i + 2] == 'l' && line[i + 3] == 's'
                            && line[i + 3] == 'e')//если false
                        {
                            scanned.Add(new Token(15, Array.IndexOf(source, line) + 1, i + 1));//добавление char в список токенов
                            i += 4;
                            continue;
                        }

                    switch (line[i])
                    {
                        case ';':
                            scanned.Add(new Token(12, Array.IndexOf(source, line) + 1, i + 1));
                            break;
                        case '=':
                            scanned.Add(new Token(13, Array.IndexOf(source, line) + 1, i + 1));
                            break;
                        case '-':
                            scanned.Add(new Token(16, Array.IndexOf(source, line) + 1, i + 1));
                            break;
                        case ' ':
                            break;
                        default:
                            Errors.Add(new Error(6, Array.IndexOf(source, line) + 1, i + 1));
                            break;
                    }
                }
                scannedText.Add(scanned);//добавление в список списков токенов, т.е. строки состоящей из кодирования всего, что в строке
            }
            return scannedText;
        }
        public int NumResolution(string str, bool fraction)//функция сравнения чисел на привышение своего типа
        {
            int result = 0;
            if (!fraction)
            {
                result = String.Compare(str, "2147483647");
                if (result < 0 && str.Length < 11) return 8;
                result = String.Compare(str, "4294967295");
                if (result < 0 && str.Length < 11) return 17;
            }
            else
            {

                result = String.Compare(str, "2147483647");
                //  if ( str.Length < 10) return 9;
                if (result < 0 && str.Length < 11) return 9;
                result = String.Compare(str, "9223372036854775807");
                if (result < 0 && str.Length < 20) return 18;
            }
            return 19;
        }

    }
    public class Syntax
    {
        public List<Error> Errors { get; set; }// список ошибок
        List<Token> line;//список токенов
        int iterator, lineNum;


        public Syntax(List<List<Token>> scanned)
        {
            Errors = new List<Error>();
            StartSyntax(scanned);//получаем список строк с кодами
        }

        private void StartSyntax(List<List<Token>> scanned)//функция определения переменных
        {
            if (Errors.Count != 0)
                return;
            foreach (var Line in scanned)
            {
                line = Line;
                iterator = 0;
                lineNum = scanned.IndexOf(Line) + 1;
                logexp();
            }
        }

        private void logexp()//функция начала разбора
        {
            if (iterator >= line.Count) { return; }
            if (line[iterator].Code != 1 && line[iterator].Code != 2 && line[iterator].Code != 3 && line[iterator].Code != 4 &&
                line[iterator].Code != 5 && line[iterator].Code != 6 && line[iterator].Code != 7)//если не тип переменной
            {
                if (line[iterator + 1].Code == 13)
                    Errors.Add(new Error(10, line[iterator].Line, line[iterator].Column));
                else
                {
                    Next();
                    if (line[iterator].Code == 11)
                        Errors.Add(new Error(3, line[iterator].Line, line[iterator].Column));//добавить ошибку тип переменной отсутствует
                }
            }
            if (iterator + 1 < line.Count)
                if (line[iterator + 1].Code == 2 || line[iterator + 1].Code == 5)
                    Next();

            if (line[iterator].Code == 11 && line[iterator + 1].Code == 11)
                Errors.Add(new Error(2, line[iterator].Line, line[iterator].Column));//добавить ошибку неизвестный тип переменной
            Next();
            assign();
        }

        private void Next()//перейти на след символ
        {
            if (iterator >= line.Count - 1)
            {
                //Errors.Add(new Error(1, line[iterator].Line, line[iterator].Column));
                return;
            }
            else
                iterator++;//передвижение каретки 
        }

        //1 bool
        //2 int
        //3 char
        //4 double
        //5 float
        //6 long
        //7 unsigned
        //8 целое число int
        //9 число с плавающей точкой
        //10 символ для присваивания
        //11 имя переменной
        //12 ;
        //13 =
        //14 true
        //15 false
        //16 –
        private void assign()
        {
            if (Errors.Count == 0)
            {
                if (line[iterator].Code != 11) { Errors.Add(new Error(9, line[iterator].Line, line[iterator].Column)); }//ошибка, если не переменная
                else Next();
            }
            if (line[iterator].Code == 12) return;//если после переменной стоит ';'
            if (line[iterator].Code != 13) { Errors.Add(new Error(8, line[iterator].Line, line[iterator].Column)); Next(); }//ошибка, если не '='
            else Next();

            switch (line[0].Code)
            {
                case 1:
                    {
                        if (line[iterator].Code == 14 || line[iterator].Code == 15) Next();//просматриваем правильное присвоение bool
                        else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        break;
                    }
                case 2:
                    {
                        if (line[iterator].Code == 16) Next();//если '-'
                        if (line[iterator].Code == 8) Next();//просматриваем правильное присвоение int
                        else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        break;
                    }
                case 3:
                    {
                        if (line[iterator].Code == 10) Next();//просматриваем правильное присвоение char
                        else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        break;
                    }
                case 4:
                    {
                        if (line[iterator].Code == 16) Next();//если '-'
                        if (line[iterator].Code == 9 || line[iterator].Code == 18) Next();//просматриваем правильное присвоение double
                        else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        break;
                    }
                case 5:
                    {
                        if (line[iterator].Code == 16) Next();//если '-'
                        if (line[iterator].Code == 9) Next();//просматриваем правильное присвоение float
                        else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        break;
                    }
                case 6:
                    {
                        if (line[1].Code == 2)
                        {
                            if (line[iterator].Code == 16) Next();//если '-'
                            if (line[iterator].Code == 8) Next();//просматриваем правильное присвоение long
                            else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        }
                        if (line[1].Code == 5)
                        {
                            if (line[iterator].Code == 16) Next();//если '-'
                            if (line[iterator].Code == 9 || line[iterator].Code == 18) Next();//просматриваем правильное присвоение long
                            else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        }
                        break;
                    }
                case 7:
                    {
                        if (line[1].Code == 2)
                        {
                            if (line[iterator].Code == 16) Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); //если '-'
                            if (line[iterator].Code == 17 || line[iterator].Code == 8) Next();//просматриваем правильное присвоение unsigned int
                            else { Errors.Add(new Error(4, line[iterator].Line, line[iterator].Column)); Next(); }
                        }
                        break;
                    }
                default: Next(); break;
            }


            if (line[iterator].Code != 12) Errors.Add(new Error(7, line[iterator].Line, line[iterator].Column));//ошибка, если нет ';'

        }
    }

}
