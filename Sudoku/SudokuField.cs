using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SudokuField
// by Daniel Cerman
// 2015-12-12

namespace Sudoku
{
    public class SudokuField
    {
        const int MinPos = 0, MaxPos = 8;
        const int MinNum = 1, MaxNum = 9;
        
        private int[,] _field;

        public int[,] Field
        {
            get { return _field; }
            set {
                if (value.Length == (MaxPos+1) * (MaxPos+1))
                {
                    // Initialize cell-by-cell to normalize out-of-range values to 0.
                    for (int row = MinPos; row <= MaxPos; row++)
                    {
                        for (int col = MinPos; col <= MaxPos; col++)
                        {
                            SetCell(row, col, value[row, col]);
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", "int[9,9] required.");
                }
            }
        }

        public SudokuField() :
            this(new int[MaxPos+1, MaxPos+1])
        {}

        public SudokuField(int[,] initialField)
        {
            _field = initialField;
        }

        public int GetCell(int row, int col) {
            if (row < MinPos || row > MaxPos) throw new ArgumentOutOfRangeException();
            if (col < MinPos || col > MaxPos) throw new ArgumentOutOfRangeException();
            return _field[row, col];
        }

        public int[] GetZone(int zone)
        {
            if (zone < MinPos || zone > MaxPos) throw new ArgumentOutOfRangeException();
            int[] values = new int[MaxPos+1];
            int row, column;

            for (int position = MinPos; position < MaxPos; position++)
            {
                row = zone / 3 * 3 + position / 3;
                column = zone % 3 * 3 + position % 3;
                values[position] = _field[row, column];
            }
            return values;
        }

        public void SetCell(int row, int column, int number)
        {
            if (row < MinPos || row > MaxPos) throw new ArgumentOutOfRangeException();
            if (column < MinPos || column > MaxPos) throw new ArgumentOutOfRangeException();
            if (number >= MinNum && number <= MaxNum)
            {
                _field[row, column] = number;
            }
            else
            {
                // Treat out-of-range numbers as blank cells, represented by 0
                _field[row, column] = 0;
            }
        }

        public bool IsValidColumn(int column)
        {
            int[] appears = new int[10];
            for (int row = MinPos; row < MaxPos; row++)
            {
                int number = _field[row, column];
                if (number < MinNum || number > MaxNum)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public bool IsValidField()
        {
            for (int pos = MinPos; pos <= MaxPos; pos++)
            {
                if (!IsValidRow(pos)) return false;
                if (!IsValidColumn(pos)) return false;
                if (!IsValidZone(pos)) return false;
            }
            return true;
        }
        
        public bool IsValidRow(int row)
        {
            int[] appears = new int[10];
            for (int col = MinPos; col <= MaxPos; col++)
            {
                int number = _field[row, col];
                if (number < MinNum || number > MaxNum)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public bool IsValidZone(int zone)
        {
            int[] values = GetZone(zone);

            int[] appears = new int[10];
            for (int position = MinPos; position <= MaxPos; position++)
            {
                int number = values[position];
                if (number < MinNum || number > MaxNum)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int r = MinPos; r <= MaxPos; r++)
            {
                if (r % 3 == 0)
                {
                    sb.AppendLine("+-------+-------+-------+");
                }
                for (int c = MinPos; c <= MaxPos; c++)
                {
                    if (c % 3 == 0)
                    {
                        sb.Append("| ");
                    }
                    if (_field[r, c] >= MinNum && _field[r, c] <= MaxNum)
                    {
                        sb.Append(_field[r, c].ToString() + ' ');
                    }
                    else
                    {
                        sb.Append("  ");
                    }
                }
                sb.AppendLine("|");
            }
            sb.AppendLine("+-------+-------+-------+");
            return sb.ToString();
        }
        
    }
}
