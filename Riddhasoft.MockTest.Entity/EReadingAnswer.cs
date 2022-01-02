using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    /// <summary>
    /// Every reading types are stored in separate tabeles
    /// </summary>
    public class EReadingTypeOneAnswer
    {
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType questionType { get; set; }
        public int StudentId { get; set; }
        public string Answer { get; set; }
        public bool ISCorrectAnswer { get; set; }
    }


    public class EReadingTypeTwoAnswer
    {
        /// <summary>
        /// multiple choice answers are stored in same answer field and are separated by comma.
        /// Total Number of correct answers given by the user is stored
        /// </summary>
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class EReadingTypeThreeAnswer
    {
        /// <summary>
        /// all the paragraphs arranged in order by student is stored in a answers fild and are separated by comma
        /// number of paragraphs placed in correct positions are counted and stored
        /// </summary>
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }


    public class EReadingTypeFourAnswer
    {
        /// <summary>
        /// answers filled are stored and are separataed by comma in the answers field
        /// number of correct answers are counted and stored
        /// </summary>
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }

    public class EReadingTypeFiveAnswer
    {
        /// <summary>
        /// answers selected from dropdowns are stored and are separated by comma
        /// number of correct answers are counted and stored
        /// </summary>
        public int Id { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
        public string Answers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
    }
}
