using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class HromMagu
    {
        public List<List<string>> Hrom(List<string> magu, int vershina)
        {
            List<string> listMagu = magu;
            List<string> ishod = NahojdenieDiz(magu, vershina);//развертывание в днф
            List<string> nedostatok = Nedostat(ishod, vershina);//нахождение слагаемых в ввиде цифр
            List<List<string>> listConvert = Convertt(nedostatok); // конвертанция в буквенные представления где элементы 
                                                                   //list[i]-это коньюкция, а элементы list[i][j]- дизъюнкции    
            DNF df = new DNF();
            listConvert = df.umnojenie(listConvert);
            listConvert[0] = df.delOdinakovSimvolovVStroke(listConvert[0]);
            listConvert[0] = df.poglaychenie(listConvert[0]);  //определили все слогаемые 
            listMagu = listConvert[0];

            List<string> minValue = nahojdenieMin(listMagu);
            List<List<string>> result = HromGraph(minValue, magu);
            
            return result;
        } 
        public List<string> NahojdenieDiz(List<string> list, int value)  //получаем устойчивые множества и разворачиваем их в дизъюнкции
        {
            List<string> mnojestva = new List<string>();

            SortedSet<char> vershini = new SortedSet<char>();
            for (int i = 0; i < value; i++)
                vershini.Add(Convert.ToChar(65 + i));

            for(int i = 0; i < list.Count; i++)
            {
                string mnojestvo = list[i];
                SortedSet<char> vershina = new SortedSet<char>();

                for (int j = 0; j < mnojestvo.Count(); j++)
                    vershina.Add(mnojestvo[j]);

                IEnumerable<char> mnojestvo2 = vershini.Except(vershina);
                string elementStr2 = "";
                foreach (char a in mnojestvo2)
                    elementStr2 = elementStr2 + a;
                mnojestva.Add(elementStr2);

            }
            return mnojestva;
        }
        public List<string> Nedostat(List<string> list, int value) //метод формирующий конъюкции y
        {
            List<string> result = new List<string>();

            for(int i = 0; i < value; i++)
            {
                List<int> number = new List<int>();
                string stroka = "";
                for (int j = 0; j < list.Count(); j++)
                {                   
                    bool flag = false;
                    string str = list[j];
                    for(int q = 0; q < str.Count(); q++)
                        if (Convert.ToChar(65 + i) == str[q]) 
                            flag = true;

                    if (!flag) number.Add(j);
                }

                for (int t = 0; t < number.Count(); t++)
                    stroka = stroka + Convert.ToString(number[t]);
                if(stroka.Count()!= 0)
                    result.Add(stroka);
            }

            return result;
        }
        public List<List<string>> Convertt(List<string> list) //конвертируем числа в буквы
        {
            List<List<string>> result = new List<List<string>>();
            for (int i = 0; i < list.Count(); i++)
                result.Add(new List<string>());
            for(int i = 0; i < list.Count; i++)
            {
                string str = list[i];
                for(int j = 0; j < str.Length; j++)
                {
                    string a = "";
                    a = a + str[j];
                    result[i].Add(Convert.ToString(Convert.ToChar(65 + int.Parse(a))));
                }
            }
            return result;
        }
        public List<string> nahojdenieMin(List<string> list)
        {
            List<string> result = new List<string>();
            int[] mas = new int[list.Count()];
            for (int i = 0; i < mas.Length; i++)
                mas[i] = list[i].Count()-1;
            int min = mas.Min();
            for (int i = 0; i < mas.Length; i++)
                if (mas[i] == min) result.Add(list[i]);

            return result;
        }
        public List<List<string>> HromGraph(List<string> list, List<string> magu)
        {
            List<List<string>> result = new List<List<string>>();
            for (int i = 0; i < list.Count(); i++)
                result.Add(new List<string>());

            List<string> chisla = new List<string>();
            // сконвертируем данные на численные представления
            for (int i = 0; i < list.Count(); i++)
            {
                string str = list[i];
                string str2 = "";
                for(int j = 0; j < list[i].Length; j++)           
                    str2 += Convert.ToString(  ( (Convert.ToInt32(str[j])) - 65));
                chisla.Add(str2);
            }

            //сопоставляем 
            
            for (int i = 0; i < chisla.Count(); i++)
            {                                                   //с кода 48 начинатеся 0, с кода 65 начинается английская А
                SortedSet<char> vershini = new SortedSet<char>();
                string str = chisla[i];
                for (int j = 0; j < str.Length; j++)
                {
                    SortedSet<char> prom = new SortedSet<char>();    //промежуточное множество
                    int index = (Convert.ToInt32(Convert.ToChar(str[j]))) - 48; //нахождение мн-ва из листа магу

                    string str2 = magu[index];
                    for (int t = 0; t < str2.Length; t++)
                        prom.Add(str2[t]);

                    IEnumerable<char> element = prom.Except(vershini);
                    string elementStr = "";
                    foreach (char a in element)
                        elementStr = elementStr + a;

                    result[i].Add(elementStr);

                    foreach (char a in prom)
                        vershini.Add(a);
                    
                }
            }


            return result;
        }
    }
}
