using Spectre.Console;

namespace Flashcards.Views
{
    public class FlashcardView
    {
        public void Menu(DatabaseContext context)
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Manage Flashcard Menu"));
        }
    }
}
