using System;

namespace ModbusDemo
{
    public class ValueHelper //Big-Endian可以直接转换
    {
        public virtual Byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        public virtual int GetInt(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }
    }

    public class LittleEndianValueHelper
    {
        public Byte[] GetBytes(int value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public int GetInt(byte[] data)
        {
            return BitConverter.ToInt32(this.Reverse(data), 0);
        }

        private Byte[] Reverse(Byte[] data)
        {
            Array.Reverse(data);
            return data;
        }
    }
}