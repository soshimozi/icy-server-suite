using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class DataValue
    {
        private int integerValue;
        private double realValue;
        private char characterValue;
        private string stringValue;

        public int IntegerValue
        {
            get { return integerValue; }
            set { integerValue = value; }
        }

        public double RealValue
        {
            get { return realValue; }
            set { realValue = value; }
        }

        public char CharacterValue
        {
            get { return characterValue; }
            set { characterValue = value; }
        }

        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
