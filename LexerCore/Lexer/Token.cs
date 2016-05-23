using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilingPrinciples.LexerCore
{
    public abstract class Token
    {
        protected int line, col, pos, len;

        public int Line
        {
            get { return line; }
        }

        public int Column
        {
            get { return col; }
        }

        public int Position
        {
            get { return pos; }
        }

        public int Length
        {
            get { return len; }
        }

        protected Tag tag;
        public Tag Tag
        {
            get { return tag; }
        }

        public Token(int line, int col, int pos, int len, Tag tag)
        {
            this.line = line;
            this.col = col;
            this.pos = pos;
            this.len = len;
            this.tag = tag;
        }

        public abstract string GetTokenType();
        public abstract dynamic GetValue();

        public override string ToString()
        {
            return String.Format("<{0}, '{1}'>", GetTokenType(), GetValue());
        }
    }

    public class EndMarker : Token
    {
        public EndMarker(int line, int col, int pos) : base(line, col, pos, 1, Tag.EndMarker) { }

        public override string GetTokenType() { return "EndMarker"; }

        public override dynamic GetValue() { return "$"; }

        public override string ToString() { return GetValue().ToString(); }
    }

    public class InvalidToken : Token
    {
        private string token;

        public string Token
        {
            get { return token; }
        }

        public InvalidToken(int line, int col, int pos, int len, string token) : base(line, col, pos, len, Tag.InvalidToken)
        {
            this.line = line;
            this.col = col;
            this.pos = pos;
            this.len = len;
            this.token = token;
        }

        public override string GetTokenType() { return "InvalidToken"; }

        public override dynamic GetValue()
        {
            return String.Format("Col: {0} - `{1}`", col, token);
        }

        public override string ToString()
        {
            return String.Format("Invalid token at {0}:{1} - `{2}`.", line, col, token);
        }
    }

    public abstract class Word : Token
    {
        protected int idInSymbolTable;

        public int IdInSymbolTable
        {
            get { return idInSymbolTable; }
        }

        public Word(int id, int line, int col, int pos, int len, Tag tag) : base(line, col, pos, len, tag) { this.idInSymbolTable = id; }

        public override dynamic GetValue() { return String.Format("symbol table id: {0}", idInSymbolTable); }
    }

    public class Identifier : Word
    {
        public Identifier(int id, int line, int col, int pos, int len) : base(id, line, col, pos, len, Tag.Identifier) { }
        public override string GetTokenType() { return "Identifier"; }
        public override dynamic GetValue() { return idInSymbolTable; }
    }

    public abstract class Keyword : Word
    {
        protected Keyword(int id, int line, int col, int pos, int len, Tag tag) : base(id, line, col, pos, len, tag) { }

        public static Word Create(int id, string lexeme, int line, int col, int pos, int len, Tag tag)
        {
            switch (tag)
            {
                case Tag.If: return new If(id, line, col, pos);
                case Tag.Then: return new Then(id, line, col, pos);
                case Tag.Else: return new Else(id, line, col, pos);
                case Tag.While: return new While(id, line, col, pos);
                case Tag.Do: return new Do(id, line, col, pos);
                case Tag.VarType: return new VarType(id, lexeme, line, col, pos, len);

                default: // Shall never be called
                    throw new ApplicationException("Keyword Tag Mismatch");
            }
        }

        public override string ToString() { return String.Format("<{0}>", GetTokenType()); }
    }

    public class If : Keyword
    {
        public If(int id, int line, int col, int pos) : base(id, line, col, pos, 2, Tag.If) { }
        public override string GetTokenType() { return "If"; }
    }

    public class Then : Keyword
    {
        public Then(int id, int line, int col, int pos) : base(id, line, col, pos, 4, Tag.Then) { }
        public override string GetTokenType() { return "Then"; }
    }

    public class Else : Keyword
    {
        public Else(int id, int line, int col, int pos) : base(id, line, col, pos, 4, Tag.Else) { }
        public override string GetTokenType() { return "Else"; }
    }

    public class While : Keyword
    {
        public While(int id, int line, int col, int pos) : base(id, line, col, pos, 5, Tag.While) { }
        public override string GetTokenType() { return "While"; }
    }

    public class Do : Keyword
    {
        public Do(int id, int line, int col, int pos) : base(id, line, col, pos, 2, Tag.Do) { }
        public override string GetTokenType() { return "Do"; }
    }

    public class VarType : Keyword
    {
        public enum TypeEnum
        {
            Keyword = 0,

            Int,
            Float
        }

        private TypeEnum type;
        public TypeEnum Type
        {
            get { return type; }
        }

        private int width;
        public int Width
        {
            get { return width; }
        }

        public VarType(int id, TypeEnum type, int width, int line, int col, int pos, int len) : base(id, line, col, pos, len, Tag.VarType)
        {
            this.type = type;
            this.width = width;
        }

        public VarType(int id, string str, int line, int col, int pos, int len) : base(id, line, col, pos, len, Tag.VarType)
        {
            switch (str)
            {
                case "int": type = TypeEnum.Int; width = 4; break;
                case "float": type = TypeEnum.Float; width = 8; break;

                default: // Shall never be called
                    throw new ApplicationException("Variable Type Mismatch");
            }
        }

        public override string GetTokenType() { return "VarType"; }

        public override dynamic GetValue()
        {
            switch (type)
            {
                case TypeEnum.Int: return "int";
                case TypeEnum.Float: return "float";

                default: // Shall never be called
                    throw new ApplicationException("Operator Type Mismatch");
            }
        }

        public override string ToString()
        {
            return String.Format("<{0}, '{1}'>", GetTokenType(), GetValue());
        }
    }

    public class Decimal : Token
    {
        private int value;

        public Decimal(int value, int line, int col, int pos, int len) : base(line, col, pos, len, Tag.Decimal) { this.value = value; }
        public override string GetTokenType() { return "Decimal"; }
        public override dynamic GetValue() { return value; }
    }

    public class Operator : Token
    {
        public enum TypeEnum
        {
            Plus,
            Minus,
            Multiply,
            Divide,
            Greater,
            Lesser,
            Assign,
            LeftParenthesis,
            RightParenthesis,
            Equal,
            GreaterOrEqual,
            LesserOrEqual,
            NotEqual
        }

        private TypeEnum type;
        public TypeEnum Type
        {
            get { return type; }
        }

        public Operator(TypeEnum type, int line, int col, int pos, int len) : base(line, col, pos, len, Tag.Operator) { this.type = type; }

        public Operator(char ch, int line, int col, int pos, int len) : this(ch.ToString(), line, col, pos, len) { }

        public Operator(string str, int line, int col, int pos, int len) : base(line, col, pos, len, Tag.Operator)
        {
            switch (str)
            {
                case "+": type = TypeEnum.Plus; break;
                case "-": type = TypeEnum.Minus; break;
                case "*": type = TypeEnum.Multiply; break;
                case "/": type = TypeEnum.Divide; break;
                case ">": type = TypeEnum.Greater; break;
                case "<": type = TypeEnum.Lesser; break;
                case "=": type = TypeEnum.Assign; break;
                case "(": type = TypeEnum.LeftParenthesis; break;
                case ")": type = TypeEnum.RightParenthesis; break;
                case "==": type = TypeEnum.Equal; break;
                case ">=": type = TypeEnum.GreaterOrEqual; break;
                case "<=": type = TypeEnum.LesserOrEqual; break;
                case "!=": type = TypeEnum.NotEqual; break;

                default: // Shall never be called
                    throw new ApplicationException("Operator Type Mismatch");
            }
        }

        public override string GetTokenType() { return "Operator"; }
        public override dynamic GetValue()
        {
            switch (type)
            {
                case TypeEnum.Plus: return "+";
                case TypeEnum.Minus: return "-";
                case TypeEnum.Multiply: return "*";
                case TypeEnum.Divide: return "/";
                case TypeEnum.Greater: return ">";
                case TypeEnum.Lesser: return "<";
                case TypeEnum.Assign: return "=";
                case TypeEnum.LeftParenthesis: return "(";
                case TypeEnum.RightParenthesis: return ")";
                case TypeEnum.Equal: return "==";
                case TypeEnum.GreaterOrEqual: return ">=";
                case TypeEnum.LesserOrEqual: return "<=";
                case TypeEnum.NotEqual: return "!=";
                default: // Shall never be called
                    throw new ApplicationException("Operator Type Mismatch");
            }
        }
    }

    public class Separator : Token
    {
        private char ch;

        public Separator(char ch, int line, int col, int pos) : base(line, col, pos, 1, Tag.Separator) { this.ch = ch; }
        public override string GetTokenType() { return "Separator"; }
        public override dynamic GetValue() { return ch; }
    }
}
