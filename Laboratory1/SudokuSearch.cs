using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuSearch : AStarSearch
    {
        public SudokuSearch(SudokuState state) : base(state, true, true) { }

        protected override void buildChildren(IState parent)
        {
            SudokuState state = (SudokuState)parent;

            // poszukiwanie wolnego pola
            for (int i = 1; i < 10; i++)
            {
                if (state._tab[i] == 0)
                {
                    SudokuState child = new SudokuState(state, i, state._x, state._y);
                    parent.Children.Add(child);
                }
            }
        }
    

        protected override bool isSolution(IState state)
        {
            SudokuState s = (SudokuState)state;
             return s.Rozwiazanie();
        //    return state.H == 0.0;
        }
    }
}
