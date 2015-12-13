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
        private int[,] _field;

        public int[,] Field
        {
            get { return _field; }
            set {
                if (value.Length == 81)
                {
                    _field = value;
                }
            }
        }

        public SudokuField() :
            this(new int[9,9])
        {
        }

        public SudokuField(int[,] initialField)
        {
            _field = initialField;
        }

        public int GetCell(int row, int column) {
            if (row < 0 || row > 8) throw new ArgumentOutOfRangeException();
            if (column < 0 || column > 8) throw new ArgumentOutOfRangeException();
            return _field[row, column];
        }

        public int[] GetZone(int zone)
        {
            if (zone < 0 || zone > 8) throw new ArgumentOutOfRangeException();
            int[] values = new int[9];

            for (int position = 0; position < 9; position++)
            {
                values[position] = _field[zone/3*3+position/3, zone%3*3+position%3];
            }
            return values;
        }

        public void SetCell(int row, int column, int number)
        {
            if (row < 0 || row > 8) throw new ArgumentOutOfRangeException();
            if (column < 1 || column > 9) throw new ArgumentOutOfRangeException();
            _field[row, column] = number;
        }

        public bool IsValidColumn(int column)
        {
            int[] appears = new int[10];
            for (int row = 0; row < 9; row++)
            {
                int number = _field[row, column];
                if (number < 1 || number > 9)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public bool IsValidField()
        {
            for (int rcz = 0; rcz < 9; rcz++)
            {
                if (!IsValidRow(rcz)) return false;
                if (!IsValidColumn(rcz)) return false;
                if (!IsValidZone(rcz)) return false;
            }
            return true;
        }
        
        public bool IsValidRow(int row)
        {
            int[] appears = new int[10];
            for (int column = 0; column < 9; column++)
            {
                int number = _field[row, column];
                if (number < 1 || number > 9)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public bool IsValidZone(int zone)
        {
            int[] values = GetZone(zone);

            int[] appears = new int[10];    // 0 is blank
            for (int position = 0; position < 9; position++)
            {
                int number = values[position];
                if (number < 1 || number > 9)
                    continue;
                if (++appears[number] > 1)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < 9; r++)
            {
                if (r % 3 == 0)
                {
                    sb.AppendLine("+-------+-------+-------+");
                }
                for (int c = 0; c < 9; c++)
                {
                    if (c % 3 == 0)
                    {
                        sb.Append("| ");
                    }
                    if (_field[r, c] >= 1 && _field[r, c] <= 9)
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
