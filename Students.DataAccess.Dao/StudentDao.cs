using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Students.Common.Logic;

namespace Students.DataAccess.Dao
{
    public class StudentDao : IStudentDao

    {
         string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Student Insert(Student student)
        {  
            string line = (Convert.ToString(student.ID) + "," + student.Name + "," +  student.Surname + "," + student.DNI + "," + Convert.ToString(student.Guid) + "," + Convert.ToString(student.Register) + "," + Convert.ToString(student.Age));
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\Students.txt"))
            {              
                    outputFile.WriteLine(line);
            }
            return GetLastStudent();

        }
         public Student GetLastStudent()
        {
            
            using (StreamReader inputFile = new StreamReader(mydocpath + @"\Students.txt"))
            {
                var arrayStrings = new string[8];
                while (inputFile.Peek() != -1)
                {
                    var stringLine = inputFile.ReadLine();
                    arrayStrings = stringLine.Split(',');
                }
                Student student = new Student(Convert.ToInt32(arrayStrings[0]), arrayStrings[1], arrayStrings[2], arrayStrings[3], Convert.ToDateTime(arrayStrings[4]), Guid.Parse(arrayStrings[5]), Convert.ToInt32(arrayStrings[6]), Convert.ToDateTime(arrayStrings[7]));
                return student;
            }
            

        }

        public Student AggregateJson(Student student)
        {
            {
                string json = JsonConvert.SerializeObject(student.ToString());
                //write string to file
                System.IO.File.WriteAllText((mydocpath + @"\Students.json"), json);
            }
            return GetLastJsonStudent();
        }

        public Student GetLastJsonStudent()
        {
            using (StreamReader r = new StreamReader(mydocpath + @"\Students.json"))
            {
                string json = r.ReadToEnd();
                var student = JsonConvert.DeserializeObject<Student>(json);
                return student;
            }
        }
    }
}

