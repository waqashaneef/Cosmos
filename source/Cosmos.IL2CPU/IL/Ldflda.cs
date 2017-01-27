using System;

using XSharp.Compiler;
using static XSharp.Compiler.XSRegisters;

namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode(ILOpCode.Code.Ldflda)]
    public class Ldflda : ILOp
    {
        public Ldflda(Cosmos.Assembler.Assembler aAsmblr)
            : base(aAsmblr)
        {
        }

        public override void Execute(MethodInfo aMethod, ILOpCode aOpCode)
        {
            var xOpCode = (ILOpCodes.OpField) aOpCode;
            DoExecute(Assembler, aMethod, xOpCode.Value.DeclaringType, xOpCode.Value.GetFullName(), true, DebugEnabled, aOpCode.StackPopTypes[0]);
        }

        public static void DoExecute(Cosmos.Assembler.Assembler Assembler, MethodInfo aMethod, Type aDeclaringType, string aField, bool aDerefValue, bool aDebugEnabled, Type aTypeOnStack)
        {
          var xFieldInfo = ResolveField(aDeclaringType, aField, true);
          DoExecute(Assembler, aMethod, aDeclaringType, xFieldInfo, aDerefValue, aDebugEnabled, aTypeOnStack);
        }

        public static void DoExecute(Cosmos.Assembler.Assembler Assembler, MethodInfo aMethod, Type aDeclaringType, FieldInfo aField, bool aDerefValue, bool aDebugEnabled, Type aTypeOnStack)
        {
            XS.Comment("Field: " + aField.Id);
            int xExtraOffset = 0;
            var xType = aMethod.MethodBase.DeclaringType;

            bool xNeedsGC = aDeclaringType.IsClass && !aDeclaringType.IsValueType;

            if (xNeedsGC)
            {
                xExtraOffset = 12;
            }

            var xActualOffset = aField.Offset + xExtraOffset;
            var xSize = aField.Size;
            if ((!aTypeOnStack.IsPointer)
                && (aDeclaringType.IsClass))
            {
                DoNullReferenceCheck(Assembler, aDebugEnabled, 4);
                XS.Add(ESP, 4);
            }
            else
            {
                DoNullReferenceCheck(Assembler, aDebugEnabled, 0);
            }

            if (aDerefValue && aField.IsExternalValue)
            {
                XS.Set(ESP, EAX, destinationIsIndirect: true);
            }
            else
            {
                XS.Pop(EAX);
                XS.Add(EAX, (uint)(xActualOffset));
                XS.Push(EAX);
            }
        }
    }
}
