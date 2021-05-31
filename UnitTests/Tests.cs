using NUnit.Framework;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class Tests
    {
        private AttendanceKeyService _attendanceKeyService;

        [SetUp]
        public void Setup()
        {
            _attendanceKeyService = new AttendanceKeyService();
        }

        [Test]
        public void TestAuthCodeGenerationLength()
        {
            var attendanceKey = _attendanceKeyService.GenerateKey();

            Assert.AreEqual(10, attendanceKey.Length);
        }

        [Test]
        public void TestAuthCodeGenerationUnique()
        {
            var attendanceKey = _attendanceKeyService.GenerateKey();

            var attendanceKey2 = _attendanceKeyService.GenerateKey();

            var attendanceKey3 = _attendanceKeyService.GenerateKey();

            Assert.AreNotEqual(attendanceKey2, attendanceKey);
            Assert.AreNotEqual(attendanceKey, attendanceKey3);
            Assert.AreNotEqual(attendanceKey2, attendanceKey3);
        }

        [Test]
        public void TestOverallPresenceStudent()
        {
            //Create a class
            var class1 = new ClassDto
            {
                Id = 1,
                Start = new DateTime(2021, 03, 11),
                End = new DateTime(2021, 03, 12),
                Course = new CourseDto
                {
                    Id = 1,
                    Name = "Software Development"
                }
            };

            //Create student
            var student = new UserDto
            {
                Id = 1,
                FirstName = "Thomas",
                LastName = "Jensen",
                Email = "tj@live.dk",
                Password = "123",
                Roles = new List<RoleDto>
                {
                    new RoleDto()
                    {
                        Id = 2
                    }
                }
            };

            //Create 2 lessons with same class
            var lesson1 = new LessonDto
            {
                Class = class1,
                User = student,
                Present = true
            };

            var lesson2 = new LessonDto
            {
                Class = class1,
                User = student,
                Present = false
            };

            //Set lessons for student and class
            class1.Lessons = new List<LessonDto> { lesson1, lesson2 };
            student.Lessons = new List<LessonDto> { lesson1, lesson2 };

            //Calculate the amount of classes and the amount of presence
            var lessons = student.Lessons;
            var totalAmountOfLessons = lessons.Count();
            var presenceSum = lessons.Count(l => l.Present == true);

            double attendance = (double)presenceSum / (double)totalAmountOfLessons;

            //Attendance is 50%
            Assert.AreEqual(0.5, attendance);
        }

        [Test]
        public void TestStudentPresenceForSpecificClass()
        {
            //Create a class
            var class1 = new ClassDto
            {
                Id = 1,
                Start = new DateTime(2021, 03, 11),
                End = new DateTime(2021, 03, 12),
                Course = new CourseDto
                {
                    Id = 1,
                    Name = "Software Development"
                }
            };

            //Create a class
            var class2 = new ClassDto
            {
                Id = 2,
                Start = new DateTime(2021, 03, 13),
                End = new DateTime(2021, 03, 14),
                Course = new CourseDto
                {
                    Id = 2,
                    Name = "Software Design"
                }
            };

            //Create student
            var student = new UserDto
            {
                Id = 1,
                FirstName = "Thomas",
                LastName = "Jensen",
                Email = "tj@live.dk",
                Password = "123",
                Roles = new List<RoleDto>
                {
                    new RoleDto()
                    {
                        Id = 2
                    }
                },
            };

            //Create 2 lessons with different class
            var lesson1 = new LessonDto
            {
                Class = class1,
                User = student,
                Present = true
            };

            var lesson2 = new LessonDto
            {
                Class = class2,
                User = student,
                Present = false
            };

            //Set lessons for student and class
            class1.Lessons = new List<LessonDto> { lesson1, lesson2 };
            class2.Lessons = new List<LessonDto> { lesson1, lesson2 };
            student.Lessons = new List<LessonDto> { lesson1, lesson2 };

            //Calculate the amount of classes and the amount of presence
            var lessons = student.Lessons;

            var classId = 1;

            var totalAmountOfLessons = lessons
                .Where(l => l.Class.Id == classId)
                .Count();

            var presenceSum = lessons
                .Where(l => l.Class.Id == classId)
                .Count(l => l.Present == true);

            double attendance = (double)presenceSum / (double)totalAmountOfLessons;

            //Attendance i 100%
            Assert.AreEqual(1, attendance);
        }
    }
}