using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab1
{
    public class Recursive
    {
        public enum LexemeType
        {

            O_SELECT, // select
            COLUMN, // столбец
            COMMA, // запятая
            O_FROM, // from
            TABLE, // таблица
            EOF

        }

        public class Lexeme
        {
            public LexemeType type;
            public string value;

            public Lexeme(LexemeType type, string value)
            {
                this.type = type;
                this.value = value;
            }

        }

        public static string lexAnalyze(string expText)
        {
            List<Lexeme> lexemes = new List<Lexeme>();
            int pos = 0; // позиция символа в строке
            bool s_have = false;
            bool f_have = false;
            bool past_op = false;
            int past_comma = 0;
            string analyse = "";
            while (pos < expText.Length)
            {
                char c = expText[pos];
                string word = "";



                if (Char.IsLetter(c) || c == ',')
                {

                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        sb.Append(c);
                        word += c;
                        pos++;
                        if (pos >= expText.Length) break;
                        c = expText[pos];

                    } while (Char.IsLetter(c) || c == ',');




                    if (word == "select")
                    {
                        if (past_comma > 0)

                            analyse += "Запятая перед оператором!!! \n";
                        if (s_have == false)
                        {

                            lexemes.Add(new Lexeme(LexemeType.O_SELECT, sb.ToString()));

                            analyse += "Оператор1 \n";
                            s_have = true;
                            past_op = true;
                            past_comma = 0;
                        }
                        else
                        {
                            analyse += "Select уже написан!!! \n";
                        }

                    }

                    else if (word == "from")
                    {
                        if (past_comma > 0)

                            analyse += "Запятая перед оператором!!! \n";
                        if (f_have == false)
                        {
                            if (past_op)

                                analyse += "Должен быть указан хотя бы один столбец! \n";
                            lexemes.Add(new Lexeme(LexemeType.O_FROM, sb.ToString()));

                            analyse += "Оператор2 \n";
                            f_have = true;
                            past_op = true;
                            past_comma = 0;
                        }
                        else

                            analyse += "From уже написан!!! \n";
                    }


                    else
                    {
                       
                        analyse += Analys_X(word, s_have, f_have, past_comma, past_op);
                        past_op = false;
                    }
                }

                else
                {
                    if (c != ' ')
                    {

                        analyse += "Неопознанный символ! \n";
                    }
                    pos++;
                }





            }

            lexemes.Add(new Lexeme(LexemeType.EOF, ""));

           

            return analyse;
        }

        public static string Analys_X(string word, bool s_have, bool f_have, int past_comma, bool past_op)

        {
            string analyse_X = "";
            int pos_X = 0;
            char c_X;
            while (pos_X < word.Length)
            {
                c_X = word[pos_X];

                if (Char.IsLetter(c_X))
                {

                   
                    do
                    {
                        pos_X++;
                        if (pos_X >= word.Length) break;
                        c_X = word[pos_X];

                    } while (Char.IsLetter(c_X));


                    if (!s_have)
                    {

                        analyse_X += "Не указан оператор select! \n";


                    }

                    else if (f_have == true)
                    {
                       

                        analyse_X += "Таблица \n";
                        past_comma = 0;
                        past_op = false;
                    }

                    else
                    {
                     

                        analyse_X += "Стоблец \n";
                        past_comma = 0;
                        past_op = false;
                    }

                   
                }

                else
                {
                    past_comma++;
                    if (past_op)
                        analyse_X += "Запятая после оператора !!  ";
                     if (past_comma >= 2)
                        analyse_X += "Запятая встречается больше одного раза подряд!!!  ";
                    else
                    analyse_X += "Запятая \n";
                   
                    word = word.Substring(word.IndexOf(',') + 1);
                    analyse_X += Analys_X(word, s_have, f_have, past_comma, past_op);
                }
            }


            return analyse_X;
        }
    }
}

