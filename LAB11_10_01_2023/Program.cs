using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace LAB11_10_01_2023
{
    class Kolejka
    {
        private int[] kolejka;
        private int poczatek;
        private int koniec;
        private int rozmiar;

        public Kolejka()
        {
            kolejka = new int[10];
            poczatek = 0;
            koniec = 0;
            rozmiar = 0;
        }

        public Kolejka(int rozmiarKol)
        {
            kolejka = new int[rozmiarKol];
            poczatek = 0;
            koniec = 0;
            rozmiar = 0;
        }
        //Zad. 1 a) Do klasy Kolejka dodać konstruktor Kolejka(Kolejka k), który tworzy nową kolejkę o rozmiarze
        //3 razy większym niż kolejka1c k i dodaje elementy z kolejki k.
        public Kolejka(Kolejka k)
        {
            kolejka = new int[3*k.rozmiar];
            poczatek = 0;
            koniec = 0;
            rozmiar = 0;
            for (int i = (k.poczatek % k.kolejka.Length); i < (k.poczatek % k.kolejka.Length) + k.rozmiar; i++)
                this.dodaj(k.kolejka[i % kolejka.Length]);
        }

        //Zad. 1 b) Do klasy Kolejka dodać metodę Stos pobierzStos(int n), która zwraca stos1c o rozmiarze 2 * n,
        //złożony z n elementów z aktualnej kolejki (jeśli jest mniej niż n elementów to przepisuje tyle ile jest w kolejce).
        public Stos pobierzStos(int n)
        {
            Stos stos = new Stos(2 * n);
            if (n > rozmiar)
                n = rozmiar;
            for (int i = (poczatek % kolejka.Length); i < (poczatek % kolejka.Length) + n; i++)
                stos.push(kolejka[i % kolejka.Length]);
            return stos;
        }

        public void dodaj(int element)
        {
            if (!czyPelna())
            {
                kolejka[koniec % kolejka.Length] = element;
                koniec++;
                rozmiar++;
                Console.WriteLine("Dodano element: " + element);
            }
            else
            {
                Console.WriteLine("Kolejka jest pełna");
            }
        }

        public int usun()
        {
            if (czyPusta())
            {
                Console.WriteLine("Kolejka jest pusta");
                return int.MinValue;
            }
            int element = kolejka[poczatek % kolejka.Length];
            poczatek++;
            rozmiar--;
            Console.WriteLine("Usunięto element: " +element);
            return element;
        }

        public bool czyPusta()
        {
            return (rozmiar == 0);
        }

        public bool czyPelna()
        {
            return (rozmiar == kolejka.Length);
        }

        public int pobierzPoczatkowy()
        {
            return kolejka[poczatek % kolejka.Length];
        }

        public int pobierzRozmiar()
        {
            return rozmiar;
        }

        public void wyswietl()
        {
            if (!czyPusta())
            {
                for (int i = (poczatek % kolejka.Length); i < (poczatek % kolejka.Length) + rozmiar; i++) Console.Write(kolejka[i % kolejka.Length] + " ");
            }
            else Console.WriteLine("Kolejka jest pusta");
        }

        public int Pojemnosc
        { get => kolejka.Length; }
    }

    class Stos
    {
        private int rozmiar;
        private int[] stos;
        private int top;

        public Stos(int rozmiar)
        {
            this.rozmiar = rozmiar;
            stos = new int[rozmiar];
            top = -1;
        }

        public bool push(int elem)
        {
            if (top < rozmiar - 1)
            {
                stos[++top] = elem;
                return true;
            }
            else
                return false;
        }

        public int pop()
        {
            if (top >= 0)
                return stos[top--];
            else
                return int.MinValue;//min wartosc to blad
        }

        public bool czyPusty()
        {
            return (top == -1);
        }

        public int AktualnyRozmiar
        {
            get
            {
                return top + 1;
            }
        }

        public int[] AktualnyStos
        {
            get
            {
                int[] aktualnyStos = new int[top + 1];
                for (int i = 0; i < top + 1; i++)
                    aktualnyStos[i] = stos[i];
                return aktualnyStos;
            }
        }

        public int Pojemnosc => stos.Length;

        public void wyswietl()
        {
            if (!czyPusty())
            {
                for (int i = 0; i < top + 1; i++) Console.Write(stos[i] + " ");
            }
            else Console.WriteLine("Stos jest pusty");
        }
    }

    #region klasy generyczne
    class Kolejka<T>
    {
        private T[] kolejka;
        private int poczatek;
        private int koniec;
        private int rozmiar;

        public Kolejka()
        {
            kolejka = new T[10];
            poczatek = 0;
            koniec = 0;
            rozmiar = 0;
        }

        public Kolejka(int rozmiarKol)
        {
            kolejka = new T[rozmiarKol];
            poczatek = 0;
            koniec = 0;
            rozmiar = 0;
        }

        public void dodaj(T element)
        {
            if (!czyPelna())
            {
                kolejka[koniec % kolejka.Length] = element;
                koniec++;
                rozmiar++;
                Console.WriteLine("Dodano element: " + element);
            }
            else
            {
                Console.WriteLine("Kolejka jest pełna");
            }
        }

        public T usun()
        {
            if (czyPusta())
            {
                Console.WriteLine("Kolejka jest pusta");
                return default(T);
            }
            T element = kolejka[poczatek % kolejka.Length];
            poczatek++;
            rozmiar--;
            Console.WriteLine("Usunięto element: " + element);
            return element;
        }

        public bool czyPusta()
        {
            return (rozmiar == 0);
        }

        public bool czyPelna()
        {
            return (rozmiar == kolejka.Length);
        }

        public T pobierzPoczatkowy()
        {
            return kolejka[poczatek % kolejka.Length];
        }

        public int pobierzRozmiar()
        {
            return rozmiar;
        }

        public void wyswietl()
        {
            if (!czyPusta())
            {
                for (int i = (poczatek % kolejka.Length); i < (poczatek % kolejka.Length) + rozmiar; i++) Console.Write(kolejka[i % kolejka.Length] + " ");
            }
            else Console.WriteLine("Kolejka jest pusta");
        }

        public int Pojemnosc
        {
            get => kolejka.Length;
        }
    }

    class Stos<T>
    {
        private int rozmiar;
        private T[] stos;
        private int top;

        public Stos(int rozmiar)
        {
            this.rozmiar = rozmiar;
            stos = new T[rozmiar];
            top = -1;
        }

        public bool push(T elem)
        {
            if (top < rozmiar - 1)
            {
                stos[++top] = elem;
                return true;
            }
            else
                return false;
        }

        public T pop()
        {
            if (top >= 0)
                return stos[top--];
            else
                return default(T);
        }

        public bool czyPusty()
        {
            return (top == -1);
        }

        public int AktualnyRozmiar
        {
            get
            {
                return top + 1;
            }
        }

        public T[] AktualnyStos
        {
            get
            {
                T[] aktualnyStos = new T[top + 1];
                for (int i = 0; i < top + 1; i++)
                    aktualnyStos[i] = stos[i];
                return aktualnyStos;
            }
        }

        public int Pojemnosc => stos.Length;

        public void wyswietl()
        {
            if (!czyPusty())
            {
                for (int i = 0; i < top + 1; i++) Console.Write(stos[i] + " ");
            }
            else Console.WriteLine("Stos jest pusty");
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            int seed = 01012023;
            Random random = new Random(seed);

            #region Zad. 1
            //a) Do klasy Kolejka dodać konstruktor Kolejka(Kolejka k),
            //który tworzy nową kolejkę o rozmiarze 3 razy większym niż kolejka1c k i dodaje elementy z kolejki k.
            Console.WriteLine("Zad. 1\na)");
            Kolejka kolejka1a1 = new Kolejka(4);
            kolejka1a1.dodaj(2); kolejka1a1.dodaj(4); kolejka1a1.dodaj(5); kolejka1a1.dodaj(8);
            Kolejka kolejka1a2 = new Kolejka(kolejka1a1);
            Console.Write($"Oryginalna kolejka o rozmiarze {kolejka1a1.Pojemnosc}: ");
            kolejka1a1.wyswietl();
            Console.Write($"\nStworzona na bazie pierwszej kolejka o rozmiarze {kolejka1a2.Pojemnosc}: ");
            kolejka1a2.wyswietl();
            Console.WriteLine();

            //b) Do klasy Kolejka dodać metodę Stos pobierzStos(int n), która zwraca stos o rozmiarze 2 * n,
            //złożony z n elementów z aktualnej kolejki(jeśli jest mniej niż n elementów to przepisuje tyle ile jest w kolejce).
            Kolejka kolejka1b = kolejka1a1;
            int n = 4;
            Stos stos1b = kolejka1a1.pobierzStos(n);
            Console.Write($"b)\nZ kolejki o rozmiarze {kolejka1b.Pojemnosc}: ");
            kolejka1b.wyswietl();
            Console.Write($"\nStworzono stos o rozmiarze {stos1b.Pojemnosc}: ");
            stos1b.wyswietl();
            Console.WriteLine();

            //c) Utworzyć kolejkę o dowolnym rozmiarze. Wypełnić losowymi cyframi. Odwrócić zawartość elementów
            //w kolejce z wykorzystaniem dodatkowej kolejki lub dodatkowego stosu.
            Console.WriteLine("c)");
            Kolejka kolejka1c = new Kolejka(8);
            for (int i = 0; i < 8; i++)
                kolejka1c.dodaj(random.Next(10));
            Stos stos1c = kolejka1c.pobierzStos(8);
            Console.Write($"Kolejka przed odwróceniem: ");
            kolejka1c.wyswietl();
            Console.Write("\n");
            kolejka1c = new Kolejka(8);
            while (!stos1c.czyPusty())
                kolejka1c.dodaj(stos1c.pop());
            Console.Write($"Kolejka po odwróceniu: ");
            kolejka1c.wyswietl();
            #endregion

            #region Zad. 2
            //Zad. 2: Napisać metodę Queue<int> CopyFrom(Stack<int> stos), która przepisuje elementy parzyste ze stosu do zwracanej kolejki.
            //W stosie mają pozostać tylko elementy nieparzyste w kolejności takiej jak były przed wywołaniem metody.
            Console.WriteLine("\n\nZad. 2");
            Stack<int> stack2 = new Stack<int>(8);
            for(int i=0; i<8; i++)
                stack2.Push(random.Next(10));
            Console.WriteLine($"Stos przed wywołaniem metody: {String.Join(", ", stack2.ToArray())}");
            Queue<int> queue2 = CopyFrom(stack2);
            Console.WriteLine($"Stos po wywołaniu metody: {String.Join(", ", stack2.ToArray())}");
            Console.WriteLine($"Kolejka parzystych elementów stosu: {String.Join(", ", queue2.ToArray())}");
            #endregion

            #region Zad. 3
            //Zad. 3: Utworzyć stos Stack<string> i wypełnić losowymi napisami 3-znakowymi złożonymi z liter.
            //Napisać metodę usuwającą elementy rozpoczynające się od liter ‚a’, ‚b’ lub ‚c’.
            Console.WriteLine("\nZad. 3");
            char[] letters = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
            Stack<string> stack3 = new Stack<string>(12);
            for (int i = 0; i < 16; i++)
                stack3.Push(String.Concat(letters[random.Next(letters.Length)], letters[random.Next(letters.Length)], letters[random.Next(letters.Length)]));
            Console.WriteLine($"Stos przed usunięciem określonych elementów:\t{String.Join(", ", stack3.ToArray())}");
            removeFromStack(stack3);
            Console.WriteLine($"Stos po usunięciu określonych elementów:\t{String.Join(", ", stack3.ToArray())}");
            #endregion

            #region Zad. 4
            /*Zad. 4: Wygenerować losowo słownik Dictionary<string, int> zawierający 200 elementów, w którym klucz jest w postaci cyfra + litera a wartość jest z zakresu <0, 100>.
            a) Jaka jest średnia wartość elementów?
            b) Ile jest elementów > średniej? Który element ma wartość najbardziej zbliżoną do średniej?
            c) Ile jest kluczy z przypisaną wartością 55 lub 60?
            ictionary4) Ile kluczy zawiera cyfrę 4 lub dużą literę?*/
            Console.WriteLine("\nZad. 4");
            char[] allLetters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
            Dictionary<string, int> ictionary4 = new Dictionary<string, int>(200);
            while (ictionary4.Count < 200)
            {
                string tempKey = random.Next(10).ToString() + allLetters[random.Next(letters.Length)];
                if (!ictionary4.ContainsKey(tempKey))
                    ictionary4.Add(tempKey, random.Next(101));
            }

            //a)
            int sum = 0;
            foreach (KeyValuePair<string, int> kvp in ictionary4)
                sum += kvp.Value;
            double avg = (double)sum / 200;
            Console.WriteLine($"\na) : Średnia wartość elementów w słowniku to: {avg}");
            
            //b)
            int greaterThanAvgCount = 0;
            KeyValuePair<string, int> closeKeyValuePair = new KeyValuePair<string, int>("null", int.MaxValue);
            foreach (KeyValuePair<string, int> kvp in ictionary4)
            {
                if (kvp.Value > avg)
                {
                    greaterThanAvgCount++;
                    if(kvp.Value < closeKeyValuePair.Value)
                        closeKeyValuePair = kvp;
                }
            }
            Console.WriteLine($"b) : Jest {greaterThanAvgCount} większej od średniej, i element ({closeKeyValuePair.Key}, {closeKeyValuePair.Value}) jest najbardziej zbliżony do średniej");
            
            //c)
            int valueCount = 0;
            foreach (KeyValuePair<string, int> kvp in ictionary4)
            {
                if (kvp.Value == 55 || kvp.Value == 60)
                    valueCount++;
            }
            Console.WriteLine($"c) :Jest {valueCount} wartości z przypisaną wartością 55 lub 60");
            
            //d)
            int keyCount = 0;
            foreach (string s in ictionary4.Keys)
                keyCount += Regex.IsMatch(s, @"(4)|(\p{Lu})") == true ? 1 : 0;
            Console.WriteLine($"ictionary4) : Jest {keyCount} kluczy zawierający cyfrę 4 lub wielką literę");
            #endregion

            #region testowanie kolekcji generycznych
            Console.WriteLine("\nTestowanie kolekcji generycznych\n");
            //kolejka
            Console.WriteLine("Kolejka");
            Kolejka<char> kolejkaT = new Kolejka<char>(10);
            for (int i = 0; i < 15; i++)
                kolejkaT.dodaj(letters[random.Next(letters.Length)]);
            for (int i = 0; i < 15; i++)
                kolejkaT.usun();
            //stos
            Console.WriteLine("\nStos");
            Stos<char> stosT = new Stos<char>(10);
            Console.Write("Po próbie dodania 15 elementów z pojemnością 10: ");
            for (int i = 0; i < 15; i++)
                stosT.push(letters[random.Next(letters.Length)]);
            stosT.wyswietl();
            Console.Write("\nPo usunięciu 8 elementów: ");
            for (int i = 0; i < 8; i++)
                stosT.pop();
            stosT.wyswietl();
            #endregion
        }

        #region metody pomocnicze
        public static Queue<int> CopyFrom(Stack<int> stos)
        {
            Queue<int> queue = new Queue<int>();
            Stack<int> oddStack = new Stack<int>();
            while (stos.Count > 0)
            {
                if (stos.Peek() % 2 == 0)
                    queue.Enqueue(stos.Pop());
                else
                    oddStack.Push(stos.Pop());
            }
            while (oddStack.Count > 0)
                stos.Push(oddStack.Pop());
            return queue;
        }

        public static void removeFromStack(Stack<string> s)
        {
            Stack<string> tempStack = new Stack<string>();
            while (s.Count > 0)
            {
                if (Regex.IsMatch(s.Peek(), @"(^a)|(^b)|(^c)"))
                    s.Pop();
                else
                    tempStack.Push(s.Pop());
            }
            while (tempStack.Count > 0)
                s.Push(tempStack.Pop());
        }
        #endregion
    }
}
