using System;
using System.Collections.Generic;
using System.Text;

namespace CarAdverts.Core.Exceptions
{
    public class NotFoundException :Exception
    {
        public NotFoundException(string name, object key)
            :base($"Entity \"{name}\" ({key}) not found.")
        {
            
        }
    }
}
