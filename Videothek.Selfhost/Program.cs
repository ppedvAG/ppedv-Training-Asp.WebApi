using Microsoft.Owin.Hosting;
using System;

namespace Videothek.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: "http://localhost:6666"))
            {
                Console.ReadLine();
            } 
        }
    }
}
