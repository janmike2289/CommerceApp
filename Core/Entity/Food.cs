using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    [method: System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public class Food() : Product
    {
    }

    public class Program
    {
        static void Main(string[] args) 
        {
            var food = new Food
            {
                Brand = "a"
            };
        }
    }
    
}
