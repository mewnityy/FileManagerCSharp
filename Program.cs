namespace FileManagerCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            FileManager manager = new FileManager();

            Console.WriteLine("File Manager by C#");
            Console.WriteLine("Copyright (C) 2026. All rights reserved.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("PS " + manager.CurrentDir + "> ");
                string input = Console.ReadLine();

                if (input == "")
                    continue;

                manager.AddHistory(input);

                string[] parts = CommandParser.Parse(input);
                string command = parts[0].ToLower();

                try
                {
                    switch (command)
                    {
                        case "dir":
                            manager.ShowDir();
                            break;

                        case "cd":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: cd <path>"); break; }
                            manager.ChangeDir(parts[1]);
                            break;

                        case "mkdir":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: mkdir <name>"); break; }
                            manager.CreateDir(parts[1]);
                            break;

                        case "rmdir":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: rmdir <name>"); break; }
                            manager.DeleteDir(parts[1]);
                            break;

                        case "create":
                            if (parts.Length < 3) { Console.WriteLine("Correct usage: create <name> <text>"); break; }
                            manager.CreateFile(parts[1], parts[2]);
                            break;

                        case "read":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: read <name>"); break; }
                            manager.ReadFile(parts[1]);
                            break;

                        case "del":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: del <name>"); break; }
                            manager.DeleteFile(parts[1]);
                            break;

                        case "copy":
                            if (parts.Length < 3) { Console.WriteLine("Correct usage: copy <source> <dest>"); break; }
                            manager.Copy(parts[1], parts[2]);
                            break;

                        case "move":
                            if (parts.Length < 3) { Console.WriteLine("Correct usage: move <source> <dest>"); break; }
                            manager.Move(parts[1], parts[2]);
                            break;

                        case "attr":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: attr <name>"); break; }
                            manager.ShowAttr(parts[1]);
                            break;

                        case "search":
                            if (parts.Length < 2) { Console.WriteLine("Correct usage: search <name>"); break; }
                            manager.Search(parts[1]);
                            break;

                        case "history":
                            manager.ShowHistory();
                            break;

                        case "cls":
                            Console.Clear();
                            break;

                        case "help":
                            if (parts.Length > 1)
                                manager.ShowHelpDetail(parts[1]);
                            else
                                manager.ShowHelp();
                            break;

                        case "exit":
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("'" + command + "' is not recognized. Type 'help' to see the list of them.");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error --> " + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}