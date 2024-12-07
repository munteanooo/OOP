using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string ID { get; set; }
    public string StudentName { get; set; }
    public bool IsGraduated { get; set; }

    public Student(string id, string name)
    {
        ID = id;
        StudentName = name;
        IsGraduated = false;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {StudentName}, Graduated: {IsGraduated}";
    }
}

class Faculty
{
    public string FacultyName { get; set; }
    public string Field { get; set; }
    private List<Student> CurrentStudents { get; set; }
    private List<Student> GraduatedStudents { get; set; }

    public Faculty(string name, string field)
    {
        FacultyName = name;
        Field = field;
        CurrentStudents = new List<Student>();
        GraduatedStudents = new List<Student>();
    }

    public void add_students(Student student)
    {
        CurrentStudents.Add(student);
    }

    public void graduate_student(string student_id)
    {
        var student = CurrentStudents.FirstOrDefault(s => s.ID == student_id);
        if (student != null)
        {
            student.IsGraduated = true;
            CurrentStudents.Remove(student);
            GraduatedStudents.Add(student);
        }
        else
        {
            Console.WriteLine("Student not found in this faculty.");
        }
    }

    public void display_students()
    {
        Console.WriteLine($"Current students in {FacultyName}:");
        foreach (var student in CurrentStudents)
        {
            Console.WriteLine(student);
        }
    }

    public void display_graduated()
    {
        Console.WriteLine($"Graduated students from {FacultyName}:");
        foreach (var student in GraduatedStudents)
        {
            Console.WriteLine(student);
        }
    }

    public bool has_students(string student_id)
    {
        return CurrentStudents.Any(s => s.ID == student_id) || GraduatedStudents.Any(s => s.ID == student_id);
    }
}

class University
{
    private List<Faculty> Faculties;

    public University()
    {
        Faculties = new List<Faculty>();
    }

    public void create_faculty(string name, string field)
    {
        Faculties.Add(new Faculty(name, field));
    }

    public void add_student(string faculty_name, Student student)
    {
        var faculty = Faculties.FirstOrDefault(f => f.FacultyName == faculty_name);
        if (faculty != null)
        {
            faculty.add_students(student);
        }
        else
        {
            Console.WriteLine("Faculty not found.");
        }
    }

    public void graduate_student(string faculty_name, string student_id)
    {
        var faculty = Faculties.FirstOrDefault(f => f.FacultyName == faculty_name);
        if (faculty != null)
        {
            faculty.graduate_student(student_id);
        }
        else
        {
            Console.WriteLine("Faculty not found.");
        }
    }

    public void display_faculties()
    {
        Console.WriteLine("Faculties:");
        foreach (var faculty in Faculties)
        {
            Console.WriteLine($"{faculty.FacultyName} ({faculty.Field})");
        }
    }

    public void faculties_by_field(string field)
    {
        Console.WriteLine($"Faculties in the field of {field}:");
        foreach (var faculty in Faculties.Where(f => f.Field == field))
        {
            Console.WriteLine(faculty.FacultyName);
        }
    }

    public void faculty_by_student(string student_id)
    {
        foreach (var faculty in Faculties)
        {
            if (faculty.has_students(student_id))
            {
                Console.WriteLine($"Student found in faculty: {faculty.FacultyName}");
                return;
            }
        }
        Console.WriteLine("Student not found in any faculty.");
    }
}

class Program
{
    static void Main()
    {
        var university = new University();

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Create Faculty");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. Graduate Student");
            Console.WriteLine("4. Display Faculties");
            Console.WriteLine("5. Display Students by Faculty");
            Console.WriteLine("6. Search Faculty by Student");
            Console.WriteLine("7. Display Faculties by Field");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter faculty name: ");
                    var facultyName = Console.ReadLine();
                    Console.Write("Enter faculty field: ");
                    var facultyField = Console.ReadLine();
                    university.create_faculty(facultyName, facultyField);
                    break;
                case "2":
                    Console.Write("Enter faculty name: ");
                    var facultyForStudent = Console.ReadLine();
                    Console.Write("Enter student ID: ");
                    var studentId = Console.ReadLine();
                    Console.Write("Enter student name: ");
                    var studentName = Console.ReadLine();
                    university.add_student(facultyForStudent, new Student(studentId, studentName));
                    break;
                case "3":
                    Console.Write("Enter faculty name: ");
                    var gradFaculty = Console.ReadLine();
                    Console.Write("Enter student ID: ");
                    var gradStudentId = Console.ReadLine();
                    university.graduate_student(gradFaculty, gradStudentId);
                    break;
                case "4":
                    university.display_faculties();
                    break;
                case "5":
                    Console.Write("Enter faculty name: ");
                    var facultyToDisplay = Console.ReadLine();
                    var faculty = university.Faculties.FirstOrDefault(f => f.FacultyName == facultyToDisplay);
                    if (faculty != null)
                    {
                        faculty.display_students();
                    }
                    else
                    {
                        Console.WriteLine("Faculty not found.");
                    }
                    break;
                case "6":
                    Console.Write("Enter student ID: ");
                    var searchStudentId = Console.ReadLine();
                    university.faculty_by_student(searchStudentId);
                    break;
                case "7":
                    Console.Write("Enter field name: ");
                    var fieldName = Console.ReadLine();
                    university.faculties_by_field(fieldName);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
