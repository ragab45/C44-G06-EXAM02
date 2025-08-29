namespace Exam
{
    namespace Exame
    {
        #region Exam
        using System;
        using System.Collections.Generic;
        using System.Diagnostics;

        
        #region Answer Class

       
        public class Answer
        {
            public int AnswerId { get; set; }
            public string AnswerText { get; set; }

            public Answer(int id, string text)
            {
                AnswerId = id;
                AnswerText = text;
            }

            public override string ToString()
            {
                return $"[{AnswerId}] {AnswerText}";
            }
        }
        #endregion
        #region Base Question

       
        public abstract class Question : ICloneable, IComparable<Question>
        {
            public string Header { get; set; }
            public string Body { get; set; }
            public int Mark { get; set; }
            public List<Answer> AnswerList { get; set; }
            public int CorrectAnswerId { get; set; }

            public Question(string header, string body, int mark)
            {
                Header = header;
                Body = body;
                Mark = mark;
                AnswerList = new List<Answer>();
            }

            public abstract void ShowQuestion();

            public object Clone() => MemberwiseClone();

            public int CompareTo(Question other)
            {
                return this.Mark.CompareTo(other.Mark);
            }

            public override string ToString()
            {
                return $"{Header} - {Body} (Mark: {Mark})";
            }
        }
        #endregion
        #region  Derived Questions 

       
        public class TrueFalseQuestion : Question
        {
            public TrueFalseQuestion(string header, string body, int mark) : base(header, body, mark)
            {
                AnswerList.Add(new Answer(1, "True"));
                AnswerList.Add(new Answer(2, "False"));
            }

            public override void ShowQuestion()
            {
                Console.WriteLine($"Q: {Body} ({Mark} marks)");
                foreach (var ans in AnswerList)
                    Console.WriteLine(ans);
            }
        }

        public class MCQQuestion : Question
        {
            public MCQQuestion(string header, string body, int mark) : base(header, body, mark) { }

            public override void ShowQuestion()
            {
                Console.WriteLine($"Q: {Body} ({Mark} marks)");
                foreach (var ans in AnswerList)
                    Console.WriteLine(ans);
            }
        }
        #endregion
        #region Base Exam

      
        public abstract class Exam
        {
            public int Time { get; set; }
            public int NumberOfQuestions { get; set; }
            public List<Question> Questions { get; set; }

            public Exam(int time, int numQ)
            {
                Time = time;
                NumberOfQuestions = numQ;
                Questions = new List<Question>();
            }

            public abstract void ShowExam();
        }
        #endregion
#region Derived Exams

	       public class FinalExam : Exam
        {
            public FinalExam(int time, int numQ) : base(time, numQ) { }

            public override void ShowExam()
            {
                Console.WriteLine("--- Final Exam ---");
                int score = 0;
                int totalMarks = 0;
                List<string> results = new List<string>();

                Stopwatch sw = Stopwatch.StartNew();

                foreach (var q in Questions)
                {
                    q.ShowQuestion();
                    int userAns = GetValidAnswer(q);

                    totalMarks += q.Mark;

                    if (userAns == q.CorrectAnswerId)
                    {
                        Console.WriteLine("Correct!\n");
                        score += q.Mark;
                        results.Add($"{q.Body} => Correct");
                    }
                    else
                    {
                        Console.WriteLine($"Wrong! Correct answer is {q.CorrectAnswerId}\n");
                        results.Add($"{q.Body} => Wrong (Correct was {q.CorrectAnswerId})");
                    }
                }

                sw.Stop();

                Console.WriteLine("\n===== Exam Finished =====");
                Console.WriteLine($"Time Taken: {sw.Elapsed.TotalSeconds} seconds");
                Console.WriteLine($"Your Score: {score}/{totalMarks}");
                Console.WriteLine("\nAnswers Summary:");
                foreach (var r in results)
                    Console.WriteLine(r);
            }

            private int GetValidAnswer(Question q)
            {
                int ans;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter your answer: ");
                        ans = int.Parse(Console.ReadLine());

                        if (q.AnswerList.Exists(a => a.AnswerId == ans))
                            return ans;

                        throw new Exception("Invalid choice! Please select one of the available options.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message} Try again...");
                    }
                }
            }
        }

        public class PracticalExam : Exam
        {
            public PracticalExam(int time, int numQ) : base(time, numQ) { }

            public override void ShowExam()
            {
                Console.WriteLine("--- Practical Exam ---");
                int score = 0;
                int totalMarks = 0;
                List<string> results = new List<string>();

                Stopwatch sw = Stopwatch.StartNew();

                foreach (var q in Questions)
                {
                    q.ShowQuestion();
                    int userAns = GetValidAnswer(q);

                    totalMarks += q.Mark;

                    if (userAns == q.CorrectAnswerId)
                    {
                        Console.WriteLine("Correct!\n");
                        score += q.Mark;
                        results.Add($"{q.Body} => Correct");
                    }
                    else
                    {
                        Console.WriteLine($"Wrong! Correct answer is {q.CorrectAnswerId}\n");
                        results.Add($"{q.Body} => Wrong (Correct was {q.CorrectAnswerId})");
                    }
                }

                sw.Stop();

                Console.WriteLine("\n===== Exam Finished =====");
                Console.WriteLine($"Time Taken: {sw.Elapsed.TotalSeconds} seconds");
                Console.WriteLine($"Your Score: {score}/{totalMarks}");
                Console.WriteLine("\nAnswers Summary:");
                foreach (var r in results)
                    Console.WriteLine(r);
            }

            private int GetValidAnswer(Question q)
            {
                int ans;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter your answer: ");
                        ans = int.Parse(Console.ReadLine());

                        if (q.AnswerList.Exists(a => a.AnswerId == ans))
                            return ans;

                        throw new Exception("Invalid choice! Please select one of the available options.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message} Try again...");
                    }
                }
            }
        }
        #endregion 

        public class Subject
        {
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public Exam ExamOfSubject { get; set; }

            public Subject(int id, string name)
            {
                SubjectId = id;
                SubjectName = name;
            }

            public void CreateExam(Exam exam)
            {
                ExamOfSubject = exam;
            }

            public override string ToString()
            {
                return $"Subject: {SubjectName} (ID: {SubjectId})";
            }
        }

        // ---------------------- Main Program ----------------------
        class Program
        {
            static void Main()
            {
                int type;
                while (true)
                {
                    try
                    {
                        Console.Write("Choose Exam Type (1-Final, 2-Practical): ");
                        type = int.Parse(Console.ReadLine());

                        if (type == 1 || type == 2)
                            break;

                        throw new Exception("Invalid choice! You must choose 1 or 2.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message} Try again...");
                    }
                }

                Console.Write("Enter Exam Time (minutes): ");
                int time = int.Parse(Console.ReadLine());

                Console.Write("Enter number of questions: ");
                int numQ = int.Parse(Console.ReadLine());

                Exam exam = (type == 1) ? new FinalExam(time, numQ) : new PracticalExam(time, numQ);

                for (int i = 1; i <= numQ; i++)
                {
                    int qtype;
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine($"\nQuestion {i}: ");
                            Console.Write("Enter Question Type (1-MCQ, 2-True/False): ");
                            qtype = int.Parse(Console.ReadLine());

                            if (qtype == 1 || qtype == 2)
                                break;

                            throw new Exception("Invalid choice! You must choose 1 or 2.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message} Try again...");
                        }
                    }

                    Console.Write("Enter Question Body: ");
                    string body = Console.ReadLine();

                    Console.Write("Enter Mark: ");
                    int mark = int.Parse(Console.ReadLine());

                    Question q;

                    if (qtype == 1)
                    {
                        q = new MCQQuestion($"Q{i}", body, mark);
                        Console.WriteLine("Enter 3 choices:");
                        for (int j = 1; j <= 3; j++)
                        {
                            Console.Write($"Choice {j}: ");
                            string opt = Console.ReadLine();
                            q.AnswerList.Add(new Answer(j, opt));
                        }
                    }
                    else
                    {
                        q = new TrueFalseQuestion($"Q{i}", body, mark);
                    }

                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter Correct Answer Id: ");
                            int correct = int.Parse(Console.ReadLine());

                            if (q.AnswerList.Exists(a => a.AnswerId == correct))
                            {
                                q.CorrectAnswerId = correct;
                                break;
                            }

                            throw new Exception("Invalid choice! Please select one of the available options.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message} Try again...");
                        }
                    }

                    exam.Questions.Add(q);
                }

                Console.WriteLine("\nExam Created Successfully!\n");
                exam.ShowExam();
            }
        }

        #endregion
    }


}
