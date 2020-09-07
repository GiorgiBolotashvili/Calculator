using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    class MyForm : Form
    {
        private List<Button> _buttons;
        private Stack<string> _symbol;
        private Stack<string> _number;
        private Stack<bool> _check;
        private Color _myColor;
        private Label _label;
        private TextBox _historyTextBox;
        private int _sizeX;
        private int _sizeY;
        private int _count;
        private int _counter;
        private StringBuilder _labelAllText;
        private StringBuilder _history;
        private string _font = "DIGITALDREAM";
        private bool _checkHistory = true;

        public MyForm(int sizeX, int sizeY, Color color)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _myColor = color;
            _count = 20;
            _counter = 0;
            _symbol = new Stack<string>();
            _number = new Stack<string>();
            _labelAllText = new StringBuilder();
            _history = new StringBuilder();
            _check = new Stack<bool>();
            _buttons = new List<Button>();

            _label = new Label();
            _label.Location = new Point(10, 50);
            _label.Size = new Size(sizeX * 4, sizeY / 2 * 3);
            _label.BackColor = Color.WhiteSmoke;
            _label.TextAlign = ContentAlignment.BottomRight;
            _label.Text = "MY CALCULATOR";
            _label.Font = new Font(_font, Convert.ToInt16(sizeX / 10 * 4));
            _label.BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(_label);

            CreateNumber();
            CreateSymbol();
            CreateLocations();
            CreateText();
            AddButtons();
            AddHistory();
        }

        private void AddHistory()
        {
            Button button = new Button();
            button.Size = new Size(100, 40);
            button.BackColor = Color.AntiqueWhite;
            button.ForeColor = Color.DarkBlue;
            button.Location = new Point(230, 10);
            button.Text = "H i s t o r y";
            button.Font = new Font(_font, Convert.ToInt16(_sizeX / 20 * 3), FontStyle.Bold);
            button.Click += CreateHistory;
            Controls.Add(button);
        }

        private void CreateHistory(object sender, EventArgs e)
        {
            Button button = new Button();
            _historyTextBox = new TextBox();
            if (_checkHistory)
            {
                _historyTextBox.Multiline = true;
                _historyTextBox.Size = new Size(_sizeX * 4, _sizeY * 6);
                _historyTextBox.Location = new Point(100, 50);
                _historyTextBox.BackColor = Color.WhiteSmoke;
                _historyTextBox.TextAlign = HorizontalAlignment.Left;
                _historyTextBox.Text = _history.ToString();
                _historyTextBox.Name = "h";
                _historyTextBox.Font = new Font(_font, Convert.ToInt16(_sizeX / 5));
                Controls.Add(_historyTextBox);
                _historyTextBox.BringToFront();
                
                button.Size = new Size(100, 25);
                button.BackColor = Color.AntiqueWhite;
                button.ForeColor = Color.DarkBlue;
                button.Location = new Point(115, 15);
                button.Text = "Clear History";
                button.Name = "CH";
                button.Font = new Font(_font, Convert.ToInt16(_sizeX / 8), FontStyle.Bold);
                button.Click += ClearHistory;
                Controls.Add(button);
                _checkHistory = false;
            }
            else
            {
                Controls.RemoveByKey("CH");
                Controls.RemoveByKey("h");
                _checkHistory = true;
            }
        }

        private void ClearHistory(object sender, EventArgs e)
        {
            _history.Clear();
            _historyTextBox.Text = _history.ToString();
        }

        private void CreateSymbol()
        {
            _symbol.Push("+");
            _symbol.Push("=");
            _symbol.Push(".");
            _symbol.Push("-");
            _symbol.Push("*");
            _symbol.Push("/");
            _symbol.Push("<<");
            _symbol.Push("c");
            _symbol.Push("^");
            _symbol.Push("%");
        }

        private void CreateNumber()
        {
            for (int i = 0; i < 10; i++)
            {
                _number.Push(i.ToString());
            }
        }

        private void AddButtons()
        {
            for (int i = 0; i < _count; i++)
            {
                _buttons[i].Size = new Size(_sizeX, _sizeY);
                _buttons[i].Font = new Font(_font, Convert.ToInt16(_sizeX / 10 * 3), FontStyle.Bold);
                Controls.Add(_buttons[i]);
            }
        }

        private void CreateText()
        {
            for (int i = 1; i < _count + 1; i++)
            {
                if (_symbol.Count != 0 && _symbol.Peek() == "c")
                {
                    _buttons[i - 1].Text = _symbol.Pop();
                    _buttons[i - 1].Click += ClearAll;
                }
                else if (_symbol.Count != 0 && _symbol.Peek() == "<<")
                {
                    _buttons[i - 1].Text = _symbol.Pop();
                    _buttons[i - 1].Click += ClearButton;
                }
                else if (_symbol.Count != 0 && _symbol.Peek() == ".")
                {
                    _buttons[i - 1].Text = _symbol.Pop();
                    _buttons[i - 1].Click += ClickButtonNumber;
                }
                else if (_symbol.Count != 0 && _symbol.Peek() == "=")
                {
                    _buttons[i - 1].Text = _symbol.Pop();
                    _buttons[i - 1].Click += Calculations;
                }
                else if (i > 3 && i % 4 != 0 && _number.Count != 0)
                {

                    _buttons[i - 1].Text = _number.Pop();
                    _buttons[i - 1].Click += ClickButtonNumber;
                }
                else if (_symbol.Count != 0)
                {
                    _buttons[i - 1].Text = _symbol.Pop();
                    _buttons[i - 1].Click += ClickButtonSymbol;
                }
            }
        }

        private void CreateLocations()
        {
            for (int i = 0, j = 0; i < _count; i++, j++)
            {
                Button button = new Button();
                button.BackColor = _myColor;
                button.ForeColor = Color.Black;
                if (i % 4 == 0)
                {
                    j = 0;
                    _counter += _sizeX;
                }
                int x = 10 + j * _sizeX;
                int y = (_sizeY / 2 * 3) + _counter;
                button.Location = new Point(x, y);
                _buttons.Add(button);
            }
        }

        private void ClickButtonSymbol(object obj, EventArgs e)
        {
            var button = obj as Button;
            _labelAllText.Append(button.AccessibilityObject.Name);
            _label.Text = _labelAllText.ToString();
            _check.Push(false);
        }

        private void ClickButtonNumber(object obj, EventArgs e)
        {
            var button = obj as Button;
            _labelAllText.Append(button.AccessibilityObject.Name);
            _label.Text = _labelAllText.ToString();
            _check.Push(true);
        }

        private void ClearAll(object obj, EventArgs e)
        {
            _history.Append(_labelAllText);
            _labelAllText.Clear();
            _label.Text = _labelAllText.ToString();
            _check.Clear();
        }

        private void ClearButton(object obj, EventArgs e)
        {
            if (_labelAllText.Length == 0)
                return;
            int length = _labelAllText.Length;
            if (_labelAllText.Length != 0)
            {
                _labelAllText.Remove(length - 1, 1);
            }
            _label.Text = _labelAllText.ToString();
            _check.Pop();
        }

        private void Calculations(object obj, EventArgs e)
        {
            MyCalculations myCalculations = new MyCalculations(_labelAllText, _check);
            if (_labelAllText.Length != 0)
            {
                _history.Append(_labelAllText);
                _label.Text = myCalculations.UseButtons();
                _label.Text = myCalculations.Calculations();
                _history.Append(" = " + _label.Text);
                _history.Append(", \r\n");
            }
            _labelAllText.Clear();
        }
    }
}
