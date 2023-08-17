using EasyModbus;

namespace ModbusDemo
{
    internal class ServerDemo
    {
        public void Exe()
        {
            ModbusServer server = new ModbusServer();
            server.Port = 502;
            server.Listen();
            server.holdingRegisters[1227] = (short)1;

            short val = server.holdingRegisters[1227];
            server.StopListening();
        }
    }
}