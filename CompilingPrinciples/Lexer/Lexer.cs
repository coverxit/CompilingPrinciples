using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Symbol;

namespace Lex
{
    public class Lexer
    {
        private int line = 1;
        public int CurrentLine
        {
            get { return line; }
        }
        
        private int col = 0;
        public int CurrentColumn
        {
            get { return col; }
        }

        private int bytesAnalyzed = 0;
        public int BytesAnalyzed
        {
            get { return bytesAnalyzed; }
        }

        private Stream stream;
        private int nextByte = ' ';

        private SymbolTable symbolTable = new SymbolTable();
        public SymbolTable SymbolTable
        {
            get { return new SymbolTable(symbolTable); }
        }

        public Lexer() : this(null) { }

        public Lexer(Stream stream)
        {
            this.stream = stream;

            reserveKeyword("if", Tag.If);
            reserveKeyword("then", Tag.Then);
            reserveKeyword("else", Tag.Else);
            reserveKeyword("while", Tag.While);
            reserveKeyword("do", Tag.Do);

            reserveKeyword("int", Tag.VarType);
            reserveKeyword("float", Tag.VarType);
        }

        public void SetStream(Stream stream)
        {
            this.stream = stream;
        }

        public Token ScanNextToken()
        {
        beginScanToken:
            for (; ; readNextByte())
            {
                if (nextByte == ' ' || nextByte == '\t' || nextByte == '\r') continue;
                else if (nextByte == '\n') { line++; col = 0; }
                else if (nextByte == -1) { return null; } // EOF
                else break;
            }

            if (char.IsDigit((char)nextByte))
            {
                int value = 0;

                do
                {
                    value = 10 * value + nextByte - '0';
                    readNextByte();
                } while (char.IsDigit((char)nextByte));

                return new Lex.Decimal(value);
            }

            if (char.IsLetter((char)nextByte))
            {
                StringBuilder sb = new StringBuilder();
                do
                {
                    sb.Append((char)nextByte);
                    readNextByte();
                } while (char.IsLetterOrDigit((char)nextByte));

                string lexeme = sb.ToString();
                var ret = symbolTable.GetSymbolEntry(lexeme);

                if (ret != null)
                    return ret.Item1.Tag == Tag.Identifier ? (Word) new Identifier(id: ret.Item2) :
                                                             Keyword.Create(id: ret.Item2, tag: ret.Item1.Tag, lexeme: lexeme);

                return new Identifier(id: symbolTable.AddSymbol(lexeme, Tag.Identifier));
            }

            int curByte = nextByte;
            switch (curByte)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '(':
                case ')':
                    readNextByte();
                    return new Operator((char)curByte);

                case ';':
                    readNextByte();
                    return new Separator((char)curByte);

                case '\'': // Comment
                    while (nextByte != '\n') readNextByte();
                    goto beginScanToken;

                case '=':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.Equal);
                    else return new Operator(Operator.TypeEnum.Assign);

                case '>':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.GreaterOrEqual);
                    else return new Operator(Operator.TypeEnum.Greater);

                case '<':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.LesserOrEqual);
                    else return new Operator(Operator.TypeEnum.Lesser);

                case '!':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.NotEqual);
                    else return errorOccured(token: String.Format("{0}{1}", (char)curByte, (char)nextByte), 
                                             col: col - 1, pos: bytesAnalyzed - 2, len: 2);
                        
                default:
                    return errorOccured(token: String.Format("{0}", (char) curByte),
                                        col: col, pos: bytesAnalyzed - 1, len: 1);
            }
        }

        private void readNextByte()
        {
            nextByte = stream.ReadByte();
            col++; bytesAnalyzed++;
        }

        private bool isNextByteMatch(int by)
        {
            readNextByte();
            if (nextByte != by) return false;
            nextByte = ' ';
            return true;
        }

        private void reserveKeyword(string lexeme, Tag tag)
        {
            symbolTable.AddSymbol(lexeme, tag);
        }

        /*
        private void errorRecover()
        {
            // Dipose all chars in this line or until next semi-colon or EOF
            HashSet<int> stopChar = new HashSet<int>(new int[] { '\n', ';', -1 });

            while (!stopChar.Contains(nextByte)) { readNextByte(); }
        }
        */

        private Token errorOccured(string token, int col, int pos, int len)
        {
            int errLine = CurrentLine;
            //errorRecover();
            nextByte = ' ';

            return new InvalidToken(errLine, col, pos, len, token);
        }
    }
}
