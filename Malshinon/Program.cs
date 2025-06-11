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


            FlowManager flowManager = new FlowManager();
            flowManager.Menu();


        }
    }
}
