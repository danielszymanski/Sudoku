using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuState : State
    {

        public int _x, _y;
        public int[] _tab = new int[10];
        public const int SMALL_GRID_SIZE = 3;

        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;

        private string id;
        private int[,] table;
        public int[] tab;

        public override string ID
        {
            get { return this.id; }
        }


        public void Print()
        {
            for (int i = 0; i < GRID_SIZE; ++i)
            {
              if (i % 3 == 0) Console.WriteLine("-------------"); 
                
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    if (j % 3 == 0) Console.Write("|");
                    if (table[i, j] == 0) Console.Write(" ");
                    else
                    Console.Write( table[i, j]);
                }
                Console.WriteLine();
            }
                }

       
        
        public bool Rozwiazanie()
        {
            foreach (int i in table)
            {
                if (i == 0)
                    return (false);
            }
            return (true);
        }


        public override double ComputeHeuristicGrade()
        { 
            int x, y,licz,n;

            licz = 10;
            n = 9;

            x = 0;
            y = 0;
           

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (table[i, j] == 0)
                    {
                        licz = 0;
                        tab = new int[10] {0,0,0,0,0,0,0,0,0,0};

                        for (int k = 0; k < 9; k++)
                        {
                            tab[table[i, k]] = 1;
                            tab[table[k, j]] = 1;
                   
                           tab[table[(i / 3) * 3 + k / 3, (j / 3) * 3 + k % 3]] = 1;

                        }
                        for (int k = 1; k < 10; k++)
                            if (tab[k]==0) licz++;
                    }

                    if (licz < n)
                    {
                        n = licz;
                        x = i;
                        y = j;

                        Array.Copy(tab, _tab, 10);
                    }
                }
            }   
            
            _x = x;
            _y = y;

            this.h = n;
            return n;
        }

        public SudokuState(string sudokuPattern)
            : base()
        {
            if (sudokuPattern.Length != GRID_SIZE * GRID_SIZE)
            {
                throw new ArgumentException(" SudokuSring posiada niewlasciwa dlugosc .");
            }
            // utworzenie id
            this.id = sudokuPattern;
            // stworzenie i wypelnienie wewnetrzej tablicy przechowujacej stan sudoku
            this.table = new int[GRID_SIZE, GRID_SIZE];

            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.table[i, j] = sudokuPattern[i * GRID_SIZE + j] - 48;
                }
            }

            // obliczenie heurystyki
           this.h = ComputeHeuristicGrade();
        }

        public SudokuState(SudokuState parent, int newValue, int x, int y)
            : base(parent)
        {
            this.table = new int[GRID_SIZE, GRID_SIZE];
            // Skopiowanie stanu sudoku do nowej tabeli
            Array.Copy(parent.table, this.table, this.table.Length);
            // Ustawienie nowej wartosci w wybranym polu sudoku
            this.table[x, y] = newValue;

            // Utworzenie nowego id odpowiadajacemu aktualnemu stanowi planszy
            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();

            this.h = ComputeHeuristicGrade();
        }
    }
} 
