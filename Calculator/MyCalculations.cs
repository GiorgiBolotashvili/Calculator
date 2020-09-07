using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class MyCalculations
    {
        private StringBuilder _stringNumber;
        private StringBuilder _text;
        private Queue<double> _number;
        private Queue<string> _symbol;
        private Queue<bool> _check;
        private string _answer;
        public MyCalculations(StringBuilder text, Stack<bool> check)
        {
            _stringNumber = new StringBuilder();
            _answer = "";
            _text = new StringBuilder();
            _text = text;
            _number = new Queue<double>();
            _symbol = new Queue<string>();
            _check = new Queue<bool>();
            StackToQueue(check);
        }
        private void StackToQueue(Stack<bool> check)
        {
            List<bool> items = new List<bool>();
            while (check.Count != 0)
            {
                items.Add(check.Pop());
            }
            for (int i = items.Count - 1; i >= 0; i--)
            {
                _check.Enqueue(items[i]);
            }
        }

        public string UseButtons()
        {
            while (_text.Length != 0)
            {
                if (_check.Count != 0 && _text[0] == '-')
                {
                    if (_stringNumber.Length != 0)
                    {
                        if (_stringNumber[_stringNumber.Length - 1] == '-')
                        {
                            _stringNumber.Clear();
                        }
                        else
                        {
                            _number.Enqueue(Convert.ToDouble(_stringNumber.ToString()));
                            _stringNumber.Clear();
                            _stringNumber.Append(_text[0]);
                            _symbol.Enqueue("+");
                        }
                    }
                    else
                    {
                        _stringNumber.Append(_text[0]);
                    }
                    _text.Remove(0, 1);
                    _check.Dequeue();
                }
                else if (_check.Count != 0 && _check.Peek())
                {
                    _stringNumber.Append(_text[0]);
                    _text.Remove(0, 1);
                    _check.Dequeue();
                }
                else if (_check.Count != 0)
                {
                    try
                    {
                        _number.Enqueue(Convert.ToDouble(_stringNumber.ToString()));
                        _stringNumber.Clear();
                        _symbol.Enqueue(_text[0].ToString());
                        _text.Remove(0, 1);
                        _check.Dequeue();
                    }
                    catch (Exception ex)
                    {
                        _answer = ex.Message;
                        _text.Remove(0, 1);
                        _check.Dequeue();
                    }
                    if (_check.Count != 0 && _text[0] == '-')
                    {
                        _stringNumber.Append(_text[0]);
                        _text.Remove(0, 1);
                        _check.Dequeue();
                    }
                }
            }
            try
            {
                _number.Enqueue(Convert.ToDouble(_stringNumber.ToString()));
            }
            catch (Exception ex)
            {
                _answer = ex.Message;
                if (_text.Length != 0)
                {
                    _text.Remove(0, 1);
                    _check.Dequeue();
                }
            }
            _stringNumber.Clear();
            return _answer;
        }

        public string Calculations()
        {
            if (_number.Count==0)
            {
                return "";
            }
            double answer = _number.Dequeue();
            while (_number.Count != 0 && _symbol.Count != 0)
            {

                answer = CalculationsMethod(_symbol.Dequeue(), answer, _number.Dequeue());
            }
            return _answer = answer.ToString();
        }

        private double CalculationsMethod(string symbol, double first, double second)
        {
            double answer = 0;
            switch (symbol)
            {
                case "+":
                    answer = first + second;
                    break;
                case "-":
                    answer = first - second;
                    break;
                case "*":
                    answer = first * second;
                    break;
                case "/":
                    answer = first / second;
                    break;
                case "%":
                    answer = first / 100 * second;
                    break;
                case "^":
                    answer = Math.Pow(first, second);
                    break;
            }
            return answer;
        }
    }
}
