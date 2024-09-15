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
            table.AddColumn("Question");
            table.AddColumn("Answer");

            foreach (var entity in entities)
            {
                table.AddRow(
                    entity.Question,
                    entity.Answer
                    );
            }

            AnsiConsole.Write(table);
            Console.WriteLine();    
        }

        public void Insert(string question, string answer)
        {
            var flashcard = new Flashcard
            {
                Question = question,
                Answer = answer,
                StackId = stackId
            };
            _context.Add(flashcard);
            AnsiConsole.Markup("[red]Row inserted[/]\n");
            _context.SaveChanges();
        }
    }
}
