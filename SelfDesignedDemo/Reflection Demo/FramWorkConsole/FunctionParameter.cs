//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FramWorkConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class FunctionParameter
    {
        public int ParameterID { get; set; }
        public int FunctionID { get; set; }
        public string ParameterName { get; set; }
        public string DataType { get; set; }
        public string RequireFlag { get; set; }
    
        public virtual Function Function { get; set; }
    }
}
