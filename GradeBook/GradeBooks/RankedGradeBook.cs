using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("There must be at least 5 students for ranked grading to work.");
            }

            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
            var threshold = (int)Math.Ceiling(grades.Count * 0.2);

            if (averageGrade >= grades[threshold - 1])
                return 'A';
            if (averageGrade >= grades[(threshold * 2) - 1])
                return 'B';
            if (averageGrade >= grades[(threshold * 3) - 1])
                return 'C';
            if (averageGrade >= grades[(threshold * 4) - 1])
                return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
