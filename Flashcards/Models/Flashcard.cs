﻿using System.ComponentModel.DataAnnotations;

namespace Flashcards.Models
{
    public class Flashcard
    {
        [Key]
        public int CardId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int StackId { get; set; }
        public Stack Stack { get; set; }
    }
}
