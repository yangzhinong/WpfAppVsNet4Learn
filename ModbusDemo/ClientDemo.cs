using EasyModbus;
using System;

namespace ModbusDemo
{
    internal class ClientDemo
    {
        public void Exe()
        {
            ModbusClient client = new ModbusClient("127.0.0.1", 502);

            client.Connect();

            var ret = client.ReadHoldingRegisters(0, 2);  // 寄存器: 0x1234 0x5678

            var i = ModbusClient.ConvertRegistersToInt(ret, ModbusClient.RegisterOrder.HighLow);  // LowHigh=0x5678_1234  HighLow= 0x12345678
            //ReadWrite(client);

            client.Disconnect();                                                //Disconnect from Server

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        private static void ReadWrite(ModbusClient client)
        {
            client.WriteMultipleCoils(4, new bool[] { true, true, true, true, true, true, true, true, true, true });    //Write Coils starting with Address 5
            bool[] readCoils = client.ReadCoils(9, 10);                        //Read 10 Coils from Server, starting with address 10
            int[] readHoldingRegisters = client.ReadHoldingRegisters(0, 10);    //Read 10 Holding Registers from Server, starting with Address 1

            // Console Output
            for (int i = 0; i < readCoils.Length; i++)
            {
                Console.WriteLine("Value of Coil " + (9 + i + 1) + " " + readCoils[i].ToString());
            }

            for (int i = 0; i < readHoldingRegisters.Length; i++)
            {
                Console.WriteLine("Value of HoldingRegister " + (i + 1) + " " + readHoldingRegisters[i].ToString());
            }

            client.ReadDiscreteInputs(0, 1)[0].ToString(); //bool
            client.ReadInputRegisters(9, 1)[0].ToString(); //int
            client.ReadHoldingRegisters(19, 1)[0].ToString(); //int

            client.WriteSingleRegister(17, 19); //写单个寄存器
        }
    }
}