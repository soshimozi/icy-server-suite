using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class Enums
    {
        public enum TokenCode
        {
            Dummy,
            Identifier, Number, String, EndOfFile, Error,
            LParen, RParen, Equal, LBracket, RBracket, Colon, Semicolon, Lt,
            Gt, Comma, Period, Le, Ge, Ne, From, As, Where, Question, Like,
            In
        }

        public enum CharCode
        {
            Letter,
            Digit,
            Special,
            Quote,
            WhiteSpace,
            EndOfFile,
            Error
        }

        public enum ErrorCode
        {
            None,
            Unrecognizable,
            TooMany,
            UnexpectedEndOfFile,
            InvalidNumber,
            InvalidFraction,
            InvalidExponent,
            TooManyDigits,
            RealOutOfRange,
            IntegerOutOfRange,
            MissingRightParen,
            InvalidExpression,
            InvalidAssignment,
            MissingIdentifier,
            MissingColonEqual,
            UndefinedIdentifier,
            StackOverflow,
            InvalidStatement,
            UnexpectedToken,
            MissingSemicolon,
            MissingComma,
            MissingDO,
            MissingUNTIL,
            MissingTHEN,
            InvalidFORControl,
            MissingOF,
            InvalidConstant,
            MissingConstant,
            MissingColon,
            MissingEND,
            MissingTOorDOWNTO,
            RedefinedIdentifier,
            MissingEqual,
            InvalidType,
            NotATypeIdentifier,
            InvalidSubrangeType,
            NotAConstantIdentifier,
            MissingDotDot,
            IncompatibleTypes,
            InvalidTarget,
            InvalidIdentifierUsage,
            IncompatibleAssignment,
            MinGtMax,
            MissingLeftBracket,
            MissingRightBracket,
            InvalidIndexType,
            MissingBEGIN,
            MissingPeriod,
            TooManySubscripts,
            InvalidField,
            NestingTooDeep,
            MissingPROGRAM,
            AlreadyForwarded,
            WrongNumberOfParms,
            InvalidVarParm,
            NotARecordVariable,
            MissingVariable,
            CodeSegmentOverflow,
            UnimplementedFeature,
        }


    }
}
