using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors.Messages
{
    public class SimpleResponseMessage
    {
        public int Value { get; private set; }
        public SimpleResponseMessage(int value)
        {
            Value = value;
        }
    }
}
