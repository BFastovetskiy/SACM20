using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core
{
    public interface IClass
    {
        Guid Id { get; }

        string Serialize();

        static T Deserialize<T>(string sourceString) => throw new NotImplementedException();
    }
}
