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
    
    public partial class TaskFunction
    {
        public int TaskFunctionUID { get; set; }
        public int TaskUID { get; set; }
        public int FunctionUID { get; set; }
        public int ExecutionOrder { get; set; }
        public string ExecuteCondition { get; set; }
    
        public virtual Task Task { get; set; }
    }
}
