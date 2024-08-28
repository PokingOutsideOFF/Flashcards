namespace Flashcards.Models
{
     public class Flashcard
    {
        public int CardID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int StackID { get; set; }
    }
}
