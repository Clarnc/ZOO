using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class ScreenDefinition
    {

        public List<ScreenLineEntry> lineEntries = new List<ScreenLineEntry>(){
            new ScreenLineEntry(
                ConsoleColor.Black.ToString(),
                ConsoleColor.White.ToString(),
                ""
                )
        };
        
         
    }
}
