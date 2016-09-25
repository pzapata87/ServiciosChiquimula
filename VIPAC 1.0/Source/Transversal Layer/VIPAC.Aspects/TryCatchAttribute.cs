using System;
using System.Reflection;
using log4net;
using PostSharp.Aspects;
using VIPAC.Common;

namespace VIPAC.Aspects
{
    [Serializable]
    public sealed class TryCatchAttribute : OnMethodBoundaryAspect
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool RethrowException { get; set; }
        public Type ExceptionTypeExpected { get; set; }

        public TryCatchAttribute()
        {
            AspectPriority = 9;
            AttributePriority = 9;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            //if (args.Exception.GetType() != ExceptionTypeExpected) return;

            Log.Error(UtilsComun.GetExceptionMessage(args.Exception), args.Exception);
            args.FlowBehavior = RethrowException ? FlowBehavior.RethrowException : FlowBehavior.Continue;
        }
    }
}