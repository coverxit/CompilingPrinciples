﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.IntermediateCodeGenCore
{
    public class IntermediateCodeGenException : Exception
    {
        public IntermediateCodeGenException(int line, string reason) : base("line " + line + ": " + reason) { }
    }
}
