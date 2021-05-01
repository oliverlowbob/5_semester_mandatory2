//using NUnit.Framework;

//using Skoleprotokol.Models;

//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace UnitTests
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void TestAuthCodeGeneration()
//        {
//            Random random = new Random();

//            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

//            //Generate ID that is 10 characters long
//            var generatedId = Enumerable.Repeat(chars, 10)
//              .Select(s => s[random.Next(s.Length)]).ToList();

//            Assert.AreEqual(10, generatedId.Count());
//        }

//        [Test]
//        public void TestOverallPresenceStudent()
//        {
//            //Create a class
//            var class1 = new Class
//            {
//                Id = 1,
//                Start = new DateTime(2021, 03, 11),
//                End = new DateTime(2021, 03, 12),
//                Course = new Course
//                {
//                    Id = 1,
//                    Name = "Software Development"
//                }
//            };

//            //Create student
//            var student = new User
//            {
//                Id = 1,
//                FirstName = "Thomas",
//                LastName = "Jensen",
//                Email = "tj@live.dk",
//                Password = "123",
//                Roles = new List<Role>
//                {
//                    new Role()
//                    {
//                        Id = 2
//                    }
//                }
//            };

//            //Create 2 lessons with same class
//            var lesson1 = new Lesson
//            {
//                Class = class1,
//                User = student,
//                Present = true
//            };

//            var lesson2 = new Lesson
//            {
//                Class = class1,
//                User = student,
//                Present = false
//            };

//            //Set lessons for student and class
//            class1.Lessons = new List<Lesson> { lesson1, lesson2 };
//            student.Lessons = new List<Lesson> { lesson1, lesson2 };

//            //Calculate the amount of classes and the amount of presence
//            var lessons = student.Lessons;
//            var totalAmountOfLessons = lessons.Count();
//            var presenceSum = lessons.Count(l => l.Present == true);

//            double attendance = (double)presenceSum / (double)totalAmountOfLessons;

//            //Attendance is 50%
//            Assert.AreEqual(0.5, attendance);
//        }

//        [Test]
//        public void TestStudentPresenceForSpecificClass()
//        {
//            //Create a class
//            var class1 = new Class
//            {
//                Id = 1,
//                Start = new DateTime(2021, 03, 11),
//                End = new DateTime(2021, 03, 12),
//                Course = new Course
//                {
//                    Id = 1,
//                    Name = "Software Development"
//                }
//            };

//            //Create a class
//            var class2 = new Class
//            {
//                Id = 2,
//                Start = new DateTime(2021, 03, 13),
//                End = new DateTime(2021, 03, 14),
//                Course = new Course
//                {
//                    Id = 2,
//                    Name = "Software Design"
//                }
//            };

//            //Create student
//            var student = new User
//            {
//                Id = 1,
//                FirstName = "Thomas",
//                LastName = "Jensen",
//                Email = "tj@live.dk",
//                Password = "123",
//                Roles = new List<Role>
//                {
//                    new Role()
//                    {
//                        Id = 2
//                    }
//                },
//            };

//            //Create 2 lessons with different class
//            var lesson1 = new Lesson
//            {
//                Class = class1,
//                User = student,
//                Present = true
//            };

//            var lesson2 = new Lesson
//            {
//                Class = class2,
//                User = student,
//                Present = false
//            };

//            //Set lessons for student and class
//            class1.Lessons = new List<Lesson> { lesson1, lesson2 };
//            class2.Lessons = new List<Lesson> { lesson1, lesson2 };
//            student.Lessons = new List<Lesson> { lesson1, lesson2 };

//            //Calculate the amount of classes and the amount of presence
//            var lessons = student.Lessons;

//            var classId = 1;

//            var totalAmountOfLessons = lessons
//                .Where(l => l.Class.Id == classId)
//                .Count();

//            var presenceSum = lessons
//                .Where(l => l.Class.Id == classId)
//                .Count(l => l.Present == true);

//            double attendance = (double)presenceSum / (double)totalAmountOfLessons;

//            //Attendance i 100%
//            Assert.AreEqual(1, attendance);
//        }

//        [Test]
//        public void TestShowStudentClassEnrolled()
//        {
//            //Create a class
//            var class1 = new Class
//            {
//                Id = 1,
//                Start = new DateTime(2021, 03, 11),
//                End = new DateTime(2021, 03, 12),
//                Course = new Course
//                {
//                    Id = 1,
//                    Name = "Software Development"
//                }
//            };

//            //Create a student
//            var student = new User
//            {
//                Id = 1,
//                FirstName = "Thomas",
//                LastName = "Jensen",
//                Email = "tj@live.dk",
//                Password = "123",
//                Roles = new List<Role>
//                {
//                    new Role()
//                    {
//                        Id = 2
//                    }
//                },
//                Classes = new List<Class> { class1 }
//            };

//            //Student is enrolled in 1 class
//            Assert.AreEqual(1, student.Classes.Count());
            
//        }

//        [Test]
//        public void TestShowAllStudentsInClass()
//        {
//            //Create a class
//            var class1 = new Class
//            {
//                Id = 1,
//                Start = new DateTime(2021, 03, 11),
//                End = new DateTime(2021, 03, 12),
//                Course = new Course
//                {
//                    Id = 1,
//                    Name = "Software Development"
//                }
//            };

//            //Create a student
//            var student = new User
//            {
//                Id = 1,
//                FirstName = "Thomas",
//                LastName = "Jensen",
//                Email = "tj@live.dk",
//                Password = "123",
//                Roles = new List<Role>
//                {
//                    new Role()
//                    {
//                        Id = 2
//                    }                },
//                Classes = new List<Class> { class1 }
//            };

//            //Create a student
//            var student2 = new User
//            {
//                Id = 2,
//                FirstName = "Dennis",
//                LastName = "Jensen",
//                Email = "dj@live.dk",
//                Password = "123",
//                Roles = new List<Role>
//                {
//                    new Role()
//                    {
//                        Id = 2
//                    }                },
//                Classes = new List<Class> { class1 }
//            };

//            class1.Users = new List<User> { student, student2 };

//            //Class has two students
//            Assert.AreEqual(2, class1.Users.Count());
//        }
//    }
//}