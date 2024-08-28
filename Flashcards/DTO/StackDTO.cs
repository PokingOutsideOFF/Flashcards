//When you list flashcards in a stack, use this DTO to show the question, answer,
//and a sequential number starting from 1 (which is not the same as the FlashcardId in the database).
using Flashcards.Models;

namespace Flashcards.DTO
{
    public class StackDTO
    { 
        public string StackName { get; set; }
        public List<Flashcard> Flashcards { get; set; }
    }
}
