using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account_Manager
{
    internal class Globals
    {
        // this class will make the logged in user if global everywhere in the application
        // https://www.codeproject.com/Questions/233650/How-to-define-Global-veriable-in-Csharp-net

        public static int GlobalUserId { get; private set; }
        public static void setGlobalUserId(int userId) 
        { 
            GlobalUserId = userId; 
        }
    }
}
