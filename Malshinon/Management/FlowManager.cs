using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using Malshinon.DAL;
using Malshinon.Models;
using MySqlX.XDevAPI.Common;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

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
            Console.WriteLine("\n--- Welcome to the Malshinon app! ---");
            Console.WriteLine("\nEnter your SecretCode: ");
            string SecretCode = Console.ReadLine();
            reporter = GetPersonFlow(SecretCode);
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("\n1. Add Report");
                Console.WriteLine("2. Display Reports By Reporter id");
                Console.WriteLine("3. Get your secret code");
                Console.WriteLine("4. Get All Peopl");
                Console.WriteLine("5. Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddReportFlow();
                        break;
                    case "2":
                        DisplayReportsByReporterFlow();
                        break;
                    case "3":
                        GetSecretCode();
                        break;
                    case "4":
                        GetAllThePeople();
                        break;
                    case "5":
                        Console.WriteLine("\nExiting.");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
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
            Console.WriteLine("\nEnter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("\nEnter last name:");
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
            Console.WriteLine("\nPerson added successfully.");
            newPerson = personDAL.GetPersonBySecretCode(secretCode);
            return newPerson;
        }


        private void AddReportFlow()
        {
            Console.WriteLine("\nEnter target SecretCode:");
            string targetSecretCode = Console.ReadLine();
            targrt = GetPersonFlow(targetSecretCode);

            Console.WriteLine("\nEnter report text:");
            string reportText = Console.ReadLine();

            IntelReport newReport = new IntelReport
            {
                ReporterId = reporter.Id,
                TargetId = targrt.Id,
                Text = reportText,
            };

            reportDAL.AddReport(newReport);
            Console.WriteLine("\nReport added successfully.");
            UpdateReporter(reporter);
            Updatetargrt(targrt);
        }

        private void UpdateReporter(Person person)
        {
            person.NumReports++;

            if (person.Type == "target")
            {
                person.Type = "both";
            }
            person.Type = "reporter";
            personDAL.UpdatePerson(person);
        }

        private void Updatetargrt(Person person)
        {
            person.NumMentions++;

            if (person.Type == "reporter")
            {
                person.Type = "both";
            }
            person.Type = "target";
            personDAL.UpdatePerson(person);
        }

        public void DisplayReportsByReporterFlow()
        {
            Console.WriteLine("\nEnter reporter ID:");
            int reporterId = int.Parse(Console.ReadLine());

            List<IntelReport> reports = reportDAL.GetReportsByReporter(reporterId);

            if (reports.Count == 0)
            {
                Console.WriteLine("\nNo reports found for this reporter.");
                return;
            }

            foreach (var report in reports)
            {
                Console.WriteLine($"\nReport ID: {report.Id}, \nTarget ID: {report.TargetId}, \nText: {report.Text}, \nTimestamp: {report.Timestamp}");
            }
        }

        public void GetSecretCode()
        {
            Console.WriteLine("\nEnter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("\nEnter last name:");
            string lastName = Console.ReadLine();

            string cod = personDAL.GetSecretCodeByName(firstName, lastName);
            string eror = "The name entered does not match what is found in the system.";

            if (cod != null)
            {
                Console.WriteLine(cod);
            }
            Console.WriteLine(eror);
        }

        private void GetAllThePeople()
        {
            List<Person> persons = personDAL.GetAllPeople();

            foreach (var person in persons)
            {
                Console.WriteLine($"\nPerson ID: {person.Id}, \nFirst Name: {person.FirstName}, \nLast Name: {person.LastName}, \nType: {person.Type}, \nNumber Of Reports: {person.NumReports}");
            }
        }

    } 

}