using System;
using Malshinon.DAL;
using Malshinon.Database;
using Malshinon.Management;
using Malshinon.Models;

namespace Malshinon
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbConnection db = new DbConnection();
            db.Connect();

            //FlowManager flowManager = new FlowManager();



            //flowManager.AddPersonFlow();


            PersonDAL personDal = new PersonDAL();
            
            Person newPerson1 = new Person
            {
                FirstName = "David",
                LastName = "Cohen",
                SecretCode = "Alpha123",
                Type = "Agent",
                NumReports = 5,
                NumMentions = 10
            };

            personDal.UpdatePerson(newPerson1);

            

            //    personDal.AddPerson(newPerson1);
            //    Console.WriteLine("Person added successfully.");


            //var personFromDb = personDal.GetPersonByName("David", "Cohen");
            //if (personFromDb != null)
            //{
            //    Console.WriteLine($"Found person: {personFromDb.FirstName} {personFromDb.LastName}, Secret Code: {personFromDb.SecretCode}");
            //}
            //else
            //{
            //    Console.WriteLine("Person not found in database.");
            //}


        }
    }
}
