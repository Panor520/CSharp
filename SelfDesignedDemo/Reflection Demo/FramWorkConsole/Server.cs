using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FramWorkConsole
{
    public class Server
    {
        public object ExecuteCurrentFunction(int CurrentFunctionUID, int CurrentTaskUID)
        {
            string dllName = null;
            string spaceName = null;
            string className = null;
            string functionName = null;

            var functionDM = new ReflectionDemoEntities();
            var functionlist = functionDM.Functions.Where(p => p.FunctionID == CurrentFunctionUID);
            var functionParameterList = functionDM.FunctionParameters.Where(p => p.FunctionID == CurrentFunctionUID);
            var functionParameterValueList = functionDM.TaskFunctionParamValues.Where(p => p.TaskFunctionUID == CurrentFunctionUID && p.TaskUID == CurrentTaskUID);

            Dictionary<string, object> pairs = new Dictionary<string, object>();

            if (functionlist != null)
            {
                dllName = functionlist.FirstOrDefault().DllName;
                spaceName = functionlist.FirstOrDefault().SpaceName;
                className = functionlist.FirstOrDefault().ClassName;
                functionName = functionlist.FirstOrDefault().FunctionName;
            }

            if (functionParameterValueList != null)
            {
                foreach (var item in functionParameterValueList)
                {
                    string value = item.TaskFunctionParamValue1;
                    if (value.Contains("FunctionReturn"))
                    {
                        int taskid = Convert.ToInt32(value.Substring(value.IndexOf('(')+1,1));
                        value = FunctionReturn.Where(p => p.Key == taskid).Select(x => x.Value).FirstOrDefault().ToString();
                    }
                    pairs.Add(functionParameterList.Where(p => p.ParameterID == item.TaskFunctionParamUID).Select(p => p.DataType).FirstOrDefault(), value);
                }
            }

            Reflection program = new Reflection();
            object result = program.ExecuteFunction(dllName, spaceName, className, functionName, pairs);
            return result;
        }

        public void ExecuteTask()
        {
            object resultObj = new object();
            ReflectionDemoEntities entities = new ReflectionDemoEntities();
            var currenctTask = entities.TaskFunctions.Where(p => p.TaskUID == CurrentTaskUID).OrderBy(p => p.ExecutionOrder);
            Dictionary<int, string> TaskList = currenctTask.ToDictionary(x => x.FunctionUID, y => y.ExecuteCondition);

            CurrentTaskFunctionUID = TaskList.Keys.First();

            int index = 1;
            string executeConditionStr = string.IsNullOrEmpty(TaskList.Values.First()) ? "True" : TaskList.Values.First();
            bool executeCondition = Convert.ToBoolean(executeConditionStr);
            while (CurrentTaskFunctionUID != -1)
            {
                if (executeCondition)
                {
                    resultObj = ExecuteCurrentFunction(CurrentTaskFunctionUID, CurrentTaskUID);
                    
                    FunctionReturn.Add(CurrentTaskFunctionUID, resultObj);
                }

                if (CurrentTaskFunctionUID != -1)
                {
                    int i = index + 1;
                    if (i <= TaskList.Count)
                    {
                        int j = 1;
                        foreach (var item in TaskList)
                        {
                            if (j == i)
                            {
                                CurrentTaskFunctionUID = item.Key;
                                executeConditionStr = TaskList.Where(p => p.Key == CurrentTaskFunctionUID).Select(p => p.Value).FirstOrDefault();
                                executeConditionStr = string.IsNullOrEmpty(executeConditionStr) ? "True" : executeConditionStr;
                                executeCondition = Convert.ToBoolean(executeConditionStr);
                                index++;
                                break;
                            }

                            ++j;
                        }
                    }
                    else
                    {
                        CurrentTaskFunctionUID = -1;
                    }
                }
            }

            Thread.Sleep(5000);
            return; //resultObj;
        }

        public Dictionary<int, object> FunctionReturn = new Dictionary<int, object>();
        internal int CurrentTaskFunctionUID { get; set; }
        internal int CurrentTaskUID { get; set; }
    }
}
