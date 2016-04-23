using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using CompilingPrinciples.SymbolEnvironment;

namespace CompilingPrinciples.LexerModule
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

        private int bytes = 0;
        public int BytesLexed
        {
            get { return bytes; }
        }

        private Stream stream;
        private int nextByte = ' ';

        private SymbolTable symbolTable;
        public SymbolTable SymbolTable
        {
            get { return new SymbolTable(symbolTable); }
        }

        public Lexer(SymbolTable symbolTable, Stream stream)
        {
            this.symbolTable = symbolTable;
            this.stream = stream;

            reserveKeyword("if", Tag.If);
            reserveKeyword("then", Tag.Then);
            reserveKeyword("else", Tag.Else);
            reserveKeyword("while", Tag.While);
            reserveKeyword("do", Tag.Do);

            reserveKeyword("int", Tag.VarType);
            reserveKeyword("float", Tag.VarType);
        }

        public Token ScanNextToken()
        {
        beginScanToken:
            for (; ; readNextByte())
            {
                if (nextByte == ' ' || nextByte == '\t' || nextByte == '\r') continue;
                else if (nextByte == '\n') { line++; col = 0; }
                else if (nextByte == -1) { return new EndMarker(line, col, bytes - 1); } // EOF
                else break;
            }

            if (char.IsDigit((char)nextByte))
            {
                int value = 0;
                int startCol = col, startPos = bytes - 1;

                do
                {
                    value = 10 * value + nextByte - '0';
                    readNextByte();
                } while (char.IsDigit((char)nextByte));

                return new Decimal(value, line, startCol, startPos, bytes - startPos - 1);
            }

            if (char.IsLetter((char)nextByte))
            {
                StringBuilder sb = new StringBuilder();
                int startCol = col, startPos = bytes - 1;

                do
                {
                    sb.Append((char)nextByte);
                    readNextByte();
                } while (char.IsLetterOrDigit((char)nextByte));

                string lexeme = sb.ToString();
                var ret = symbolTable.GetSymbolEntry(lexeme);

                if (ret != null)
                    return ret.Item1.Tag == Tag.Identifier ? (Word) new Identifier(id: ret.Item2, line: line, col: startCol, pos: startPos, len: bytes - startPos - 1) :
                                                             Keyword.Create(id: ret.Item2, lexeme: lexeme, line: line, col: startCol, pos: startPos, len: bytes - startPos - 1, tag: ret.Item1.Tag);

                return new Identifier(id: symbolTable.AddSymbol(lexeme, Tag.Identifier), line: line, col: startCol, pos: startPos, len: bytes - startPos - 1);
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
                    return new Operator((char)curByte, line, col, bytes - 2, 1);

                case ';':
                    readNextByte();
                    return new Separator((char)curByte, line, col, bytes - 2);

                case '\'': // Comment
                    while (nextByte != '\n') readNextByte();
                    goto beginScanToken;

                case '=':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.Equal, line, col - 1, bytes - 2, 2);
                    else return new Operator(Operator.TypeEnum.Assign, line, col - 1, bytes - 2, 1);

                case '>':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.GreaterOrEqual, line, col - 1, bytes - 2, 2);
                    else return new Operator(Operator.TypeEnum.Greater, line, col, bytes - 2, 1);

                case '<':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.LesserOrEqual, line, col - 1, bytes - 2, 2);
                    else return new Operator(Operator.TypeEnum.Lesser, line, col - 1, bytes - 2, 1);

                case '!':
                    if (isNextByteMatch('=')) return new Operator(Operator.TypeEnum.NotEqual, line, col - 1, bytes - 2, 2);
                    else return errorOccured(token: String.Format("{0}{1}", (char)curByte, (char)nextByte), 
                                             col: col - 1, pos: bytes - 2, len: 2);
                        
                default:
                    return errorOccured(token: String.Format("{0}", (char) curByte),
                                        col: col, pos: bytes - 1, len: 1);
            }
        }

        private void readNextByte()
        {
            nextByte = stream.ReadByte();
            col++; bytes++;
        }

        private bool isNextByteMatch(int by)
        {
            readNextByte();
            // Is match?
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
