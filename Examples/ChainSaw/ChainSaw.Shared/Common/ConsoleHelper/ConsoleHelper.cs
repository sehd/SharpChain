using System;
using Con = System.Console;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChainSaw.ConsoleHelper
{
    [ContainAs(typeof(IConsoleHelper))]
    public class ConsoleHelper : IConsoleHelper
    {
        private StringBuilder stringBuilder;
        private int currentIndex, startingLeft, startingTop, previousLeft, previousTop;

        public string CancellableReadLine(CancellationToken cancellationToken)
        {
            stringBuilder = new StringBuilder();
            Task.Run(() =>
            {
                try
                {
                    ConsoleKeyInfo keyInfo;
                    startingLeft = Con.CursorLeft;
                    startingTop = Con.CursorTop;
                    currentIndex = 0;
                    do
                    {
                        previousLeft = Con.CursorLeft;
                        previousTop = Con.CursorTop;
                        while (!Con.KeyAvailable)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            Thread.Sleep(50);
                        }
                        keyInfo = Con.ReadKey();
                        if (keyInfo.Key.IsActiveKey())
                            InsertActiveKey(keyInfo);
                        else
                            InsertInactiveKey(keyInfo);
                    } while (keyInfo.Key != ConsoleKey.Enter);
                    Con.WriteLine();
                }
                catch
                {
                    stringBuilder.Clear();
                }
            }).Wait();
            return stringBuilder.ToString();
        }

        private void InsertActiveKey(ConsoleKeyInfo keyInfo)
        {
            stringBuilder.Insert(currentIndex, keyInfo.KeyChar);
            currentIndex++;
            if (currentIndex < stringBuilder.Length)
            {
                var left = Con.CursorLeft;
                var top = Con.CursorTop;
                Con.Write(stringBuilder.ToString().Substring(currentIndex));
                Con.SetCursorPosition(left, top);
            }
        }

        private void InsertInactiveKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Backspace:
                    currentIndex = Backspace();
                    break;
                case ConsoleKey.Delete:
                    Delete();
                    break;
                case ConsoleKey.LeftArrow:
                    currentIndex = GoLeft();
                    break;
                case ConsoleKey.RightArrow:
                    currentIndex = GoRight();
                    break;
                case ConsoleKey.Home:
                    currentIndex = GoToStart();
                    break;
                case ConsoleKey.End:
                    currentIndex = GoToEnd();
                    break;
                default:
                    Con.SetCursorPosition(previousLeft, previousTop);
                    break;
            }
        }

        private int GoToEnd()
        {
            if (currentIndex < stringBuilder.Length)
            {
                Con.SetCursorPosition(previousLeft, previousTop);
                Con.Write(stringBuilder[currentIndex]);
                var left = previousLeft + stringBuilder.Length - currentIndex;
                var top = previousTop;
                while (left > Con.BufferWidth)
                {
                    left -= Con.BufferWidth;
                    top++;
                }
                currentIndex = stringBuilder.Length;
                Con.SetCursorPosition(left, top);
            }
            else
                Con.SetCursorPosition(previousLeft, previousTop);
            return currentIndex;
        }

        private int GoToStart()
        {
            if (stringBuilder.Length > 0 && currentIndex != stringBuilder.Length)
            {
                Con.SetCursorPosition(previousLeft, previousTop);
                Con.Write(stringBuilder[currentIndex]);
            }
            Con.SetCursorPosition(startingLeft, startingTop);
            currentIndex = 0;
            return currentIndex;
        }

        private int GoRight()
        {
            if (currentIndex < stringBuilder.Length)
            {
                Con.SetCursorPosition(previousLeft, previousTop);
                Con.Write(stringBuilder[currentIndex]);
                currentIndex++;
            }
            else
            {
                Con.SetCursorPosition(previousLeft, previousTop);
            }

            return currentIndex;
        }

        private int GoLeft()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                var left = Con.CursorLeft - 2;
                var top = Con.CursorTop;
                if (left < 0)
                {
                    left = Con.BufferWidth + left;
                    top--;
                }
                Con.SetCursorPosition(left, top);
                if (currentIndex < stringBuilder.Length - 1)
                {
                    Con.Write(stringBuilder[currentIndex].ToString() + stringBuilder[currentIndex + 1]);
                    Con.SetCursorPosition(left, top);
                }
            }
            else
            {
                Con.SetCursorPosition(startingLeft, startingTop);
                if (stringBuilder.Length > 0)
                    Con.Write(stringBuilder[0]);
                Con.SetCursorPosition(startingLeft, startingTop);
            }

            return currentIndex;
        }

        private void Delete()
        {
            if (stringBuilder.Length > currentIndex)
            {
                stringBuilder.Remove(currentIndex, 1);
                Con.SetCursorPosition(previousLeft, previousTop);
                Con.Write(stringBuilder.ToString().Substring(currentIndex) + " ");
                Con.SetCursorPosition(previousLeft, previousTop);
            }
            else
                Con.SetCursorPosition(previousLeft, previousTop);
        }

        private int Backspace()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                stringBuilder.Remove(currentIndex, 1);
                var left = Con.CursorLeft;
                var top = Con.CursorTop;
                if (left == previousLeft)
                {
                    left = Con.BufferWidth - 1;
                    top--;
                    Con.SetCursorPosition(left, top);
                }
                Con.Write(stringBuilder.ToString().Substring(currentIndex) + " ");
                Con.SetCursorPosition(left, top);
            }
            else
            {
                Con.SetCursorPosition(startingLeft, startingTop);
            }

            return currentIndex;
        }
    }
}
