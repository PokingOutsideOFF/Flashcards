using Flashcards.DTO;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.Repository
{
    public class FlashcardRepository
    {
        private DatabaseContext _context;
        private int stackId;

        public FlashcardRepository(DatabaseContext context, int stackId)
        {
            _context = context;
            this.stackId = stackId;
        }

        public int GetAllCards()
        {
            /*var entities = (from flashcard in _context.Flashcard
                                        join stack in _context.Stack
                                        on flashcard.StackId equals stack.StackId
                                        select new FlashcardDTO
                                        {
                                            Question = flashcard.Question,
                                            Answer = flashcard.Answer,
                                        }).ToList();*/
            var entities = _context.Flashcard
                          .Where(flashcard => flashcard.StackId == stackId)
                          .Select(flashcard => new FlashcardDTO
                          {
                              StackCardId = flashcard.StackCardId,
                              Question = flashcard.Question,
                              Answer = flashcard.Answer, 
                          })
                          .ToList();
            GetTable(entities);
            return entities.Count;
        }

        public void GetXCards(int limit)
        {
            var entities = _context.Flashcard
                          .Where(flashcard => flashcard.StackId == stackId)
                          .Select(flashcard => new FlashcardDTO
                          {
                              StackCardId = flashcard.StackCardId,
                              Question = flashcard.Question,
                              Answer = flashcard.Answer,
                          })
                          .Take(limit)
                          .ToList();
            GetTable(entities);
        }



        public void GetTable(List<FlashcardDTO> entities)
        {
            if (entities.Count() == 0)
            {
                AnsiConsole.Markup("[red]Table is empty...[/]\n\n");
                Thread.Sleep(1000);
                return;
            }

            AnsiConsole.Markup("\n[blue]Stack Table[/]\n");
            var table = new Table();
            table.AddColumn("Card Id");
            table.AddColumn("Question");
            table.AddColumn("Answer");

            foreach (var entity in entities)
            {
                table.AddRow(
                    entity.StackCardId.ToString(),
                    entity.Question,
                    entity.Answer
                    );
            }

            AnsiConsole.Write(table);
            Console.WriteLine();    
        }

        public void Insert(string question, string answer)
        {
            var maxNumber = _context.Flashcard
                            .Where(flashcard => flashcard.StackId == stackId)
                            .Max(flashcard => (int?)flashcard.StackCardId) ?? 0;
            var flashcard = new Flashcard
            {
                Question = question,
                Answer = answer,
                StackId = stackId,
                StackCardId = maxNumber + 1
            };
            _context.Add(flashcard);
            AnsiConsole.Markup("[red]Row inserted[/]\n");
            _context.SaveChanges();
        }
        public void UpdateQuestion(int cardId, string question)
        {
            var entity = _context.Flashcard.FirstOrDefault(f => f.StackCardId == cardId);
            if (entity == null)
            {
                AnsiConsole.Markup("[red]Id not found. Returning to Stack Menu[/]\n\n");
                return;
            }

            entity.Question = question;
            _context.SaveChanges();
            AnsiConsole.Markup("[red]Row updated[/]\n\n");
            GetAllCards();
        }

        public void UpdateAnswer(int cardId, string answer)
        {
            var entity = _context.Flashcard.FirstOrDefault(f => f.StackCardId == cardId);
            if (entity == null)
            {
                AnsiConsole.Markup("[red]Id not found. Returning to Stack Menu[/]\n\n");
                return;
            }

            entity.Answer = answer;
            _context.SaveChanges();
            AnsiConsole.Markup("[red]Row updated[/]\n\n");
            GetAllCards();
        }

        public void UpdateQuestionAnswer(int cardId, string question, string answer)
        {
            var entity = _context.Flashcard.FirstOrDefault(f => f.StackCardId == cardId);
            if (entity == null)
            {
                AnsiConsole.Markup("[red]Id not found. Returning to Stack Menu[/]\n\n");
                return;
            }

            entity.Question = question;
            entity.Answer = answer;
            _context.SaveChanges();
            AnsiConsole.Markup("[red]Row updated[/]\n\n");
            GetAllCards();
        }

        public void Delete(int cardId)
        {
            var entity  = _context.Flashcard.FirstOrDefault(f => f.StackCardId == cardId && f.StackId == stackId);
            if(entity == null)
            {
                AnsiConsole.Markup("[red]Id not found. Returning to Flashcard Menu.[/]\n\n");
                return;
            }
            
            _context.Flashcard.Remove(entity);
            _context.SaveChanges();

            var rowsToBeUpdated = _context.Flashcard
                                    .Where(f => f.StackCardId > cardId)
                                    .ToList();
            foreach(var row in rowsToBeUpdated)
            {
                row.StackCardId -= 1;
            }

            _context.SaveChanges();

            AnsiConsole.Markup("\n[red]Row deleted[/]\n\n");
            GetAllCards();

        }
    }
}
