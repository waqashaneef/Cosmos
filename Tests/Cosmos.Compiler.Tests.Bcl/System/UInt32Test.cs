﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.TestRunner;

namespace Cosmos.Compiler.Tests.Bcl.System
{
    class UInt32Test
    {
        public static void Execute()
        {
            uint value;
            string result;
            string expectedResult;

            value = uint.MaxValue;

            result = value.ToString();
            expectedResult = "4294967295";

            //Assert.IsTrue((result == expectedResult), "UInt32.ToString doesn't work");

            // Now let's try to concat to a String using '+' operator
            result = "The Maximum value of an UInt32 is " + value;
            expectedResult = "The Maximum value of an UInt32 is 4294967295";

            Assert.IsTrue((result == expectedResult), "String concat (UInt32) doesn't work");

            // Now let's try to use '$ instead of '+'
            result = $"The Maximum value of an UInt32 is {value}";
            // Actually 'expectedResult' should be the same so...
            Assert.IsTrue((result == expectedResult), "String format (UInt32) doesn't work");

            // Now let's Get the HashCode of a value
            int resultAsInt = value.GetHashCode();

            // actually the Hash Code of a Int32 is the same value (but expressed as Int32 so could have sign!)
            Assert.IsTrue((resultAsInt == (int)value), "UInt32.GetHashCode() doesn't work");

            // basic bit operations

            uint val2;

            value = 0x0C; // low-order bits: 0b0000_1100

            val2 = ~value; // val2 = ~value = low-order bits: 0b1111_0011
            Assert.IsTrue(val2 == 0xFFFFFFF3, "UInt32 bitwise not doesn't work got: " + val2);

            val2 = value & 0x06; // low-order bits: val2 = value & 0b0000_0110 = 0b0000_0100
            Assert.IsTrue(val2 == 0x04, "UInt32 bitwise and doesn't work got: " + val2);

            val2 = value | 0x06; // low-order bits: val2 = value | 0b0000_0110 = 0b0000_1110
            Assert.IsTrue(val2 == 0x0E, "UInt32 bitwise or doesn't work got: " + val2);

            val2 = value ^ 0x06; // low-order bits: val2 = value ^ 0b0000_0110 = 0b0000_1010
            Assert.IsTrue(val2 == 0x0A, "UInt32 bitwise xor doesn't work got: " + val2);

            val2 = value >> 0x02; // low-order bits: val2 = value >> 0b0000_0010 = 0b0000_0011
            Assert.IsTrue(val2 == 0x03, "UInt32 left shift doesn't work got: " + val2);

            val2 = value << 0x02; // low-order bits: val2 = value << 0b0000_0010 = 0b0011_0000
            Assert.IsTrue(val2 == 0x30, "UInt32 right shift doesn't work got: " + val2);

            // basic arithmetic operations

            value = 60;

            val2 = value + 5;
            Assert.IsTrue(val2 == 65, "UInt32 addition doesn't work got: " + val2);

            val2 = value - 5;
            Assert.IsTrue(val2 == 55, "UInt32 subtraction doesn't work got: " + val2);

            val2 = value * 5;
            Assert.IsTrue(val2 == 300, "UInt32 multiplication doesn't work got: " + val2);

            val2 = value / 5;
            Assert.IsTrue(val2 == 12, "UInt32 division doesn't work got: " + val2);

            val2 = value % 7;
            Assert.IsTrue(val2 == 4, "UInt32 remainder doesn't work got: " + val2);

#if false
            // Now let's try ToString() again but printed in hex (this test fails for now!)
            result = value.ToString("X2");
            expectedResult = "0x7FFFFFFF";

            Assert.IsTrue((result == expectedResult), "Int32.ToString(X2) doesn't work");
#endif

            // Now test conversions

            uint maxValue = uint.MaxValue;
            uint minValue = uint.MinValue;

            // TODO: some convert instructions aren't being emitted, we should find other ways of getting them emitted

            // Test Conv_I1
            Assert.IsTrue((sbyte)maxValue == -1, "Conv_I1 for UInt32 doesn't work");
            Assert.IsTrue((sbyte)minValue == 0, "Conv_I1 for UInt32 doesn't work");

            // Test Conv_U1
            Assert.IsTrue((byte)maxValue == 0xFF, "Conv_U1 for UInt32 doesn't work");
            Assert.IsTrue((byte)minValue == 0x00, "Conv_U1 for UInt32 doesn't work");

            // Test Conv_I2
            Assert.IsTrue((short)maxValue == -0x0001, "Conv_I2 for UInt32 doesn't work");
            Assert.IsTrue((short)minValue == 0x0000, "Conv_I2 for UInt32 doesn't work");

            // Test Conv_U2
            Assert.IsTrue((ushort)maxValue == 0xFFFF, "Conv_U2 for UInt32 doesn't work");
            Assert.IsTrue((ushort)minValue == 0x0000, "Conv_U2 for UInt32 doesn't work");

            // Test Conv_I4
            Assert.IsTrue((int)maxValue == -0x00000001, "Conv_I4 for UInt32 doesn't work");
            Assert.IsTrue((int)minValue == 0x00000000, "Conv_I4 for UInt32 doesn't work");

            // Test Conv_U4
            Assert.IsTrue((uint)maxValue == 0xFFFFFFFF, "Conv_U4 for UInt32 doesn't work");
            Assert.IsTrue((uint)minValue == 0x00000000, "Conv_U4 for UInt32 doesn't work");

            // Test Conv_I8
            Assert.IsTrue((long)maxValue == 0xFFFFFFFF, "Conv_I8 for UInt32 doesn't work");
            Assert.IsTrue((long)minValue == 0x00000000, "Conv_I8 for UInt32 doesn't work");

            // Test Conv_U8
            Assert.IsTrue((ulong)maxValue == 0xFFFFFFFF, "Conv_U8 for UInt32 doesn't work");
            Assert.IsTrue((ulong)minValue == 0x00000000, "Conv_U8 for UInt32 doesn't work");

            // Test Methods
            val2 = TestMethod(value);
            Assert.IsTrue(value == 60, "Passing an UInt32 as a method parameter doesn't work");
            Assert.IsTrue(val2 == 61, "Returning an UInt32 value from a method doesn't work");

            ByRefTestMethod(ref value);
            Assert.IsTrue(value == 61, "Passing an UInt32 by ref to a method doesn't work");
        }

        public static uint TestMethod(uint aParam)
        {
            aParam++;
            return aParam;
        }

        public static void ByRefTestMethod(ref uint aParam)
        {
            aParam++;
        }
    }
}
