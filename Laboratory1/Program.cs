using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Laboratory1
{
    class Program
    {
        static void Main(string[] args) 
        {
              //string sudokuPattern = "000079065000003002005060093340050106000000000608020059950010600700600000820390000";
               string sudokuPattern = "002008050000040070480072000008000031600080005570000600000960048090020000030800900";
            //  String sudokuPattern = "219685743400000001800000005600000007547218936900000004100000008700000009386549172";


         SudokuState startState = new SudokuState ( sudokuPattern ) ;
            startState.Print();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            SudokuSearch searcher = new SudokuSearch ( startState ) ;
         searcher . DoSearch () ;

            
            IState state = searcher . Solutions [0];

         List < SudokuState > solutionPath = new List < SudokuState >() ;
             while ( state != null ) 
            {
             solutionPath.Add(( SudokuState )state) ;
                state = state.Parent;
            }

            stopwatch.Stop();

            solutionPath.Reverse() ;
            foreach ( SudokuState s in solutionPath )
            {
                Console.Clear();
                s.Print () ;
              Console.ReadKey();
            }
            Console.WriteLine();
            Console.WriteLine("Czas obliczen: {0}", stopwatch.Elapsed);

            Console.ReadKey();
               
        }

    }
}
