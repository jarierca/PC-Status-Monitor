using System;
using System.Threading;
using OpenHardwareMonitor.Hardware;

namespace JariTemp
{
    class Program
    {
        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }
        static void GetSystemInfo()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.FanControllerEnabled = true;
            computer.GPUEnabled = true;
            computer.HDDEnabled = true;
            computer.MainboardEnabled = true;
            computer.RAMEnabled = true;

            computer.Accept(updateVisitor);

            for (int i = 0; i < computer.Hardware.Length; i++)
            {

                //CPU
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {

                    Console.WriteLine("[+] CPU -- " + computer.Hardware[i].Name + " --\r");

                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {

                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            Console.WriteLine("   [-]" + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "ºC\r");

                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Voltage)
                            Console.WriteLine("   [-]" + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "V \r");

                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Clock)
                            Console.WriteLine("   [-]" + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "Mhz \r");

                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                            Console.WriteLine("   [-]" + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "% \r");

                    }
                }
                //GPU NVIDIA && GPU ATI / AMD 
                if (computer.Hardware[i].HardwareType == HardwareType.GpuNvidia || computer.Hardware[i].HardwareType == HardwareType.GpuAti)
                {

                    Console.WriteLine("[+] GPU -- " + computer.Hardware[i].Name + " --\r");

                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {

                        //GPU Temperature
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "ºC \r");

                        //GPU Core
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Clock)
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + " Mhz \r");

                        //GPU Memory Total
                        if (computer.Hardware[i].Sensors[j].Name.Equals("GPU Memory Total"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + " MB \r");

                        //GPU Memory Used
                        if (computer.Hardware[i].Sensors[j].Name.Equals("GPU Memory Used"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + " MB \r");

                        //GPU Memory Free
                        if (computer.Hardware[i].Sensors[j].Name.Equals("GPU Memory Free"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + " MB \r");

                        //GPU Load
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "% \r");

                        //GPU Fan
                        if (computer.Hardware[i].Sensors[j].Name.Equals("GPU Fan"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "% \r");
                    }
                }

                //RAM
                if (computer.Hardware[i].HardwareType == HardwareType.RAM)
                {

                    Console.WriteLine("[+] RAM -- " + computer.Hardware[i].Name + " --\r");

                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {

                        if (computer.Hardware[i].Sensors[j].Name.Equals("Used Memory"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "MB \r");

                        if (computer.Hardware[i].Sensors[j].Name.Equals("Memory"))
                            Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + "% \r");
                    }
                }

                //MOBA
                if (computer.Hardware[i].HardwareType == HardwareType.Mainboard)
                {

                    Console.WriteLine("[+]  -- " + computer.Hardware[i].Name + " --\r");
                }

                //HDD
                if (computer.Hardware[i].HardwareType == HardwareType.HDD)
                {

                    Console.WriteLine("[+] HDD -- " + computer.Hardware[i].Name + " --\r");

                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        Console.WriteLine("   [-] " + computer.Hardware[i].Sensors[j].Name + ": " + computer.Hardware[i].Sensors[j].Value.ToString() + " %\r");
                    }
                }

            }
            Console.WriteLine("X=-------------={END}=--------------=X");
            Thread.Sleep(2000);
            computer.Close();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                GetSystemInfo();
            }
        }
    }
}
