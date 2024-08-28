namespace Flashcards.Models
{
    public class Stack
    {
        public int StackId { get; set; }
        public string StackName { get; set; }
        public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    }
}
