namespace prakt4
{
    internal class Program
    {
        static DateTime selectedDate = DateTime.Now;
        static List<Note> notesList = new List<Note>();
        static List<Note> displayedNotesList = new List<Note>();
        static Note selectedNote = new Note();
        static void Main()
        {
            CreateDefaultNotes();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("F1 - Создать записку | F2 - Удалить записку");
                Console.WriteLine($"Выбранная дата {selectedDate.ToString("d")}");
                GetNotes(selectedDate);
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        GetDate(key.Key);
                        SelectedNoteChanged(ConsoleKey.NoName);
                        break;
                    case ConsoleKey.RightArrow:
                        GetDate(key.Key);
                        SelectedNoteChanged(ConsoleKey.NoName);
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedNoteChanged(ConsoleKey.DownArrow);
                        break;
                    case ConsoleKey.UpArrow:
                        SelectedNoteChanged(ConsoleKey.UpArrow);
                        break;
                    case ConsoleKey.Enter:
                        GetNoteInfo();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.F1:
                        AddNote();
                        SelectedNoteChanged(ConsoleKey.NoName);
                        break;
                    case ConsoleKey.F2:
                        DeleteNote();
                        SelectedNoteChanged(ConsoleKey.NoName);
                        break;
                }
            }
        }
        static void CreateDefaultNotes()
        {
            Note note = new Note();
            note.Title = "Еду на учебу";
            note.SelectionPrefix = "--->";
            note.Description = "Встаю утром и еду на учебу";
            note.Date = DateTime.Now;
            notesList.Add(note);
            selectedNote = note;

            Note note2 = new Note();
            note2.Title = "Сижу на учебе";
            note2.Description = "Приехал на учебу и сижу на парах";
            note2.Date = new DateTime(2022, 10, 26);
            notesList.Add(note2);

            Note note3 = new Note();
            note3.Title = "Отдых";
            note3.Description = "Сижу дома и отдыхаю";
            note3.Date = new DateTime(2022, 10, 24);
            notesList.Add(note3);

            Note note4 = new Note();
            note4.Title = "Встреча с друзьями";
            note4.Description = "После учебы встретиться с друзьями и провести время вместе";
            note4.Date = new DateTime(2022, 10, 22);
            notesList.Add(note4);

            Note Note5 = new Note();
            Note5.Title = "План эвакуации";
            Note5.Description = "Проснуться,покушать и лететь делать план эвакуации по БЖД";
            Note5.Date = new DateTime(2022, 10, 23);
            notesList.Add(Note5);
        }
        static void AddNote()
        {
            Console.Clear();
            Note newNote = new Note();
            Console.Write("Название:  ");
            newNote.Title = Console.ReadLine();
            Console.Write("Описание:  ");
            newNote.Description = Console.ReadLine();
            newNote.Date = selectedDate;
            newNote.SelectionPrefix = " ";
            notesList.Add(newNote);
        }
        static void DeleteNote()
        {
            if (selectedNote != null)
            {
                notesList.Remove(selectedNote);
                displayedNotesList.Remove(selectedNote);
            }
        }
        static DateTime GetDate(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    selectedDate = selectedDate.AddDays(-1);
                    return selectedDate;
                case ConsoleKey.RightArrow:
                    selectedDate = selectedDate.AddDays(1);
                    return selectedDate;
            }
            return selectedDate;
        }
        static void GetNotes(DateTime date)
        {
            displayedNotesList.Clear();
            foreach (var note in notesList)
            {
                if (note.Date.ToString("d") == date.ToString("d"))
                {
                    displayedNotesList.Add(note);
                    Console.WriteLine($"{note.SelectionPrefix} {note.Title}");
                }
            }
        }
        static void GetNoteInfo()
        {
            Console.Clear();
            foreach (var note in notesList)
            {
                if (note == selectedNote)
                {
                    Console.WriteLine(note.Title);
                    Console.WriteLine($"Описание: {note.Description}");
                    Console.WriteLine($"Дата: {selectedDate.ToString("d")}");
                    break;
                }
            }
            bool exit = false;
            while (!exit)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        exit = true;
                        break;
                }
            }
        }
        static void SelectedNoteChanged(ConsoleKey key)
        {
            int index = 0;
            if (key == ConsoleKey.UpArrow)
            {
                if (displayedNotesList.Count != 0)
                {
                    if (selectedNote != displayedNotesList[0])
                    {
                        for (int i = 0; i < displayedNotesList.Count; i++)
                        {
                            if (selectedNote == displayedNotesList[i])
                            {
                                index = i - 1;
                            }
                        }
                        foreach (var note in displayedNotesList)
                        {
                            if (!String.IsNullOrWhiteSpace(note.SelectionPrefix))
                                note.SelectionPrefix = " ";
                            if (note == displayedNotesList[index])
                            {
                                note.SelectionPrefix = "--->";
                                selectedNote = note;
                            }
                        }
                    }
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (displayedNotesList.Count != 0)
                {
                    if (selectedNote != displayedNotesList[displayedNotesList.Count - 1])
                    {
                        for (int i = 0; i < displayedNotesList.Count; i++)
                        {
                            if (selectedNote == displayedNotesList[i])
                            {
                                index = i + 1;
                            }
                        }
                        foreach (var note in displayedNotesList)
                        {
                            if (!String.IsNullOrWhiteSpace(note.SelectionPrefix))
                                note.SelectionPrefix = " ";
                            if (note == displayedNotesList[index])
                            {
                                note.SelectionPrefix = "--->";
                                selectedNote = note;
                            }
                        }
                    }
                }
            }
            else
            {
                displayedNotesList.Clear();
                foreach (var note in notesList)
                {
                    if (!String.IsNullOrWhiteSpace(note.SelectionPrefix))
                        note.SelectionPrefix = " ";
                    if (note.Date.ToString("d") == selectedDate.ToString("d"))
                    {
                        displayedNotesList.Add(note);
                        if (note == displayedNotesList[0])
                        {
                            note.SelectionPrefix = "--->";
                            selectedNote = note;
                        }
                    }
                }
            }
        }
    }
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string SelectionPrefix { get; set; }
    }
}