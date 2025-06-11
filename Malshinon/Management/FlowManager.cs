using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.DAL;
using Malshinon.Models;

namespace Malshinon.Management
{
    public class FlowManager
    {
        private PersonDAL personDAL = new PersonDAL();
        private ReportDAL reportDAL = new ReportDAL();
        public Person reporter = null;
        public Person targrt = null;


        public FlowManager()
        {
            Console.WriteLine("Enter your SecretCode: ");
            string SecretCode = Console.ReadLine();
            reporter = GetPersonFlow(SecretCode);
            AddReportFlow();
        }

        public Person GetPersonFlow(string secretCode)
        {
            Person person = personDAL.GetPersonBySecretCode(secretCode);
            if (person == null)
            {
                person = AddPersonFlow(secretCode);
            }
            return person;
        }


        private Person AddPersonFlow(string secretCode)
        {
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Person newPerson = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                SecretCode = secretCode,
                Type = "",
                NumReports = 0,
                NumMentions = 0
            };

            personDAL.AddPerson(newPerson);
            Console.WriteLine("Person added successfully.");
            newPerson = personDAL.GetPersonBySecretCode(secretCode);
            return newPerson;
        }


        private void AddReportFlow()
       {
            Console.WriteLine("Enter target SecretCode:");
            string targetSecretCode = Console.ReadLine();
            targrt = GetPersonFlow(targetSecretCode);

            Console.WriteLine("Enter report text:");
            string reportText = Console.ReadLine();

            IntelReport newReport = new IntelReport
            {
                ReporterId = reporter.Id,
                TargetId = targrt.Id,
                Text = reportText,
            };

            reportDAL.AddReport(newReport);
            Console.WriteLine("Report added successfully.");
            UpdateNumReports(reporter);
            UpdateNumMentions(targrt);
        }

        private void UpdateNumReports(Person person)
        {
            person.NumReports++;
            personDAL.UpdatePerson(person);
        }

        private void UpdateNumMentions(Person person)
        {
            person.NumMentions++;
            personDAL.UpdatePerson(person);
        }
    }

}