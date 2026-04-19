# FileManagerCSharp

A simple console file manager written in C#. I made this as a school project to practice working with the file system and console input parsing.

---

## What it does

You type commands in the terminal and it does file/folder stuff. Kind of like Windows CMD but i wrote it myself. It has a basic command history, colored error messages and supports quoted paths with spaces.

---

## Project Structure

```
FileManagerCSharp/
├── Program.cs          # main loop + command routing
├── FileManager.cs      # all file/folder logic
└── CommandParser.cs    # parses input into command + args
```

---

## Commands

| Command | Usage | Description |
|---------|-------|-------------|
| `dir` | `dir` | show files and folders in current directory |
| `cd` | `cd <path>` | change directory (`cd ..` to go back) |
| `mkdir` | `mkdir <name>` | create a new folder |
| `rmdir` | `rmdir <name>` | delete folder with everything inside |
| `create` | `create <name> <text>` | create a text file with content |
| `read` | `read <name>` | read and print a text file |
| `del` | `del <name>` | delete a file |
| `copy` | `copy <source> <dest>` | copy file or folder |
| `move` | `move <source> <dest>` | move file or folder |
| `attr` | `attr <name>` | show file name, size, dates, attributes |
| `search` | `search <name>` | search files/folders by name recursively |
| `history` | `history` | show all commands you typed this session |
| `cls` | `cls` | clear the screen |
| `help` | `help [command]` | show all commands, or details for one |
| `exit` | `exit` | close the program |

---

## How CommandParser works

It splits the input by spaces but handles quoted strings correctly. So something like that:

```
create myfile.txt "hello world this is my file"
```

...it gets parsed as 3 parts, not 8. Without quotes the text would get cut off at the first space.

---
## Example session

```
File Manager by C#
Copyright (C) 2026. All rights reserved.

PS C:\Users\me> mkdir testfolder
Directory created --> testfolder

PS C:\Users\me> cd testfolder
PS C:\Users\me\testfolder> create notes.txt "this is my note"
File created --> notes.txt

PS C:\Users\me\testfolder> read notes.txt
this is my note

PS C:\Users\me\testfolder> attr notes.txt
Name       --> notes.txt
Size       --> 18 bytes
Created    --> 26.03.2026 14:32:01
Modified   --> 26.03.2026 14:32:01
Attributes --> Archive
```

---

## Notes

- Errors are shown in red so they are like very easy to spot
- `rmdir` deletes everything inside the folder too, so be careful
- `search` goes through all subfolders recursively
- UTF-8 encoding is set on startup so special characters work fine
- Command history only lasts for the current session, it resets on exit

---

*Made by a student learning C#*
