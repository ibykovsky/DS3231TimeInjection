using System;
using System.IO.Ports;

namespace DS3231TimeInjection
{
    class Program
    {
        const String SerialPortName = "COM7";
        static SerialPort sp = new SerialPort(SerialPortName);

        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;

            sp.DataReceived += SerialPortDataReceived;
            sp.BaudRate = 9600;

            sp.Open();
            sp.WriteLine(dt.ToString("O").Substring(0, 19));

            Console.WriteLine("Datetime:{0}\nYear:{1}\nMonth:{2}\nDay:{3}\nDay:{4}", dt.ToString("O"), dt.Year, dt.Month, dt.Day, weekDay(dt.Year, dt.Month, dt.Day));

            Console.ReadKey();

        }

        private static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.Write(indata);
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            sp.Close();
        }

        static int weekDay(int year, int month, int day)
        {

            if (3 > month)
            {
                --year;
                month += 10;
            }
            else
            {
                month -= 2;
            }

            return (day + (31 * month) / 12 + year + year / 4 - year / 100 + year / 400) % 7;
        }
    }
}
