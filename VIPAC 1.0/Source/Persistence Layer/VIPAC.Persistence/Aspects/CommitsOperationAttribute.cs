using System;
using PostSharp.Aspects;

namespace VIPAC.Persistence.Aspects
{
    [Serializable]
    public sealed class CommitsOperationAttribute : MethodInterceptionAspect
    {
        public CommitsOperationAttribute()
        {
            AspectPriority = 10;
            AttributePriority = 10;
        }

        public bool SaveLogGeneral { get; set; }
        public int TablaId { get; set; }
        public int TipoAccionId { get; set; }
        public int UsuarioId { get; set; }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var dispatcher = new MessageDispatcher();

            if (args.ReturnValue == null)
                dispatcher.HandleCommand(args.Proceed);
        }
    }
}