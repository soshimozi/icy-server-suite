using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class DataType
    {
        public enum TypeCode
        {
            Dummy,
            Integer,
            Real,
            Character,
            String,
            Boolean
        }

        private static DataType booleanType;
        private static DataType realType;
        private static DataType intType;
        private static DataType dummyType;
        private static DataType charType;
        private static DataType stringType;


        private TypeCode typeCode;

        public TypeCode Code
        {
            get { return typeCode; }
            set { typeCode = value; }
        }

        static DataType()
        {
            InitializeBuiltInTypes();
        }

        private static void InitializeBuiltInTypes()
        {
            booleanType = new DataType();
            booleanType.Code = DataType.TypeCode.Boolean;

            realType = new DataType();
            realType.Code = DataType.TypeCode.Real;

            intType = new DataType();
            intType.Code = DataType.TypeCode.Integer;

            dummyType = new DataType();
            dummyType.Code = DataType.TypeCode.Dummy;

            stringType = new DataType();
            stringType.Code = TypeCode.String;

            charType = new DataType();
            charType.Code = TypeCode.Character;
        }


        public static DataType IntegerType
        {
            get { return intType; }
        }

        public static DataType RealType
        {
            get { return realType; }
        }

        public static DataType BooleanType
        {
            get { return booleanType; }
        }

        public static DataType CharType
        {
            get { return charType; }
        }

        public static DataType StringType
        {
            get { return stringType; }
        }

        public static DataType DummyType
        {
            get { return dummyType; }
        }

        public static bool CheckAssignmentTypeCompatible(DataType targetType, DataType valueType, IInputBuffer buffer)
        {
            //--Two identical types.
            if (targetType == valueType) return true;

            //--real := integer
            if ((targetType == DataType.RealType)
            && (valueType == DataType.IntegerType)) return true;

            return false;

        }


        public static DataType GetType(TypeCode typeCode)
        {
            DataType type;

            switch (typeCode)
            {
                case TypeCode.Dummy:
                    type = dummyType;
                    break;

                case TypeCode.Character:
                    type = charType;
                    break;

                case TypeCode.Boolean:
                    type = booleanType;
                    break;

                case TypeCode.Integer:
                    type = intType;
                    break;

                case TypeCode.Real:
                    type = realType;
                    break;

                case TypeCode.String:
                    type = stringType;
                    break;

                default:
                    type = dummyType;
                    break;
            }

            return type;

        }
    }
}
