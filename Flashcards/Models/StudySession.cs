using System.ComponentModel.DataAnnotations;

namespace Flashcards.Models
{
    public class StudySession
    {
        [Key]
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public string duration { get; set; }
        public int StackId { get; set; }
        public Stack Stack { get; set; }
    }
}
