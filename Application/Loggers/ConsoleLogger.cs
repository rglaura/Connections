using Application.Constants;
using Entities.Constants;
using Entities.Loggers;
using System;

namespace Application.Loggers
{
    public class ConsoleLogger : Logger
    {
        public override void PrintStatus(string message)
        {
            var spaced = message.PadRight(
                PrintTransmissionConstants.BoxSize,
                PrintTransmissionConstants.SpacerChar);

            WriteChar(PrintTransmissionConstants.BorderChar);

            foreach (var character in spaced)
            {
                switch (character)
                {
                    case PrintTransmissionConstants.SpacerChar:
                        WriteChar(character);
                        break;

                    case TransmissionConstants.FullChar:
                        WriteChar(character, ConsoleColor.Green);
                        break;

                    case TransmissionConstants.EmptyChar:
                        WriteChar(character, ConsoleColor.DarkGray);
                        break;

                    default:
                        WriteChar(character, ConsoleColor.White);
                        break;
                }
            }

            WriteChar(PrintTransmissionConstants.BorderChar);
            Console.WriteLine();
        }

        public override void PrintSeparator(char separator)
        {
            Console.WriteLine(
                $"{ PrintTransmissionConstants.BorderChar}" +
                $"{ new String(separator, PrintTransmissionConstants.BoxSize) }" +
                $"{ PrintTransmissionConstants.BorderChar}");
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public void ReadLine()
        {
            Console.ReadLine();
        }

        public ConsoleKey ReadKey(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var read = Console.ReadKey().Key;
            Console.ResetColor();

            return read;
        }

        #region helpers

        private void WriteChar(char character)
        {
            Console.Write(character);
        }

        private void WriteChar(char character, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(character);
            Console.ResetColor();
        }

        #endregion helpers
    }
}