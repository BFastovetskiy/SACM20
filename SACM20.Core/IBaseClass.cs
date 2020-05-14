using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core
{
    public delegate StateResult RenameObjectHandler(string oldTitle, string newTitle);

    public interface IBaseClass : IClass
    {
        event RenameObjectHandler RenameObject;

        string Title { get; internal set; }
        
        string Description { get; set; }
        
        string ConfigurationItem { get; set; }

        StateResult Rename(string newTitle);

        static IBaseClass Create() => throw new NotImplementedException();
    }
}
