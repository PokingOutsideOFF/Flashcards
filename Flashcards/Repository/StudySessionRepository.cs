using Flashcards.DTO;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.Repository
{
    public class StudySessionRepository
    {
        private DatabaseContext _context;

        public StudySessionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Insert(int stackId, string stackName, int score, DateTime startDate, string duration)
        {
            var session = new StudySession
            {
                StackId = stackId,
                StackName = stackName,
                Score = score,
                Date = startDate,
                Duration = duration
            };

            _context.StudySession.Add(session);
            _context.SaveChanges();

        }

        public void GetSession()
        {
            var entities = _context.StudySession.ToList();
            if(entities.Count == 0)
            {
                AnsiConsole.Markup("No sessions started");
                return;
            }

            AnsiConsole.Markup("\n[blue]Study Session Table[/]\n");
            var table = new Table();
            table.AddColumn("StackName");
            table.AddColumn("Score");
            table.AddColumn("Start Time");
            table.AddColumn("Session Duration");

            foreach(var entity in entities)
            {
                int count = _context.Flashcard.Count(f => f.StackId == entity.StackId);
                table.AddRow(
                    entity.StackName,
                    entity.Score.ToString() + "/" + count.ToString(),
                    entity.Date.ToString(),
                    entity.Duration);
            }

            AnsiConsole.Write(table);
        }
    }
}
