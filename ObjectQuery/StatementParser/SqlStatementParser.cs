using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class SqlStatementParser
    {
        public SqlStatementParser()
        {
        }

        public SqlCriteria ParseStatement(string sql)
        {
            TextBuffer buffer = new TextBuffer(sql);
            SqlScanner scanner = new SqlScanner(buffer);

            SqlCriteria statement = null;

            // first token should be from
            IToken token = scanner.Get();
            if (token.Code == Enums.TokenCode.From)
            {
                string typeNameString;
                string placeHolderString;
                WhereClause [] whereClauses;
                if (parseFrom(scanner, out typeNameString))
                {
                    if (parseAs(scanner, out placeHolderString))
                    {
                        if (parseWhere(scanner, out whereClauses))
                        {
                            statement = new SqlCriteria();

                            statement.PlaceholderName = placeHolderString;
                            statement.TypeName = typeNameString;
                            statement.WhereClauses = whereClauses;
                        }
                    }
                }
            }

            return statement;
        }

        private bool parseWhere(SqlScanner scanner, out WhereClause [] whereClauses)
        {
            bool parseValid = false;
            List<WhereClause> clauses = new List<WhereClause>();

            Enums.TokenCode lastTokenCode = Enums.TokenCode.Dummy;
            IToken token = scanner.Get();

            StringBuilder currentFieldNameBuilder = new StringBuilder();

            WhereClause currentClause = new WhereClause();
            while (token.Code != Enums.TokenCode.EndOfFile && token.Code != Enums.TokenCode.Error)
            {
                switch (token.Code)
                {
                    case Enums.TokenCode.Identifier:
                        if (lastTokenCode == Enums.TokenCode.Dummy ||
                            lastTokenCode == Enums.TokenCode.Comma || 
                            lastTokenCode == Enums.TokenCode.Period)
                        {
                            lastTokenCode = token.Code;
                            currentFieldNameBuilder.Append(token.TokenString);
                            //names.Add(token.TokenString);
                            token = scanner.Get();
                        }
                        else
                        {
                            token.Code = Enums.TokenCode.Error;
                        }
                        break;

                    case Enums.TokenCode.Period:
                        if (lastTokenCode == Enums.TokenCode.Identifier)
                        {
                            lastTokenCode = token.Code;
                            currentFieldNameBuilder.Append(".");
                            token = scanner.Get();
                        }
                        else
                        {
                            token.Code = Enums.TokenCode.Error;
                        }
                        break;

                    case Enums.TokenCode.Comma:
                        if (lastTokenCode == Enums.TokenCode.Question || lastTokenCode == Enums.TokenCode.In)
                        {
                            lastTokenCode = token.Code;
                            token = scanner.Get();
                        }
                        else
                        {
                            token.Code = Enums.TokenCode.Error;
                        }
                        break;

                    case Enums.TokenCode.Question:
                        if (lastTokenCode == Enums.TokenCode.Equal ||
                            lastTokenCode == Enums.TokenCode.Le ||
                            lastTokenCode == Enums.TokenCode.Ge ||
                            lastTokenCode == Enums.TokenCode.Gt ||
                            lastTokenCode == Enums.TokenCode.Lt ||
                            lastTokenCode == Enums.TokenCode.Ne)
                        {
                            lastTokenCode = token.Code;
                            token = scanner.Get();
                        }
                        else
                        {
                            token.Code = Enums.TokenCode.Error;
                        }
                        break;

                    case Enums.TokenCode.Equal:
                    case Enums.TokenCode.Le:
                    case Enums.TokenCode.Ne:
                    case Enums.TokenCode.Gt:
                    case Enums.TokenCode.Lt:
                    case Enums.TokenCode.Ge:
                    case Enums.TokenCode.Like:
                    case Enums.TokenCode.In:
                        if (lastTokenCode == Enums.TokenCode.Identifier)
                        {
                            if (parseRVal(scanner, token, currentClause))
                            {
                                currentClause.FieldName = currentFieldNameBuilder.ToString();
                                clauses.Add(currentClause);

                                currentClause = new WhereClause();
                                currentFieldNameBuilder = new StringBuilder();

                                lastTokenCode = token.Code;
                                token = scanner.Get();
                            }
                            else
                            {
                                token.Code = Enums.TokenCode.Error;
                            }
                        }
                        else
                        {
                            token.Code = Enums.TokenCode.Error;
                        }
                        break;

                    default:
                        token.Code = Enums.TokenCode.Error;
                        break;
                }
            }
            whereClauses = null;

            if (token.Code != Enums.TokenCode.Error)
            {
                whereClauses = clauses.ToArray();
                parseValid = true;
            }

            return parseValid;
        }

        private bool parseRVal(SqlScanner scanner, IToken token, WhereClause currentClause)
        {
            bool parseSuccess = true;

            // convert token code to operation
            if (token.Code == Enums.TokenCode.Equal)
            {
                currentClause.FieldOperation = WhereClause.Operation.Equal;
            }
            else if (token.Code == Enums.TokenCode.Le)
            {
                currentClause.FieldOperation = WhereClause.Operation.LessThanOrEqual;
            }
            else if (token.Code == Enums.TokenCode.Ne)
            {
                currentClause.FieldOperation = WhereClause.Operation.NotEqual;
            }
            else if (token.Code == Enums.TokenCode.Gt)
            {
                currentClause.FieldOperation = WhereClause.Operation.GreaterThan;
            }
            else if (token.Code == Enums.TokenCode.Lt)
            {
                currentClause.FieldOperation = WhereClause.Operation.LessThan;
            }
            else if (token.Code == Enums.TokenCode.Ge)
            {
                currentClause.FieldOperation = WhereClause.Operation.GreaterThanOrEqual;
            }
            else if (token.Code == Enums.TokenCode.Like)
            {
                currentClause.FieldOperation = WhereClause.Operation.Like;
            }
            else if (token.Code == Enums.TokenCode.In)
            {
                currentClause.FieldOperation = WhereClause.Operation.Set;
                parseSuccess = parseSet(scanner, currentClause);
            }
            else
            {
                parseSuccess = false;
            }

            return parseSuccess;
        }

        private bool parseSet(SqlScanner scanner, WhereClause currentClause)
        {
            bool parseSuccess = false;

            IToken token = scanner.Get();
            if (token.Code == Enums.TokenCode.LBracket)
            {
                token = scanner.Get();
                if (token.Code == Enums.TokenCode.Number)
                {
                    int setSize = token.TokenValue.IntegerValue;
                    currentClause.SetSize = setSize;

                    token = scanner.Get();
                    if (token.Code == Enums.TokenCode.RBracket)
                    {
                        parseSuccess = true;
                    }
                }
            }

            return parseSuccess;
        }

        private bool parseAs(SqlScanner scanner, out string text)
        {
            IToken token = scanner.Get();
            text = "";
            bool parseValid = false;
            string identifierName = string.Empty;
            if (token.Code == Enums.TokenCode.Identifier)
            {
                identifierName = token.TokenString;
                if (scanner.Get().Code == Enums.TokenCode.Where)
                {
                    text = identifierName;
                    parseValid = true;
                }
            }

            return parseValid;
        }

        private bool parseFrom(SqlScanner scanner, out string text)
        {
            // parse the doman name
            IToken token;
            StringBuilder typeNameBuider = new StringBuilder();
            Enums.TokenCode lastTokenCode = Enums.TokenCode.Dummy;

            text = "";
            bool parseValid = false;
            while ((token = scanner.Get()).Code != Enums.TokenCode.As)
            {
                switch (token.Code)
                {
                    case Enums.TokenCode.Period:
                        if (lastTokenCode != Enums.TokenCode.Identifier)
                        {
                            // TODO: error handling
                        }
                        else
                        {
                            typeNameBuider.Append(".");
                            lastTokenCode = token.Code;
                        }
                        break;

                    case Enums.TokenCode.Identifier:
                        if (lastTokenCode == Enums.TokenCode.Dummy || lastTokenCode == Enums.TokenCode.Period)
                        {
                            typeNameBuider.Append(token.TokenString);
                            lastTokenCode = token.Code;
                        }
                        else
                        {
                            // TODO: error handling
                        }
                        break;
                }

                if (token.Code == Enums.TokenCode.EndOfFile)
                {
                    // time to bail
                    break;
                }
            }

            if (lastTokenCode != Enums.TokenCode.Identifier)
            {
                // TODO: error handling
            }
            else
            {
                parseValid = true;
                text = typeNameBuider.ToString();
            }

            return parseValid;
        }
    }
}
