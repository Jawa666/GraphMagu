using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class DNF
    {
        public List<string> DNFMagu(int[,] massiv)
        {
            List<List<string>> list = new List<List<string>>();   //Создание Листа, и инициализация его строками букв         

            //Алгоритм записи простых дизъюнкций. Тоесть нахождение смежных вершин графа
            int q = 0;
            for (int i = 0; i < massiv.GetLength(0); i++)
            {
                for (int j = 0; j < massiv.GetLength(1); j++)
                {

                    if(massiv[i,j] == 1)
                    {
                        list.Add(new List<string>());
                        list[q].Add(Convert.ToString(Convert.ToChar(65 + i)));
                        list[q].Add(Convert.ToString(Convert.ToChar(65 + j)));
                        q++;
                    }     
                }
            }
            for (int i = 0; i < q; i++)
                sortBuk(list[i]);

            list = delOdinakov(list);
            list = umnojenie(list);
            list[0] = delOdinakovSimvolovVStroke(list[0]);
            list[0] = poglaychenie(list[0]);

            int kolVershin = massiv.GetLength(0);
            List<string> mnojestva = vnutrenieUstPodmnojestva(list, kolVershin);

            return mnojestva;
        }
        
        public List<string> poglaychenie(List<string> list)  //метод который  стирает повторяющиеся, и выполняет поглащение A+AB => A.
        {
                
            for(int i = 1; i < list.Count(); i++)      //Сортировка по количеству символов в строках   
            {
                string str = list[i];
                int j = i - 1;
                while (j >= 0 && str.Length < list[j].Length)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = str;
            }

            for (int i = 0; i < list.Count(); i++)
                for (int j = 0; j < list.Count(); j++)
                    if (mnojestvo(list[i],list[j]) && i!=j)   // является ли первый  праметр подмножеством второго
                    {
                        list.RemoveAt(j);
                        j--;
                    }                                               
            return list;
        }
        public bool mnojestvo(string str1, string str2) // является ли первое вхождение подмножеством второго
        {           
            SortedSet<char> s1 = new SortedSet<char>();
            SortedSet<char> s2 = new SortedSet<char>();
            for (int i = 0; i < str1.Length; i++)
                s1.Add(str1[i]);
            for (int i = 0; i < str2.Length; i++)
                s2.Add(str2[i]);         
            bool flag = s1.IsSubsetOf(s2);

            return flag;
        }

        public List<List<string>> delOdinakov(List<List<string>> list) // метод уничтожения одинаковых элементов 2мерного листа
        {
            int q = list.Count();
            for (int i = 0; i < q; i++)
                for (int j = 0; j < q; j++)
                    if (list[i].SequenceEqual(list[j]) && i != j)
                        list[j].Clear();
            list.RemoveAll(x => x == null || x.Count == 0);
            return list;
        } 
        public void sortBuk(List<string> list)  //метод сортировки по элементам в листе 
        {
            list.Sort();    
        }

        public List<List<string>> umnojenie(List<List<string>> list)// метод перемножения.
        {
           
            while (list.Count() != 1)
            {
                List<List<string>> list2 = new List<List<string>>();
                list2.Add(new List<string>());

                for (int i = 0; i < list[0].Count();i++)
                    for(int j = 0; j < list[1].Count(); j++)
                    {
                        if(list[0][i] != list[1][j])
                            list2[0].Add(list[0][i] + list[1][j]);
                        else list2[0].Add(list[0][i]);
                    }
                list[1].Clear();
                list[0] = list2[0];
                list.RemoveAll(x => x == null || x.Count == 0);               
            }
            return list;
        }

        public List<string> delOdinakovSimvolovVStroke(List<string> list) // метод удаляющий одинаковые символы и сортирующий их
        {
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string str = list[i];
                string str2 = "";
                for (int j = 0; j < str.Length; j++)
                    list2.Add(str[j].ToString());

                for (int j = 0; j < list2.Count(); j++) 
                { 
                    for (int t = 0; t < list2.Count(); t++)
                        if (j != t && list2[j] == list2[t])
                        {
                            list2.RemoveAt(t);
                            t--;
                        }
                }
                list2.Sort();
                for (int j = 0; j < list2.Count(); j++)
                    str2 = str2 + list2[j];
                list3.Add(str2);
                list2.Clear();
            }          
            return list3;
        }
        public List<string> vnutrenieUstPodmnojestva(List<List<string>> list, int value)
        {
            List<string> mnojestva = new List<string>();
            List<string> newList = list[0];

            SortedSet<char> vershini = new SortedSet<char>();
            for (int i = 0; i < value; i++)
                vershini.Add(Convert.ToChar(65+i));

            for(int i = 0; i < list[0].Count(); i++)
            {
                SortedSet<char> element = new SortedSet<char>();
                string elementStr = newList[i];

                for (int j = 0; j < elementStr.Length; j++)
                    element.Add(elementStr[j]);
                
                IEnumerable<char> element2 = vershini.Except(element);
                string elementStr2 = "";
                foreach (char a in element2)
                    elementStr2 = elementStr2 + a;
                mnojestva.Add(elementStr2);                
            }


            return mnojestva;
        }
    }
}
