using DomainModel;
using Mm.BusinessLayer;
using System;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        private static IBusinessLayer businessLayer = new BuinessLayer();

        static void Main(string[] args)
        {
            run();
        }

        /// <summary>
        /// Display the menu and get user selection until exit.
        /// </summary>
        public static void run()
        {
            bool repeat = true;
            int input;

            do
            {
                Menu.displayMenu();
                input = Validator.getMenuInput();

                switch (input)
                {
                    case 0:
                        repeat = false;
                        break;
                    case 1:
                        Menu.clearMenu();
                        addTeacher();
                        break;
                    case 2:
                        Menu.clearMenu();
                        updateTeacher();
                        break;
                    case 3:
                        Menu.clearMenu();
                        removeTeacher();
                        break;
                    case 4:
                        Menu.clearMenu();
                        listTeachers();
                        break;
                    case 5:
                        Menu.clearMenu();
                        listTeacherCourses();
                        break;
                    case 6:
                        Menu.clearMenu();
                        addCourse();
                        break;
                    case 7:
                        Menu.clearMenu();
                        updateCourse();
                        break;
                    case 8:
                        Menu.clearMenu();
                        removeCourse();
                        break;
                    case 9:
                        Menu.clearMenu();
                        listCourses();
                        break;
                }
            } while (repeat);
        }

        //CRUD for teachers

        /// <summary>
        /// Add a teacher to the database.
        /// </summary>
        public static void addTeacher()
        {   //YOUR CODE TO ADD A TEACHER THE DATABASE
            //Create a teacher object, set EntityState to Added, add the teacher 
            //object to the database using the businessLayer object, and display
            //a message to the console window that the teacher has been added
            //to the database.
            Console.WriteLine("Enter a teacher name: ");
            Teacher teacher = new Teacher
            {
                EntityState = EntityState.Added,
                TeacherName = Console.ReadLine()
            };
            businessLayer.AddTeacher(teacher);
            Console.WriteLine("Teacher {0} Added.", teacher.TeacherName);
        }

        /// <summary>
        /// Update the name of a teacher.
        /// </summary>
        public static void updateTeacher()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listTeachers();
            Teacher teacher = null;
            //Find by a teacher's name
            if (input == 1)
            {   //YOUR CODE TO UPDATE A TEACHER THE DATABASE
                //Create a teacher object, input the name of the teacher,
                //and get the teacher by name using a method in the class BusinessLayer.
                //If the teacher object is not null, change the teacher's based on the input user
                //enters, set EntityState to Modified, update the teacher 
                //object to the database using the businessLayer object.
                //If teacher is null, display a message "Teacher does not exist"
                //to the database.
                Console.WriteLine("Enter the teacher name to update: ");
                teacher = businessLayer.GetTeacherByName(Console.ReadLine());
            }
            //find by a teacher's id
            else if (input == 2)
            {
                Console.WriteLine("Enter the teacher ID to update: ");
                teacher = businessLayer.GetTeacherById(Validator.getId());
            }

            if (teacher != null)
            {
                string teacherOldName = teacher.TeacherName;
                Console.WriteLine("Enter a new name for {0}: ", teacher.TeacherName);
                teacher.TeacherName = Console.ReadLine();
                teacher.EntityState = EntityState.Modified;
                businessLayer.UpdateTeacher(teacher);
                Console.WriteLine("Teacher {0} has been updated to {1}.", teacherOldName, teacher.TeacherName);
            } else
            {
                Console.WriteLine("Teacher does not exist!");
            }
        }

        /// <summary>
        /// Remove a teacher from the database.
        /// </summary>
        public static void removeTeacher()
        {
            listTeachers();
            int id = Validator.getId();
            //YOUR CODE TO REMOVE A TEACHER THE DATABASE
            //Get the teacher. If the teacher object is not null, display the message that
            //the teacher has been removed. Remove the teacher from the database.
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                teacher.EntityState = EntityState.Deleted;
                businessLayer.RemoveTeacher(teacher);
                Console.WriteLine("Teacher has been removed!");
            }
            else
            {
                Console.WriteLine("Teacher not found!");
            }
            
        }

        /// <summary>
        /// List all teachers in the database.
        /// </summary>
        public static void listTeachers()
        {   //Call a method from the class BusinessLayer to get all the teacher and assign
            //the return to an object of type IList<Teacher>
            //Display the all the teacher id and name.
            //Your code
            IList<Teacher> teacherList = businessLayer.GetAllTeachers();
            foreach (Teacher teacher in teacherList)
            {
                Console.WriteLine("Teacher Name: {0}, ID: {1}.", teacher.TeacherName, teacher.TeacherId);
            }

        }

        /// <summary>
        /// List the courses of a specified teacher.
        /// </summary>
        public static void listTeacherCourses()
        {
            listTeachers();
            int id = Validator.getId();
            //Get a Teacher object by on the teacher id input
            //If the teacher object is not null
            //   Display teacher id and teacher name
            //   List all the course the teacher teaches
            //Else
            //Display a message " No course for the teacher id and name". Display
            //the teacher's id and name
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                Console.WriteLine("Teacher Name: {0}, ID: {1}.", teacher.TeacherName, teacher.TeacherId);
                //TODO Determine if courses are valid
                Console.WriteLine("Courses teaching:");
                foreach (Course course in businessLayer.GetCoursesByTeacherId(id))
                {
                    Console.WriteLine("Course Name: {0}, ID: {1}. ", course.CourseName, course.CourseId);
                }
            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            };
        }

        //CRUD for courses

        /// <summary>
        /// Add a course to a teacher.
        /// </summary>
        public static void addCourse()
        {
            Console.WriteLine("Enter a course name: ");
            string courseName = Console.ReadLine();

            listTeachers();
            Console.WriteLine("Select a teacher ID for this course: ");
            int id = Validator.getId();
            //Get the teacher object using the id
            //your code
            Teacher teacher = businessLayer.GetTeacherById(id);
            if (teacher != null)
            {
                //create course with name, teacher id, and set entity state to added
                //your code
                Course course = new Course
                {
                    CourseName = courseName,
                    TeacherId = id,
                    EntityState = EntityState.Added
                };
                //Set the Entity of the teacher object modified
                //Set the entity state of each course from the teacher to Unchanged
                //Add the course to the teacher
                //Update the teacher by calling a method from the class BusinessLayer
                //Display a message the course name has been added to the database.
                //your code
                teacher.EntityState = EntityState.Modified;
                foreach (Course c in teacher.Courses)
                {
                    c.EntityState = EntityState.Unchanged;
                }
                teacher.Courses.Add(course);                
                businessLayer.UpdateTeacher(teacher);
                Console.WriteLine("Course {0} successfully added to {1}.", courseName, teacher.TeacherName);           
            }
            else
            {
                Console.WriteLine("Teacher does not exist.");
            };
        }

        /// <summary>
        /// Update the name of a course.
        /// </summary>
        public static void updateCourse()
        {
            Menu.displaySearchOptions();
            int input = Validator.getOptionInput();
            listCourses();
            Course course = null;

            //find course by name
            if (input == 1)
            {
                Console.WriteLine("Enter a course name: ");
                //Get a course object by name
                //Your code
                course = businessLayer.GetCourseByName(Console.ReadLine());
              
            }
            //find course by id
            else if (input == 2)
            {
                int id = Validator.getId();
                //Get the course by course id
                //Your code
                course = businessLayer.GetCourseById(id);
            }

            if (course != null)
            {   //Update the course
                //Your code
                string courseOldName = course.CourseName;
                Console.WriteLine("Enter a new course name for {0}: ", course.CourseName);
                course.CourseName = Console.ReadLine();
                course.EntityState = EntityState.Modified;
                businessLayer.UpdateCourse(course);
                Console.WriteLine("Course {0} successfully changed to {1}", courseOldName, course.CourseName);
            }
            else
            {
                Console.WriteLine("Course does not exist.");
            };
        }

        /// <summary>
        /// Remove a course in the database.
        /// </summary>
        public static void removeCourse()
        {
            listCourses();
            int id = Validator.getId();
            //Get a Course object by id
            Course course = businessLayer.GetCourseById(id);
            //Your code
            if (course != null)
            {   //Display the message the course name has been removed
                //Remove the course
                //Your code
                course.EntityState = EntityState.Deleted;
                businessLayer.RemoveCourse(course);
                Console.WriteLine("Course removed successfully");
            }
            else
            {
                Console.WriteLine("Course does not exist.");
            };
        }


        /// <summary>
        /// List all courses in the database.
        /// </summary>
        public static void listCourses()
        {   //List all the courses by id and name
            //Display course id and course name
            //Your code
            IList<Course> courses = businessLayer.GetAllCourses();
            foreach (Course course in courses)
            {
                Console.WriteLine("Teacher Name: {0}, ID: {1}.", course.CourseName, course.CourseId);
            }
        }
    }
}