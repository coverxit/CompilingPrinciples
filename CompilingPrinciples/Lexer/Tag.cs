﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lex
{
    public enum Tag
    {
        Identifier = 1,
        Decimal = 2,

        Operator = 3,
        Separator = 4,

        If = 5,
        Then = 6,
        Else = 7,
        While = 8,
        Do = 9,

        VarType = 10,

        InvalidToken = 65536,
    }
}
