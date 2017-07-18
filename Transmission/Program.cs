using Application;
using Application.Loggers;
using Entities.Builders;
using Entities.Contracts;
using Entities.Entities;
using Entities.Extensions;
using System;
using System.Collections.Generic;

namespace Transmission
{
    internal class Program
    {
        private static ConsoleLogger _logger;

        public static void Main(string[] args)
        {
            _logger = new ConsoleLogger();

            var a = new Information("a");

            var A = new Emitter("A", new List<Information>() { a });
            var B = new TransceiverBuilder("B", 3, 3).Get();
            var C = new TransceiverBuilder("C", 3, 3).Get();
            var D = new TransceiverBuilder("D", 2, 2).Get();

            A.Reaches(B, C, D);
            B.Reaches(C, D);
            C.Reaches(B, D);
            D.Reaches(B, C);

            var level2 = new Level(
                new List<Information> { a },
                new List<INode> { A, B, C, D },
                _logger);

            var transmission = new TransmissionGame(new List<Level> { level2 });

            Play(level2);
        }

        #region helpers

        private static void Play(Level level)
        {
            bool exit;

            level.PrintStatus();

            while (true)
            {
                _logger.WriteLine();
                _logger.WriteLine("What do you want to do? (ENTER to exit / R to reset)");
                _logger.WriteLine("(1) Enter to connection mode.");
                _logger.WriteLine("(2) Remove a connection.");

                var read = _logger.ReadKey(ConsoleColor.Magenta);

                switch (read)
                {
                    case ConsoleKey.Enter:
                        return;

                    case ConsoleKey.R:
                        level.Reset();

                        _logger.WriteLine();
                        _logger.WriteLine();

                        level.PrintStatus();

                        _logger.WriteLine();
                        break;

                    case ConsoleKey.D1:
                        CreateConnection(level, out exit);

                        if (exit)
                        {
                            return;
                        }

                        break;

                    case ConsoleKey.D2:
                        RemoveConnection(level, out exit);
                        break;

                    default:
                        ShowError("This is not a valid option.");
                        break;
                }
            }
        }

        private static void CreateConnection(Level level, out bool exit)
        {
            var back = false;
            exit = false;

            _logger.WriteLine();
            _logger.WriteLine();
            _logger.WriteLine("What do you want to connect? (ENTER to exit / R to reset / ESCAPE to see menu)");

            var read = _logger.ReadKey(ConsoleColor.Magenta);

            switch (read)
            {
                case ConsoleKey.Enter:
                    exit = true;
                    return;

                case ConsoleKey.Escape:
                    back = true;
                    break;

                case ConsoleKey.R:
                    level.Reset();
                    _logger.WriteLine();
                    break;

                default:
                    _logger.Write(" -> ", ConsoleColor.Magenta);

                    var source = read.ToString();
                    var target = _logger.ReadKey(ConsoleColor.Magenta).ToString();

                    _logger.WriteLine();
                    _logger.WriteLine();
                    _logger.WriteLine($"Trying to connect {source} and {target}...", ConsoleColor.Cyan);

                    try
                    {
                        level.Connect(
                            level.TryGet<IEmitter>(source),
                            level.TryGet<IReceiver>(target));
                    }
                    catch (Exception exception)
                    {
                        ShowError(exception.Message);
                        WaitAnyAction();
                    }

                    break;
            }

            _logger.WriteLine();
            level.PrintStatus();

            if (!back)
            {
                CreateConnection(level, out exit);
            }
        }

        private static void RemoveConnection(Level level, out bool exit)
        {
            exit = false;

            _logger.WriteLine();
            _logger.WriteLine();
            _logger.WriteLine("Which connection do you want to remove? (ENTER to exit / R to reset / ESCAPE to see menu)");

            var read = _logger.ReadKey(ConsoleColor.Magenta);

            switch (read)
            {
                case ConsoleKey.Enter:
                    exit = true;
                    return;

                case ConsoleKey.Escape:
                    break;

                case ConsoleKey.R:
                    level.Reset();
                    _logger.WriteLine();
                    break;

                default:
                    _logger.Write(" -> ", ConsoleColor.Magenta);

                    var source = read.ToString();
                    var target = _logger.ReadKey(ConsoleColor.Magenta).ToString();

                    _logger.WriteLine();
                    _logger.WriteLine();
                    _logger.WriteLine($"Trying to remove connection between {source} and {target}...", ConsoleColor.Cyan);

                    try
                    {
                        level.RemoveConnection(
                            level.TryGet<IEmitter>(source),
                            level.TryGet<IReceiver>(target));
                    }
                    catch (Exception exception)
                    {
                        ShowError(exception.Message);
                        WaitAnyAction();
                    }

                    break;
            }

            _logger.WriteLine();
            level.PrintStatus();
        }

        private static void ShowError(string message)
        {
            _logger.WriteLine();
            _logger.WriteLine(message, ConsoleColor.Red);
            _logger.WriteLine("[Please, press ENTER to continue]", ConsoleColor.DarkCyan);
        }

        private static void WaitAnyAction()
        {
            _logger.ReadLine();
        }

        #endregion helpers
    }
}