using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListApplicants applicants = new ListApplicants
                (
                    new List<Applicant>
                    {
                        new Applicant(1, "John", 'M', 2021, 174),
                        new Applicant(2, "Sara", 'F', 2021, 193),
                        new Applicant(3, "Elisabet", 'F', 2022, 160),
                        new Applicant(4, "Richard", 'M', 2021, 180),
                        new Applicant(5, "Fred", 'M', 2020, 185),
                        new Applicant(6, "Leo", 'M', 2021, 177)
                    }
                );
            applicants.Show();
            applicants.Add(new Applicant(7, "Rebeca", 'F', 2020, 189));
            applicants.Show();
            applicants.EditZno(3, 170);
            applicants.Show();
            Console.Write("Введіть рік закіньчення школи: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Введіть рік бал зно: ");
            int zno = int.Parse(Console.ReadLine());
            applicants.TotalApplicantsInYearByZno(year, zno);
            applicants.TotalApplicantsInYear();
        }
    }
    
    public class Applicant
    {
        protected int id;
        public int Id { get { return id; } set { id = value; } }

        protected string name;
        public string Name { get { return name; } set { name = value; } }

        protected char sex;
        public char Sex { get { return sex; } set { sex = ((value == 'M' || value == 'F') ? value : 'U'); } }

        protected int year;
        public int Year { get { return year; } set { year = (value > 0 ? value: 0); } }

        protected float zno;
        public float Zno { get { return zno; } set { zno = (value >= 0 ? value : 0); } }

        public Applicant(int id, string name, char sex, int year, float zno)
        {
            this.id = id;
            this.name = name;
            this.sex = sex;
            this.year = year;
            this.zno = zno;
        }

        public override string ToString()
        {
            return $"Applicant {name} ended school in {year}, ZNO - {zno}";
        }
    }

    public interface IListApplicants
    {
        void Add(Applicant introduction);
        void Delete(int id);
        void EditZno(int id, float zno);
        void Show();
        void TotalApplicantsInYearByZno(int year, float zno);
        void TotalApplicantsInYear();
    }

    public class ListApplicants : IListApplicants
    {
        protected List<Applicant> applicants;
        public List<Applicant> Applicants { get { return applicants; } set { applicants = value; } }

        public ListApplicants(List<Applicant> applicants)
        {
            this.applicants = applicants;
        }

        public void Add(Applicant applicant)
        {
            applicants.Add(applicant);
        }

        public void Delete(int id)
        {
            try
            {
                applicants = applicants.Where(item => item.Id != id).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditZno(int id, float zno)
        {
            try
            {
                applicants.First(item => item.Id == id).Zno = zno;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Show()
        {
            foreach (Applicant applicant in applicants)
            {
                Console.WriteLine(applicant);
            }
            Console.WriteLine();
        }
        public void TotalApplicantsInYearByZno(int year, float zno)
        {
            
            foreach (Applicant applicant in applicants)
            {
                if (applicant.Year == year && applicant.Zno >= zno)
                {
                    Console.WriteLine(applicant);
                }
            }
            Console.WriteLine();
        }
        public void TotalApplicantsInYear()
        {
            int counter = 0;
            List<int> array = new List<int>();
            foreach(Applicant applicant in applicants)
            {
                if (array.Contains(applicant.Year))
                {
                    continue;
                }
                array.Add(applicant.Year);
            }
            for(int i = 0; i < array.Count; i++)
            {
                foreach(Applicant applicant in applicants)
                {
                    if (array[i] == applicant.Year)
                    {
                        counter++;
                    }
                }
                Console.WriteLine($"{array[i]} - {counter}");
                counter = 0;
            }
        }
    }
}
